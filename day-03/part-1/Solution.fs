module Solution

open System


let private getBandNumber address =
    let sqrt = Math.Sqrt (float address)
    let closestRoot = Math.Ceiling sqrt |> int

    closestRoot / 2

type BandInfo =
    { Number : int
      StartingAddress : int
      Length : int }

let private getBandInfo bandNumber =
    { Number = bandNumber;
      StartingAddress = (pown (bandNumber * 2 - 1) 2) + 1
      Length = bandNumber * 8 }

let getAddressCartesianCoordinates address =
    let bandInfo = address |> getBandNumber |> getBandInfo
    let sideLength = bandInfo.Length / 4
    let side, offset = Math.DivRem ((address - bandInfo.StartingAddress), sideLength)

    match side with
    | 0 -> (bandInfo.Number, 1 - bandInfo.Number + offset)
    | 1 -> (bandInfo.Number - 1 - offset, bandInfo.Number)
    | 2 -> (-bandInfo.Number, bandInfo.Number - 1 - offset)
    | 3 -> (1 - bandInfo.Number + offset, -bandInfo.Number)
    | _ -> failwithf "Unexpected side number: %d" side

let computeAccessPortDistance (address : int) =
    if address = 1 then
        0
    else
        let x, y = address |> getAddressCartesianCoordinates
        abs x + abs y