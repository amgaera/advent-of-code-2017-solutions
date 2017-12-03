module Solution


let private getCandidatePairs row =
    let ascSortedRow = List.ofSeq row |> List.sort
    let destSortedRow = List.rev ascSortedRow

    ascSortedRow
    |> Seq.collect
        (fun valueA ->
            destSortedRow
            |> Seq.takeWhile (fun valueB -> valueA * 2 <= valueB)
            |> Seq.map (fun valueB -> valueB, valueA))

let private computeRowResult row =
    let valueA, valueB =
        getCandidatePairs row
        |> Seq.find (fun (valueA, valueB) -> valueA % valueB = 0)

    valueA / valueB

let computeSpreadsheetResult (spreadsheet : int seq seq) =
    spreadsheet
    |> Seq.sumBy computeRowResult
