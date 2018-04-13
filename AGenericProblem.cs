using System;
using System.Diagnostics;
using System.IO;

namespace Contest
{
    public abstract class AGenericProblem : IProblem
    {
        protected abstract void ReadInput(StreamReader reader);

        public void Run()
        {
            StreamReader reader = null;
            StreamWriter writer = null;
            try
            {
                reader = new StreamReader("input.txt");
                writer = new StreamWriter("output.txt");

                int problemCount = Converters.ParseInt(reader.ReadLine());
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

        protected abstract string SolveAndWrite(int problemIndex, StreamWriter writer);
    }
}
