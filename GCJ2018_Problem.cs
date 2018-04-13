using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Contest
{
    public class GCJ2018_Problem
    {
        protected void SolveAndWrite(int problemIndex, TextWriter writer)
        {
        }

        protected void ReadInput(TextReader reader)
        {
        }

        public void Run()
        {
            TextReader reader = null;
            TextWriter writer = null;
            try
            {
                reader = new StreamReader("input.txt");
                writer = new StreamWriter("output.txt");

                // The default buffer size for the console is 256 bytes.
                // Depending on the size of the input this might not be enough.
                reader = Console.In;
                writer = Console.Out;

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

    /// <summary>
    /// Helpers for converting input/output data.
    /// </summary>
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
            return Int32.Parse(value, FormatProvider);
        }

        public static double ParseDouble(string value)
        {
            return Double.Parse(value, FormatProvider);
        }

        public static float ParseFloat(string value)
        {
            return Single.Parse(value, FormatProvider);
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

        public static string ToStr(double v)
        {
            return v.ToString(FormatProvider);
        }

        public static readonly IFormatProvider FormatProvider = CultureInfo.InvariantCulture;
    }
}
