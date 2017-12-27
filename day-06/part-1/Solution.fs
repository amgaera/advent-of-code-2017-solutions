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

let getUniqueDistributionCount (distribution : int64 array) =
    Seq.unfold getNextDistribution distribution
    |> Seq.scan
        (fun (_isDuplicate, pastDistributions) distribution ->
            if Set.contains distribution pastDistributions then
                true, pastDistributions
            else
                false, Set.add distribution pastDistributions)
        (false, Set.singleton distribution)
    |> Seq.takeWhile (fst >> not)
    |> Seq.last
    |> snd
    |> Set.count
