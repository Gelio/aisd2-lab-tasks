using System;
using System.Linq;
using ASD.Graphs;

namespace lab06
{
    class Pathfinder
    {
        Graph RoadsGraph;
        int[] CityCosts;
        static double NoPathPenalty = 10000;

        public Pathfinder(Graph roads, int[] cityCosts)
        {
            RoadsGraph = roads;
            CityCosts = cityCosts;
        }

        //uwagi do wszystkich części (graf najkrótszych ścieżek)
        //   Jeżeli nie ma ścieżki pomiędzy miastami A i B to tworzymy sztuczną krawędź od A do B o karnym koszcie 10 000.

        // return: tablica kosztów organizacji ŚDM dla poszczególnym miast gdzie
        // za koszt organizacji ŚDM uznajemy sumę kosztów dostania się ze wszystkim miast do danego miasta, bez uwzględnienia kosztów przechodzenia przez miasta.
        // minCost: najmniejszy koszt
        // paths: graf skierowany zawierający drzewo najkrótyszch scieżek od wszyskich miast do miasta organizującego ŚDM (miasta z najmniejszym kosztem organizacji). 
        public double[] FindBestLocationWithoutCityCosts(out double minCost, out Graph paths)
        {
            int n = RoadsGraph.VerticesCount;
            double[] costs = new double[n];
            double[,] distances = new double[n, n];
            int[,] pathVia = new int[n, n];

            // Początkowa inicjalizacja
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                {
                    distances[i, j] = double.PositiveInfinity;
                    pathVia[i, j] = -1;
                }
                    
            // Naniesienie informacji o istniejących krawędziach w grafie
            for (int v=0; v < n; v++)
            {
                foreach (Edge e in RoadsGraph.OutEdges(v))
                {
                    distances[v, e.To] = e.Weight;
                    pathVia[v, e.To] = e.To;
                }
                    
                distances[v, v] = 0;
            }
            minCost = double.PositiveInfinity;

            // Znalezienie ścieżek między miastami
            for (int middleV = 0; middleV < n; middleV++)
                for (int startV = 0; startV < n; startV++)
                    for (int endV = 0; endV < n; endV++)
                    {
                        double prospectiveDistance = distances[startV, middleV] + distances[middleV, endV];
                        if (distances[startV, endV] > prospectiveDistance)
                        {
                            distances[startV, endV] = prospectiveDistance;
                            pathVia[startV, endV] = middleV;
                        }
                            
                    }

            // Wybór najlepszego miasta
            paths = null;
            int minCostVertex = -1;
            for (int v = 0; v < n; v++)
            {
                for (int startV = 0; startV < n; startV++)
                {
                    if (double.IsPositiveInfinity(distances[startV, v]))
                        costs[v] += NoPathPenalty;
                    else
                        costs[v] += distances[startV, v];
                }
                if (minCost > costs[v])
                {
                    minCost = costs[v];
                    minCostVertex = v;
                }
            }

            // Tworzenie grafu ścieżek
            paths = RoadsGraph.IsolatedVerticesGraph(true, n);
            for (int startV = 0; startV < n; startV++)
            {
                if (double.IsPositiveInfinity(distances[startV, minCostVertex]))
                    paths.AddEdge(startV, minCostVertex, NoPathPenalty);
                else
                {
                    // Find path between startV and minCostVertex
                    int currentV = startV;
                    while (currentV != minCostVertex)
                    {
                        int middleV = pathVia[currentV, minCostVertex];
                        paths.AddEdge(currentV, middleV, RoadsGraph.GetEdgeWeight(currentV, middleV));
                        currentV = middleV;
                    }
                }
            }
            
            return costs;
        }

        // return: tak jak w punkcie poprzednim, ale tym razem
        // za koszt organizacji ŚDM uznajemy sumę kosztów dostania się ze wszystkim miast do wskazanego miasta z uwzględnieniem kosztów przechodzenia przez miasta (cityCosts[]).
        // Nie uwzględniamy kosztu przejścia przez miasto które organizuje ŚDM.
        // minCost: najlepszy koszt
        // paths: graf skierowany zawierający drzewo najkrótyszch scieżek od wszyskich miast do miasta organizującego ŚDM (miasta z najmniejszym kosztem organizacji). 
        public double[] FindBestLocation(out double minCost, out Graph paths)
        {
            int n = RoadsGraph.VerticesCount;
            double[] costs = new double[n];
            double[,] distances = new double[n, n];
            int[,] pathVia = new int[n, n];

            // Początkowa inicjalizacja
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                {
                    distances[i, j] = double.PositiveInfinity;
                    pathVia[i, j] = -1;
                }

            // Naniesienie informacji o istniejących krawędziach w grafie
            for (int v = 0; v < n; v++)
            {
                foreach (Edge e in RoadsGraph.OutEdges(v))
                {   
                    // Jedyna zmiana pomiędzy częścią 0 i 1 jest tutaj (trzeba doliczyć, że wychodząc z miasta od razu mamy koszt)
                    distances[v, e.To] = CityCosts[v] + e.Weight;
                    pathVia[v, e.To] = e.To;
                }

                distances[v, v] = 0;
            }
            minCost = double.PositiveInfinity;

            // Znalezienie ścieżek między miastami
            for (int middleV = 0; middleV < n; middleV++)
                for (int startV = 0; startV < n; startV++)
                    for (int endV = 0; endV < n; endV++)
                    {
                        // Tu nie trzeba doliczać kosztów wyjścia z miast, bo one są już wliczone w poszczególnych komórkach macierzy distances
                        double prospectiveDistance = distances[startV, middleV] + distances[middleV, endV];
                        if (distances[startV, endV] > prospectiveDistance)
                        {
                            distances[startV, endV] = prospectiveDistance;
                            pathVia[startV, endV] = middleV;
                        }

                    }

            // Wybór najlepszego miasta
            paths = null;
            int minCostVertex = -1;
            for (int v = 0; v < n; v++)
            {
                for (int startV = 0; startV < n; startV++)
                {
                    if (double.IsPositiveInfinity(distances[startV, v]))
                        costs[v] += NoPathPenalty;
                    else
                        costs[v] += distances[startV, v];
                }
                if (minCost > costs[v])
                {
                    minCost = costs[v];
                    minCostVertex = v;
                }
            }

            // Tworzenie grafu ścieżek
            paths = RoadsGraph.IsolatedVerticesGraph(true, n);
            for (int startV = 0; startV < n; startV++)
            {
                if (double.IsPositiveInfinity(distances[startV, minCostVertex]))
                    paths.AddEdge(startV, minCostVertex, NoPathPenalty);
                else
                {
                    // Find path between startV and minCostVertex
                    int currentV = startV;
                    while (currentV != minCostVertex)
                    {
                        int middleV = pathVia[currentV, minCostVertex];
                        paths.AddEdge(currentV, middleV, RoadsGraph.GetEdgeWeight(currentV, middleV));
                        currentV = middleV;
                    }
                }
            }

            return costs;
        }

        // return: tak jak w punkcie poprzednim, ale tym razem uznajemy zarówno koszt przechodzenia przez miasta, jak i wielkość miasta startowego z którego wyruszają pielgrzymi.
        // Szczegółowo opisane jest to w treści zadania "Częśc 2". 
        // minCost: najlepszy koszt
        // paths: graf skierowany zawierający drzewo najkrótyszch scieżek od wszyskich miast do miasta organizującego ŚDM (miasta z najmniejszym kosztem organizacji). 
        public double[] FindBestLocationSecondMetric(out double minCost, out Graph paths)
        {
            int n = RoadsGraph.VerticesCount;
            double[] costs = new double[n];
            double[][,] distances = new double[n][,];
            int[][,] pathVia = new int[n][,];
            for (int i = 0; i < n; i++)
            {
                distances[i] = new double[n, n];
                pathVia[i] = new int[n, n];
            }
            // Początkowa inicjalizacja
            for (int k = 0; k < n; k++)
                for (int i = 0; i < n; i++)
                    for (int j = 0; j < n; j++)
                    {
                        distances[k][i, j] = double.PositiveInfinity;
                        pathVia[k][i, j] = -1;
                    }

            // Naniesienie informacji o istniejących krawędziach w grafie
            for (int k = 0; k < n; k++)
            {
                for (int v = 0; v < n; v++)
                {
                    foreach (Edge e in RoadsGraph.OutEdges(v))
                    {
                        distances[k][v, e.To] = Math.Min(CityCosts[k], CityCosts[v]) + Math.Min(CityCosts[k], e.Weight);
                        pathVia[k][v, e.To] = e.To;
                    }

                    distances[k][v, v] = 0;
                }
            }

            minCost = double.PositiveInfinity;

            // Znalezienie ścieżek między miastami
            for (int middleV = 0; middleV < n; middleV++)
                for (int startV = 0; startV < n; startV++)
                    for (int endV = 0; endV < n; endV++)
                    {
                        double prospectiveDistance = distances[startV][startV, middleV] + distances[startV][middleV, endV];
                        if (distances[startV][startV, endV] > prospectiveDistance)
                        {
                            distances[startV][startV, endV] = prospectiveDistance;
                            pathVia[startV][startV, endV] = middleV;
                        }

                    }

            // Wybór najlepszego miasta
            paths = null;
            int minCostVertex = -1;
            for (int v = 0; v < n; v++)
            {
                for (int startV = 0; startV < n; startV++)
                {
                    if (double.IsPositiveInfinity(distances[startV][startV, v]))
                        costs[v] += NoPathPenalty;
                    else
                        costs[v] += distances[startV][startV, v];
                }
                if (minCost > costs[v])
                {
                    minCost = costs[v];
                    minCostVertex = v;
                }
            }

            // Tworzenie grafu ścieżek
            paths = RoadsGraph.IsolatedVerticesGraph(true, n);
            for (int startV = 0; startV < n; startV++)
            {
                if (double.IsPositiveInfinity(distances[startV][startV, minCostVertex]))
                    paths.AddEdge(startV, minCostVertex, NoPathPenalty);
                else
                {
                    // Find path between startV and minCostVertex
                    int currentV = startV;
                    while (currentV != minCostVertex)
                    {
                        int middleV = pathVia[startV][currentV, minCostVertex];
                        paths.AddEdge(currentV, middleV, RoadsGraph.GetEdgeWeight(currentV, middleV));
                        currentV = middleV;
                    }
                }
            }

            return costs;
        }

    }
}
