using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;

namespace discs
{
    public struct Point
    {
        public Point(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }

        public double X { get; private set; }
        public double Y { get; private set; }

        public override string ToString()
        {
            return string.Format("[{0};{1}]", X, Y);
        }


        public bool IsRightOf(Point b)
        {
            return (this.X > b.X || (this.X == b.X && this.Y > b.Y));
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Point))
                return false;

            Point other = (Point)obj;
            return X == other.X && Y == other.Y;
        }
    }

    public enum IntersectionType
    {
        Disjoint,
        Contains,
        IsContained,
        Identical,
        Touches,
        Crosses
    }

    public struct Disk
    {
        public Point Center { get; private set; }
        public double Radius { get; private set; }

        public Disk(Point center, double radius)
        {
            this.Center = center;
            this.Radius = radius;
        }

        public bool Contains(Point p)
        {
            return (p.X - Center.X) * (p.X - Center.X) + (p.Y - Center.Y) * (p.Y - Center.Y) <= Radius * Radius + Program.epsilon;
        }

        /// <summary>
        ///  Funkcja sprawdza wzajemne położenie dwóch kół.
        /// </summary>
        /// <param name="other">drugie koło</param>
        /// <param name="crossingPoints">
        /// Punkty przecięcia obwodów kół, jeśli zwracana jest wartość: Touches albo Crosses.
        /// Pusta tablica wpp.
        /// <returns>
        /// Disjoint - kiedy koła nie mają punktów wspólnych
        /// Contains - kiedy pierwsze koło całkowicie zawiera drugie
        /// IsContained - kiedy pierwsze koło jest całkowicie zawarte w drugim
        /// Identical - kiedy koła pokrywają się
        /// Touches - kiedy koła mają dokładnie jeden punkt wspólny
        /// Crosses - kiedy obwody kół mają dokładnie dwa punkty wspólne
        /// </returns>
        public IntersectionType GetIntersectionType(Disk other, out Point[] crossingPoints)
        {
            double dX = other.Center.X - this.Center.X;
            double dY = other.Center.Y - this.Center.Y;
            double dist2 = dX * dX + dY * dY;
            double dist = Math.Sqrt(dist2);

            /*
             * tu zajmij się wszystkimi przypadkami wzajemnego położenia kół,
             * oprócz Crosses i Touches
             */
            crossingPoints = new Point[0];

            if (Math.Abs(Radius - other.Radius) < Program.epsilon && Center.Equals(other.Center))
                return IntersectionType.Identical;

            double radiusSum = Radius + other.Radius;
            if (dist - radiusSum > Program.epsilon)
                return IntersectionType.Disjoint;

            if (Radius - (dist + other.Radius) + Program.epsilon >= 0)
                return IntersectionType.Contains;
            if (other.Radius - (dist + Radius) + Program.epsilon >= 0)
                return IntersectionType.IsContained;


            // odległość od środka aktualnego koła (this) do punktu P,
            // który jest punktem przecięcia odcinka łączącego środki kół (this i other)
            // z odcinkiem łączącym punkty wspólne obwodów naszych kół
            double a = (this.Radius * this.Radius - other.Radius * other.Radius + dist2) / (2 * dist);

            // odległość punktów przecięcia obwodów do punktu P
            double h = Math.Sqrt(this.Radius * this.Radius - a * a);

            // punkt P
            double px = this.Center.X + (dX * a / dist);
            double py = this.Center.Y + (dY * a / dist);

            /*
             * teraz wiesz już wszystko co potrzebne do rozpoznania położenia Touches
             * zajmij się tym
             */
            if (h < Program.epsilon)
            {
                crossingPoints = new Point[1] { new Point(px, py) };
                return IntersectionType.Touches;
            }

            // przypadek Crosses - dwa punkty przecięcia - już jest zrobiony

            double rX = -dY * h / dist;
            double rY = dX * h / dist;

            crossingPoints = new Point[2];
            crossingPoints[0] = new Point(px + rX, py + rY);
            crossingPoints[1] = new Point(px - rX, py - rY);
            return IntersectionType.Crosses;
        }


        /*
         * dopisz wszystkie inne metody, które uznasz za stosowne         
         * 
         */
        public bool ContainsPoint(Point point)
        {
            return (Distance2FromPoint(point) <= Radius * Radius + Program.epsilon);
        }

        public double Distance2FromPoint(Point point)
        {
            double dX = point.X - Center.X;
            double dY = point.Y - Center.Y;

            return dX * dX + dY * dY;
        }
    }

    static class IntersectionFinder
    {

        public static Point? FindCommonPoint(Disk[] disks)
        {
            int n = disks.Length;

            if (n == 1)
                return disks[0].Center;

            List<Point> rightmostPoints = new List<Point>();

            for (int i = 0; i < n; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    IntersectionType intersectionType = disks[i].GetIntersectionType(disks[j], out Point[] crossingPoints);
                    Point? rightmostPoint = null;

                    switch (intersectionType)
                    {
                        case IntersectionType.Disjoint:
                            return null;

                        case IntersectionType.Identical:
                            rightmostPoint = new Point(disks[i].Center.X + disks[i].Radius, disks[i].Center.Y);
                            break;

                        case IntersectionType.Contains:
                            rightmostPoint = new Point(disks[j].Center.X + disks[j].Radius, disks[j].Center.Y);
                            break;
                        case IntersectionType.IsContained:
                            rightmostPoint = new Point(disks[i].Center.X + disks[i].Radius, disks[i].Center.Y);
                            break;

                        case IntersectionType.Touches:
                            rightmostPoint = crossingPoints[0];
                            break;

                        case IntersectionType.Crosses:
                            // TODO: fix choosing the righmost point here
                            rightmostPoint = crossingPoints[0].X > crossingPoints[1].X
                                ? crossingPoints[0]
                                : crossingPoints[1];
                            break;
                    }

                    // Can be optimized to compare against the leftmost point instead of adding to the list and comparing outside this loop
                    rightmostPoints.Add(rightmostPoint.Value);
                }
            }

            Point leftmostPoint = rightmostPoints[0];
            for (int i = 1; i < rightmostPoints.Count; i++)
            {
                if (leftmostPoint.X > rightmostPoints[i].X)
                    leftmostPoint = rightmostPoints[i];
            }

            foreach (Disk disk in disks)
            {
                if (!disk.ContainsPoint(leftmostPoint))
                    return null;
            }

            return leftmostPoint;
        }

    }
}
