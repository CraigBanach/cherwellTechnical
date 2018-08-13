# Building and running #

This is a .NET Core solution. Load and build the cherwellTechnical solution file that contains both the web API project and the test library.

# Design Decisions #

* To get a triangle based upon a cell designation, send a GET request to `api/Triangle/:designation`
* To get a triangle based upon a set of coordinates, send a POST request to api/Triangle with the following body (application/json format):

  `[
	{
		"X": 0,
		"Y": 0
	},
	{
		"X": 10,
		"Y": 10
	},
	{
		"X": 10,
		"Y": 0
	}
]`

There is an argument to be had about whether to implement IEquatable in the models to ease testing. On the one hand it greatly eases testing, on the other, it's not required for the program to function.

The triangle factory handles validation of the triangle object from the controller. It could be argued this needs it's own TriangleValidator.

The triangle model can calculate it's own Properties. This is to aid the possibilty of substitution. An interface `IShape` could be written with methods `GetCoordinates()` and `GetDesignation()`

When converting from a set of coordinates to a designation, I find the 10x10 square that the triangle resides in and then look to see if the vertical side is on the left or the right of the triangle. This allows the triangle to be uniquely identified.

# Next Steps #

Fully test TriangleFactory to add full branch coverage for `CoordinatesAreValid( Collection<Coordinate> coordinates )`.
