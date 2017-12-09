open System


[<EntryPoint>]
let main argv =
    let passphrases =
        match argv with
        | [| passphrases |] -> passphrases.Split (Environment.NewLine)
        | _ -> failwithf "Expected a single argument, got %d" (Array.length argv)

    let solution = Solution.countValidPassphrases passphrases
    printfn "Out of the '%d' provided passphrases '%d' are valid" passphrases.Length solution

    0 // return an integer exit code
