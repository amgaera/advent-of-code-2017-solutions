module Tests

open Xunit


[<Fact>]
let ``Distribution loop size is 4 for the example distribution`` () =
    let result = Solution.getCycleSizeForDistribution [| 0L; 2L; 7L; 0L |]
    Assert.Equal(4, result)

[<Fact>]
let ``Distribution loop size is 8038 for my advent distribution`` () =
    let result =
        Solution.getCycleSizeForDistribution [| 4L; 10L; 4L; 1L; 8L; 4L; 9L; 14L; 5L; 1L; 14L; 15L; 0L; 15L; 3L; 5L |]
    Assert.Equal(8038, result)
