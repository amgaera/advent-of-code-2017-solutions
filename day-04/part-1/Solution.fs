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

let isPassphraseValid (passphrase : string) =
    passphrase.Split ()
    |> Array.countBy id
    |> Array.forall (fun (_, count) -> count <= 1)

let countValidPassphrases passphrases =
    passphrases
    |> Seq.filter isPassphraseValid
    |> Seq.length
