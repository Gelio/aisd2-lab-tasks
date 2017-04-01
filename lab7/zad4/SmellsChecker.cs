using System;

namespace Lab07
{

    public class SmellsChecker
    {

        private readonly int smellCount;
        private readonly int[][] customerPreferences;
        private readonly int satisfactionLevel;

        /// <summary>
        ///   
        /// </summary>
        /// <param name="smellCount">Liczba zapachów, którymi dysponuje sklep</param>
        /// <param name="customerPreferences">Preferencje klientów
        /// Każda tablica -- element tablicy tablic -- to preferencje jednego klienta.
        /// Preferencje klienta mają długość smellCount, na i-tej pozycji jest
        ///  1 -- klient preferuje zapach
        ///  0 -- zapach neutralny
        /// -1 -- klient nie lubi zapachu
        /// 
        /// Zapachy numerujemy od 0
        /// </param>
        /// <param name="satisfactionLevel">Oczekiwany poziom satysfakcji</param>
        public SmellsChecker(int smellCount, int[][] customerPreferences, int satisfactionLevel)
        {
            this.smellCount = smellCount;
            this.customerPreferences = customerPreferences;
            this.satisfactionLevel = satisfactionLevel;
        }

        /// <summary>
        /// Implementacja etapu 1
        /// </summary>
        /// <returns><c>true</c>, jeśli przypisanie jest możliwe <c>false</c> w p.p.</returns>
        /// <param name="smells">Wyjściowa tablica rozpylonych zapachów realizująca rozwiązanie, jeśli się da. null w p.p. </param>
        public Boolean AssignSmells(out bool[] smells)
        {
            bool[] smellsUsed = new bool[smellCount];
            int[] customerSatisfaction = new int[customerPreferences.Length];

            bool assignmentFound = AssignSmellsHelper(smellsUsed, customerSatisfaction);
            if (!assignmentFound)
            {
                smells = null;
                return false;
            }
            else
            {
                smells = smellsUsed;
                return true;
            }
        }

        public bool AssignSmellsHelper(bool[] smellsUsed, int[] customerSatisfaction, int nextSmell = 0)
        {
            bool allCustomersSatisfied = true;
            for (int i=0; i < customerSatisfaction.Length; i++)
            {
                if (customerSatisfaction[i] < satisfactionLevel)
                {
                    allCustomersSatisfied = false;
                    break;
                }
            }

            if (allCustomersSatisfied)
                return true;

            if (nextSmell >= smellCount)
                return false;

            // Check if adding nextSmell to the list of smells will improve overall
            // satisfaction levels
            int nextSmellSatisfactionGain = 0;
            for (int i = 0; i < customerSatisfaction.Length; i++)
                nextSmellSatisfactionGain += customerPreferences[i][nextSmell];


            bool assignmentSatisfactory = false;
            if (nextSmellSatisfactionGain > 0)
            {
                // Add the smell and check further with nextSmell selected
                smellsUsed[nextSmell] = true;
                for (int i = 0; i < customerSatisfaction.Length; i++)
                    customerSatisfaction[i] += customerPreferences[i][nextSmell];

                assignmentSatisfactory = AssignSmellsHelper(smellsUsed, customerSatisfaction, nextSmell + 1);
                if (assignmentSatisfactory)
                    return true;

                // Remove the smell
                smellsUsed[nextSmell] = false;
                for (int i = 0; i < customerSatisfaction.Length; i++)
                    customerSatisfaction[i] -= customerPreferences[i][nextSmell];
            }
            else
            {
                // Check further without nextSmell in the set of smells used
                assignmentSatisfactory = AssignSmellsHelper(smellsUsed, customerSatisfaction, nextSmell + 1);
                if (assignmentSatisfactory)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Implementacja etapu 2
        /// </summary>
        /// <returns>Maksymalna liczba klientów, których można usatysfakcjonować</returns>
        /// <param name="smells">Wyjściowa tablica rozpylonych zapachów, realizująca ten poziom satysfakcji</param>
        public int AssignSmellsMaximizeHappyCustomers(out bool[] smells)
        {
            bool[] smellsUsed = new bool[smellCount];
            int[] customerSatisfaction = new int[customerPreferences.Length];

            int maxHappyCustomers = AssignSmellsMaximizeHappyCustomersHelper(ref smellsUsed, ref customerSatisfaction);
            smells = smellsUsed;
            return maxHappyCustomers;
        }

        public int AssignSmellsMaximizeHappyCustomersHelper(ref bool[] smellsUsed, ref int[] customerSatisfaction, int nextSmell = 0)
        {
            int happyCustomers = 0;
            for (int i = 0; i < customerSatisfaction.Length; i++)
            {
                if (customerSatisfaction[i] >= satisfactionLevel)
                    happyCustomers++;
            }

            // All customers happy, cannot satisfy more
            if (happyCustomers == customerSatisfaction.Length)
                return happyCustomers;

            // No more smells available
            if (nextSmell >= smellCount)
                return happyCustomers;

            // Current set of smells is the best so far
            bool[] bestSmellsUsed = smellsUsed;
            int[] bestCustomerSatisfaction = customerSatisfaction;
            int bestHappyCustomers = happyCustomers;

            // Prepare variables for further checking
            int nextHappyCustomers = happyCustomers;
            bool[] nextSmellsUsed = smellsUsed.Clone() as bool[];
            int[] nextCustomerSatisfaction = customerSatisfaction.Clone() as int[];


            bool isNextSmellPositiveForAnyone = false;
            for (int i = 0; i < customerSatisfaction.Length; i++)
            {
                if (customerPreferences[i][nextSmell] > 0)
                {
                    isNextSmellPositiveForAnyone = true;
                    break;
                }
            }

            if (isNextSmellPositiveForAnyone)
            {
                // Add the smell and check further with nextSmell selected
                nextSmellsUsed[nextSmell] = true;
                for (int i = 0; i < customerSatisfaction.Length; i++)
                    nextCustomerSatisfaction[i] += customerPreferences[i][nextSmell];

                nextHappyCustomers = AssignSmellsMaximizeHappyCustomersHelper(ref nextSmellsUsed, ref nextCustomerSatisfaction, nextSmell + 1);
                if (nextHappyCustomers > bestHappyCustomers)
                {
                    bestSmellsUsed = nextSmellsUsed;
                    bestCustomerSatisfaction = nextCustomerSatisfaction;
                    bestHappyCustomers = nextHappyCustomers;
                }

                nextSmellsUsed = smellsUsed.Clone() as bool[];
                nextCustomerSatisfaction = customerSatisfaction.Clone() as int[];
            }
            

            // Check further without nextSmell in the set of smells used
            nextHappyCustomers = AssignSmellsMaximizeHappyCustomersHelper(ref nextSmellsUsed, ref nextCustomerSatisfaction, nextSmell + 1);
            if (nextHappyCustomers > bestHappyCustomers)
            {
                bestSmellsUsed = nextSmellsUsed;
                bestCustomerSatisfaction = nextCustomerSatisfaction;
                bestHappyCustomers = nextHappyCustomers;
            }

            // Return best results
            smellsUsed = bestSmellsUsed;
            customerSatisfaction = bestCustomerSatisfaction;
            return bestHappyCustomers;
        }

    }

}

