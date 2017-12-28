module Solution

open System


let private getNextDistribution distribution =
    let maxBankIndex, maxBlockCount =
        distribution
        |> Array.indexed
        |> Array.maxBy snd
    let commonBlockCount, extraBlockCount = Math.DivRem (int64 maxBlockCount, distribution.LongLength)

    let distributedBlockCounts =
        [ Seq.replicate (extraBlockCount |> int) (commonBlockCount + 1L);
          Seq.replicate (distribution.LongLength - extraBlockCount |> int) commonBlockCount ]
        |> Seq.concat
        |> Seq.replicate 2
        |> Seq.concat
        |> Seq.skip (distribution.Length - maxBankIndex - 1)
        |> Seq.take distribution.Length

    let newDistribution =
        distribution
        |> Seq.mapi (fun index currentBlockCount -> if index = maxBankIndex then 0L else currentBlockCount)
        |> Seq.zip distributedBlockCounts
        |> Seq.map (fun (distributedBlockCount, currentBlockCount) -> currentBlockCount + distributedBlockCount)
        |> Array.ofSeq

    Some (newDistribution, newDistribution)

let getCycleSizeForDistribution (distribution : int64 array) =
    [ Seq.singleton distribution;
      Seq.unfold getNextDistribution distribution ]
    |> Seq.concat
    |> Seq.indexed
    |> Seq.scan
        (fun (_cycleSize, pastDistributions) (index, distribution) ->
            match Map.tryFind distribution pastDistributions with
            | Some duplicateIndex -> Some (index - duplicateIndex), pastDistributions
            | None -> None, Map.add distribution index pastDistributions)
        (None, Map.empty)
    |> Seq.choose fst
    |> Seq.head
