using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace scsp.Controllers
{
    public class HelperFunctions
    {

        public static double epoch_seconds(DateTime date){
            var td = date - DateTime.UnixEpoch;
            return td.Days * 86400 + td.Seconds + ((double)(td.Milliseconds) / 1000);
        }
        public static double hotrank(int likes, int dislikes, DateTime date){
            double s = likes - dislikes;
            double order = Math.Log(Math.Max(Math.Abs(s), 1), 10);
            double sign = ( s > 0 ? 1 : ( s < 0 ? -1 : 0 ) ) ;
            double seconds = epoch_seconds(date) - 1664562600;
            return Math.Round(sign * order + seconds / 45000, 7);
        }

        public static double confidence(int likes, int dislikes){
            var n = likes + dislikes;
            if (n == 0) return 0;

            var z = 1.281551565545;
            var p = (double)(likes) / n;

            var left = p + 1/(2*n)*z*z;
            var right = z*Math.Sqrt(p*(1-p)/n + z*z/(4*n*n));
            var under = 1+1/n*z*z;

            return (left - right) / under; 
        }
    }
}