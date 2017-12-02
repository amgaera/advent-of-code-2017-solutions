module Solution

open System


let private getDigitValue digit =
    match digit.ToString () |> Int32.TryParse with
    | true, value -> value
    | false, _ -> failwithf "Failed to parse digit '%c'" digit

let solveInverseCaptcha (input : string) =
    Seq.append input input
    |> Seq.take (input.Length + 1)
    |> Seq.pairwise
    |> Seq.filter (fun (digitA, digitB) -> digitA = digitB)
    |> Seq.map fst
    |> Seq.sumBy getDigitValue