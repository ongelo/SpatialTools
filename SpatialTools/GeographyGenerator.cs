using System;
using NetTopologySuite.Geometries;

namespace SpatialTools
{
    public static class GeographyGenerator
    {
        public static Polygon BuildPolygonOnEarth(Point point, double distanceInMeters, int n)
        {
            Point[] vertices = new Point[n + 1];
            double degreeIncrement = 360 / n;
            double bearing = 0;
            for (int i = 0; i < n; i++)
            {
                vertices[i] = FindPointOnEarth(point, bearing, distanceInMeters);
                bearing += degreeIncrement;
            }

            // Add the closing vertex
            vertices[n] = vertices[0];

            return new Polygon(new LinearRing(vertices.ToCoordinates()));
        }

        public const double EARTH_RADIUS_IN_METERS = 6371000.0;

        /*
         * Given a starting point, direction and a distance;
         * finds the destination point
         * Reference: Calculation of the destination point
         * https://www.sunearthtools.com/tools/distance/php#contents
         */
        public static Point FindPointOnEarth(Point origin, double degrees, double distanceInMeters)
        {
            // Convert values to radians
            var originLat = GeometryUtils.DegreesToRadians(origin.X);
            var originLon = GeometryUtils.DegreesToRadians(origin.Y);
            var bearing = GeometryUtils.DegreesToRadians(degrees);
            var d = distanceInMeters / EARTH_RADIUS_IN_METERS;

            var lat = Math.Asin(Math.Sin(originLat) * Math.Cos(d) +
                Math.Cos(originLat) * Math.Sin(d) * Math.Cos(bearing));
            var lon = originLon + Math.Atan2(Math.Sin(bearing) * Math.Sin(d) * Math.Cos(originLat),
                Math.Cos(d) - Math.Sin(originLat) * Math.Sin(lat));

            // Convert radians back
            var latDecimal = GeometryUtils.RadiansToDegrees(lat);
            var lonDecimal = GeometryUtils.RadiansToDegrees(lon);

            var coordinate = new Coordinate(latDecimal, lonDecimal);
            return new Point(coordinate);
        }

        private static Coordinate[] ToCoordinates(this Point[] points)
        {
            int length = points.Length;
            Coordinate[] coordinates = new Coordinate[length];
            for (int i = 0; i < length; i++)
            {
                coordinates[i] = new Coordinate(points[i].X, points[i].Y);
            }

            return coordinates;
        }
    }
}
