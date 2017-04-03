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
            CalculatePossibleSatisfaction();

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

        private int[][] possibleSatisfactionLeft;

        private void CalculatePossibleSatisfaction()
        {
            possibleSatisfactionLeft = new int[customerPreferences.Length][];
            for (int customer = 0; customer < customerPreferences.Length; customer++)
            {
                possibleSatisfactionLeft[customer] = new int[smellCount];
                possibleSatisfactionLeft[customer][smellCount - 2] = Math.Max(customerPreferences[customer][smellCount - 1], 0);
                for (int smell = smellCount - 3; smell >= 0; smell--)
                    possibleSatisfactionLeft[customer][smell] = possibleSatisfactionLeft[customer][smell + 1] + Math.Max(customerPreferences[customer][smell + 1], 0);
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
            bool isNextSmellViable = true;
            for (int i = 0; i < customerSatisfaction.Length; i++)
            {
                if (customerSatisfaction[i] + customerPreferences[i][nextSmell] + possibleSatisfactionLeft[i][nextSmell] < satisfactionLevel)
                {
                    isNextSmellViable = false;
                    break;
                }
            }


            bool assignmentSatisfactory = false;
            if (isNextSmellViable)
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
            
            // Check further without nextSmell in the set of smells used
            assignmentSatisfactory = AssignSmellsHelper(smellsUsed, customerSatisfaction, nextSmell + 1);
            if (assignmentSatisfactory)
                return true;

            return false;
        }

        /// <summary>
        /// Implementacja etapu 2
        /// </summary>
        /// <returns>Maksymalna liczba klientów, których można usatysfakcjonować</returns>
        /// <param name="smells">Wyjściowa tablica rozpylonych zapachów, realizująca ten poziom satysfakcji</param>
        public int AssignSmellsMaximizeHappyCustomers(out bool[] smells)
        {
            bestSmellsUsed = new bool[smellCount];
            if (satisfactionLevel <= 0)
            {
                smells = bestSmellsUsed;
                return customerPreferences.Length;
            }

            CalculatePossibleSatisfaction();


            bestHappyCustomers = 0;

            bool[] smellsUsed = new bool[smellCount];
            int[] customerSatisfaction = new int[customerPreferences.Length];
            AssignSmellsMaximizeHappyCustomersHelper(smellsUsed, customerSatisfaction);

            smells = bestSmellsUsed;
            return bestHappyCustomers;
        }

        private bool[] bestSmellsUsed;
        private int bestHappyCustomers;

        public void AssignSmellsMaximizeHappyCustomersHelper(bool[] smellsUsed, int[] customerSatisfaction, int nextSmell = 0)
        {
            int happyCustomers = 0;
            for (int i = 0; i < customerSatisfaction.Length; i++)
            {
                if (customerSatisfaction[i] >= satisfactionLevel)
                    happyCustomers++;
            }

            if (happyCustomers > bestHappyCustomers)
            {
                smellsUsed.CopyTo(bestSmellsUsed, 0);
                bestHappyCustomers = happyCustomers;
            }

            // All customers happy, cannot satisfy more
            if (happyCustomers == customerSatisfaction.Length)
                return;

            // No more smells available
            if (nextSmell >= smellCount)
                return;


            int possiblyHappyCustomers = 0;
            for (int i = 0; i < customerSatisfaction.Length; i++)
            {
                if (customerSatisfaction[i] + customerPreferences[i][nextSmell] + possibleSatisfactionLeft[i][nextSmell] >= satisfactionLevel)
                    possiblyHappyCustomers++;
            }

            if (possiblyHappyCustomers > bestHappyCustomers)
            {
                // Add the smell and check further with nextSmell selected
                smellsUsed[nextSmell] = true;
                for (int i = 0; i < customerSatisfaction.Length; i++)
                    customerSatisfaction[i] += customerPreferences[i][nextSmell];

                AssignSmellsMaximizeHappyCustomersHelper(smellsUsed, customerSatisfaction, nextSmell + 1);

                // Revert the smell
                smellsUsed[nextSmell] = false;
                for (int i = 0; i < customerSatisfaction.Length; i++)
                    customerSatisfaction[i] -= customerPreferences[i][nextSmell];
            }
            
            // Check further without nextSmell in the set of smells used
            AssignSmellsMaximizeHappyCustomersHelper(smellsUsed, customerSatisfaction, nextSmell + 1);
        }

    }

}

