open System


[<EntryPoint>]
let main argv =
    let programList =
        match argv with
        | [| programList |] -> programList.Split (Environment.NewLine)
        | _ -> failwithf "Expected a single argument, got %d" (Array.length argv)

    let programName, newProgramWeight = Solution.getProgramWithNewWeight programList
    printfn "The program in the provided program list that needs a new weight is '%s' (weight %d)"
        programName newProgramWeight

    0 // return an integer exit code
