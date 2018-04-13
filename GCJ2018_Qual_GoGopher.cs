using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

/// <summary>
/// The Gopher will be deployed in a Divide and Conquer manner.
/// </summary>
namespace Contest
{
    public class Rect
    {
        private readonly int _x, _y;
        private readonly int _lx, _ly;

        public Rect(int x, int y, int lx, int ly)
        {
            _x = x;
            _y = y;
            _lx = lx;
            _ly = ly;
        }

        public Rect GrowToInclude(int x, int y)
        {
            int minX = Math.Min(X, x);
            int minY = Math.Min(Y, y);
            int maxX = Math.Max(X2, x);
            int maxY = Math.Max(Y2, y);
            return new Rect(minX, minY, maxX - minX + 1, maxY - minY + 1);
        }

        public Rect GrowToInclude(Rect rect)
        {
            int minX = Math.Min(_x, rect._x);
            int minY = Math.Min(_y, rect._y);
            int maxX = Math.Max(X2, rect.X2);
            int maxY = Math.Max(Y2, rect.Y2);
            return new Rect(minX, minY, maxX - minX + 1, maxY - minY + 1);
        }

        public int MidX { get { return _x + _lx / 2; } }

        public int MidY { get { return _y + _ly / 2; } }

        public int X { get { return _x; } }

        public int Y { get { return _y; } }

        public int X2 { get { return _x + _lx - 1; } }

        public int Y2 { get { return _y + _ly - 1; } }

        public int SizeX { get { return _lx; } }

        public int SizeY { get { return _ly; } }

        public int Area { get { return _lx * _ly; } }

        public int CountIn(int[,] map)
        {
            int x2 = X2;
            int y2 = Y2;
            int n = 0;
            for (int i = _x; i <= x2; i++)
                for (int j = _y; j <= y2; j++)
                    n += map[i, j];
            return n;
        }

        public override bool Equals(object obj)
        {
            if (obj == this)
                return true;
            Rect other = (Rect)obj;
            return _x == other._x
                && _y == other._y
                && _lx == other._lx
                && _ly == other._ly;
        }

        public override int GetHashCode()
        {
            return _x + (_y << 3) + (_lx << 5) + (_ly << 7);
        }

        public override string ToString()
        {
            return string.Format("({0}, {1}) l=[{2}, {3}]", _x, _y, _lx, _ly);
        }

        public String GetDebugMap(int[,] map)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = X; i <= X2; i++)
            {
                for (int j = Y; j <= Y2; j++)
                    sb.Append(map[i, j]);

                sb.AppendLine();
            }
            String r = sb.ToString();
            return r;
        }
    }

    public class GCJ2018_Problem
    {
        private const int MAX_SIZE = 1000;
        private const int GOPHER_SIZE = 3;

        private readonly Random rand = new Random();

        private int A;
        private int[,] map;
        private Rect[] allowedFills;
        private int deployCount;

        private static Rect[] GetAllowedFills(int desiredArea)
        {
            int avg = (int)Math.Sqrt(desiredArea);
            int max = Math.Max(avg + 1, GOPHER_SIZE);

            int minA = int.MaxValue;
            List<Rect> minFills = new List<Rect>();

            for (int i = 3; i <= max; i++)
            {
                for (int j = 3; j <= max; j++)
                {
                    int a = i * j;
                    if (a < desiredArea || a > minA)
                        continue;

                    if (a < minA)
                    {
                        minA = a;
                        minFills.Clear();
                    }
                    minFills.Add(new Rect(0, 0, i, j));
                }
            }

            return minFills.ToArray();
        }

        private bool IsValidForBestFit(Rect fillRect, Rect rect, Rect[] allowedFills)
        {
            Rect increased = fillRect.GrowToInclude(rect);
            return allowedFills.Any(r => increased.SizeX <= r.SizeX && increased.SizeY <= r.SizeY);
        }

        private Rect FindBestExt(Rect fillRect, int expectedCount)
        {
            int x = fillRect.X;
            int y = fillRect.Y;
            int x2 = fillRect.X2;
            int y2 = fillRect.Y2;

            for (int depth = 1; depth < GOPHER_SIZE; depth++)
            {
                // Corners
                {
                    Rect rect;
                    rect = new Rect(x - depth, y - depth, GOPHER_SIZE, GOPHER_SIZE);
                    if (rect.CountIn(map) == expectedCount && IsValidForBestFit(fillRect, rect, allowedFills))
                        return rect;

                    rect = new Rect(x2 - depth, y2 - depth, GOPHER_SIZE, GOPHER_SIZE);
                    if (rect.CountIn(map) == expectedCount && IsValidForBestFit(fillRect, rect, allowedFills))
                        return rect;

                    rect = new Rect(x2 - depth, y - depth, GOPHER_SIZE, GOPHER_SIZE);
                    if (rect.CountIn(map) == expectedCount && IsValidForBestFit(fillRect, rect, allowedFills))
                        return rect;

                    rect = new Rect(x - depth, y2 - depth, GOPHER_SIZE, GOPHER_SIZE);
                    if (rect.CountIn(map) == expectedCount && IsValidForBestFit(fillRect, rect, allowedFills))
                        return rect;
                }

                // X Lines
                for (int i = x; i <= x2; i++)
                {
                    Rect rect = new Rect(i, y - depth, GOPHER_SIZE, GOPHER_SIZE);
                    if (rect.CountIn(map) != expectedCount)
                        continue;
                    if (!IsValidForBestFit(fillRect, rect, allowedFills))
                        break;
                    return rect;
                }
                for (int i = x; i <= x2; i++)
                {
                    Rect rect = new Rect(i, y2 - depth, GOPHER_SIZE, GOPHER_SIZE);
                    if (rect.CountIn(map) != expectedCount)
                        continue;
                    if (!IsValidForBestFit(fillRect, rect, allowedFills))
                        break;
                    return rect;
                }

                // Y Lines
                for (int i = y; i <= y2; i++)
                {
                    Rect rect = new Rect(x - depth, i, GOPHER_SIZE, GOPHER_SIZE);
                    if (rect.CountIn(map) != expectedCount)
                        continue;
                    if (!IsValidForBestFit(fillRect, rect, allowedFills))
                        break;
                    return rect;
                }
                for (int i = y; i <= y2; i++)
                {
                    Rect rect = new Rect(x2 - depth, i, GOPHER_SIZE, GOPHER_SIZE);
                    if (rect.CountIn(map) != expectedCount)
                        continue;
                    if (!IsValidForBestFit(fillRect, rect, allowedFills))
                        break;
                    return rect;
                }
            }

            return null;
        }

        private Rect FindBest(Rect fillRect, int expectedCount)
        {
            int x = fillRect.X;
            int y = fillRect.Y;
            int x2 = fillRect.X2 - GOPHER_SIZE + 1;
            int y2 = fillRect.Y2 - GOPHER_SIZE + 1;

            // X Lines
            for (int i = x; i <= x2; i++)
                for (int j = y; j <= y2; j++)
                {
                    Rect rect = new Rect(i, j, GOPHER_SIZE, GOPHER_SIZE);
                    if (rect.CountIn(map) != expectedCount)
                        continue;
                    return rect;
                }

            return null;
        }

        protected void SolveAndWrite(int problemIndex, TextReader reader, TextWriter writer)
        {
            const int GOPHER_AREA = GOPHER_SIZE * GOPHER_SIZE;

            map = new int[MAX_SIZE, MAX_SIZE];
            allowedFills = GetAllowedFills(A);
            deployCount = 0;

            Rect fillRect = new Rect(MAX_SIZE / 2 - 1, MAX_SIZE / 2 - 1, GOPHER_SIZE, GOPHER_SIZE);
            {
                int[] r = Deploy(reader, writer, fillRect.MidX, fillRect.MidY);
                map[r[0], r[1]] = 1;
            }
            while (fillRect.Area < A)
            {
                // Find the best way to increase the search area.
                Rect best = null;
                for (int expectedCount = 0; expectedCount < GOPHER_AREA; expectedCount++)
                {
                    best = FindBestExt(fillRect, expectedCount);
                    if (best != null)
                        break;
                }

                int[] r = Deploy(reader, writer, best.MidX, best.MidY);
                if (IsDoneOrError(r))
                    return;
                int rx = r[0];
                int ry = r[1];
                map[rx, ry] = 1;
                fillRect = fillRect.GrowToInclude(rx, ry);
            }

            while (true)
            {
                Rect best = null;
                for (int expectedCount = 0; expectedCount < GOPHER_AREA; expectedCount++)
                {
                    best = FindBest(fillRect, expectedCount);
                    if (best != null)
                        break;
                }

                if (best == null)
                    break;

                int[] r = Deploy(reader, writer, best.MidX, best.MidY);
                if (IsDoneOrError(r))
                    return;
                map[r[0], r[1]] = 1;
            }
        }

        private bool IsDoneOrError(int[] r)
        {
            if (deployCount >= 1000)
                return true;
            return r[0] <= 0 && r[1] <= 0;
        }
        
        private int[] Deploy(TextReader reader, TextWriter writer, int x, int y)
        {
            deployCount++;
            writer.WriteLine(x + " " + y);
            writer.Flush();
            int[] result = Helper.ParseInts(reader.ReadLine());

            // Fake the read for testing:
            //int[] result = new int[] { x + rand.Next(3) - 1, y + rand.Next(3) - 1 };
            return result;
        }

        protected void ReadInput(TextReader reader)
        {
            A = Helper.ParseInt(reader.ReadLine());
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
                    SolveAndWrite(problemIndex, reader, writer);
                }
            }
            catch (Exception e)
            {
                String msg = "Exception: " + e;
                Console.Error.WriteLine(msg);
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
