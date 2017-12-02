open System


[<EntryPoint>]
let main argv =
    let captcha =
        match argv with
        | [| captcha |] -> captcha
        | _ -> failwithf "Expected a single argument, got %d" (Array.length argv)

    if captcha.Length % 2 <> 0 then failwith "Captcha must contain an even number of digits"
    if Seq.forall Char.IsDigit captcha |> not then failwith "Captcha must contain digits only"

    let solution = Solution.solveInverseCaptcha captcha
    printfn "The solution to inverse captcha '%s' is '%d'" captcha solution

    0 // return an integer exit code