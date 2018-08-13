using cherwellTechnical.Factories;
using cherwellTechnical.Models;
using System;
using System.Collections.ObjectModel;
using Xunit;

namespace cherwellTechnicalTests.Factories
{
    public class TriangleFactoryTest
    {
        public class ToTriangleFromDesignation
        {
            [Fact]
            public void ThrowsIfColumnNotInAcceptedRange()
            {
                TriangleFactory factory = new TriangleFactory();

                Assert.Throws<ArgumentException>( () => factory.ToTriangle( "G1" ));
            }

            [Theory]
            [InlineData( "A-1" )]
            [InlineData( "B13" )]
            public void ThrowsIfRowNotInAcceptedRange( string designation )
            {
                TriangleFactory factory = new TriangleFactory();

                Assert.Throws<ArgumentException>( () => factory.ToTriangle( designation ) );
            }

            [Theory]
            [ClassData(typeof(DesignationTestData))]
            public void ReturnsTriangleIfInputAreValid( 
                string designation,
                Triangle expected)
            {
                TriangleFactory factory = new TriangleFactory();

                Triangle result = factory.ToTriangle( designation );

                Assert.True( expected.Designation.Column == result.Designation.Column );
                Assert.True( expected.Designation.Row == result.Designation.Row );
                Assert.True( result.Coordinates.Count == 3 );
            }

            public class DesignationTestData : TheoryData<string, Triangle>
            {
                public DesignationTestData()
                {
                    Add(
                        "A3",
                        new Triangle()
                        {
                            Designation = new Cell( "A", 3 ),
                            Coordinates = new Collection<Coordinate>()
                            {
                                new Coordinate( 10, 0 ),
                                new Coordinate( 10, 10 ),
                                new Coordinate( 20, 10 )
                            }
                        }
                    );
                    Add(
                        "F12",
                        new Triangle()
                        {
                            Designation = new Cell( "F", 12 ),
                            Coordinates = new Collection<Coordinate>()
                            {
                                new Coordinate( 50, 50 ),
                                new Coordinate( 60, 50 ),
                                new Coordinate( 60, 60 )
                            }
                        }
                    );
                    Add(
                        "C7",
                        new Triangle()
                        {
                            Designation = new Cell( "C", 7 ),
                            Coordinates = new Collection<Coordinate>()
                            {
                                new Coordinate( 30, 20 ),
                                new Coordinate( 30, 30 ),
                                new Coordinate( 40, 30 )
                            }
                        }
                    );
                }
            }
        }

        public class ToTriangleFromCoordinates
        {
            [Fact]
            public void ThrowsIfThereAreNotThreeCoordinates()
            {
                TriangleFactory factory = new TriangleFactory();

                Assert.Throws<ArgumentException>( () => 
                    factory.ToTriangle(new Collection<Coordinate>() ) );
            }

            [Fact]
            public void ThrowsIfAnyCoordinateIsInvalid()
            {
                TriangleFactory factory = new TriangleFactory();

                Assert.Throws<ArgumentException>( () =>
                    factory.ToTriangle( new Collection<Coordinate>()
                    {
                        new Coordinate(10, 10),
                        new Coordinate(10, 10),
                        new Coordinate(10, 7)
                    }
                ) );
            }

            [Theory]
            [ClassData( typeof( CoordinateTestData ) )]
            public void ReturnsTriangleIfInputAreValid(
                Collection<Coordinate> coordinates,
                Triangle expected )
            {
                TriangleFactory factory = new TriangleFactory();

                Triangle result = factory.ToTriangle( coordinates );

                Assert.True( expected.Designation.Column == result.Designation.Column );
                Assert.True( expected.Designation.Row == result.Designation.Row );
                Assert.True( result.Coordinates.Count == 3 );
            }

            public class CoordinateTestData : TheoryData<Collection<Coordinate>, Triangle>
            {
                public CoordinateTestData()
                {
                    Add(
                        new Collection<Coordinate>()
                            {
                                new Coordinate( 10, 0 ),
                                new Coordinate( 10, 10 ),
                                new Coordinate( 20, 10 )
                            },
                        new Triangle()
                        {
                            Designation = new Cell( "A", 3 ),
                            Coordinates = new Collection<Coordinate>()
                            {
                                new Coordinate( 10, 0 ),
                                new Coordinate( 10, 10 ),
                                new Coordinate( 20, 10 )
                            }
                        }
                    );
                    Add(
                        new Collection<Coordinate>()
                            {
                                new Coordinate( 50, 50 ),
                                new Coordinate( 60, 50 ),
                                new Coordinate( 60, 60 )
                            },
                        new Triangle()
                        {
                            Designation = new Cell( "F", 12 ),
                            Coordinates = new Collection<Coordinate>()
                            {
                                new Coordinate( 50, 50 ),
                                new Coordinate( 60, 50 ),
                                new Coordinate( 60, 60 )
                            }
                        }
                    );
                    Add(
                        new Collection<Coordinate>()
                            {
                                new Coordinate( 30, 20 ),
                                new Coordinate( 30, 30 ),
                                new Coordinate( 40, 30 )
                            },
                        new Triangle()
                        {
                            Designation = new Cell( "C", 7 ),
                            Coordinates = new Collection<Coordinate>()
                            {
                                new Coordinate( 30, 20 ),
                                new Coordinate( 30, 30 ),
                                new Coordinate( 40, 30 )
                            }
                        }
                    );
                }
            }
        }
    }
}
