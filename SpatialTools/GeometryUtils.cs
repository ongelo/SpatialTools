using System;

namespace SpatialTools
{
    public static class GeometryUtils
    {
        public static double DegreesToRadians(double degrees) => degrees * Math.PI / 180;

        public static double RadiansToDegrees(double radians) => radians * 180 / Math.PI;
    }
}
