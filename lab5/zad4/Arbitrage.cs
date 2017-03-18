using System;
using System.Collections.Generic;

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
            //
            // uzupelnic
            //
        }

        // wynik: true jesli nie na cyklu ujemnego
        // currency: waluta "startowa"
        // bestPrices: najlepszy (najwyzszy) kurs wszystkich walut w stosunku do currency (byc mo¿e osiagalny za pomoca wielu wymian)
        //   jesli wynik == false to bestPrices = null
        public bool findBestPrice(int currency, out double[] bestPrices)
        {
            //
            // wywolac odpowiednio FordBellmanShortestPaths
            // i na tej podstawie obliczyc bestPrices
            //
            bestPrices = null;
            return true;
        }

        // wynik: true jesli jest mozliwosc arbitrazu, false jesli nie ma (nie rzucamy wyjatkow!)
        // currency: waluta "startowa"
        // exchangeCycle: a cycle of currencies starting from 'currency' and ending with 'currency'
        //  jesli wynik == false to exchangeCycle = null
        public bool findArbitrage(int currency, out int[] exchangeCycle)
        {
            //
            // Czêœæ 1: wywolac odpowiednio FordBellmanShortestPaths
            // Czêœæ 2: dodatkowo wywolac odpowiednio FindNegativeCostCycle
            //
            exchangeCycle = null;
            return true;
        }

        // wynik: true jesli nie na cyklu ujemnego
        // s: wierzcho³ek startowy
        // dist: obliczone odleglosci
        // prev: tablica "poprzednich"
        private bool FordBellmanShortestPaths(int s, out double[] dist, out int[] prev)
        {
            dist = null;
            prev = null;
            //
            // implementacja algorytmu Forda-Bellmana
            //
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