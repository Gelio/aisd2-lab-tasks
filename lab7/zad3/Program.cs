using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Knapsack_problem
{
    public partial class Program
    {
        /*
         * Problem upakowania przedmiotów (things) do plecaka o rozmiarze limit.
         * 
         * Każdy przedmiot ma swoją wagę (Weight) oraz wartość (Value).
         * Cel: spakować plecak w taki sposób, żeby miał jak największą wartość.
         * 
         * Każdego przedmiotu możemy używać nieograniczoną ilość razy!
         * 
         */
        public static int Unbounded_Knapsack_Problem(int limit, IList<Thing> things, out IList<Thing> knapsack)
        {
            knapsack = new List<Thing>();
            return 0;
        }

        /*
         * Problem upakowania przedmiotów (things) do plecaka o rozmiarze limit.
         * 
         * Każdy przedmiot ma swoją wagę (Weight) oraz wartość (Value).
         * Cel: spakować plecak w taki sposób, żeby miał jak największą wartość.
         * 
         * Każdego przedmiotu możemy użyć co najwyżej raz!
         * 
         */
        public static int Discrete_Knapsack_Problem(int limit, IList<Thing> things, out IList<Thing> knapsack)
        { 
            knapsack = new List<Thing>();
            return 0;
        }
    }
}
