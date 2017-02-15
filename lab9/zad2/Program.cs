using System;
using ASD.Graph;
using System.Linq;

namespace lab9
{
    class Test
    {
        public IGraph graph;
        public int[] ins;
        public int[] outs;
        public Edge[] solutions;
        public int expectedTime;
        public int acceptableTime;
        public int flowValue;

        public Test(IGraph graph, int[] ins, int[] outs, Edge[] solutions, int expectedTime, int acceptableTime, int flowValue)
        {
            this.graph = graph;
            this.ins = ins;
            this.outs = outs;
            this.solutions = solutions;
            this.expectedTime = expectedTime;
            this.acceptableTime = acceptableTime;
            this.flowValue = flowValue;
        }
    }

	public class MainClass
	{
		public static void Main (string[] args)
		{
			IGraph graph1 = new AdjacencyListsGraph<SimplyAdjacencyList> (true, 9);
			int[] in1 = {0,1,2};
			int[] out1 = {6,7,8};
			for(int i=0;i<3;i++){
				graph1.AddEdge(i,i+3,1);
				graph1.AddEdge(i+3,i+6,1);
			}

			IGraph graph2 = new AdjacencyListsGraph<SimplyAdjacencyList> (true, 12);
			int[] in2 = {0,1,2};
			int[] out2 = {9,10,11};
			for(int i=0;i<3;i++){
				graph2.AddEdge(i,i+3,2);
				graph2.AddEdge(i+3,i+6,1);
				graph2.AddEdge(i+6,i+9,1);
			}

            IGraph graph3 = new AdjacencyListsGraph<SimplyAdjacencyList>(true, 12);
            int[] in3 = { 0, 1, 2 };
            int[] out3 = { 9, 10, 11 };
            for (int i = 0; i < 3; i++)
            {
                graph3.AddEdge(i, i + 3, 1);
                graph3.AddEdge(i + 3, i + 6, 2);
                graph3.AddEdge(i + 6, i + 9, 1);
            }

            IGraph graph4 = new AdjacencyMatrixGraph(true, 12);
            int[] in4 = { 0, 1 };
            int[] out4 = { 11 };
            for (int i = 2; i < 11; i++)
            {
                for (int j = i+1; j < 11; j++)
                {
                    graph4.AddEdge(i, j, 10);
                }
            }
            graph4.AddEdge(10, 11, 2);
            graph4.AddEdge(0, 2, 1);
            graph4.AddEdge(1, 2, 1);

            IGraph graph5 = new AdjacencyMatrixGraph(true, 12);
            int[] in5 = { 0, 1 };
            int[] out5 = { 11 };
            for (int i = 2; i < 11; i++)
            {
                for (int j = i+1; j < 11; j++)
                {
                    graph5.AddEdge(i, j, 10);
                }
            }
            graph5.AddEdge(10, 11, 2);
            graph5.AddEdge(0, 2, 2);
            graph5.AddEdge(1, 2, 1);

            IGraph graph6 = new AdjacencyMatrixGraph(true, 12);
            int[] in6 = { 0, 1 };
            int[] out6 = { 11 };
            for (int i = 3; i < 11; i++)
            {
                for (int j = i+1; j < 11; j++)
                {
                    graph6.AddEdge(i, j, 10);
                }
            }
            graph6.AddEdge(10, 11, 3);
            graph6.AddEdge(0, 2, 1);
            graph6.AddEdge(1, 2, 1);
            graph6.AddEdge(2, 3, 2);

            IGraph graph7 = new AdjacencyListsGraph<SimplyAdjacencyList>(true, 40000);
            int[] in7 = { 0 };
            int[] out7 = { 39999 };
            for (int i = 3; i < 40000; i++)
                graph7.AddEdge(i-1, i, 2);
            graph7.AddEdge(0, 1, 2);
            graph7.AddEdge(1, 2, 1);

            Test[] tests = {
                 new Test(graph1, in1, out1, null, 800, 4500,3), 
                 new Test(graph2, in2, out2, new Edge[] {new Edge(3,9,1),
                     new Edge(4,9,1), new Edge(5,9,1),new Edge(3,10,1),
                     new Edge(4,10,1), new Edge(5,10,1),new Edge(3,11,1),
                     new Edge(4,11,1), new Edge(5,11,1)}, 1100, 8000,3 ), 
                 new Test(graph3, in3, out3, null, 1100, 8000,3),
                 new Test(graph4,in4,out4,null,3500, 18000,2),
                 new Test(graph5, in5, out5, new Edge[] {
                     new Edge(2,11,1), new Edge(3,11,1), new Edge(4,11,1), new Edge(5,11,1),
                     new Edge(6,11,1), new Edge(7,11,1), new Edge(8,11,1), new Edge(9,11,1),
                     new Edge(10,11,1)}, 6000, 18000,2),
                 new Test(graph6, in6, out6, new Edge[] {
                     new Edge(0,3,1), new Edge(0,4,1), new Edge(0,5,1), new Edge(0,6,1),
                     new Edge(0,7,1), new Edge(0,8,1), new Edge(0,9,1), new Edge(0,10,1),
                     new Edge(1,3,1), new Edge(1,4,1), new Edge(1,5,1), new Edge(1,6,1),
                     new Edge(1,7,1), new Edge(1,8,1), new Edge(1,9,1), new Edge(0,10,1)},
                     2500, 18000,2),
                 new Test(graph7,in7,out7,new Edge[0],1200000,10000000,1)
             };

            for (int i = 0; i < tests.Length; i++)
            {
                PerformTest(i, tests[i]);
            }
		}

        private static bool PerformTest(int id, Test t)
        {
            int fv;
            long start, end;
            start = Graph.Counter;
            Console.Out.Write("Test {0}",id);
            Edge? result = t.graph.ImprovementChecker(t.ins, t.outs, out fv);
            end = Graph.Counter;
            Console.Out.WriteLine(", czas {1}, spodziewany oko³o: poziom2: {2}, poziom1: {3}", id, end - start, t.expectedTime, t.acceptableTime);
            Console.Out.WriteLine("Test {0} -- {1}, wartosc przeplywu {2}, spodziewane {3}", id, fv==t.flowValue?"OK":"B£¥D", fv, t.flowValue);
            if ( id==6 )
                {
                if ( result==null )
                    {
                    Console.Out.WriteLine("Test {0} -- B£¥D, nie znaleziono rozwi¹zania", id);
                    return false;
                    }
                else
                    if ( result.Value.From>=0 && result.Value.From<20000 && result.Value.To>=20000 && result.Value.To<40000 && ( result.Value.From!=0 || result.Value.To!=39999 ) )
                        {
                        Console.Out.WriteLine("Test {0} -- OK, znaleziono {1}", id, result);
                        return true;
                        }
                    else
                        {
                        Console.Out.WriteLine("Test {0} -- B£¥D, niepoprawne rozwi¹zanie {1}", id, result);
                        return false;
                        }
                }
            if (t.solutions == null)
            {
                if (result == null)
                {
                    Console.Out.WriteLine("Test {0} -- OK, brak rozwi¹zañ", id);
                    return true;
                }
                else
                {
                    Console.Out.WriteLine("Test {0} -- B£¥D, powinno byæ: brak rozwi¹zañ, jest: {1}", id, result);
                    return false;
                }
            }
            else
            {
                if (result == null)
                {
                    Console.Out.WriteLine("Test {0} -- B£¥D, nie znaleziono rozwi¹zania", id);
                    return false;
                }
                else if (!t.solutions.Contains((Edge)result))
                {
                    Console.Out.WriteLine("Test {0} -- B£¥D, niepoprawne rozwi¹zanie {1}", id, result);
                    return false;
                }
                else
                {
                    Console.Out.WriteLine("Test {0} -- OK, znaleziono {1}", id, result);
                    return true;
                }
            }
        }
	}
}
