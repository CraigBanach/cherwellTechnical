using cherwellTechnical.Mappings;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace cherwellTechnical.Models
{
    public class Triangle
    {
        private const int SIDES = 3;
        private const int SIDE_LENGTH = 10;

        public Cell Designation { get; set; } = new Cell();
        public ICollection<Coordinate> Coordinates { get; set; } = new Collection<Coordinate>();

        public ICollection<Coordinate> GetCoordinates()
        {
            if (this.Coordinates?.Count != 3 && this.Designation.HasValues())
            {
                this.CalculateCoordinates();
            }

            return this.Coordinates;
        }

        private void CalculateCoordinates()
        {
            this.Coordinates = new Collection<Coordinate>();

            // Both triangles belonging to a square start in the top left and 
            // end in the bottom right. xBase is the left-most x value. yBase is 
            // the highest y point.
            int yBase, xBase;
            CalculateFurthestCorners( out yBase, out xBase );
            CalculateRightCorner( yBase, xBase );
        }

        private void CalculateFurthestCorners( out int yBase, out int xBase )
        {
            yBase = LetterMapping.ToPosition[ this.Designation.Row ] * SIDE_LENGTH;
            xBase = ( ( this.Designation.Column.GetValueOrDefault() - 1 ) / 2 ) * SIDE_LENGTH;

            // xBase, yBase is the upper left corner of the triangle. Each 
            // pairing of triangles in a 10 by 10 area has the same top left 
            // and bottom right corner, so we add these now and calc the 
            // remaning coordinate below.
            this.Coordinates.Add( new Coordinate( xBase, yBase ) );
            this.Coordinates.Add( new Coordinate( xBase + SIDE_LENGTH, yBase + SIDE_LENGTH ) );
        }

        private void CalculateRightCorner( int yBase, int xBase )
        {
            // A left sided triangle has it's vertical side to the left.
            // left = 1, right = 0
            int leftOrRight = this.Designation.Column.GetValueOrDefault() % 2;

            if ( leftOrRight == 1 )
            {
                this.Coordinates.Add( new Coordinate( xBase, yBase + SIDE_LENGTH ) );
            }
            else
            {
                this.Coordinates.Add( new Coordinate( xBase + SIDE_LENGTH, yBase ) );
            }
        }

        public Cell GetDesignation()
        {
            if (!this.Designation.HasValues() && this.Coordinates.Count == SIDES )
            {
                this.CalculateDesignation();
            }

            return this.Designation;
        }

        private void CalculateDesignation()
        {
            Coordinate centroid = this.GetCentroid();
            this.CalculateDesignationFromCentroid(centroid);
        }

        private Coordinate GetCentroid()
        {
            // integer division is not a problem given the scale (10px) of the 
            // triangles. If we need to go smaller, more precise division will
            // be required.
            int averageX = this.Coordinates.Sum( coord => coord.X ) / SIDES;
            int averageY = this.Coordinates.Sum( coord => coord.Y ) / SIDES;

            return new Coordinate( averageX, averageY );
        }
        
        private void CalculateDesignationFromCentroid(Coordinate centroid)
        {
            this.Designation = new Cell
            {
                Row = LetterMapping.ToLetter[ centroid.Y / SIDE_LENGTH ],
                Column = ( centroid.X / ( SIDE_LENGTH / 2 ) + 1 )
            };
        }
    }
}
