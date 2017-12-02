module Solution

open System


let private getDigitValue digit =
    match digit.ToString () |> Int32.TryParse with
    | true, value -> value
    | false, _ -> failwithf "Failed to parse digit '%c'" digit

let solveInverseCaptcha (input : string) =
    let shiftedInput =
        Seq.append input input
        |> Seq.skip (input.Length / 2)
        |> Seq.take input.Length

    Seq.zip input shiftedInput
    |> Seq.filter (fun (digitA, digitB) -> digitA = digitB)
    |> Seq.map fst
    |> Seq.sumBy getDigitValue