
using System.Collections.Generic;

namespace cherwellTechnical.Mappings
{
    public static class LetterMapping
    {
        public static IDictionary<int, string> ToLetter = new Dictionary<int, string>()
        {
            { 0, "A" },
            { 1, "B" },
            { 2, "C" },
            { 3, "D" },
            { 4, "E" },
            { 5, "F" }
        };

        public static IDictionary<string, int> ToPosition = new Dictionary<string, int>()
        {
            { "A", 0 },
            { "B", 1 },
            { "C", 2 },
            { "D", 3 },
            { "E", 4 },
            { "F", 5 }
        };
    }
}
