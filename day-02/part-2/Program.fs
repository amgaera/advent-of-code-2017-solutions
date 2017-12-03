open System


let parseSpreadsheet (spreadsheet : string) =
    spreadsheet.Split (Environment.NewLine)
    |> Seq.map (fun row -> row.Split (' ') |> Seq.map int)


[<EntryPoint>]
let main argv =
    let spreadsheet =
        match argv with
        | [| input |] -> parseSpreadsheet input
        | _ -> failwithf "Expected a single argument, got %d" (Array.length argv)

    let solution = Solution.computeSpreadsheetResult spreadsheet
    printfn "The result for the provided spreadsheet is '%d'" solution

    0 // return an integer exit code
