module Solution

open System
open System.Text.RegularExpressions


type private ProgramInfo =
    { Name : string
      Weight : uint32
      SupportedProgramNames : string array }

let private parseProgramInfo (line : string) =
    let nameWeight, supportedProgramNames =
        match line.Split (" -> ") with
        | [| nameWeight; supportedProgramNames |] -> nameWeight, supportedProgramNames.Split (", ")
        | [| nameWeight |] -> nameWeight, Array.empty
        | _ -> failwithf "Failed to parse program info from line '%s'" line

    let nameWeightRegexMatch = Regex.Match (nameWeight, @"^(\w+) \((\d+)\)$")
    let name, weight =
        if nameWeightRegexMatch.Success then
            nameWeightRegexMatch.Groups.Item (1), nameWeightRegexMatch.Groups.Item (2)
        else
            failwithf "Failed to parse program name and weight from string '%s'" nameWeight
    let weight =
        match UInt32.TryParse weight.Value with
        | true, weight -> weight
        | false, _ -> failwithf "Failed to parse program weight from string '%s'" weight.Value

    { Name = name.Value; Weight = weight; SupportedProgramNames = supportedProgramNames }

let private getBottomProgramName (programInfos : Map<string, ProgramInfo>) =
    programInfos
    |> Map.toSeq
    |> Seq.map snd
    |> Seq.filter (fun programInfo -> Array.isEmpty programInfo.SupportedProgramNames |> not)
    |> Seq.fold
        (fun (potentialBottomProgramNames, allSupportedProgramNames) programInfo ->
            let potentialBottomProgramNames = Set.add programInfo.Name potentialBottomProgramNames
            let allSupportedProgramNames =
                programInfo.SupportedProgramNames
                |> Set.ofArray
                |> Set.union allSupportedProgramNames
            potentialBottomProgramNames, allSupportedProgramNames)
        (Set.empty, Set.empty)
    ||> Set.difference
    |> Seq.exactlyOne

type private ComputeTotalWeightsAndFindUnbalancedProgramLoopState =
    { ProgramsToProcess : string list
      TotalProgramWeights : Map<string, uint32>
      UnbalancedProgramPath : ProgramInfo list option }

let rec private computeTotalWeightsAndFindUnbalancedProgramLoop programInfos loopState =
    match loopState.ProgramsToProcess with
    | programName :: remainingPrograms ->
        let program = Map.find programName programInfos
        let newLoopState =
            if Array.isEmpty program.SupportedProgramNames then
                { loopState with
                    ProgramsToProcess = remainingPrograms
                    TotalProgramWeights = Map.add program.Name program.Weight loopState.TotalProgramWeights }
            else
                let supportedProgramTotalWeights =
                    program.SupportedProgramNames
                    |> Array.map (fun programName -> Map.tryFind programName loopState.TotalProgramWeights)

                if Array.forall Option.isSome supportedProgramTotalWeights then
                    let supportedProgramTotalWeights = Array.map Option.get supportedProgramTotalWeights
                    let totalProgramWeight = program.Weight + Seq.sum supportedProgramTotalWeights

                    let areSupportedProgramsBalanced =
                        supportedProgramTotalWeights |> Seq.pairwise |> Seq.forall (fun (a, b) -> a = b)
                    let unbalancedProgramPath =
                        if not areSupportedProgramsBalanced && Option.isNone loopState.UnbalancedProgramPath then
                            Some [ program ]
                        else loopState.UnbalancedProgramPath
                    
                    { loopState with
                        ProgramsToProcess = remainingPrograms
                        TotalProgramWeights = Map.add program.Name totalProgramWeight loopState.TotalProgramWeights
                        UnbalancedProgramPath = unbalancedProgramPath }
                else
                    let programsToProcess =
                        List.append (List.ofArray program.SupportedProgramNames) loopState.ProgramsToProcess
                    { loopState with ProgramsToProcess = programsToProcess }

        let newLoopState =
            match newLoopState.UnbalancedProgramPath with
            | Some ((unbalancedProgram :: _) as unbalancedProgramPath)
                when Array.contains unbalancedProgram.Name program.SupportedProgramNames ->
                    { newLoopState with UnbalancedProgramPath = Some (program :: unbalancedProgramPath) }
            | _ -> newLoopState

        computeTotalWeightsAndFindUnbalancedProgramLoop programInfos newLoopState
    | [] -> Option.map List.rev loopState.UnbalancedProgramPath, loopState.TotalProgramWeights

let private computeTotalWeightsAndFindUnbalancedProgram programInfos =
    let bottomProgramName = getBottomProgramName programInfos
    let initialLoopState =
        { ProgramsToProcess = [ bottomProgramName ]; TotalProgramWeights = Map.empty; UnbalancedProgramPath = None }
    let unbalancedProgramPath, totalProgramWeights =
        computeTotalWeightsAndFindUnbalancedProgramLoop programInfos initialLoopState

    match unbalancedProgramPath with
    | None -> failwithf "No unbalanced program found"
    | Some (unbalancedProgram :: _) ->
        let supportedProgramByTotalWeight =
            unbalancedProgram.SupportedProgramNames
            |> Array.groupBy (fun programName -> Map.find programName totalProgramWeights)

        let wrongWeightProgramName, weightDelta =
            match supportedProgramByTotalWeight with
            | [| (wrongWeight, [| programName |]); (expectedWeight, _) |] ->
                programName, int expectedWeight - int wrongWeight
            | [| (expectedWeight, _); (wrongWeight, [| programName |]) |] ->
                programName, int expectedWeight - int wrongWeight
            | _ ->
                failwithf "Expected supported programs to have 2 distinct total weights, found '%d'"
                    supportedProgramByTotalWeight.Length

        let wrongWeightProgram = Map.find wrongWeightProgramName programInfos
        wrongWeightProgramName, uint32 (int wrongWeightProgram.Weight + weightDelta)
    | _ -> failwithf "Not implemented yet"

let getProgramWithNewWeight programList =
    let programInfos =
        programList
        |> Seq.map parseProgramInfo
        |> Seq.map (fun programInfo -> programInfo.Name, programInfo)
        |> Map.ofSeq
    computeTotalWeightsAndFindUnbalancedProgram programInfos
