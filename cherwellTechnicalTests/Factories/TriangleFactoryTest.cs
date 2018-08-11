using cherwellTechnical.Factories;
using cherwellTechnical.Models;
using System;
using Xunit;

namespace cherwellTechnicalTests.Factories
{
    public class TriangleFactoryTest
    {
        public class ToTriangle
        {
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
            public void ReturnsCellIfInputIsValid( 
                string designation,
                Cell expected)
            {
                TriangleFactory factory = new TriangleFactory();

                Triangle result = factory.ToTriangle( designation );

                Assert.True( expected.Column == result.Designation.Column );
                Assert.True( expected.Row == result.Designation.Row );
            }

            public class DesignationTestData : TheoryData<string, Cell>
            {
                public DesignationTestData()
                {
                    Add( "A3", new Cell( "A", 3 ) );
                    Add( "F12", new Cell( "F", 12 ) );
                    Add( "C7", new Cell( "C", 7 ) );
                }
            }
        }
    }
}
