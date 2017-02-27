using System;

namespace AsdLab5
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			ExchangePair[] exchangeSet1 = new ExchangePair[] { };
			ExchangePair[] exchangeSet2 = new ExchangePair[] { new ExchangePair(0, 1, 2.0) };
			ExchangePair[] exchangeSet3 = new ExchangePair[] { new ExchangePair(0, 1, 2.0), new ExchangePair(1, 0, 0.4) };
			ExchangePair[] exchangeSet4 = new ExchangePair[] { new ExchangePair(0, 1, 2.0), new ExchangePair(1, 0, 0.6) };
			ExchangePair[] exchangeSet5 = new ExchangePair[] { new ExchangePair(0, 1, 3.0), new ExchangePair(1, 0, 0.25), new ExchangePair(3, 1, 3.0),
				new ExchangePair(2, 3, 0.5), new ExchangePair(3, 0, 1.1), new ExchangePair(0, 2, 2.0), new ExchangePair(1, 4, 7.0) };
			ExchangePair[] exchangeSet6 = new ExchangePair[] { new ExchangePair(5, 4, 3.0), new ExchangePair(5, 3, 2.0), new ExchangePair(3, 0, 4.0),
				new ExchangePair(3, 1, 5.0), new ExchangePair(4, 1, 6.0), new ExchangePair(4, 2, 7.0), new ExchangePair(5, 2, 1.0) };
			ExchangePair[] exchangeSet7 = new ExchangePair[] { new ExchangePair(1, 0, 1.0), new ExchangePair(1, 2, 2.0), new ExchangePair(0, 3, 3.0), new ExchangePair(2, 3, 2.0) };

			//**************************************************
			// TEST 1
			//
			Console.WriteLine ("Testing findArbitrage...");

			int testId = 0;
			int success = 0;

			success += performTestPart1 (++testId, false, 2, 0, exchangeSet1);
			success += performTestPart1 (++testId, false, 2, 1, exchangeSet1);
			success += performTestPart1 (++testId, false, 2, 0, exchangeSet2);
			success += performTestPart1 (++testId, false, 2, 1, exchangeSet2);
			success += performTestPart1 (++testId, false, 2, 0, exchangeSet3);
			success += performTestPart1 (++testId, false, 2, 1, exchangeSet3);
			success += performTestPart1 (++testId, true, 2, 0, exchangeSet4);
			success += performTestPart1 (++testId, true, 2, 1, exchangeSet4);
			success += performTestPart1 (++testId, true, 5, 0, exchangeSet5);
			success += performTestPart1 (++testId, false, 5, 4, exchangeSet5);
			success += performTestPart1 (++testId, true, 5, 2, exchangeSet5);
			success += performTestPart1 (++testId, true, 5, 3, exchangeSet5);

			Console.WriteLine ("");

			//**************************************************
			// TEST 2
			//
			Console.WriteLine ("Testing findBestPrice...");

			success += performTestPart2 (++testId, new double[] { 1, 2 }, 2, 0, exchangeSet3);
			success += performTestPart2 (++testId, new double[] { 8, 18, 21, 2, 3, 1 }, 6, 5, exchangeSet6);
			success += performTestPart2 (++testId, new double[] { 1, 1, 2, 4 }, 4, 1, exchangeSet7);

			Console.WriteLine ("=== PART 1 SUMMARY: {0}/{1} passed", success, testId);
			Console.WriteLine ("");

			//**************************************************
			// TEST 3
			//
			Console.WriteLine ("Testing findArbitrage + NegativeCostCycle...");

			testId = 0;
			success = 0;

			success += performTestPart3 (++testId, 2, 0, exchangeSet4);
			success += performTestPart3 (++testId, 2, 1, exchangeSet4);
			success += performTestPart3 (++testId, 5, 0, exchangeSet5);
			success += performTestPart3 (++testId, 5, 2, exchangeSet5);
			success += performTestPart3 (++testId, 5, 3, exchangeSet5);

			Console.WriteLine ("=== PART 2 SUMMARY: {0}/{1} passed", success, testId);
			Console.WriteLine ("");
		}

		private static int performTestPart1(int testId, bool expectedResult, int currencyCount, int currency, ExchangePair [] exchanges)
		{
			int[] cycle;
			CurrencyGraph cg = new CurrencyGraph(currencyCount, exchanges);
			bool result = cg.findArbitrage(currency, out cycle);

			bool success = result == expectedResult;

			Console.Write ("Test #{0}: {1}", testId, success ? "SUCCESS" : "FAILURE");

			if (!success)
				Console.Write (" (details: test result: {0}; expected result: {1})", result, expectedResult);

			Console.WriteLine ("");
			return success ? 1 : 0;
		}

		private static int performTestPart2(int testId, double []expectedBestPrices, int currencyCount, int currency, ExchangePair [] exchanges)
		{
			double[] bestPrices;
			CurrencyGraph cg = new CurrencyGraph(currencyCount, exchanges);
			bool result = cg.findBestPrice(currency, out bestPrices);

			bool success = result && bestPrices!=null && bestPrices.Length == expectedBestPrices.Length;

			if (success)
			{
				for (int i = 0; i < bestPrices.Length; ++i)
					if (Math.Abs (bestPrices [i] - expectedBestPrices [i]) > 10e-8) {
						success = false;
						break;
					}
			}

			Console.Write ("Test #{0}: {1}", testId, success ? "SUCCESS" : "FAILURE");

			//if (!success)
			//	Console.Write (" (details: test result: [{0}]; expected result: [{1}])", string.Join(", ", bestPrices), string.Join(", ", expectedBestPrices));

			Console.WriteLine ("");
			return success ? 1 : 0;
		}

		private static int performTestPart3(int testId, int currencyCount, int currency, ExchangePair [] exchanges)
		{
			int[] cycle;
			CurrencyGraph cg = new CurrencyGraph(currencyCount, exchanges);
			bool result = cg.findArbitrage(currency, out cycle);
			bool success = result && cycle!=null;
			string reason = "";

			if (success) {
				if (cycle.Length == 0 || cycle [0] != cycle [cycle.Length - 1]) {
					success = false;
				} else {
					for (int i = 1; i < cycle.Length; ++i)
						if (!isValidExchange (cycle [i - 1], cycle [i], exchanges)) {
							success = false;
							reason = string.Format (" (details: invalid cycle)");
							break;
						}

					if (success) {
						double v = 1.0;

						for (int i = 1; i < cycle.Length; ++i)
							v *= findExchangePrice (cycle [i - 1], cycle [i], exchanges);

						if (v <= 1.0) {
							success = false;
							reason = string.Format (" (details: {0} <= 1.0)", v);
						}
					}
				}
			}

			Console.WriteLine ("Test #{0}: {1} {2}", testId, success ? "SUCCESS" : "FAILURE", reason);
			return success ? 1 : 0;
		}

		private static bool isValidExchange(int from, int to, ExchangePair [] exchanges)
		{
			foreach (ExchangePair e in exchanges)
				if (e.From == from && e.To == to)
					return true;
			return false;
		}

		private static double findExchangePrice(int from, int to, ExchangePair [] exchanges)
		{
			foreach (ExchangePair e in exchanges)
				if (e.From == from && e.To == to)
					return e.Price;
			return 0.0;
		}
	}
}