using cherwellTechnical.Factories.Interfaces;
using cherwellTechnical.Models;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace cherwellTechnical.Factories
{
    public class TriangleFactory : ITriangleFactory
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
                Triangle triangle = new Triangle()
                {
                    Designation = new Cell( row, column )
                };
                triangle.GetCoordinates();
                return triangle;
            }
        }

        public Triangle ToTriangle( Collection<Coordinate> coordinates )
        {
            if ( coordinates.Count != Triangle.SIDES )
            {
                throw new ArgumentException();
            }
            else if ( !this.CoordinatesAreValid( coordinates ) )
            {
                throw new ArgumentException();
            }
            else
            {
                Triangle triangle = new Triangle()
                {
                    Coordinates = coordinates
                };
                triangle.GetDesignation();
                return triangle;
            }
        }

        private bool CoordinatesAreValid( Collection<Coordinate> coordinates )
        {
            Coordinate lowestMagnitude = coordinates.MinBy( coord => coord.X + coord.Y ).First();
            Coordinate highestMagnitude = new Coordinate(
                lowestMagnitude.X + Triangle.SIDE_LENGTH,
                lowestMagnitude.Y + Triangle.SIDE_LENGTH );
            Coordinate rightUpperCoord = new Coordinate(
                lowestMagnitude.X + Triangle.SIDE_LENGTH,
                lowestMagnitude.Y );
            Coordinate leftLowerCoord = new Coordinate(
                lowestMagnitude.X,
                lowestMagnitude.Y + Triangle.SIDE_LENGTH );

            // Check that the triangle is a right-angled triangle and has side
            // length 10
            if (!coordinates.Any(coord => 
                    coord.X == highestMagnitude.X 
                &&  coord.Y == highestMagnitude.Y))
            {
                return false;
            } else if ( !coordinates.Any( coord =>
                        coord.X == rightUpperCoord.X
                    &&  coord.Y == rightUpperCoord.Y ) 
                &&  !coordinates.Any( coord => 
                        coord.X == leftLowerCoord.X
                    &&  coord.Y == leftLowerCoord.Y ) )
            {
                return false;
            }

            return true;
        }
    }
}
