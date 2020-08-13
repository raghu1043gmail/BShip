using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BShip.Utils
{
    public static class Helper
    {
       public static int RoundUp(int toRound)
        {
            if (toRound % 10 == 0) return toRound;
            return (10 - toRound % 10) + toRound;
        }
    }
}