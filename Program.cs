namespace Contest
{
    class Program
    {
        /// <summary>
        /// Run one problem at a time.
        /// Just initialize the solver with the correct one.
        /// </summary>
        static void Main(string[] args)
        {
            IProblem problem = new GCJ2017_B_TidyNumbers();
            problem.Run();
        }
    }
}
