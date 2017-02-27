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
            smells = null;
            return false;
        }

        /// <summary>
        /// Implementacja etapu 2
        /// </summary>
        /// <returns>Maksymalna liczba klientów, których można usatysfakcjonować</returns>
        /// <param name="smells">Wyjściowa tablica rozpylonych zapachów, realizująca ten poziom satysfakcji</param>
        public int AssignSmellsMaximizeHappyCustomers(out bool[] smells)
        {
            smells = new bool[smellCount];
            return -1;
        }

    }

}

