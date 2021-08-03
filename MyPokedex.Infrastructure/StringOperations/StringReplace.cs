using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPokedex.Infrastructure.StringOperations
{
    public static class StringReplace
    {
        public static string ConvertEscapeCharactersToSpaces(string input)
        {
            return input.Replace("\n", " ").Replace("\f", " ");
        }
    }
}
