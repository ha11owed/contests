using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Contest
{
    public struct Point2D
    {
        private readonly double _x, _y;

        public double X { get { return _x; } }
        public double Y { get { return _y; } }

        public Point2D(double x, double y)
        {
            _x = x;
            _y = y;
        }

        public static double DistanceSquare(Point2D p1, Point2D p2)
        {
            return (p1.X - p2.X) * (p1.X - p2.X) + (p1.Y - p2.Y) * (p1.Y - p2.Y);
        }

        public static double Distance(Point2D p1, Point2D p2)
        {
            return Math.Sqrt(DistanceSquare(p1, p2));
        }

        public static Point2D MiddlePoint(Point2D p1, Point2D p2)
        {
            return new Point2D(0.5 * (p1.X + p2.X), 0.5 * (p1.Y + p2.Y));
        }

        public override string ToString()
        {
            return String.Format(Helper.FormatProvider, "({0}, {1})", X, Y);
        }
    }

    public struct Vector3D
    {
        private double _x, _y, _z;

        public double X { get { return _x; } }
        public double Y { get { return _y; } }
        public double Z { get { return _z; } }

        public Vector3D(double x, double y, double z)
        {
            _x = x;
            _y = y;
            _z = z;
        }
    }

    public class GCJ2018_Problem
    {
        private static readonly double SQRT2 = Math.Sqrt(2.0);

        private double A;

        private static Vector3D[] GetVectors1Plane()
        {
            return new Vector3D[]
            {
                new Vector3D(0.5, 0.0, 0.0),
                new Vector3D(0.0, 0.5, 0.0),
                new Vector3D(0.0, 0.0, 0.5),
            };
        }

        private static Vector3D[] GetVectors2Planes(double area)
        {
            Vector3D[] vectors = GetVectors2Planes(SQRT2);

            area = area - SQRT2;

            double areaSQ;
            double sideSQ;
            if (Math.Abs(SQRT2 - area) < 0.000001)
            {
                area = SQRT2;
                areaSQ = 2;
                sideSQ = 0;
            }
            else
            {
                areaSQ = area * area;
                sideSQ = 2.0 - areaSQ;
            }

            double hP1 = Math.Sqrt(sideSQ);
            Point2D p1 = new Point2D(-0.5 * area, 0.5 * hP1);
            Point2D p2 = new Point2D(0.5 * area, -0.5 * hP1);

            // Dot Product is zero => p1.x * p3.x + p1.y * p3.y == 0
            double p3x = p1.Y / p1.X;
            double p3y = -1.0;
            double p3len = Math.Sqrt(2.0 * (p3x * p3x + 1));
            Point2D p3 = new Point2D(p3x / p3len, p3y / p3len);
            Point2D p13mid = Point2D.MiddlePoint(p1, p3);
            Point2D p23mid = Point2D.MiddlePoint(p2, p3);

            return new Vector3D[]
            {
                new Vector3D(0.3535533905932738, 0.3535533905932738, 0.0),
                new Vector3D(-0.3535533905932738, 0.3535533905932738, 0.0),
                new Vector3D(0.0, 0.0, 0.5),
            };
        }

        private static Vector3D[] GetVectors3Planes(double area)
        {
            double areaSQ;
            double sideSQ;
            if (Math.Abs(SQRT2 - area) < 0.000001)
            {
                area = SQRT2;
                areaSQ = 2;
                sideSQ = 0;
            }
            else
            {
                areaSQ = area * area;
                sideSQ = 2.0 - areaSQ;
            }

            double hP1 = Math.Sqrt(sideSQ);
            Point2D p1 = new Point2D(-0.5 * area, 0.5 * hP1);
            Point2D p2 = new Point2D(0.5 * area, -0.5 * hP1);

            // Dot Product is zero => p1.x * p3.x + p1.y * p3.y == 0
            double p3x = p1.Y / p1.X;
            double p3y = -1.0;
            double p3len = Math.Sqrt(2.0 * (p3x * p3x + 1));
            Point2D p3 = new Point2D(p3x / p3len, p3y / p3len);
            Point2D p13mid = Point2D.MiddlePoint(p1, p3);
            Point2D p23mid = Point2D.MiddlePoint(p2, p3);

            return new Vector3D[]
            {
                new Vector3D(p13mid.X, p13mid.Y, 0.0),
                new Vector3D(p23mid.X, p23mid.Y, 0.0),
                new Vector3D(0.0, 0.0, 0.5),
            };
        }

        private static double CalculateSideInRect(double hypotenuse, double otherSide)
        {
            if (Math.Abs(hypotenuse - otherSide) < 0.000001)
                return 0.0;
            double side = Math.Sqrt(hypotenuse * hypotenuse - otherSide * otherSide);
            return side;
        }

        private double[] Rotate(double x, double y, double angle)
        {
            double ca = Math.Cos(angle);
            double sa = Math.Sin(angle);
            return new double[] { ca * x - sa * y, sa * x + ca * y };
        }

        private double[] SolveQuadraticEquation(double a, double b, double c)
        {
            double[] r = new double[]
            {
                (-b + Math.Sqrt(b * b - 4 * a * c)) / (2 * a),
                (-b - Math.Sqrt(b * b - 4 * a * c)) / (2 * a),
            };
            return r;
        }

        protected void SolveAndWrite(int problemIndex, TextWriter writer)
        {
            Vector3D[] vectors = null;

            if(A == 1.0)
            {
                vectors = GetVectors1Plane();
            }
            else if(A <= 1.414213f)
            {
                vectors = GetVectors2Planes(A);
            }
            else
            {

            }
            foreach (Vector3D vector in vectors)
            {
                writer.WriteLine();
                writer.Write(String.Format(Helper.FormatProvider, "{0} {1} {2}", vector.X, vector.Y, vector.Z));
            }
        }

        protected void ReadInput(TextReader reader)
        {
            A = Helper.ParseFloat(reader.ReadLine());
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
                    writer.Write("Case #{0}:", problemIndex);
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
