using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Contest
{
    public class GCJ2017_D_FashionShow : AGenericProblem
    {
        private int N;
        private int M;
        private char[,] data;

        protected override void ReadInput(StreamReader reader)
        {
            string line = reader.ReadLine();
            string[] parts = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            N = Converters.ParseInt(parts[0]);
            M = Converters.ParseInt(parts[1]);

            data = new char[N, N];
            for (int i = 0; i < M; i++)
            {
                line = reader.ReadLine();
                parts = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                char c = parts[0][0];
                int rowIndex = Converters.ParseInt(parts[1]);
                int colIndex = Converters.ParseInt(parts[2]);
                data[rowIndex, colIndex] = c;
            }
        }

        protected override string SolveAndWrite(int problemIndex, StreamWriter writer)
        {
            throw new NotImplementedException();
        }
    }
}
