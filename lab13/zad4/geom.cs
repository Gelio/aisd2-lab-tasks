
using System;

namespace ASD
{

public static class Geometry
    {

    public static double Epsilon = 0.00000001;

    public struct Point
        {
        public double x;
        public double y;

        public Point(double px, double py) { x=px; y=py; }

        public static Point operator+(Point p1, Point p2) { return new Point(p1.x+p2.x,p1.y+p2.y); }

        public static Point operator-(Point p1, Point p2) { return new Point(p1.x-p2.x,p1.y-p2.y); }

        public static bool operator==(Point p1, Point p2) { return p1.x==p2.x && p1.y==p2.y; }

        public static bool operator!=(Point p1, Point p2) { return !(p1==p2); }

        public double this[int i]
            {
            get {
                switch ( i )
                    {
                    case 0:
                        return x;
                    case 1:
                        return y;
                    default:
                        throw new IndexOutOfRangeException();
                    }
                }
            set {
                switch ( i )
                    {
                    case 0:
                        x=value;
                        return;
                    case 1:
                        y=value;
                        return;
                    default:
                        throw new IndexOutOfRangeException();
                    }
                }
            }

        public override bool Equals(object obj)
            {
            return base.Equals(obj);
            }

        public override int GetHashCode()
            {
            return base.GetHashCode();
            }

        public override string ToString()
            {
            return String.Format("({0},{1})", x, y);
            }

        public static double CrossProduct(Point p1, Point p2) { return p1.x*p2.y-p2.x*p1.y; }

        public static double DotProduct(Point p1, Point p2) { return p1.x*p2.x+p1.y*p2.y; }

        public static double Distance(Point p1, Point p2)
            {
            double dx,dy;
            dx=p1.x-p2.x;
            dy=p1.y-p2.y;
            return Math.Sqrt(dx*dx+dy*dy);
            }

        }

    public struct Segment
        {

        public Point ps;  // poczatek odcinka
        public Point pe;  // koniec odcinka
    
        public Segment(Point pps,Point ppe) { ps=pps; pe=ppe; }

        public Point this[int i]
            {
            get {
                switch ( i )
                    {
                    case 0:
                        return ps;
                    case 1:
                        return pe;
                    default:
                        throw new IndexOutOfRangeException();
                    }
                }
            set {
                switch ( i )
                    {
                    case 0:
                        ps=value;
                        return;
                    case 1:
                        pe=value;
                        return;
                    default:
                        throw new IndexOutOfRangeException();
                    }
                }
            }
        }

    public struct Rectangle
        {
        public double MinX;
        public double MaxX;
        public double MinY;
        public double MaxY;

        public Rectangle(double minX, double maxX, double minY, double maxY)
            {
            MinX = minX;
            MaxX = maxX;
            MinY = minY;
            MaxY = maxY;
            }

        }

   } // static partial class Geometry

} // namespace ASD
