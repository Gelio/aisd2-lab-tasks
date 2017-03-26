
// Autor:      Marek G¹golewski
// Poprawi³:   Jan Bródka

namespace ASD.Graphs
{
    using System;
    using System.Collections.Generic;
    using System.Drawing; // uwaga: nalezy dodac "referencje" do System.Drawing!

    /* Klasa Terrain reprezentuje mape wirtualnego terenu.
      Teren jest kwadratem o boku Size, implementujacym
      interfejs IGraph - sklada siê z Size*Size (VerticesCount) wierzcholkow,
      z ktorych kazdy reprezentuje wysokosc pewnego punktu na siatce
      rownoleznikowo-poludnikowej.
      Wspolrzedne (x,y) sa liczbami calkowitymi, obydwie ze zbioru {0,1,...,Size-1}.

      Garsc informacji o metodach:

      void ExportImage(string filename) sluzy do zapisu mapy do pliku graficznego
      void ExportImage(string filename, Edge[] path) sluzy do zapisu mapy do pliku graficznego
        + zaznaczenie jakiejs scie¿ki na mapie (dobre np. do wizualizacji wyniku
        dzialania algorytmu A*)

      int GetVertexNum(int x1, int y1) konwertuje wspolrzedne mapy (w pikselach,
      numerowane od 0) na numer wierzcholka grafu

      double GetHeight(int vertex) - zwraca wysokosc terenu n.p.m. odpowiadajacego
      danemu wierzcholkowi

      Szacowanie odleglosci m/y wierzcholkami:
      int GetDistanceTaxi(int, int) - metryka "taksowkowa"
      int GetDistanceE(int, int)    - metryka euklidesowa
    */

    public sealed class Terrain : Graph
    {
    private readonly double mH; // chropowatosc terenu
    private readonly int mSizeLog;
    private readonly int mSize; // rozmiar boku planszy
    private double[,] mHeight;
    private readonly int mRandomSeed;
    private double mMinHeight = 0.0;
    private double mMaxHeight = 0.0;
    private const double mWaterLevel = 0.0;
    private static readonly Color mAreaLandColor = Color.ForestGreen;
    private static readonly Color mAreaOpenColor = Color.DarkKhaki;
    private static readonly Color mAreaCloseColor = Color.Khaki;
    private static readonly Color mAreaWaterColor = Color.DarkBlue;
    private static readonly Color mPathColor = Color.Brown;
    private static readonly int[] mNeighborSearchOrderX = {   0,   1,   1,   1,   0,  -1,  -1,  -1 };
    private static readonly int[] mNeighborSearchOrderY = {  -1,  -1,   0,   1,   1,   1,   0,  -1 };
    private static readonly int[] mNeighborWeight       = { 100, 142, 100, 142, 100, 142, 100, 142 };

    // h - chropowatosc terenu
    // sizelog - logarytm rozmiaru boku kwadratu mapy
    // randomseed - ziarno generatora liczb pseudolosowych
    public Terrain(double h, int sizelog, int randomseed) : base(true,((1<<sizelog)+1)*((1<<sizelog)+1))
        {
        mSizeLog = sizelog;
        mH = h;
        mSize = (1 << mSizeLog) + 1;
        mRandomSeed = randomseed;
        generate();
        }

    public override IEnumerable<Edge> OutEdges(int from)
        {
        int y = from / mSize;
        int x = from % mSize;
        int nx, ny;
        for ( int i=0 ; i<mNeighborSearchOrderX.Length ; ++i )
            {
            nx = x + mNeighborSearchOrderX[i];
            ny = y + mNeighborSearchOrderY[i];
            if ( nx>=0 && nx<mSize && ny>=0 && ny<mSize && mHeight[nx,ny]>=mWaterLevel)
                {
                Access();
                yield return new Edge(from, nx+mSize*ny, mNeighborWeight[i] );
                }
            }
          }

    // odleglosc euklidesowa miedzy dwoma wierzcholkami
    public int GetDistanceE(int from, int to)
        {
        double y1 = from / mSize;
        double x1 = from % mSize;
        double y2 = to / mSize;
        double x2 = to % mSize;
        return (int)(100.0*Math.Sqrt((y1-y2)*(y1-y2)+(x1-x2)*(x1-x2)));
        }

    // "wysokosc" terenu pod danym wierzcholkiem
    public double GetHeight(int vertex)
        {
        int y1 = vertex / mSize;
        int x1 = vertex % mSize;
        return mHeight[x1, y1];
        }

      // tlumaczy wspolrzedne terenu na numer wewnetrzny wierzcholka
    public int GetVertexNum(int x1, int y1)
        {
        return x1 + y1 * mSize;
        }

    // ----------------------------------------------------------------
    // Generowanie pseudolosowego terenu i jego rysowanie
    // ----------------------------------------------------------------

    /*
    * Implementacja algorytmu "przesuniecia punktu srodkowego" Fourniera i in. (1982)
    * do generowania przyblizenia powierzchni wg ulamkowego ruchu Browna.
    */
    private void generate()
        {
        int i, j;
        mHeight = new double[mSize, mSize];
        double dy = 1.0f;
        double reduce = Math.Pow(2.0, -mH);
        int k, subd;
        Random rand = new Random(mRandomSeed);
        mHeight[0,0] = 0.0f;
        mHeight[mSize-1,0] = 0.0f;
        mHeight[mSize-1,mSize-1] = 0.0f;
        mHeight[0,mSize-1] = 0.0f;

        k = 1;
        subd = mSize/2;

        while (k<=mSizeLog)
            {
            // square
            for (i=subd; i<mSize; i+=2*subd)
                for (j=subd; j<mSize; j+=2*subd)
                   {
                   mHeight[j,i] = mHeight[j-subd,i-subd] + mHeight[j+subd,i-subd] + mHeight[j-subd,i+subd] + mHeight[j+subd,i+subd];
                   mHeight[j,i] /= 4.0f; // srednia z wartosci na sasiadujacym kwadracie
                   mHeight[j,i] += (rand.NextDouble()*2.0-1.0)*dy; // z dodana losowa wartoscia
                   }

            // diamond
            for (i=0; i<mSize; i+=2*subd)
                for (j=subd; j<mSize; j+=2*subd)
                    {
                    if (i==mSize-1)
                        {
                        mHeight[j,i] = mHeight[j,0];
                        mHeight[i,j] = mHeight[0,j];
                        }
                    else
                        {
                        mHeight[j,i] = mHeight[j,((i-subd>=0)?(i-subd):(mSize+i-subd-1))] + mHeight[j,((i+subd<mSize)?(i+subd):(i-subd))] + mHeight[j-subd,i] + mHeight[j+subd,i];
                        mHeight[j,i] /= 4.0f; // srednia z wartosci na sasiadujacym rombie
                        mHeight[j,i] += (rand.NextDouble()*2.0-1.0)*dy; // z dodana losowa wartoscia
                        mHeight[i,j] = mHeight[i,(j-subd)] + mHeight[i,(j+subd)] + mHeight[((i-subd>=0)?(i-subd):(mSize+i-subd-1)),j] + mHeight[((i+subd<mSize)?(i+subd):(i-subd)),j];
                        mHeight[i,j] /= 4.0f; // srednia z wartosci na sasiadujacym rombie
                        mHeight[i,j] += (rand.NextDouble()*2.0-1.0)*dy; // z dodana losowa wartoscia
                        }
                    }

            k++;
            subd /= 2;
            dy *= reduce;
            }

        // normalizacja wysokosci za chwile nastapi...
        mMinHeight = 1e99;
        mMaxHeight = 1e-99;
        for (i = 0; i < mSize; ++i)
            for (j = 0; j < mSize; ++j)
                {
                if (mHeight[i,j] < mMinHeight) mMinHeight = mHeight[i,j];
                if (mHeight[i,j] > mMaxHeight) mMaxHeight = mHeight[i,j];
                }

        for (i = 0; i < mSize; ++i)
            for (j = 0; j < mSize; ++j)
                mHeight[i,j] = (mHeight[i,j]-mMinHeight)/(mMaxHeight-mMinHeight)*2.0-1.0-mWaterLevel;

        mMaxHeight = 1.0-mWaterLevel;
        mMinHeight = -1.0-mWaterLevel;

        // ustaw vertices i edgesCount
        EdgesCount = 0;
        for (int y = 0; y < mSize; ++y)
            for (int x = 0; x < mSize; ++x)
                {
                int nr = GetVertexNum(x, y);
                inDegreeTable[nr] = 0;
                for (i = 0; i < mNeighborSearchOrderX.Length; ++i)
                    if ( x + mNeighborSearchOrderX[i] >= 0 && x + mNeighborSearchOrderX[i] < mSize &&
                         y + mNeighborSearchOrderY[i] >= 0 && y + mNeighborSearchOrderY[i] < mSize &&
                         mHeight[x+mNeighborSearchOrderX[i],y+mNeighborSearchOrderY[i]]>=mWaterLevel ) ++inDegreeTable[nr];

                outDegreeTable[nr] = inDegreeTable[nr];
                EdgesCount += inDegreeTable[nr];
                }
        EdgesCount /= 2;
    }

    public void ExportImage(string fileName, Edge[] path, System.Collections.Generic.Dictionary<int,string> description)
        {
        Bitmap bmp = new Bitmap(mSize, mSize);
        int x, y;

        DrawImage(bmp, description);

        for (int i = 0; i < path.Length; ++i)
            {
            x = path[i].From % mSize;
            y = path[i].From / mSize;
            bmp.SetPixel(x, y, mPathColor);
            }

         x = path[path.Length-1].To % mSize;
         y = path[path.Length-1].To / mSize;
         bmp.SetPixel(x, y, mPathColor);
         bmp.Save(fileName,System.Drawing.Imaging.ImageFormat.Bmp);
    }

      // metoda pomocnicza dla ExportImage
    private void DrawImage(Bitmap bmp,System.Collections.Generic.Dictionary<int,string> description)
        {
        for (int i = 0; i < mSize; ++i)
            for (int j = 0; j < mSize; ++j)
                if (mHeight[j,i] >= mWaterLevel)
                    if ( description.ContainsKey(GetVertexNum(j,i)) )
                        switch ( description[GetVertexNum(j,i)] )
                            {
                            case "open":
                                bmp.SetPixel(j, i,
                                             Color.FromArgb(Math.Min(mAreaOpenColor.R + (int)(mHeight[j, i] * 40.0), 255),
                                             Math.Min(mAreaOpenColor.G + (int)(mHeight[j, i] * 40.0), 255),
                                             Math.Min(mAreaOpenColor.B + (int)(mHeight[j, i] * 40.0), 255)));
                                break;
                            case "close":
                                bmp.SetPixel(j, i,
                                             Color.FromArgb(Math.Min(mAreaCloseColor.R + (int)(mHeight[j, i] * 40.0), 255),
                                             Math.Min(mAreaCloseColor.G + (int)(mHeight[j, i] * 40.0), 255),
                                             Math.Min(mAreaCloseColor.B + (int)(mHeight[j, i] * 40.0), 255)));
                                break;
                            }
                        else
                            bmp.SetPixel(j, i,
                                         Color.FromArgb(Math.Min(mAreaLandColor.R + (int)(mHeight[j, i] * 40.0), 255),
                                         Math.Min(mAreaLandColor.G + (int)(mHeight[j, i] * 40.0), 255),
                                         Math.Min(mAreaLandColor.B + (int)(mHeight[j, i] * 40.0), 255)));
                    else
                        bmp.SetPixel(j, i,
                                     Color.FromArgb(Math.Max(mAreaWaterColor.R+(int)(mHeight[j,i]*40.0), 0),
                                     Math.Max(mAreaWaterColor.G+(int)(mHeight[j,i]*40.0), 0),
                                     Math.Max(mAreaWaterColor.B+(int)(mHeight[j,i]*40.0), 0)));
        }

    // -----------------------------------------------------------------
    // tych skladowych interfejsu nie implementujemy - nie ma potrzeby:
    // -----------------------------------------------------------------

    public override Graph Clone()
        {
        throw new NotSupportedException("Operation is not supported.");
        }

    public override Graph IsolatedVerticesGraph()
        {
        throw new NotSupportedException("Operation is not supported.");
        }

    public override Graph IsolatedVerticesGraph(bool directed, int verticesCount)
        {
        throw new NotSupportedException("Operation is not supported.");
        }

    public override bool AddEdge(int from, int to, double weight)
        {
        throw new NotSupportedException("Operation is not supported.");
        }

    public bool AddEdge(int from, int to)
        {
        throw new NotSupportedException("Operation is not supported.");
        }

    public override bool DelEdge(int from, int to)
        {
        throw new NotSupportedException("Operation is not supported.");
        }

    public override double GetEdgeWeight(int from, int to)
        {
        throw new NotSupportedException("Operation is not supported.");
        }

    public int? ModifyEdgeWeight(int from, int to, int add)
        {
        throw new NotSupportedException("Operation is not supported.");
        }

        public override double ModifyEdgeWeight(int from, int to, double add)
        {
            throw new NotImplementedException();
        }
    }

}
