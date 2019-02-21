using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web.Util.View
{
    public class Trimer
    {
        public static string Trim(string str, int lenght)
        {
            if (str.Length > lenght)
                return string.Join("", str.Take(lenght)) + "...";

            return str;
        }
    }
}
