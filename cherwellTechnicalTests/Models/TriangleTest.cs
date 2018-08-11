using cherwellTechnical.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using Xunit;

namespace cherwellTechnicalTests.Models
{
    public class TriangleTest
    {
        public class GetDesignation
        {
            [Fact]
            public void ThrowsIfDesignationAndCoordsAreNull()
            {
                Triangle triangle = new Triangle();
                triangle.Coordinates = null;
                triangle.Designation = null;

                Assert.Throws<NullReferenceException>( () => triangle.GetDesignation() );
            }

            [Fact]
            public void ReturnDesignationIfDesignationNotNull()
            {
                var designation = new Cell( "A", 1 );

                Triangle triangle = new Triangle();
                triangle.Coordinates = null;
                triangle.Designation = designation;

                Assert.Equal( designation , triangle.GetDesignation() );
            }

            [Theory]
            [ClassData(typeof(CoordinateTestData))]
            public void ReturnsDesignationIfCoordinateNotNullAndDesignationNull(
                Collection<Coordinate> coordinates,
                Cell designation)
            {
                Triangle triangle = new Triangle();
                triangle.Coordinates = coordinates;

                Assert.Equal( designation.ToString(), triangle.GetDesignation().ToString() );
            }

            public class CoordinateTestData : TheoryData<Collection<Coordinate>, Cell>
            {
                public CoordinateTestData()
                {
                    Add(
                        new Collection<Coordinate>
                        {
                            new Coordinate( 10, 10 ),
                            new Coordinate( 10, 20 ),
                            new Coordinate( 20, 20 )
                        },
                        new Cell( "B", 3 )
                    );
                    Add(
                        new Collection<Coordinate>
                        {
                            new Coordinate( 40, 40 ),
                            new Coordinate( 50, 40 ),
                            new Coordinate( 50, 50 )
                        },
                        new Cell( "E", 10 )
                    );
                    Add(
                        new Collection<Coordinate>
                        {
                            new Coordinate( 20, 30 ),
                            new Coordinate( 30, 30 ),
                            new Coordinate( 30, 40 )
                        },
                        new Cell( "D", 6 )
                    );
                }
            }
        }

        public class GetCoordinates
        {
            [Fact]
            public void ThrowsIfDesignationAndCoordsAreNull()
            {
                Triangle triangle = new Triangle();
                triangle.Coordinates = null;
                triangle.Designation = null;

                Assert.Throws<NullReferenceException>( () => triangle.GetCoordinates() );
            }

            [Fact]
            public void ReturnCoordinatesIfCoordinatesNotNull()
            {
                Collection<Coordinate> coordinates = new Collection<Coordinate>
                        {
                            new Coordinate( 10, 10 ),
                            new Coordinate( 10, 20 ),
                            new Coordinate( 20, 20 )
                        };

                Triangle triangle = new Triangle();
                triangle.Coordinates = coordinates;
                triangle.Designation = null;

                Assert.Equal( coordinates, triangle.GetCoordinates() );
            }

            [Theory]
            [ClassData( typeof( DesignationTestData ) )]
            public void ReturnsCoordinatesIfCoordinatesNullAndDesignationNot(
                Cell designation,
                Collection<Coordinate> coordinates )
            {
                Triangle triangle = new Triangle();
                triangle.Coordinates = null;
                triangle.Designation = designation;

                Assert.True( triangle.GetCoordinates().Any( entry => entry.X == coordinates[ 0 ].X && entry.Y == coordinates[ 0 ].Y ) );
            }

            public class DesignationTestData : TheoryData<Cell, Collection<Coordinate>>
            {
                public DesignationTestData()
                {
                    Add(
                        new Cell("B", 3),
                        new Collection<Coordinate>
                        {
                            new Coordinate( 10, 10 ),
                            new Coordinate( 10, 20 ),
                            new Coordinate( 20, 20 )
                        }
                    );
                    Add(
                        new Cell("E", 10),
                        new Collection<Coordinate>
                        {
                            new Coordinate( 40, 40 ),
                            new Coordinate( 50, 40 ),
                            new Coordinate( 50, 50 )
                        }
                    );
                    Add(
                        new Cell("D", 6),
                        new Collection<Coordinate>
                        {
                            new Coordinate( 20, 30 ),
                            new Coordinate( 30, 30 ),
                            new Coordinate( 30, 40 )
                        }
                    );
                }
            }
        }
    }
}
