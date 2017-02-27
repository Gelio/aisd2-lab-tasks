using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace ASD
{
    public enum TestResult
    {
        PASSED,
        TIMEOUT,
        EXCEPTION,
        BAD_RESULT,
        BAD_SOLUTION
    };

    public abstract class TestCaseBase
    {
        private static int instanceCounter = 0;
        protected int myNumber;
        public static readonly int debugerThreadingTime = 80;
        public readonly double timeout;
        protected abstract TestResult IsGoodResult(bool verbose);
        protected TestResult testResult;
        private Thread thread;
        private Exception exception;
        protected Stopwatch sw;

        public TestCaseBase(double timeout)
        {
            this.timeout = timeout;
            myNumber = ++instanceCounter;
        }

        public virtual void PrintResult()
        {
            Console.WriteLine(testResult);
        }

        protected TestResult CheckResults(Exception exception, bool verbose)
        {
            if (exception != null)
                return testResult=TestResult.EXCEPTION;
            return IsGoodResult(verbose);
        }

        protected abstract void YourTest();

        public TestResult PerformTest(bool enforceTimeout, double systemSpeedFactor, bool verbose, bool swallowExceptions)
        {
            sw = new Stopwatch();
            exception = null;
            CreateThread(swallowExceptions);
            var result =  RunThread(enforceTimeout, systemSpeedFactor, verbose);
            PrintResult();
            return result;
        }

        private TestResult RunThread(bool enforceTimeout, double systemSpeedFactor, bool verbose)
        {
            thread.Start();
            if (enforceTimeout)
            {
                if (!thread.Join(debugerThreadingTime + (int)Math.Ceiling(timeout * systemSpeedFactor)))
                {
                    thread.Abort();
                    return testResult=TestResult.TIMEOUT;
                }
                else
                {
                    return CheckResults(exception, verbose);
                }
            }
            else
            {
                thread.Join();
                return CheckResults(exception, verbose);
            }
        }

        private void CreateThread(bool swallowExceptions)
        {
            thread = new Thread(() =>
            {
                try
                {
                    YourTest();
                }
                catch (Exception ex)
                {
                    if (!swallowExceptions)
                    {
                        throw ex;
                    }
                    else
                    {
                        exception = ex;
                    }
                }

            });
        }
    }

    public class TestCaseSegment : TestCaseBase
    {
        public readonly List<Geometry.Segment> segments;
        double correctResult;
        double studentsResult;

        public TestCaseSegment(List<Geometry.Segment> segments, double correctAnswer, double timeout)
            : base(timeout)
        {
            this.segments = segments;
            this.correctResult = correctAnswer;
        }

        protected override TestResult IsGoodResult(bool verbose)
        {
            if (Math.Abs(correctResult - studentsResult) <= 0.005)
                return testResult=TestResult.PASSED;
            else
                return testResult=TestResult.BAD_RESULT;
        }

        protected override void YourTest()
        {
            sw.Start();
            SweepLine sl = new SweepLine();
            studentsResult = sl.VerticalSegmentsUnionLength(segments.ToArray());
            sw.Stop();
        }

        public override void PrintResult()
        {
            switch(testResult)
            {
                case TestResult.PASSED:
                    Console.WriteLine("Test {2,2}: Gratulacje! Wynik ktory wyszedl to: {0,3}, a oczekiwalismy {1,3}",studentsResult, correctResult, myNumber);
                    break;
                case TestResult.BAD_RESULT:
                    Console.WriteLine("Test {2,2}: Porazka! Wynik ktory wyszedl to: {0,3}, a oczekiwalismy {1,3}", studentsResult, correctResult, myNumber);
                    break;
                default:
                    Console.WriteLine("Test {0,2}: {1}", myNumber, testResult);
                    break;
            }
        }
    }

    public class TestCaseRectangle : TestCaseBase
    {
        public readonly List<Geometry.Rectangle> rectangles;
        double correctResult;
        double studentsResult;

        public TestCaseRectangle(List<Geometry.Rectangle> rectangles, double correctAnswer, double timeout)
            : base(timeout)
        {
            this.rectangles = rectangles;
            this.correctResult = correctAnswer;
        }

        protected override TestResult IsGoodResult(bool verbose)
        {
            if (Math.Abs(correctResult - studentsResult) <= 0.005)
                return testResult = TestResult.PASSED;
            else
                return testResult = TestResult.BAD_RESULT;
        }

        protected override void YourTest()
        {
            sw.Start();
            SweepLine sl = new SweepLine();
            studentsResult = sl.RectanglesUnionArea(rectangles.ToArray());
            sw.Stop();
        }

        public override void PrintResult()
        {
            switch (testResult)
            {
                case TestResult.PASSED:
                    Console.WriteLine("Test {2,2}: Gratulacje! Wynik ktory wyszedl to: {0,3}, a oczekiwalismy {1,3}", studentsResult, correctResult, myNumber);
                    break;
                case TestResult.BAD_RESULT:
                    Console.WriteLine("Test {2,2}: Porazka! Wynik ktory wyszedl to: {0,3}, a oczekiwalismy {1,3}", studentsResult, correctResult, myNumber);
                    break;
                default:
                    Console.WriteLine("Test {0,2}: {1}", myNumber, testResult);
                    break;
            }
        }
    }



    public class Program
    {

        public static int fib(int n)
        {
            if (n == 1 || n == 2)
                return 1;

            return fib(n - 1) + fib(n - 2);

        }

        public static void Main(string[] args)
        {
            // czy wypisywać nieprawidłowe przypisania?
            const Boolean verbose = false;

            // czy przechwytywać wyjątki?
            const Boolean swallowExceptions = true;

            Stopwatch sw = new Stopwatch();
            sw.Start();
            fib(26);
            sw.Stop();

            const double jajkoConstant = 0.5;

            double speedFactor = sw.ElapsedMilliseconds * jajkoConstant;

            System.Console.WriteLine("System performance meter: {0}\n", speedFactor);
            Console.WriteLine("ETAP 1:\n");

            List<Geometry.Segment> testSegment1 = new List<Geometry.Segment>();
            testSegment1.Add(new Geometry.Segment(new Geometry.Point(0, -2), new Geometry.Point(0, -1)));

            List<Geometry.Segment> testSegment2 = new List<Geometry.Segment>();
            testSegment2.Add(new Geometry.Segment(new Geometry.Point(0, -1), new Geometry.Point(0, -2)));

            List<Geometry.Segment> testSegment3 = new List<Geometry.Segment>();
            testSegment3.Add(new Geometry.Segment(new Geometry.Point(0, -2), new Geometry.Point(0, -1)));
            testSegment3.Add(new Geometry.Segment(new Geometry.Point(0, 0), new Geometry.Point(0, 1)));

            List<Geometry.Segment> testSegment4 = new List<Geometry.Segment>();
            testSegment4.Add(new Geometry.Segment(new Geometry.Point(0, -2), new Geometry.Point(0, -1)));
            testSegment4.Add(new Geometry.Segment(new Geometry.Point(0, 0), new Geometry.Point(0, 1)));
            testSegment4.Add(new Geometry.Segment(new Geometry.Point(0, 1), new Geometry.Point(0, 2)));

            List<Geometry.Segment> testSegment5 = new List<Geometry.Segment>();
            testSegment5.Add(new Geometry.Segment(new Geometry.Point(0, -2), new Geometry.Point(0, -1)));
            testSegment5.Add(new Geometry.Segment(new Geometry.Point(0, 0), new Geometry.Point(0, 1)));
            testSegment5.Add(new Geometry.Segment(new Geometry.Point(0, 0.5), new Geometry.Point(0, 2)));

            List<Geometry.Segment> testSegment6 = new List<Geometry.Segment>();
            testSegment6.Add(new Geometry.Segment(new Geometry.Point(0, -2), new Geometry.Point(0, -1)));
            testSegment6.Add(new Geometry.Segment(new Geometry.Point(0, 0), new Geometry.Point(0, 1)));
            testSegment6.Add(new Geometry.Segment(new Geometry.Point(0, 1), new Geometry.Point(0, 3)));

            List < Geometry.Segment > testSegment7 = new List<Geometry.Segment>();
            testSegment7.Add(new Geometry.Segment(new Geometry.Point(0, 1), new Geometry.Point(0, 2)));
            testSegment7.Add(new Geometry.Segment(new Geometry.Point(0, 3), new Geometry.Point(0, 5)));
            testSegment7.Add(new Geometry.Segment(new Geometry.Point(0, 3), new Geometry.Point(0, 7)));
            testSegment7.Add(new Geometry.Segment(new Geometry.Point(0, 4), new Geometry.Point(0, 7)));
            testSegment7.Add(new Geometry.Segment(new Geometry.Point(0, 6), new Geometry.Point(0, 8)));
            testSegment7.Add(new Geometry.Segment(new Geometry.Point(0, 5), new Geometry.Point(0, 7)));

            List<Geometry.Segment> testSegment8 = new List<Geometry.Segment>();
            testSegment8.Add(new Geometry.Segment(new Geometry.Point(0, 0), new Geometry.Point(0, 0)));
            testSegment8.Add(new Geometry.Segment(new Geometry.Point(0, 0), new Geometry.Point(0, 0)));
            testSegment8.Add(new Geometry.Segment(new Geometry.Point(0, 0), new Geometry.Point(0, 0)));
            testSegment8.Add(new Geometry.Segment(new Geometry.Point(0, 0), new Geometry.Point(0, 0)));
            testSegment8.Add(new Geometry.Segment(new Geometry.Point(0, 0), new Geometry.Point(0, 0)));

            TestCaseBase[] tc = {
                new TestCaseSegment(testSegment1,1,0.0001),
                new TestCaseSegment(testSegment2,1,0.0001),
                new TestCaseSegment(testSegment3,2,0.0001),
                new TestCaseSegment(testSegment4,3,0.0001),
                new TestCaseSegment(testSegment5,3,0.0001),
                new TestCaseSegment(testSegment6,4,0.0001),
                new TestCaseSegment(testSegment7,6,0.0001),
                new TestCaseSegment(testSegment8,0,0.0001)
            };
            
            TestResult[] etap1 = tc.Select(t => t.PerformTest(false, speedFactor, verbose, swallowExceptions)).ToArray();

            bool passedStage1 = etap1.All(r => r == TestResult.PASSED);
            Console.WriteLine("\nETAP 1 status: {0}\n", passedStage1 ? "OK" : "FAILED");
            Console.WriteLine("ETAP 2:\n");

            List<Geometry.Rectangle> testRectangle1 = new List<Geometry.Rectangle>();
            testRectangle1.Add(new Geometry.Rectangle(-1, 1, -1, 1));

            List<Geometry.Rectangle> testRectangle2 = new List<Geometry.Rectangle>();
            testRectangle2.Add(new Geometry.Rectangle(-1, 1, -1, 1));
            testRectangle2.Add(new Geometry.Rectangle(-2, 2, -2, 2));

            List<Geometry.Rectangle> testRectangle3 = new List<Geometry.Rectangle>();
            testRectangle3.Add(new Geometry.Rectangle(-1, 1, -1, 1));
            testRectangle3.Add(new Geometry.Rectangle(-2, 2, -2, 2));
            testRectangle3.Add(new Geometry.Rectangle(2, 3, -2, 2));

            List<Geometry.Rectangle> testRectangle4 = new List<Geometry.Rectangle>();
            testRectangle4.Add(new Geometry.Rectangle(-1, 1, -1, 1));
            testRectangle4.Add(new Geometry.Rectangle(-2, 2, -2, 2));
            testRectangle4.Add(new Geometry.Rectangle(2, 3, -2, 2));
            testRectangle4.Add(new Geometry.Rectangle(1.5, 2.5, -3, -1.5));

            List<Geometry.Rectangle> testRectangle5 = new List<Geometry.Rectangle>();
            testRectangle5.Add(new Geometry.Rectangle(0, 0, 0, 0));
            testRectangle5.Add(new Geometry.Rectangle(0, 0, 0, 0));
            testRectangle5.Add(new Geometry.Rectangle(0, 0, 0, 0));
            testRectangle5.Add(new Geometry.Rectangle(0,0,0,0));

            TestCaseBase[] tc2 = {
                new TestCaseRectangle(testRectangle1,4,0.05),
                new TestCaseRectangle(testRectangle2,16,0.05),
                new TestCaseRectangle(testRectangle3,20,0.001),
                new TestCaseRectangle(testRectangle4,21,0.001),
                new TestCaseRectangle(testRectangle5,0,0.001),
            };
            TestResult[] etap2 = tc2.Select(t => t.PerformTest(false, speedFactor, verbose, swallowExceptions)).ToArray();

            bool passedStage2 = etap2.All(r => r == TestResult.PASSED);
            Console.WriteLine("\nETAP 2 status: {0}\n", passedStage2 ? "OK" : "FAILED");
        }
    }
}

