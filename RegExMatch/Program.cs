using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RegExMatch
{
    class Program
    {
        static void Main(string[] args)
        {
            string text = "One car red car blue car";
            string pat = @"(\w+)\s+(car)";

            // Instantiate the regular expression object.
            Regex r = new Regex(pat, RegexOptions.IgnoreCase);

            // Match the regular expression pattern against a text string.
            Match m = r.Match(text);
            int matchCount = 0;
            while (m.Success)
            {
                Console.WriteLine("Match {0} --- {1}", (++matchCount), m.Value);
                for (int i = 1; i < m.Groups.Count; i++)
                {
                    Group g = m.Groups[i];
                    Console.WriteLine("Group" + i + "='" + g + "'");
                    CaptureCollection cc = g.Captures;
                    for (int j = 0; j < cc.Count; j++)
                    {
                        Capture c = cc[j];
                        System.Console.WriteLine("Capture" + j + "='" + c + "', Position=" + c.Index);
                    }
                }
                m = m.NextMatch();
            }

            Console.ReadKey();
        }
    }
}
