open System


[<EntryPoint>]
let main argv =
    let distribution =
        match argv with
        | [| distribution |] -> distribution
        | _ -> failwithf "Expected a single argument, got %d" (Array.length argv)

    let distribution =
        distribution.Split ()
        |> Array.map
            (fun blockCount -> 
                match Int64.TryParse blockCount with
                | true, value -> value
                | false, _ -> failwithf "The provided block count '%s' could not be parsed" blockCount)

    let solution = Solution.getCycleSizeForDistribution distribution
    printfn "The reallocation cycle size for the provided distribution is '%d'" solution

    0 // return an integer exit code
