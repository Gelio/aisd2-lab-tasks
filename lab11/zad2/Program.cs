using System;
using ASD.Graph;

namespace asd2_lab11
{
	abstract class Test
	{
		protected int myNum;
		protected IGraph g;
		
		public Test(int myNum, IGraph g)
		{
			this.myNum = myNum;
			this.g = g;
		}
		
		public abstract void doTheTest();
	}
	
	class MinNumberTest: Test
	{
		int expected_result;
		int expected_cost;
		public MinNumberTest(int myNum, IGraph g, int result, int cost):base(myNum,g)
		{
			expected_cost = cost;
			expected_result = result;
		}
		
		int alg_result;
		int alg_cost;
		IGraph alg_trails;
		
		public override void doTheTest()
		{
			Console.WriteLine("Wariant A, test {0}",myNum);
            IGraph gg=g.Clone();
			alg_result = g.MinimumNumberOfTrails(out alg_cost,out alg_trails);
			if(alg_result==expected_result)
				Console.WriteLine("\t Wynik ok");
			else
				Console.WriteLine("\t BLEDNY wynik (liczba szlakow). Jest {0}, powinno byc {1}",alg_result,expected_result);
			if(alg_cost==expected_cost)
				Console.WriteLine("\t Koszt ok");
			else
				Console.WriteLine("\t BLEDNY koszt. Jest {0}, powinno byc {1}",alg_cost,expected_cost);
			if ( !g.IsEqual(gg) )
				Console.WriteLine("\t BLAD - zmieniono zadany graf");
            checkTrails();
		}
		
		void checkTrails()
		{
			if(alg_trails==null)
				Console.WriteLine("\t BRAK wynikowego grafu");
			else if (g.VerticesCount != alg_trails.VerticesCount)
				Console.WriteLine("\t BLEDNY graf, ma {0} zamiast {1} wierzcholkow",alg_trails.VerticesCount,g.VerticesCount);
			else if (alg_trails.EdgesCount != g.VerticesCount - expected_result)
				Console.WriteLine("'t BLEDNY graf, ma {0} zamiast {1} krawedzi", alg_trails.EdgesCount, g.VerticesCount - expected_result);
			else
			{
				int cres = 0;
				bool[] vout = new bool[g.VerticesCount];
				bool[] vin = new bool[g.VerticesCount];
				for(int i=0;i<g.VerticesCount;i++)
					foreach(Edge e in alg_trails.OutEdges(i))
				{
					if(vout[e.From])
					{
						Console.WriteLine("\t BLEDNY graf, z wierzcholka {0} wychodzi wiecej niz jedna krawedz",e.From);
						return;
					}
					if(vin[e.To])
					{
						Console.WriteLine("\t BLEDNY graf, do wierzcholka {0} wchodzi wiecej niz jedna krawedz",e.To);
					}
					vout[e.From]=vin[e.To]=true;
					if(g.GetEdgeWeight(e.From,e.To)==null)
					{
						Console.WriteLine("\t BLEDNY graf, ma krawedz {0} nieistniejaca w grafie g (byc moze jest tych krawedzi wiecej)",e);
						return;
					}
					cres+=(int)g.GetEdgeWeight(e.From,e.To);
				}
				if(cres == expected_cost)
					Console.WriteLine("\t Graf ok");
				else 
					Console.WriteLine("\t BLEDNY graf - suma kosztow krawedzi to {0} zamiast {1}",cres,expected_cost);
			}
		}
		
	}
	
	class MinCostTest: Test
	{
		int[] costs;
		int expected_cost;
		public MinCostTest(int myNum, IGraph g, int[] costs, int cost):base(myNum, g)
		{
			expected_cost = cost;
			this.costs = costs;
		}
		
		int alg_cost;
		IGraph alg_trails;
		
		public override void doTheTest()
		{
			Console.WriteLine("Wariant B, test {0}",myNum);
            IGraph gg=g.Clone();
			alg_cost = g.MinimumCostOfTrails(costs,out alg_trails);
			if(alg_cost==expected_cost)
				Console.WriteLine("\t Koszt ok");
			else
				Console.WriteLine("\t BLEDNY koszt. Jest {0}, powinno byc {1}",alg_cost,expected_cost);
			if ( !g.IsEqual(gg) )
				Console.WriteLine("\t BLAD - zmieniono zadany graf");
			checkTrails();
		}
		
		
		void checkTrails()
		{
			if(alg_trails==null)
				Console.WriteLine("\t BRAK wynikowego grafu");
			else if (g.VerticesCount != alg_trails.VerticesCount)
				Console.WriteLine("\t BLEDNY graf, ma {0} zamiast {1} wierzcholkow",alg_trails.VerticesCount,g.VerticesCount);
			else
			{
				int cres = 0;
				bool[] vout = new bool[g.VerticesCount];
				bool[] vin = new bool[g.VerticesCount];
				for(int i=0;i<g.VerticesCount;i++)
					foreach(Edge e in alg_trails.OutEdges(i))
				{
					if(vout[e.From])
					{
						Console.WriteLine("\t BLEDNY graf, z wierzcholka {0} wychodzi wiecej niz jedna krawedz",e.From);
						return;
					}
					if(vin[e.To])
					{
						Console.WriteLine("\t BLEDNY graf, do wierzcholka {0} wchodzi wiecej niz jedna krawedz",e.To);
					}
					vout[e.From]=vin[e.To]=true;
					if(g.GetEdgeWeight(e.From,e.To)==null)
					{
						Console.WriteLine("\t BLEDNY graf, ma krawedz {0} nieistniejaca w grafie g (byc moze jest tych krawedzi wiecej)",e);
						return;
					}
					cres+=(int)g.GetEdgeWeight(e.From,e.To);
				}
				for(int i=0;i<g.VerticesCount;i++)
					if(!(vout[i]))
						cres+=costs[i];
				if(cres == expected_cost)
					Console.WriteLine("\t Graf ok");
				else 
					Console.WriteLine("\t BLEDNY graf - suma kosztow krawedzi to {0} zamiast {1}",cres,expected_cost);
			}
		}
	}
		
	class Program
	{
		public static void Main(string[] args)
		{
			IGraph g0 = new AdjacencyListsGraph<HashTableAdjacencyList>(true,9);
			g0.AddEdge(0, 1, 25);
			g0.AddEdge(1, 4, 25);
			g0.AddEdge(4, 5, 25);
			g0.AddEdge(5, 7, 10);
			g0.AddEdge(0, 3, 10);
			g0.AddEdge(1, 3, 25);
			g0.AddEdge(2, 3, 25);
			g0.AddEdge(3, 6, 10);
			g0.AddEdge(5, 6, 25);
			g0.AddEdge(1, 5, 10);
			g0.AddEdge(3, 5, 25);
			
			int[] c0 = {30, 30, 30, 30, 30, 30, 30, 30, 30};
			
			IGraph g1 = new AdjacencyListsGraph<HashTableAdjacencyList>(true,8);
			g1.AddEdge(0, 1, 5);
			g1.AddEdge(1, 2, 5);
			g1.AddEdge(1, 3, 5);
			g1.AddEdge(4, 6, 5);
			g1.AddEdge(5, 6, 5);
			g1.AddEdge(6, 7, 5);
			
			int[] c1 = {10, 20, 30, 40, 40, 30, 20, 10};
			
			IGraph g2 = new AdjacencyListsGraph<HashTableAdjacencyList>(true,4);
			g2.AddEdge(0, 1, 100);
			g2.AddEdge(2,1,10);
			g2.AddEdge(2,3,100);
			
			int[] c2 = {100, 10, 10, 100};
			
			int a=5;
			int b=12;
			IGraph g3 = new AdjacencyListsGraph<HashTableAdjacencyList>(true,a*b);
			for(int i=0;i<b-1;i++)
				for(int j=0;j<a;j++)
					for(int k=0;k<a;k++)
						g3.AddEdge(i*a+j,(i+1)*a+k,20+(j+k)%a);
			int[] c3 = new int[a*b];
			for(int i=0;i<b;i++)
				for(int j=0;j<a;j++)
					if(i==b/2)
						c3[a*i+j]=5;
					else
						c3[a*i+j]=50;
			
			Random rand = new Random(13);
			int n=60;
			IGraph g4 = new AdjacencyListsGraph<HashTableAdjacencyList>(true,n);
			for(int i=0;i<n;i++)
				for(int j=i+1;j<n;j++)
					if(rand.Next(10)==0)
						g4.AddEdge(i,j,rand.Next(20, 50));
			int[] c4 = new int[n];
			for(int i=0;i<n;i++)
				c4[i] = rand.Next(20, 50);
			
			
			Test[] testy = {
				new MinNumberTest(0,g0,3,120),
				new MinNumberTest(1,g1,4, 20),
				new MinNumberTest(2, g2, 2, 200),
				new MinNumberTest(3, g3, a, 20*(b-1)*a),
				new MinNumberTest(4, g4, 13, 1505),
				new MinCostTest(0,g0,c0,190),
				new MinCostTest(1, g1, c1, 130),
				new MinCostTest(2, g2, c2, 220),
				new MinCostTest(3, g3, c3, 20*(b-2)*a+55*a),
				new MinCostTest(4, g4, c4, 1726)
			};
			
			foreach(Test t in testy)
				t.doTheTest();
		}
	}
}