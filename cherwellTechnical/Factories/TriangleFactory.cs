using cherwellTechnical.Models;
using System;
using System.Collections.Generic;

namespace cherwellTechnical.Factories
{
    public class TriangleFactory
    {
        private const int ROW_START = 0;
        private const int ROW_END = 1;
        private static HashSet<string> POSSIBLE_ROWS = new HashSet<string>() {
            "A",
            "B",
            "C",
            "D",
            "E",
            "F"
        };

        public Triangle ToTriangle( string designation )
        {
            string row = designation.Substring( ROW_START, ROW_END );
            int column = Convert.ToInt32(designation.Substring( ROW_END ));

            if (!POSSIBLE_ROWS.Contains(row))
            {
                throw new ArgumentException();
            } else if (column < 1 || column > 12)
            {
                throw new ArgumentException();
            } else
            {
                return new Triangle()
                {
                    Designation = new Cell( row, column )
                };
            }
        }
    }
}
