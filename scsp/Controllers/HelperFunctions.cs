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
            double seconds = epoch_seconds(date) - 1134028003;
            return Math.Round(sign * order + seconds / 45000, 7);
        }
    }
}