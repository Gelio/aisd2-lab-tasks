
using System;

namespace ASD
{

public static partial class Geometry
    {

    public const int ClockWise      = -1;
    public const int Collinear      =  0;
    public const int AntiClockWise  =  1;

    public static double Epsilon = 0.00000001;

    public static readonly Point NullPoint = new Point(0,0);
    public static readonly Segment NullSegment  = new Segment(NullPoint,NullPoint);

    public struct Direction
        {
        private int x;

        private Direction(double d)
            {
            x = (Math.Abs(d)<Epsilon) ? 0 : ( d>0 ? 1 : -1 ) ;
            }

        public static bool operator==(Direction a, Direction b)
            {
            return a.x==b.x;
            }

        public static bool operator!=(Direction a, Direction b)
            {
            return !(a==b);
            }

        public static int operator*(Direction a, Direction b)
            {
            return a.x*b.x;
            }

        public static implicit operator int(Direction d)
            {
            return d.x;
            }

        public static explicit operator Direction(double d)
            {
            return new Direction(d);
            }

        public override bool Equals(object obj)
            {
            return base.Equals(obj);
            }

        public override int GetHashCode()
            {
            return base.GetHashCode();
            }

        }

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
        
        // funkcja zwraca orientacje punktu p wzgledem odcinka
        public Direction Direction(Point p)
            {
            return (Direction)Point.CrossProduct(pe-ps,p-ps);
            }

        }
        
    // funkcja zwraca true jesli punkt q nalezy do
    // prostokata wyznaczonego przez punkty p1 i p2
    public static bool OnRectangle(Point p1, Point p2, Point q)
        {
        return Math.Min(p1.x,p2.x)<=q.x && q.x<=Math.Max(p1.x,p2.x) && Math.Min(p1.y,p2.y)<=q.y && q.y<=Math.Max(p1.y,p2.y) ;
        }

    public static bool Intersection(Segment s1, Segment s2)
        {
        Direction s1s_s2 = s2.Direction(s1.ps);   // polozenie poczatku odcinka s1 wzgledem odcinka s2
        Direction s1e_s2 = s2.Direction(s1.pe);   // polozenie konca    odcinka s1 wzgledem odcinka s2
        Direction s2s_s1 = s1.Direction(s2.ps);   // polozenie poczatku odcinka s2 wzgledem odcinka s1
        Direction s2e_s1 = s1.Direction(s2.pe);   // polozenie konca    odcinka s2 wzgledem odcinka s1

        int s12 = s1s_s2 * s1e_s2;   // polozenie odcinka s1 wzgledem odcinka s2
        int s21 = s2s_s1 * s2e_s1;   // polozenie odcinka s2 wzgledem odcinka s1

        // konce jednego z odcinkow leza po tej samej stronie drugiego
        if ( s12>0 || s21>0 ) return false;   // odcinki nie przecinaja sie

        // konce zadnego z odcinkow nie leza po tej samej stronie drugiego
        // i konce jednego z odcinkow leza po przeciwnych stronach drugiego
        if ( s12<0 || s21<0 ) return true;    // odcinki przecinaja sie

        if ( s1s_s2==0 && OnRectangle(s2.ps,s2.pe,s1.ps) ) return true;
        if ( s1e_s2==0 && OnRectangle(s2.ps,s2.pe,s1.pe) ) return true;
        if ( s2s_s1==0 && OnRectangle(s1.ps,s1.pe,s2.ps) ) return true;
        if ( s2e_s1==0 && OnRectangle(s1.ps,s1.pe,s2.pe) ) return true;

        return false;
        }

    // sortowanie katowe punktow z tablicy p w kierunku przeciwnym do ruchu wskazowek zegara wzgledem punktu centralnego c
    // czyli sortowanie wzgledem roznacych katow odcinka (c,p[i]) z osia x
    // przy pomocy parametru ifAngleEqual mo¿na doprwcyzowaæ kryterium sortowania gdy katy sa rowne
    // (domyslnie nic nie doprecyzowujemy, pozostawiamy rowne)
    public static Point[] AngleSort(Point c, Point[] p, System.Comparison<Point> ifAngleEqual=null)
        {
        if ( ifAngleEqual==null ) ifAngleEqual = (p1,p2)=>0 ;
        if ( p==null) throw new System.ArgumentNullException();
        if ( p.Length<2 ) return p;
        System.Comparison<Point> cmp = delegate(Point p1, Point p2)
            {
            int r = -(new Geometry.Segment(c,p1)).Direction(p2);
            return r!=0 ? r : ifAngleEqual(p1,p2);
            };
        var s1 = new System.Collections.Generic.List<Point>();
        var s2 = new System.Collections.Generic.List<Point>();
        for ( int i=0 ; i<p.Length ; ++i ) 
            if ( p[i].y>c.y || ( p[i].y==c.y && p[i].x>=c.x ) )
                s1.Add(p[i]);
            else
                s2.Add(p[i]);
        s1.Sort(cmp);
        s2.Sort(cmp);
        s1.AddRange(s2);
        return s1.ToArray();
        }

   } // static partial class Geometry

} // namespace ASD
