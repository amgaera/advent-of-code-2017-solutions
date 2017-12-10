module Solution


let rec private getNumberOfStepsToExitFromIndex index (jumpOffsets : int array) stepCount =
    let offset = jumpOffsets.[index]
    let newIndex = index + offset

    if newIndex >= jumpOffsets.Length then
        stepCount + 1
    else
        jumpOffsets.[index] <- offset + 1
        getNumberOfStepsToExitFromIndex newIndex jumpOffsets (stepCount + 1)

let getNumberOfStepsToExit (jumpOffsets : int seq) =
    getNumberOfStepsToExitFromIndex 0 (Array.ofSeq jumpOffsets) 0
