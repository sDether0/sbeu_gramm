using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBEU.Gramm.Middleware
{
    public class TwoPointDistance
    {
        /// <summary>
        /// The Haversine formula is used to calculate the distance between two points on a sphere given
        /// their latitudes and longitudes
        /// </summary>
        /// <param name="lat1">Latitude of the first point</param>
        /// <param name="lng1">longitude of the first point</param>
        /// <param name="lat2">the latitude of the second point</param>
        /// <param name="lng2">longitude of the second point</param>
        /// <returns>
        /// The distance between two points in kilometers.
        /// </returns>
        public static double Calculate(double lat1, double lng1, double lat2, double lng2)
        {

            var result = 2 * Math.Asin(Math.Sqrt(Math.Pow(Math.Sin(DegToRad((lat1 - lat2) / 2)), 2) +
                                                 Math.Cos(DegToRad(lat1)) * Math.Cos(DegToRad(lat2)) *
                                                 Math.Pow(Math.Sin(DegToRad((lng1 - lng2) / 2)), 2))) * 6378245;
            return result/1000;
        }

        /// <summary>
        /// Convert degrees to radians.
        /// </summary>
        /// <param name="x">Angle in degree.</param>
        /// <returns>
        /// Angle in radians.
        /// </returns>
        static double DegToRad(double x)
        {
            var y = x * Math.PI / 180;
            return y;
        }
    }
}
