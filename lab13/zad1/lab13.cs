using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Point = ASD.Geometry.Point;
using Triangle = ASD.Geometry.Triangle;
using Segment = ASD.Geometry.Segment;

namespace ASD
{
    partial class lab13
    {
        /// <summary>
        /// 
        /// (1p.)
        /// 
        /// Wielokąt monotoniczny w kierunku poziomym to wielokąt, dla którego każda pionowa prosta 
        /// przecina go w maksymalnie dwóch punktach.
        /// Zbiór krawędzi takiego wielokąta można podzielić na dwa łańcuchy: 
        /// - górny (tworzony przez krawędzie, które stanowią górne punkty przecięcia prostych pionowych),
        /// - dolny (tworzony przez krawędzie, które stanowią dolne punkty przecięcia prostych pionowych).
        /// 
        /// Zadanie: sprawdź czy podany wielokąt jest monotoniczny i jeśli jest, to zwróć 
        /// listę jego wierzchołków w kolejności od wierzchołka o najmniejszej wartości x,
        /// w p.p. zwróć pustą tablicę.
        /// Dodatkowo zakłada się, że przekazywany jako argument wielokąt nie ma przecinających się 
        /// wzajemnie krawędzi ani dwóch wierzchołków w tym samym punkcie.
        /// </summary> 
        /// 
        /// <param name="polygon">Tablica wierzchołków wielokąta począwszy od nieznanego wierzchołka </param>
        /// <param name="sortedPolygon"> Tablica wierzchołków wielokąta począwszy od wierzchołka o najmniejszej 
        /// wartości x </param>
        /// 
        /// <returns>True jeśli wielokąt jest monotoniczny, False - w p.p.</returns>  
        /// 
        public static bool isMonotone(Point[] polygon, out Point[] sortedPolygon)
        {



            sortedPolygon = new Point[0];
            return false;
        }



        /// <summary>
        /// 
        /// (3p.)
        /// 
        /// Zadanie: dokonaj traingulacji czyli podziału wielokąta będącego monotonicznym w kierunku poziomym
        /// na trójkąty na podstawie wskazówek:
        /// - dodawaj do listy kolejne wierzchołki wg ich wartości współrzednej x rozpocznając 
        ///   od skrajnie lewego wierzchołka,
        /// - sprawdź czy z już dodanych wierzchołków można utworzyć trójkąt, a może mieć to miejsce 
        ///   w dwóch przypadkach:
        ///    a) wierzchołki należą do tego samego łańcucha i tworzą trójkąt leżący wewnątrz wielokąta, 
        ///    b) wierzchołki należą do różnych łańcuchów (tutaj zakładamy, że nie trzeba sprawdzać czy 
        ///       cały trójkąt leży wewnątrz wielokąta),
        /// - jeśli zachodzi warunek a) należy utworzyć trójkąt lub trójkąty z wierzchołków z końca listy 
        ///   do momentu gdy tworzą one trójkąt leżący wewnątrz wielokąta usuwając dla każdego nowo 
        ///   utworzonego trójkąta po jednym wierzchołku z listy,
        /// - jeśli zachodzi warunek b) należy utworzyć trójkąt lub trójkąty począwszy od początku listy 
        ///   usuwając dla każdego nowo utworzonego trójkąta po jednym wierzchołku z listy,
        /// - aby określić czy dany trójkąt tworzony z wierzchołków tego samego łańcucha leży wewnątrz 
        ///   wielokąta wystarczy określić po której stronie prostej zawierającej dwa wierzchołki 
        ///   o mniejszej wartości x leży trzeci wierzchołek.  
        /// </summary> 
        /// 
        /// <param name="polygon">Tablica wierzchołków wielokąta począwszy od wierzchołka o najmniejszej 
        /// wartości x </param>
        /// <param name="triangulation"> Tablica trójkątów </param>
        /// 
        /// <returns>Liczba trójkątów</returns>  
        /// 
        public static int triangulateMonotone(Point[] polygon, out Triangle[] triangulation)
        {



            triangulation = null;
            return 0;
        }



        /// <summary>
        /// 
        /// (1p.)
        /// 
        /// Zadanie: wyznacz pole wielokata na podstawie jego triangulacji
        /// </summary> 
        /// 
        /// <param name="triangulation"> Tablica trójkątów będących wynikiem triangulacji wielokąta
        /// </param>
        /// 
        /// <returns>Pole wielokąta</returns>
        /// 
        public static double polygonArea(Triangle[] triangulation)
        {


            return 0;
        }
    }
}
