using System;
using System.Text.RegularExpressions;

namespace Sprinter.Helpers
{
    public static class EnumExtensions
    {
        public static string ToReadable(this Enum e)
        {
            return ToReadable(e.ToString());
        }

        /// <summary>
        /// Add spaces to separate the capitalized words in the string, 
        /// i.e. insert a space before each uppercase letter that is 
        /// either preceded by a lowercase letter or followed by a 
        /// lowercase letter (but not for the first char in string). 
        /// This keeps groups of uppercase letters - e.g. acronyms - together.
        /// </summary>
        /// <param name="pascalCaseString">A string in PascalCase</param>
        /// <returns></returns>
        private static string ToReadable(string pascalCaseString)
        {
            var r = new Regex("(?<=[a-z])(?<x>[A-Z])|(?<=.)(?<x>[A-Z])(?=[a-z])");
            var spacedString = r.Replace(pascalCaseString, " ${x}");

            return spacedString.Replace(" And ", " and ");
        }
    }
}