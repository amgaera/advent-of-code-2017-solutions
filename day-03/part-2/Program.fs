open System


[<EntryPoint>]
let main argv =
    let value =
        match argv with
        | [| value |] -> value
        | _ -> failwithf "Expected a single argument, got %d" (Array.length argv)

    let value =
        match Int32.TryParse value with
        | true, value -> value
        | false, _ -> failwithf "The provided value '%s' could not be parsed as an integer" value

    let solution = Solution.getFirstValueGreaterThan value
    printfn "The first value that is larger than '%d' is '%d'" value solution

    0 // return an integer exit code
