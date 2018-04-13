using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Contest
{
    /// <summary>
    /// TODO: optimization for the big input
    /// </summary>
    public class GCJ2018_Problem
    {
        private int[] v;
        private int n;

        private void TroubleSort()
        {
            bool done = false;
            while (!done)
            {
                done = true;
                for (int i = 0; i < n - 2; i++)
                {
                    if (v[i] > v[i + 2])
                    {
                        done = false;
                        int tmp = v[i];
                        v[i] = v[i + 2];
                        v[i + 2] = tmp;
                    }
                }
            }
        }

        private int IncorrectIndex()
        {
            int failIndex = -1;
            for (int i = 0; i < n - 1; i++)
            {
                if (v[i] > v[i + 1])
                {
                    failIndex = i;
                    break;
                }
            }
            return failIndex;
        }

        protected void SolveAndWrite(int problemIndex, TextWriter writer)
        {
            TroubleSort();
            int failIndex = IncorrectIndex();

            if (failIndex >= 0)
            {
                writer.Write(failIndex);
            }
            else
            {
                writer.Write("OK");
            }
        }

        protected void ReadInput(TextReader reader)
        {
            n = Helper.ParseInt(reader.ReadLine());
            v = Helper.ParseInts(reader.ReadLine());
            if (v.Length != n)
            {
                throw new InvalidOperationException();
            }
        }

        public void Run()
        {
            TextReader reader = null;
            TextWriter writer = null;
            try
            {
                reader = new StreamReader("input.txt");
                writer = new StreamWriter("output.txt");
                //reader = Console.In;
                //writer = Console.Out;

                int problemCount = Helper.ParseInt(reader.ReadLine());
                for (int problemIndex = 1; problemIndex <= problemCount; problemIndex++)
                {
                    ReadInput(reader);
                    writer.Write("Case #{0}: ", problemIndex);
                    SolveAndWrite(problemIndex, writer);
                    writer.WriteLine();
                }
            }
            catch (Exception e)
            {
                String msg = "Exception: " + e;
                Console.WriteLine(msg);
                Debug.Fail(msg);
            }
            finally
            {
                if (reader != null)
                    reader.Dispose();
                if (writer != null)
                    writer.Dispose();
            }
        }
    }

    public class Helper
    {
        /// <summary>
        /// Run one problem at a time.
        /// Just initialize the solver with the correct one.
        /// </summary>
        public static void Main(string[] args)
        {
            GCJ2018_Problem instance = new GCJ2018_Problem();
            instance.Run();
        }

        public static int ParseInt(string value)
        {
            return Int32.Parse(value, formatProvider);
        }

        public static int[] ParseInts(string line)
        {
            string[] parts = line.Split(' ');
            int[] result = new int[parts.Length];
            for (int i = 0; i < parts.Length; i++)
            {
                result[i] = ParseInt(parts[i]);
            }
            return result;
        }

        private static readonly IFormatProvider formatProvider = CultureInfo.InvariantCulture;
    }
}
