using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Knapsack_problem
{
    public partial class Program
    {
        public static void Main(string[] args)
        {
            List<TestSet> unbouded_tests = new List<TestSet>();
            List<TestSet> discrete_tests = new List<TestSet>();
            TestSet unbounded_test1 = new TestSet();
            unbounded_test1.Name = "u1";
            unbounded_test1.Things =
                new List<Thing>()
                {
                    new Thing(6, 6),
                    new Thing(2, 4), 
                    new Thing(3, 5), 
                    new Thing(2, 7), 
                    new Thing(3, 10), 
                    new Thing(1, 2), 
                };
            unbounded_test1.Limit = 23;
            unbounded_test1.Value = 80;
            unbounded_test1.Efficiency = 300;
            unbouded_tests.Add(unbounded_test1);

            TestSet unbounded_test2 = new TestSet();
            unbounded_test2.Name = "u2";
            unbounded_test2.Things =
                new List<Thing>()
                {
                    new Thing(20, 5),
                };
            unbounded_test2.Limit = 23;
            unbounded_test2.Value = 5;
            unbounded_test2.Efficiency = 100;
            unbouded_tests.Add(unbounded_test2);

            TestSet unbounded_test3 = new TestSet();
            unbounded_test3.Name = "u3";
            unbounded_test3.Things =
                new List<Thing>()
                {
                    new Thing(1, 4),
                    new Thing(2, 7),
                    new Thing(3, 5),
                    new Thing(4, 10),
                    new Thing(5, 5),
                    new Thing(6, 25),
                    new Thing(7, 26),
                    new Thing(8, 35),
                };
            unbounded_test3.Limit = 23;
            unbounded_test3.Value = 99;
            unbounded_test3.Efficiency = 400;
            unbouded_tests.Add(unbounded_test3);

            TestSet discrete_test1 = new TestSet();
            discrete_test1.Name = "d1";
            discrete_test1.Things =
                new List<Thing>()
                {
                    new Thing(6, 6),
                    new Thing(2, 4), 
                    new Thing(3, 5), 
                    new Thing(2, 7), 
                    new Thing(3, 10), 
                    new Thing(1, 2), 
                };
            discrete_test1.Limit = 10;
            discrete_test1.Value = 26;
            discrete_test1.Efficiency = 200;
            discrete_tests.Add(discrete_test1);

            TestSet discrete_test2 = new TestSet();
            discrete_test2.Name = "d2";
            discrete_test2.Things =
                new List<Thing>()
                {
                    new Thing(20, 5),
                    new Thing(20, 7),
                };
            discrete_test2.Limit = 23;
            discrete_test2.Value = 7;
            discrete_test2.Efficiency = 100;
            discrete_tests.Add(discrete_test2);

            TestSet discrete_test3 = new TestSet();
            discrete_test3.Name = "d3";
            discrete_test3.Things =
                new List<Thing>()
                {
                    new Thing(1, 4),
                    new Thing(2, 7),
                    new Thing(3, 5),
                    new Thing(4, 10),
                    new Thing(5, 5),
                    new Thing(6, 30),
                    new Thing(7, 30),
                    new Thing(8, 33),
                };
            discrete_test3.Limit = 23;
            discrete_test3.Value = 100;
            discrete_test3.Efficiency = 400;
            discrete_tests.Add(discrete_test3);

            IList<Thing> knapsack;
            int efficiency;
            int value;
            Console.WriteLine("Unbounded Knapsack Problem:");
            foreach (var v in unbouded_tests)
            {
                efficiency = Thing.ReadCount;
                value = Unbounded_Knapsack_Problem(v.Limit, v.Things, out knapsack);
                efficiency = Thing.ReadCount - efficiency;

                Console.WriteLine("Test: {0}", v.Name);
                Console.WriteLine("Limit: {0}", v.Limit);
                Console.WriteLine("Value: {0}", value);
                Console.WriteLine("Efficiency: {0}, ok: {1}", efficiency, v.Efficiency);
                Console.WriteLine("Knapsack:");
                foreach (var thing in knapsack)
                {
                    Console.WriteLine("Weight: {0}, Value: {1}", thing.Weight, thing.Value);
                }
                if (value == v.Value)
                    Console.WriteLine("TEST PASSED");
                else
                    Console.WriteLine("TEST FAILED");
                Console.WriteLine();
            }

            Console.WriteLine("Discreet Knapsack Problem:");
            foreach (var v in discrete_tests)
            {
                efficiency = Thing.ReadCount;
                value = Discrete_Knapsack_Problem(v.Limit, v.Things, out knapsack);
                efficiency = Thing.ReadCount - efficiency;

                Console.WriteLine("Test: {0}", v.Name);
                Console.WriteLine("Limit: {0}", v.Limit);
                Console.WriteLine("Value: {0}", value);
                Console.WriteLine("Efficiency: {0}, ok: {1}", efficiency, v.Efficiency);
                Console.WriteLine("Knapsack:");
                foreach (var thing in knapsack)
                {
                    Console.WriteLine("Weight: {0}, Value: {1}", thing.Weight, thing.Value);
                }
                if (value == v.Value)
                    Console.WriteLine("TEST PASSED");
                else
                    Console.WriteLine("TEST FAILED");
                Console.WriteLine();
            }
        }
    }
    public class Thing
    {
        private int weight;
        private int value;

        public Thing(int w, int v)
        {
            this.Weight = w;
            this.Value = v;
        }

        public static int ReadCount { get; private set; }

        public int Weight
        {
            get
            {
                ReadCount++;
                return this.weight;
            }

            set
            {
                this.weight = value;
            }
        }


        public int Value
        {
            get
            {
                ReadCount++;
                return this.value;
            }

            set
            {
                this.value = value;
            }
        }
    }

    public struct TestSet
    {
        public string Name { get; set; }
        public int Efficiency { get; set; }
        public IList<Thing> Things { get; set; }
        public int Value { get; set; }
        public int Limit { get; set; }
    }
}
