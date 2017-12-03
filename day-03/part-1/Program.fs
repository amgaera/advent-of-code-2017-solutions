open System


[<EntryPoint>]
let main argv =
    let address =
        match argv with
        | [| address |] -> address
        | _ -> failwithf "Expected a single argument, got %d" (Array.length argv)

    let address =
        match Int32.TryParse address with
        | true, value -> value
        | false, _ -> failwithf "The provided address '%s' could not be parsed" address

    let solution = Solution.computeAccessPortDistance address
    printfn "Address '%d' is '%d' units away from the access port" address solution

    0 // return an integer exit code