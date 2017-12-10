open System


[<EntryPoint>]
let main argv =
    let jumpOffsets =
        match argv with
        | [| jumpOffsets |] -> jumpOffsets
        | _ -> failwithf "Expected a single argument, got %d" (Array.length argv)

    let jumpOffsets =
        jumpOffsets.Split (Environment.NewLine)
        |> Seq.map
            (fun offset ->
                match Int32.TryParse offset with
                | true, value -> value
                | false, _ -> failwithf "Could not parse offset '%s' as an integer" offset)

    let solution = Solution.getNumberOfStepsToExit jumpOffsets
    printfn "'%d' steps are needed to reach the exit" solution

    0 // return an integer exit code
