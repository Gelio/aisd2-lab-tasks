using System;

namespace lab4a
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			int[][] test0= new int[][] {new int[]{1,2,3,4,5},new int[]{6,7,8,12,14,15},new int[]{9,10,11,13}};
			int[][] test1= new int[][] {new int[]{1,2,3,4},new int[]{4,5,6,7,8},new int[]{8,9,10,11}};
			int[][] test2= new int[][] {new int[]{1,2,2,4},new int[]{1,2,3,4},new int[]{1,2,3,4},new int[]{1,2,3,4}};
			int[][] test3= new int[][] {new int[]{1,3,3,4},new int[]{1,2,3,4},new int[]{1,2,3,4},new int[]{1,2,3,4}};

			//Testy łatwe
			Test (0, test0, 6, new int[]{7});
			Test (1, test1, 7, new int[]{7});
			Test (2, test1, 8, new int[]{8});
			//Testy trudniejsze
			Test (3, test2, 8, new int[]{2});
			Test (4, test3, 8, new int[]{3});
			Test (5, test3, 0, new int[]{1});
		}

		public static void Test(int testnum, int[][] input, int k, int[] desiredHeights) 
		{
			int winnerArray;
			int winnerArrayPos;
			int actualHeight = Competition.FindWinner(input, k, out winnerArray, out winnerArrayPos);

			foreach(int height in desiredHeights) 
			{
				if(height == actualHeight)
				{
					Console.Out.WriteLine("Test {0}: otrzymano prawidłową wysokość {1}", testnum, height);
					if(input[winnerArray][winnerArrayPos] == height)
						Console.Out.WriteLine("Wskaźniki na zwycięzcę -- OK input[{0}][{1}]=={2}", winnerArray, winnerArrayPos, height);
					else
						Console.Out.WriteLine("Wskaźniki na zwycięzcę -- Błąd! input[{0}][{1}]=={2}, powinno być {3}", winnerArray,
						                      winnerArrayPos, input[winnerArray][winnerArrayPos],height);
					return;
				}
			}

			Console.Out.WriteLine("Test {0}: BŁĄD, otrzymana wysokość: {1}. Pozycja input[{2}][{3}]",
			                      testnum, actualHeight, winnerArray, winnerArrayPos);
		}
	}
}
