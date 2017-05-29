using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
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
            int leftmostPointIndex = 0,
                rightmostPointIndex = 0,
                n = polygon.Length;
            for (int i = 1; i < n; i++)
            {
                if (polygon[i].x < polygon[leftmostPointIndex].x)
                    leftmostPointIndex = i;
                if (polygon[i].x > polygon[rightmostPointIndex].x)
                    rightmostPointIndex = i;
            }

            sortedPolygon = new Point[n];

            int currentPoint = leftmostPointIndex,
                j = 0;
            while (currentPoint != rightmostPointIndex)
            {
                // Lower trail
                int nextPoint = (currentPoint + 1) % n;

                if (polygon[nextPoint].x <= polygon[currentPoint].x)
                {
                    sortedPolygon = null;
                    return false;
                }

                sortedPolygon[j++] = polygon[currentPoint];
                currentPoint = nextPoint;
            }

            while (currentPoint != leftmostPointIndex)
            {
                // Upper tail (reverse)
                int nextPoint = (currentPoint + 1) % n;

                if (polygon[nextPoint].x >= polygon[currentPoint].x)
                {
                    sortedPolygon = null;
                    return false;
                }

                sortedPolygon[j++] = polygon[currentPoint];
                currentPoint = nextPoint;
            }

            return true;
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
            int n = polygon.Length;
            HashSet<Point> lowerTrail = new HashSet<Point>(),
                upperTrail = new HashSet<Point>();

            lowerTrail.Add(polygon[0]);
            upperTrail.Add(polygon[0]);

            bool firstTimeUpperTrail = true;

            for (int i = 1; i < n; i++)
            {
                if (polygon[i].x > polygon[i - 1].x)
                {
                    lowerTrail.Add(polygon[i]);
                }
                else
                {
                    if (firstTimeUpperTrail)
                    {   
                        // the rightmost point is both in lower and upper trail (as is the leftmost one)
                        upperTrail.Add(polygon[i - 1]);
                        firstTimeUpperTrail = false;
                    }

                    upperTrail.Add(polygon[i]);
                }
            }

            List<Triangle> triangles = new List<Triangle>();
            List<Point> currentPoints = new List<Point>(n);
            foreach (Point p in polygon.OrderBy(p => p.x))
            {
                bool triangleConstructed = false;

                // Try to construct a triangle from the same trail as the current point
                if (lowerTrail.Contains(p))
                {
                    for (int i = 0; i < currentPoints.Count; i++)
                    {
                        if (triangleConstructed)
                            break;
                        Point p1 = currentPoints[i];

                        if (!lowerTrail.Contains(p1))
                            continue;
                        if (upperTrail.Contains(p1))
                            continue;

                        for (int j = i + 1; j < currentPoints.Count; j++)
                        {
                            if (i == j)
                                continue;
                            Point p2 = currentPoints[j];
                            if (!lowerTrail.Contains(p2))
                                continue;

                            if (Point.CrossProduct(p1 - p, p2 - p1) > 0)
                            {
                                triangleConstructed = true;
                                triangles.Add(new Triangle(p1, p2, p));
                                currentPoints.Remove(p1);
                                break;
                            }
                        }
                    }
                }

                // Same, but for the upper trail
                if (!triangleConstructed && upperTrail.Contains(p))
                {
                    for (int i = 0; i < currentPoints.Count; i++)
                    {
                        if (triangleConstructed)
                            break;
                        Point p1 = currentPoints[i];

                        if (!upperTrail.Contains(p1))
                            continue;
                        if (lowerTrail.Contains(p1))
                            continue;

                        for (int j = i + 1; j < currentPoints.Count; j++)
                        {
                            if (i == j)
                                continue;
                            Point p2 = currentPoints[j];
                            if (!upperTrail.Contains(p2))
                                continue;

                            if (Point.CrossProduct(p1 - p, p2 - p1) < 0)
                            {
                                triangleConstructed = true;
                                triangles.Add(new Triangle(p1, p2, p));
                                currentPoints.Remove(p1);
                                break;
                            }
                        }
                    }
                }


                // Now try to construct the triangle from points from different trails
                // This part is for when the current point is in the lower trail
                if (!triangleConstructed && lowerTrail.Contains(p))
                {
                    for (int i = 0; i < currentPoints.Count; i++)
                    {
                        if (triangleConstructed)
                            break;

                        Point pL = currentPoints[i];
                        if (!lowerTrail.Contains(pL))
                            continue;

                        for (int j = 0; j < currentPoints.Count; j++)
                        {
                            if (i == j)
                                continue;
                            Point pU = currentPoints[j];
                            if (!upperTrail.Contains(pU))
                                continue;

                            triangleConstructed = true;
                            triangles.Add(new Triangle(pL, pU, p));
                            currentPoints.Remove(pL);
                            break;
                        }
                    }
                }

                // Same, but for the upper trail
                if (!triangleConstructed && upperTrail.Contains(p))
                {
                    for (int i = 0; i < currentPoints.Count; i++)
                    {
                        if (triangleConstructed)
                            break;

                        Point pL = currentPoints[i];
                        if (!lowerTrail.Contains(pL))
                            continue;

                        for (int j = 0; j < currentPoints.Count; j++)
                        {
                            if (i == j)
                                continue;
                            Point pU = currentPoints[j];
                            if (!upperTrail.Contains(pU))
                                continue;

                            triangleConstructed = true;
                            triangles.Add(new Triangle(pL, pU, p));
                            currentPoints.Remove(pU);
                            break;
                        }
                    }
                }

                currentPoints.Add(p);
            }


            triangulation = triangles.ToArray();
            return triangles.Count;
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
            double area = 0;
            foreach (Triangle triangle in triangulation)
                area += Math.Abs(Point.CrossProduct(triangle.b - triangle.a, triangle.c - triangle.a)) / 2;

            return area;
        }
    }
}
