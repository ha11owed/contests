using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Contest
{
    public class GCJ2017_B_TidyNumbers : ALineProblem
    {
        protected override string SolveProblem(int problemIndex, string line)
        {
            // Read data
            int n = line.Length;
            int[] digits = new int[n];
            for (int i = 0; i < n; i++)
            {
                digits[i] = line[i] - '0';
            }

            // Solve
            while (true)
            {
                bool restart = false;
                for (int i = 0; i < n - 1; i++)
                {
                    if (digits[i] > digits[i + 1])
                    {
                        digits[i]--;
                        for (int j = i + 1; j < n; j++)
                            digits[j] = 9;
                        restart = true;
                        break;
                    }
                }

                if (!restart)
                    break;
            }
            // Print
            StringBuilder sb = new StringBuilder();
            int start = (digits[0] == 0) ? 1 : 0;
            for (int i = start; i < n; i++)
            {
                sb.Append(digits[i]);
            }
            string result = sb.ToString();
            return result;
        }
    }
}
