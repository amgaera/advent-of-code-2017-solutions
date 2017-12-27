module Tests

open Xunit


[<Fact>]
let ``5 unique distributions can be generated from the example distribution`` () =
    let result = Solution.getUniqueDistributionCount [| 0L; 2L; 7L; 0L |]
    Assert.Equal(5, result)

[<Fact>]
let ``140 unique distributions can be generated from my advent distribution`` () =
    let result =
        Solution.getUniqueDistributionCount [| 4L; 10L; 4L; 1L; 8L; 4L; 9L; 14L; 5L; 1L; 14L; 15L; 0L; 15L; 3L; 5L |]
    Assert.Equal(12841, result)
