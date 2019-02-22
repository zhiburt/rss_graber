using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace web.Util.View
{
    public class Trimer
    {
        /// <summary>
        /// Trim return substring with lenght
        /// when len(string) < lenght return sub 
        /// </summary>
        /// <param name="str">input</param>
        /// <param name="lenght">lenght</param>
        /// <returns>substring</returns>
        public static string Trim(string str, int lenght)
        {
            if (str.Length > lenght)
                return string.Join("", str.Take(lenght)) + "...";

            return str;
        }

        /// <summary>
        /// RemoveAllHTMLTags delete all html tags in string
        /// </summary>
        /// <param name="input">input</param>
        /// <returns></returns>
        public static string RemoveAllHTMLTags(string input)
        {
            return Regex.Replace(input, "<.*?>", String.Empty);
        }
    }
}
