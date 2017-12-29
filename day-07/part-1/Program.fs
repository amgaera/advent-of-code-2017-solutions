open System


[<EntryPoint>]
let main argv =
    let programList =
        match argv with
        | [| programList |] -> programList.Split (Environment.NewLine)
        | _ -> failwithf "Expected a single argument, got %d" (Array.length argv)

    let solution = Solution.getBottomProgram programList
    printfn "The bottom program in the provided program list is '%s'" solution

    0 // return an integer exit code
