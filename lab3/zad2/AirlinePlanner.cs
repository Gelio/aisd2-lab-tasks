using System;
using System.Collections.Generic;
using ASD.Graph;


namespace zadanie3
{
    class AirlinePlanner
    {
        int num;
        IGraph airline;
        GraphExport ge;
        
        // Możesz dodać nowe składowe


        public AirlinePlanner(string path,int n)
        {
             num = n;
             ge = new GraphExport(false, path);
        }

        public void CreateNetwork(City[] coordinates)
        {
        // uzupełnij
        }

        public int[] FindNewBase()
        {
        // uzupełnij

        // najprostszy algorytm polega na powtarzaniu procesu
        //  - wyznaczania liści w grafie (drzewie)
        //  - usuwania liści
        // dopóki pozostały wiecej niż 2 nie usunięte wierzchołki

            return new int[0]; // zmień
        }

        public int GetMaximumDistance(int start)
        {
        // uzupełnij

        // można wykorzystać przeszukiwanie wszerz
        // rozważ użycie metody GeneralSearchFrom

        // Zabronione jest korzystanie z metod rozszerzających zdefiniowanych w klasie ShortestPathsGraphExtender !!!!

            return 0; // zmień
        }

        public void Show()
        {
        if ( airline!=null )
            ge.Export(airline,null,string.Format("Test{0}",num));
        }

    }
}
