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

    let solution = Solution.getUniqueDistributionCount distribution
    printfn "The provided distribution can be reallocated '%d' times before running into a loop" solution

    0 // return an integer exit code
