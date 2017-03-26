
using System;
using ASD.Graphs;

class Lab06
{

    class Test
    {
        public double h;    // chropowatosc terenu najlepiej z przedzialu [0.3; 0.8]
        public int logsize; // rozmiar terenu - najlepiej np. 7 lub 8,
                            // tworzony jest teren (graf) o rozmiarze 2^logsize na 2^logsize
        public int seed;    // ziarno generatora liczb pseudolosowych (jedno ziarno=jedna mapa)
        public int x1, y1;  // punkt startowy (odczytaj jako wspolrzedne "zielonego" piksela na obrazku)
        public int x2, y2;  // punkt docelowy (odczytaj jako wspolrzedne "zielonego" piksela na obrazku)
        public double w;       // waga œcie¿ki
        public int c;       // liczba krawedzi
        public long l;
        public Test(double _h, int _ls, int _seed, int _x1, int _y1, int _x2, int _y2, double _w = 0, int _c = 0, long _l = 0)
        {
            h = _h; logsize = _ls; seed = _seed; x1 = _x1; y1 = _y1; x2 = _x2; y2 = _y2; w = _w; c = _c; l = _l;
        }
    }

    public static void Main()
    {
        // Wynikiem dzialania programu jest bitmapa,
        // znajdzie sie ona w katalogu programu
        // * zielone piksele - teren
        // * niebieskie - woda
        // * jasnozielone piksele - wierzcholki odwiedzone przez algorytm przeszukiwania
        // * zolte - wynikowa sciezka

        Test[] tests = new Test[] { new Test(0.80, 7, 325324,  20,  35,  70,  37,   5168,   50,    26) ,
                                    new Test(0.70, 8,   3544,  57, 165, 134,  60,  15386,  119,   219) ,
                                    new Test(0.60, 8,  32532,  51,   4,  39, 224,  27420,  228,  1169) ,
                                    new Test(0.75, 9,   8534, 380,  50, 350,  30, 126722, 1103, 13222) ,
                                    new Test(0.80, 7, 325324,  20,  35,  70,  37,   5168,   50,   198) ,
                                    new Test(0.70, 8,   3544,  57, 165, 134,  60,  15386,  119,  2307) ,
                                    new Test(0.60, 8,  32532,  51,   4,  39, 224,  27420,  228,  3010) };
        Edge[] path;
        Terrain mapa = null;
        Func<int, int, int> oszac = (nr1, nr2) => mapa.GetDistanceE(nr1, nr2); // funkcja szacujaca odleglosc
        double d;
        ulong c1, c2;

        for (int i = 0; i < tests.Length; ++i)
        {
            if (i >= 4) oszac = null;
            mapa = new Terrain(tests[i].h, tests[i].logsize, tests[i].seed); // tworzymy mape
            var description = new System.Collections.Generic.Dictionary<int, string>();
            string fileName = "test" + i.ToString() + ".bmp";
            Console.Write("\nTest {0}:  ", i);
            c1 = Graph.Counter;
            d = mapa.AStar(mapa.GetVertexNum(tests[i].x1, tests[i].y1), mapa.GetVertexNum(tests[i].x2, tests[i].y2), out path, description, oszac);
            c2 = Graph.Counter;
            if (d.IsNaN())
                if (path != null)
                {
                    Console.WriteLine("droga odnaleziona");
                    Console.WriteLine("    suma wag:          {0,7},   {1}", d, d == tests[i].w);
                    Console.WriteLine("    liczba krawedzi:   {0,7},   {1}", path.Length, path.Length == tests[i].c);
                }
                else
                {
                    path = new Edge[1];
                    path[0] = new Edge(mapa.GetVertexNum(tests[i].x1, tests[i].y1), mapa.GetVertexNum(tests[i].x2, tests[i].y2), 0);
                    Console.WriteLine("jedynie policzona odleglosc:  {0,7},   {1}", d, d == tests[i].w);
                }
            else // sciezka nieodnaleziona
            {
                path = new Edge[1];
                path[0] = new Edge(mapa.GetVertexNum(tests[i].x1, tests[i].y1), mapa.GetVertexNum(tests[i].x2, tests[i].y2), 0);
                Console.WriteLine("rozwiazanie nie znalezione");
            }
            Console.WriteLine("    licznik wydajnosci: {0,6:0} tys. (powinno byc okolo {1} tys)", (c2 - c1) / 1000.0, tests[i].l);
            mapa.ExportImage(fileName, path, description);
            using (var proc = System.Diagnostics.Process.Start("iexplore", "file:///" + System.IO.Path.GetFullPath(fileName))) { }
        }

        Console.WriteLine();
    }

}
