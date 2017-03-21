using System;
using System.Collections.Generic;
using System.Linq;

namespace AsdLab5
{
    public class InvalidExchangeException : Exception
    {
        public InvalidExchangeException()
        {
        }

        public InvalidExchangeException(string msg) : base(msg)
        {
        }

        public InvalidExchangeException(string msg, Exception ex) : base(msg, ex)
        {
        }
    }

    public struct ExchangePair
    {
        public readonly int From;
        public readonly int To;
        public readonly double Price;

        public ExchangePair(int from, int to, double price)
        {
            if (to < 0 || from < 0 || price <= 0.0)
                throw new InvalidExchangeException();
            From = from;
            To = to;
            Price = price;
        }
    }

    public class CurrencyGraph
    {
        private static double priceToWeight(double price)
        {
            return -Math.Log(price);
        }

        private static double weightToPrice(double weight)
        {
            return Math.Exp(-weight);
        }

        private double[,] weights;

        public CurrencyGraph(int n, ExchangePair[] exchanges)
        {
            weights = new double[n, n];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    weights[i, j] = double.NaN;
            foreach (ExchangePair exchange in exchanges)
            {
                weights[exchange.From, exchange.To] = priceToWeight(exchange.Price);
            }
        }

        // wynik: true jesli nie na cyklu ujemnego
        // currency: waluta "startowa"
        // bestPrices: najlepszy (najwyzszy) kurs wszystkich walut w stosunku do currency (byc mo¿e osiagalny za pomoca wielu wymian)
        //   jesli wynik == false to bestPrices = null
        public bool findBestPrice(int currency, out double[] bestPrices)
        {
            bool hasOnlyPositiveCycles = FordBellmanShortestPaths(currency, out double[] dist, out int[] prev);
            if (!hasOnlyPositiveCycles)
            {
                bestPrices = null;
                return false;
            }


            bestPrices = new List<double>(dist).Select(distance => weightToPrice(distance)).ToArray();
            return true;
        }

        // wynik: true jesli jest mozliwosc arbitrazu, false jesli nie ma (nie rzucamy wyjatkow!)
        // currency: waluta "startowa"
        // exchangeCycle: a cycle of currencies starting from 'currency' and ending with 'currency'
        //  jesli wynik == false to exchangeCycle = null
        public bool findArbitrage(int currency, out int[] exchangeCycle)
        {
            bool hasOnlyPositiveCycles = FordBellmanShortestPaths(currency, out double[] dist, out int[] prev);
            if (hasOnlyPositiveCycles)
            {
                exchangeCycle = null;
                return false;
            }

            int n = dist.Length;

            Stack<int> cycle = new Stack<int>();
            cycle.Push(currency);

            int lastCurrency = prev[currency];
            while (lastCurrency != currency && lastCurrency != -1)
            {
                lastCurrency = prev[lastCurrency];
                cycle.Push(lastCurrency);
            }

            exchangeCycle = cycle.ToArray();
            return true;
        }

        // wynik: true jesli nie na cyklu ujemnego
        // s: wierzcho³ek startowy
        // dist: obliczone odleglosci
        // prev: tablica "poprzednich"
        private bool FordBellmanShortestPaths(int s, out double[] dist, out int[] prev)
        {
            int n = weights.GetLength(0);
            dist = new double[n];
            prev = new int[n];
            for (int i = 0; i < n; i++)
            {
                dist[i] = double.PositiveInfinity;
                prev[i] = -1;
            }

            bool[] toBeChecked = new bool[n];
            toBeChecked[s] = true;
            int toBeCheckedCount = 1;
            dist[s] = 0;
            prev[s] = -1;
            int iterations = 0;

            while (toBeCheckedCount > 0 && iterations <= n)
            {
                iterations++;
                for (int fromCurrency = 0; fromCurrency < n; fromCurrency++)
                {
                    if (!toBeChecked[fromCurrency])
                        continue;

                    toBeChecked[fromCurrency] = false;
                    toBeCheckedCount--;
                    for (int toCurrency = 0; toCurrency < n; toCurrency++)
                    {
                        if (double.IsNaN(weights[fromCurrency, toCurrency]))
                            continue;

                        // istnieje konwersja walut
                        double newPossibleWeight = dist[fromCurrency] + weights[fromCurrency, toCurrency];
                        if (dist[toCurrency] > newPossibleWeight)
                        {
                            dist[toCurrency] = newPossibleWeight;
                            prev[toCurrency] = fromCurrency;
                            if (!toBeChecked[toCurrency])
                            {
                                toBeChecked[toCurrency] = true;
                                toBeCheckedCount++;
                            }
                        }
                    }
                }
            }

            if (iterations > n)
            {
                // istnieje cykl o ujemnej d³ugoœci
                return false;
            }

            return true;
        }

        // wynik: true jesli JEST cykl ujemny
        // dist: tablica odleglosci
        // prev: tablica "poprzednich"
        // cycle: wyznaczony cykl (kolejne elementy to kolejne wierzcholki w cyklu, pierwszy i ostatni element musza byc takie same - zamkniêcie cyklu)
        private bool FindNegativeCostCycle(double[] dist, int[] prev, out int[] cycle)
        {
            cycle = null;
            //
            // wyznaczanie cyklu ujemnego
            // przykladowy pomysl na algorytm
            // 1) znajdowanie wierzcholka, którego odleglosc zostalaby poprawiona w kolejnej iteracji algorytmu Forda-Bellmana
            // 2) cofanie sie po lancuchu poprzednich (prev) - gdy zaczna sie powtarzac to znaleŸlismy wierzcholek nale¿acy do cyklu o ujemnej dlugosci
            // 3) konstruowanie odpowiedzi zgodnie z wymogami zadania
            //
            return true;
        }
    }
}