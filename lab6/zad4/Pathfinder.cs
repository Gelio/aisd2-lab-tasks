using System;
using System.Linq;
using ASD.Graphs;

namespace lab06
{
    class Pathfinder
    {
        Graph RoadsGraph;
        int[] CityCosts;

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
            minCost = -1;
            paths = null;
            return null;
        }

        // return: tak jak w punkcie poprzednim, ale tym razem
        // za koszt organizacji ŚDM uznajemy sumę kosztów dostania się ze wszystkim miast do wskazanego miasta z uwzględnieniem kosztów przechodzenia przez miasta (cityCosts[]).
        // Nie uwzględniamy kosztu przejścia przez miasto które organizuje ŚDM.
        // minCost: najlepszy koszt
        // paths: graf skierowany zawierający drzewo najkrótyszch scieżek od wszyskich miast do miasta organizującego ŚDM (miasta z najmniejszym kosztem organizacji). 
        public double[] FindBestLocation(out double minCost, out Graph paths)
        {
            minCost = -1;
            paths = null;
            return null;
        }

        // return: tak jak w punkcie poprzednim, ale tym razem uznajemy zarówno koszt przechodzenia przez miasta, jak i wielkość miasta startowego z którego wyruszają pielgrzymi.
        // Szczegółowo opisane jest to w treści zadania "Częśc 2". 
        // minCost: najlepszy koszt
        // paths: graf skierowany zawierający drzewo najkrótyszch scieżek od wszyskich miast do miasta organizującego ŚDM (miasta z najmniejszym kosztem organizacji). 
        public double[] FindBestLocationSecondMetric(out double minCost, out Graph paths)
        {
            minCost = -1;
            paths = null;
            return null;
        }

    }
}
