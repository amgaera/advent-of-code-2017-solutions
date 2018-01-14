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

let getBottomProgram programList =
    programList
    |> Seq.map parseProgramInfo
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
