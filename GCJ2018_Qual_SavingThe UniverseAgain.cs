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
        private int shield;
        private char[] instructions;
        private int n;

        private bool Hack()
        {
            int n = instructions.Length;
            for (int i = 0; i < n - 1; i++)
            {
                if (instructions[i] == 'C' && instructions[i] != instructions[i + 1])
                {
                    char tmp = instructions[i];
                    instructions[i] = instructions[i + 1];
                    instructions[i + 1] = tmp;
                    return true;
                }
            }
            return false;
        }

        private int CalcDamage()
        {
            int totalDamage = 0;

            int current = 1;
            for (int i = 0; i < n; i++)
            {
                if (instructions[i] == 'S')
                {
                    totalDamage += current;
                }
                else
                {
                    current = current << 1;
                }
            }
            return totalDamage;
        }

        protected void SolveAndWrite(int problemIndex, TextWriter writer)
        {
            int hacks = 0;

            int damage = CalcDamage();
            while (damage > shield)
            {
                bool hacked = Hack();
                if (!hacked)
                {
                    break;
                }
                hacks++;
                damage = CalcDamage();
            }

            if (damage > shield)
            {
                writer.Write("IMPOSSIBLE");
            }
            else
            {
                writer.Write(hacks);
            }
        }

        protected void ReadInput(TextReader reader)
        {
            String[] parts = reader.ReadLine().Split(' ');
            shield = Helper.ParseInt(parts[0]);
            instructions = parts[1].ToCharArray();
            n = instructions.Length;
        }

        public void Run()
        {
            TextReader reader = null;
            TextWriter writer = null;
            try
            {
                //reader = new StreamReader("input.txt");
                //writer = new StreamWriter("output.txt");
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

    public enum HeapType
    {
        Max,
        Min
    }

    public sealed class Heap<T> where T : IComparable<T>
    {
        internal delegate bool IndexComparor(int childIx, int parentIx);
        internal IndexComparor IsHigherPriorityThanParent;

        /// <summary>
        /// Constructs a new max heap.
        /// </summary>
        public Heap() : this(HeapType.Max) { }

        /// <summary>
        /// Constructs a new heap with the given type, allowing you to specify if this heap will be a min or a max heap.
        /// </summary>
        /// <param name="type"></param>
        public Heap(HeapType type)
        {
            this.List = new List<T>();
            this.Init(type);
        }

        /// <summary>
        /// Constructs a new heap with the given type and initial capacity.
        /// </summary>
        /// <param name="type">The type of heap that determines if it will be a min or max heap.</param>
        /// <param name="capacity">The initial capacity of the heap.</param>
        public Heap(HeapType type, int capacity)
        {
            this.List = new List<T>(capacity);
            this.Init(type);
        }

        /// <summary>
        /// Initializes some properties of the tree
        /// </summary>
        /// <param name="type"></param>
        private void Init(HeapType type)
        {
            this.Type = type;

            if (type == HeapType.Max)
                this.IsHigherPriorityThanParent = this.IsLargerThan;
            else
                this.IsHigherPriorityThanParent = this.IsSmallerThanParent;
        }

        /// <summary>
        /// Identifies the heap as either being a max heap or a min heap
        /// </summary>
        public HeapType Type { get; private set; }

        internal List<T> List { get; set; }

        /// <summary>
        /// Returns the number of items in the heap
        /// </summary>
        public int Count { get { return this.List.Count; } }

        /// <summary>
        /// Given an index of a node, this method returns the index of that node's parent
        /// </summary>
        internal static int ParentIndexOf(int ix)
        {
            if (ix == 0)
                throw new InvalidOperationException("ParentOfRootDoesntExist");
            else
            {
                return (int)Math.Floor((double)(ix - 1) / 2);
            }
        }

        /// <summary>
        /// Given an index of a node, this method returns the index of that node's left child
        /// </summary>
        internal static int LeftChildIndexOf(int ix)
        {
            return (2 * ix) + 1;
        }

        /// <summary>
        /// Given an index of a node, this method returns the index of that node's right child
        /// </summary>
        internal static int RightChildIndexOf(int ix)
        {
            return (2 * ix) + 2;
        }

        /// <summary>
        /// Returns true if the value of the node at the first index is larger than the value of the node at the second index.
        /// </summary>
        /// <param name="first">The index of the first value</param>
        /// <param name="second">The index of the second value</param>
        internal bool IsLargerThan(int first, int second)
        {
            return this.List[first].CompareTo(this.List[second]) > 0;
        }

        /// <summary>
        /// Returns true if the value of the node at the first index is smaller than the value of the node at the second index.
        /// </summary>
        /// <param name="first">The index of the first value</param>
        /// <param name="second">The index of the second value</param>
        internal bool IsSmallerThanParent(int first, int second)
        {
            return this.List[first].CompareTo(this.List[second]) < 0;
        }

        /// <summary>
        /// Swaps the value at index ix1 with the value at index ix2
        /// </summary>
        internal void Swap(int ix1, int ix2)
        {
            T temp = this.List[ix1];
            this.List[ix1] = this.List[ix2];
            this.List[ix2] = temp;
        }

        /// <summary>
        /// Inserts the given value into the heap, maintaining the heap property as defined by Heap.HeapType
        /// </summary>
        public void Insert(T value)
        {
            int insertedAtIndex = this.List.Count;
            this.List.Add(value);
            this.HeapifyUp(insertedAtIndex);
        }

        /// <summary>
        /// Helper function that takes the node inserted at insertedIx and makes sure it is bubbled up to the correct place in the heap.
        /// </summary>
        internal void HeapifyUp(int insertedIx)
        {
            int currentIx = insertedIx;

            while (currentIx > 0)
            {
                int parentIx = ParentIndexOf(currentIx);
                if (this.IsHigherPriorityThanParent(currentIx, parentIx))
                    this.Swap(currentIx, parentIx);
                else
                    break;

                currentIx = parentIx;
            }
        }

        /// <summary>
        /// Removes the node at the top of the heap and returns its value.
        /// </summary>
        /// <returns>The largest node in the heap if the HeapType = Max, the smallest node in the heap if the HeapType = Min</returns>
        public T Remove()
        {
            int lastIx = this.List.Count - 1;
            T result = default(T);
            if (lastIx < 0)
                throw new InvalidOperationException("Cannot remove from empty heap");
            else
            {
                result = this.List[0];

                if (lastIx > 0)
                    this.List[0] = this.List[lastIx];

                this.List.RemoveAt(lastIx);
                this.HeapifyDown();
            }

            return result;
        }

        /// <summary>
        /// Starting at the top of the heap, examines all nodes underneath it ensuring that the heap property is maintained at all nodes.
        /// </summary>
        internal void HeapifyDown()
        {
            int parentIx = 0;
            while (parentIx < this.Count)
            {
                int leftIx = LeftChildIndexOf(parentIx);
                int rightIx = RightChildIndexOf(parentIx);
                int largest = parentIx;

                if (leftIx < this.Count && this.IsHigherPriorityThanParent(leftIx, largest))
                    largest = leftIx;

                if (rightIx < this.Count && this.IsHigherPriorityThanParent(rightIx, largest))
                    largest = rightIx;

                if (largest != parentIx)
                {
                    this.Swap(largest, parentIx);
                    parentIx = largest;
                }
                else
                    break;
            }
        }

        /// <summary>
        /// Returns the value of the node at the top of the heap without removing it.
        /// </summary>
        public T Peek()
        {
            if (this.List.Count > 0)
                return this.List[0];
            else
                throw new InvalidOperationException("Empty heap");
        }
    }
}
