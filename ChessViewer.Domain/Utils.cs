using System;
using System.IO;
using System.Collections.Generic;

namespace ChessViewer.Domain
{
    public static class Utils
    {
        public static IEnumerable<string> SplitToLines(this string input)
        {
            if (input == null)
            {
                yield break;
            }

            using (StringReader reader = new StringReader(input))
            {
                while (true)
                {
                    var line = reader.ReadLine();
                    if (line == null)
                        yield break;

                    yield return line;
                }
            }
        }

        public static bool IsNullOrEmpty(this string input) 
        {
            return string.IsNullOrEmpty(input);
        }
    }
}
