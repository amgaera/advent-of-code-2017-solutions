module Solution

open System


let private getBandNumber address =
    let sqrt = Math.Sqrt (float address)
    let closestRoot = Math.Ceiling sqrt |> int

    closestRoot / 2

type private BandInfo =
    { Number : int
      StartingAddress : int
      Length : int }

let private getBandInfo bandNumber =
    { Number = bandNumber;
      StartingAddress = (pown (bandNumber * 2 - 1) 2) + 1
      Length = bandNumber * 8 }

let private getAddressCartesianCoordinates address =
    let bandInfo = address |> getBandNumber |> getBandInfo
    let sideLength = bandInfo.Length / 4
    let side, offset = Math.DivRem ((address - bandInfo.StartingAddress), sideLength)

    match side with
    | 0 -> (bandInfo.Number, 1 - bandInfo.Number + offset)
    | 1 -> (bandInfo.Number - 1 - offset, bandInfo.Number)
    | 2 -> (-bandInfo.Number, bandInfo.Number - 1 - offset)
    | 3 -> (1 - bandInfo.Number + offset, -bandInfo.Number)
    | _ -> failwithf "Unexpected side number: %d" side

let private getAdjacentCoordinates x y =
    seq { for dx in -1 .. 1 do
              for dy in -1 .. 1 do
                  if dx <> 0 || dy <> 0 then yield dx, dy }
    |> Seq.map (fun (dx, dy) -> x + dx, y + dy)

let private getValueAtAddress (values : Map<int * int, int>, _) address =
    let addressCoordinates = getAddressCartesianCoordinates address
    let addressValue =
        addressCoordinates
        ||> getAdjacentCoordinates
        |> Seq.choose (fun coordinates -> Map.tryFind coordinates values)
        |> Seq.sum

    Map.add addressCoordinates addressValue values, addressValue

let getFirstValueGreaterThan (value : int) =
    let seedValues = Map.ofArray [| (0, 0), 1 |]

    Seq.initInfinite id
    |> Seq.skip 2
    |> Seq.scan getValueAtAddress (seedValues, 1)
    |> Seq.find (fun (_, lastValue) -> lastValue > value)
    |> snd
