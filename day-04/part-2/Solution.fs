module Solution

open System


let private normalizeWord (word : string) =
    let sortedCharacters = word |> Array.ofSeq |> Array.sort
    String (sortedCharacters)

let isPassphraseValid (passphrase : string) =
    passphrase.Split ()
    |> Seq.map normalizeWord
    |> Seq.scan
        (fun (_isDuplicate, words) word ->
            if Set.contains word words then
                true, words
            else
                false, Set.add word words)
        (false, Set.empty)
    |> Seq.forall (fst >> not)

let countValidPassphrases passphrases =
    passphrases
    |> Seq.filter isPassphraseValid
    |> Seq.length
