module Solution


let isPassphraseValid (passphrase : string) =
    passphrase.Split ()
    |> Array.countBy id
    |> Array.forall (fun (_, count) -> count <= 1)

let countValidPassphrases passphrases =
    passphrases
    |> Seq.filter isPassphraseValid
    |> Seq.length
