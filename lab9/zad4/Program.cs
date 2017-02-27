using System;
using System.Collections.Generic;

using ASD.Graphs;

namespace Lab09
{
    class Program
    {
        class TestEntry
        {
            public int childrenCount;
            public int sweetsCount;
            public int[][] childrenLikes;
            public int[] childrenLimits;
            public int[] sweetsLimits;
            public int flow1;
            public int flow2;
        }

        static private List<TestEntry> tests;

        static void Main(string[] args)
        {
            tests = new List<TestEntry>();

            // Test 1
            makeTest(
                3, 5,
                0, -1,
                1, -1,
                2, 3, 4, -1,
                3, 2, 1, -1,
                1, 2, 3, 4, 5, -1,
                3, 4, -2);

            // Test 2
            makeTest(
                2, 2,
                0, 1, -1,
                0, 1, -1,
                5, 1, -1,
                1, 5, -1,
                2, 6, -2);

            // Test 3
            makeTest(
                3, 3,
                1, -1,
                0, 2, -1,
                1, -1,
                1, 3, 5, -1,
                7, 5, 3, -1,
                2, 8, -2);

            // Test 4
            makeTest(
                3, 3,
                0, -1,
                0, -1,
                0, -1,
                1, 1, 1, -1,
                3, 3, 3, -1,
                1, 3, -2);

            // Test 5
            makeTest(
                5, 5,
                1, -1,
                2, -1,
                3, -1,
                4, -1,
                0, -1,
                1, 2, 3, 4, 5, -1,
                1, 2, 3, 4, 5, -1,
                5, 11, -2);

            // Test 6
            makeTest(
                5, 3,
                0, 1, 2, -1,
                0, 1, 2, -1,
                0, 1, 2, -1,
                0, 1, 2, -1,
                0, 1, 2, -1,
                1, 2, 3, 4, 5, -1,
                5, 4, 3, -1,
                3, 12, -2);

            // Test 7
            makeTest(
                3, 5,
                0, 1, 2, 3, 4, -1,
                0, 1, 2, 3, 4, -1,
                0, 1, 2, 3, 4, -1,
                5, 4, 3, -1,
                1, 2, 3, 4, 5, -1,
                3, 12, -2);

            // Test 8
            makeTest(
                6, 6,
                5, 0, -1,
                0, 1, -1,
                1, 2, -1,
                2, 3, -1,
                3, 4, -1,
                4, 5, -1,
                2, 2, 2, 2, 2, 2, -1,
                3, 1, 3, 1, 3, 1, -1,
                6, 12, -2);

            // Test 9
            makeTest(
                6, 6,
                5, 0, -1,
                0, 1, -1,
                1, 2, -1,
                2, 3, -1,
                3, 4, -1,
                4, 5, -1,
                2, 2, 2, 2, 2, 2, -1,
                1, 1, 1, 1, 1, 1, -1,
                6, 6, -2);

            // Test 10
            makeTest(
                8, 6,
                0, 2, 4, -1,
                1, 3, 5, -1,
                0, 2, 4, -1,
                1, 3, 5, -1,
                0, 2, 4, -1,
                1, 3, 5, -1,
                0, 2, 4, -1,
                1, 3, 5, -1,
                3, 3, 3, 3, 3, 3, 3, 3, -1,
                9, 1, 9, 1, 9, 1, -1,
                6, 15, -2);

            // Test 11
            makeTest(
                1, 20,
                13, -1,
                10000, -1,
                50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 13, 50, 50, 50, 50, 50, 50, -1,
                1, 13, -2);

            // Test 12
            makeTest(
                1, 20,
                0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, -1,
                10000, -1,
                50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, -1,
                1, 1000, -2);

            testTask1();
            testTask2();
            System.Console.WriteLine();
        }
        
        static void makeTest(
            int childrenCount,
            int sweetsCount,
            params int[] funcParams )
        {
            TestEntry e = new TestEntry();

            e.childrenCount = childrenCount;
            e.sweetsCount = sweetsCount;

            e.childrenLikes = new int[childrenCount][];
            int beg = 0;
            int end = 0;
            for (int i = 0; i < childrenCount; ++i)
            {
                end = System.Array.IndexOf(funcParams, -1, beg);
                e.childrenLikes[i] = new int[end - beg];
                for (int j = beg; j < end; ++j)
                    e.childrenLikes[i][j - beg] = funcParams[j];
                beg = end + 1;
            }

            end = System.Array.IndexOf(funcParams, -1, beg);
            e.childrenLimits = new int[end-beg];
            System.Array.Copy( funcParams, beg, e.childrenLimits, 0, end-beg );
            beg = end + 1;

            end = System.Array.IndexOf(funcParams, -1, beg);
            e.sweetsLimits = new int[end - beg];
            System.Array.Copy( funcParams, beg, e.sweetsLimits, 0, end-beg );
            beg = end + 1;

            e.flow1 = funcParams[beg++];
            e.flow2 = funcParams[beg++];

            System.Diagnostics.Debug.Assert(funcParams[beg] == -2, "Invalid params");

            tests.Add(e);
        }

        static private bool testTask1()
        {
            System.Console.WriteLine("\nTask 1");
            System.Console.WriteLine();
            int i = 0;
            bool ok = true;
            foreach (var e in tests)
                ok = testTask1(++i, e) && ok;

            System.Console.WriteLine();
            if (ok)
                System.Console.WriteLine("Everything ok, test PASSED");
            else
                System.Console.WriteLine("Something is wrong, test FAILED!");
            System.Console.WriteLine();

            return ok;
        }

        static private bool testTask1( int num, TestEntry e )
        {
            int[] sweets = null;

            var resultFlow1 =  Sweets.Task1(
                                e.childrenCount,
                                e.sweetsCount,
                                e.childrenLikes,
                                out sweets);

            bool test1Result = true;

            // Check if result is as expected
            test1Result = test1Result && resultFlow1 == e.flow1;

            // Check if sweets exists
            test1Result = test1Result && sweets != null;

            // sweets table should have proper size
            test1Result = test1Result && sweets.Length == e.childrenCount;

            // Check if child really likes sweet it gets
            for (int i = 0; test1Result && i < e.childrenCount; ++i )
                test1Result = sweets[i] == -1 || System.Array.IndexOf(e.childrenLikes[i], sweets[i]) != -1;

            // Check if every sweet is given only once
            if ( test1Result )
            {
            System.Array.Sort(sweets);
            for (int i = 1; test1Result && i < e.childrenCount; ++i)
                test1Result = sweets[i] == -1 || sweets[i - 1] != sweets[i];
            }

            System.Console.WriteLine("Test {0}: \t{1}", num, test1Result ? "OK": "FAIL");

            return test1Result;
        }

        static private bool testTask2()
        {
            string[] msg = {"Both 2a and 2b FAILED",
                            "Task 2a is PASSED but 2b FAILED",
                            "Task 2a is FAILED but 2b PASSED",
                            "Everything ok, test PASSED" };

            System.Console.WriteLine("\nTask 2");
            System.Console.WriteLine();
            int i = 0;
            int ok = 3;
            foreach (var e in tests)
                ok = testTask2(++i, e) & ok;

            System.Console.WriteLine();
            System.Console.WriteLine(msg[ok]);
            System.Console.WriteLine();

            return ok > 0;
        }

        static private int testTask2( int num, TestEntry e )
        {
            bool[] happyChildren;
            int[] shoppingList;

            var resultFlow2 =  Sweets.Task2(
                                e.childrenCount,
                                e.sweetsCount,
                                e.childrenLikes,
                                e.childrenLimits,
                                e.sweetsLimits,
                                out happyChildren,
                                out shoppingList);

            bool test2ResultA = true;
            bool test2ResultB = true;

            // Check if result is as expected
            test2ResultA = test2ResultA && (resultFlow2 == e.flow2) && happyChildren!=null && happyChildren.Length==e.childrenCount;

            int childrenSum = 0;
            int happyChildrenSum = 0;
            for (int i = 0; i < e.childrenCount; ++i )
                {
                childrenSum += e.childrenLimits[i];
                if ( test2ResultA && happyChildren[i])
                    happyChildrenSum += e.childrenLimits[i];
                }
            test2ResultA = test2ResultA && (resultFlow2 >=happyChildrenSum);

            int sweetsSum = 0;
            test2ResultB = test2ResultB && shoppingList!=null && shoppingList.Length==e.sweetsCount;
            if ( test2ResultB )
            foreach (int i in shoppingList)
                {
                sweetsSum += i;
                if (i<0) test2ResultB=false;
                }

            test2ResultB = test2ResultB && (e.flow2 + sweetsSum == childrenSum);

            System.Console.WriteLine("Test {0}: \t{1}\t{2}",
                                            num,
                                            test2ResultA ? "PROBABLY OK": "FAIL",
                                            test2ResultB ? "PROBABLY OK": "FAIL");

            return (test2ResultA?1:0) + (test2ResultB?2:0);
        }

    }
}
