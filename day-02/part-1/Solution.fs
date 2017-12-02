module Solution


let private minMax values =
    match Seq.tryHead values with
    | Some head ->
        let pickMinMax (currentMin, currentMax) value =
            if value < currentMin then
                value, currentMax
            else if value > currentMax then
                currentMin, value
            else
                currentMin, currentMax

        values
        |> Seq.tail
        |> Seq.fold pickMinMax (head, head)
    | None -> failwithf "Sequence must not be empty"

let computeSpreadsheetChecksum (spreadsheet : int seq seq) =
    spreadsheet
    |> Seq.map minMax
    |> Seq.sumBy (fun (min, max) -> max - min)