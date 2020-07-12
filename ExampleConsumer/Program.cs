using System;
using NetTopologySuite.Geometries;
using SpatialTools;

namespace ExampleConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            var centerPoint = new Point(new Coordinate(44.948812, -93.295647));
            var startingPoint = new Point(new Coordinate(44.948812, -93.295647));

            // Build an n side polygon with a center point and a distance from the center
            var polygon = GeographyGenerator.BuildPolygonOnEarth(centerPoint, 5000, 8);

            // Find a point on earth given a starting point, bearing and distance
            var point = GeographyGenerator.FindPointOnEarth(startingPoint, 30, 7000);

            Console.WriteLine("Welcome to example program.");
            Console.WriteLine("Polygon generated: {0}", polygon);
            Console.WriteLine("Point found: {0}", point);
        }
    }
}
