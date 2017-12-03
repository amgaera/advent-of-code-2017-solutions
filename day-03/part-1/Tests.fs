module Tests

open Xunit


[<Fact>]
let ``The distance to the access port from memory address 1 is 0`` () =
    let result = Solution.computeAccessPortDistance 1
    Assert.Equal(0, result)

[<Fact>]
let ``The distance to the access port from memory address 12 is 3`` () =
    let result = Solution.computeAccessPortDistance 12
    Assert.Equal(3, result)

[<Fact>]
let ``The distance to the access port from memory address 23 is 2`` () =
    let result = Solution.computeAccessPortDistance 23
    Assert.Equal(2, result)

[<Fact>]
let ``The distance to the access port from memory address 1024 is 31`` () =
    let result = Solution.computeAccessPortDistance 1024
    Assert.Equal(31, result)

[<Fact>]
let ``The distance to the access port from memory address 368078 is 371`` () =
    let result = Solution.computeAccessPortDistance 368078
    Assert.Equal(371, result)
