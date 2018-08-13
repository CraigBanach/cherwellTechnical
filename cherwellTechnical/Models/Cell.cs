using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cherwellTechnical.Models
{
    public class Cell
    {
        public string Row { get; set; }
        public int? Column { get; set; }

        public Cell()
        {

        }

        public Cell(string row, int? column)
        {
            this.Row = row;
            this.Column = column;
        }

        public bool HasValues()
        {
            return this.Row != null && this.Column != null;
        }

        public override string ToString()
        {
            if ( this.Row == null)
            {
                throw new NullReferenceException( "Cell.Row was null." );
            }
            else if ( this.Column == null)
            {
                throw new NullReferenceException( "Cell.Row was null." );
            }
            else
            {
                return this.Row + this.Column;
            }
        }
    }
}
