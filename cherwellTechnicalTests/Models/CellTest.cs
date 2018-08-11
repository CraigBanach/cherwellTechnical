using Xunit;
using cherwellTechnical.Models;
using System;

namespace cherwellTechnicalTests.Models
{
    public class CellTest
    {
        public class GetCell
        {
            [Fact]
            public void ThrowsIfRowIsNull()
            {
                Cell cell = new Cell
                {
                    Row = null,
                    Column = 1
                };

                Assert.Throws<NullReferenceException>( () => cell.ToString() );
            }

            [Fact]
            public void ThrowsIfColumnIsNull()
            {
                Cell cell = new Cell
                {
                    Row = "A",
                    Column = null
                };

                Assert.Throws<NullReferenceException>( () => cell.ToString() );
            }

            [Fact]
            public void ReturnsStringIfHasRowAndColmn()
            {
                Cell cell = new Cell
                {
                    Row = "A",
                    Column = 1
                };

                Assert.True( cell.ToString().Length > 1 );
                Assert.True( cell.ToString() == "A1" );
            }
        }

        public class HasValues
        {
            [Fact]
            public void ReturnsFalseIfRowIsNull()
            {
                Cell cell = new Cell
                {
                    Row = null,
                    Column = 12
                };

                Assert.False( cell.HasValues() );
            }

            [Fact]
            public void ReturnsFalseIfColumnIsNull()
            {
                Cell cell = new Cell
                {
                    Row = "F",
                    Column = null
                };

                Assert.False( cell.HasValues() );
            }

            [Fact]
            public void ReturnsTrueIfColumnAndRowAreNotNull()
            {
                Cell cell = new Cell
                {
                    Row = "F",
                    Column = 1
                };

                Assert.True( cell.HasValues() );
            }
        }
    }
}
