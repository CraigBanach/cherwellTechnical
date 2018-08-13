using cherwellTechnical.Models;
using System.Collections.ObjectModel;

namespace cherwellTechnical.Factories.Interfaces
{
    public interface ITriangleFactory
    {
        Triangle ToTriangle( string designation );
        Triangle ToTriangle( Collection<Coordinate> coordinates );
    }
}
