module Tests

open Xunit


[<Fact>]
let ``The first value greater than 1 is 2`` () =
    let result = Solution.getFirstValueGreaterThan 1
    Assert.Equal(2, result)

[<Fact>]
let ``The first value greater than 2 is 4`` () =
    let result = Solution.getFirstValueGreaterThan 2
    Assert.Equal(4, result)

[<Fact>]
let ``The first value greater than 4 is 5`` () =
    let result = Solution.getFirstValueGreaterThan 4
    Assert.Equal(5, result)

[<Fact>]
let ``The first value greater than 5 is 10`` () =
    let result = Solution.getFirstValueGreaterThan 5
    Assert.Equal(10, result)

[<Fact>]
let ``The first value greater than 25 is 26`` () =
    let result = Solution.getFirstValueGreaterThan 25
    Assert.Equal(26, result)

[<Fact>]
let ``The first value greater than 26 is 54`` () =
    let result = Solution.getFirstValueGreaterThan 26
    Assert.Equal(54, result)

[<Fact>]
let ``The first value greater than 304 is 330`` () =
    let result = Solution.getFirstValueGreaterThan 304
    Assert.Equal(330, result)

[<Fact>]
let ``The first value greater than 362 is 747`` () =
    let result = Solution.getFirstValueGreaterThan 362
    Assert.Equal(747, result)

[<Fact>]
let ``The first value greater than 368078 is 369601`` () =
    let result = Solution.getFirstValueGreaterThan 368078
    Assert.Equal(369601, result)
