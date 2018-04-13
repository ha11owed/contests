using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;

namespace Contest
{
    public class GCJ2018_Problem
    {
        private static readonly IFormatProvider formatProvider = CultureInfo.InvariantCulture;

        protected void ReadInput(StreamReader reader)
        {

        }

        protected void SolveAndWrite(int problemIndex, StreamWriter writer)
        {

        }

        public void Run()
        {
            StreamReader reader = null;
            StreamWriter writer = null;
            try
            {
                reader = new StreamReader("input.txt");
                writer = new StreamWriter("output.txt");

                int problemCount = ParseInt(reader.ReadLine());
                for (int problemIndex = 1; problemIndex <= problemCount; problemIndex++)
                {
                    ReadInput(reader);
                    writer.WriteLine("Case #{0}: ", problemIndex);
                    SolveAndWrite(problemIndex, writer);
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

        /// <summary>
        /// Run one problem at a time.
        /// Just initialize the solver with the correct one.
        /// </summary>
        public static void Main(string[] args)
        {
            GCJ2018_Problem instance = new GCJ2018_Problem();
            instance.Run();
        }
    }
}
