using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coins_hockey
{
    class geom
    {
        
    }

    class Point
    {
        public double x, y;
    }

    class Line
    {
        public double a, b, c;
        public static Line get_Line_two_point(double x1, double y1, double x2, double y2)
        {
            var an = new Line();
            an.a = y1 - y2;
            an.b = x2 - x1;
            an.c = 0 - x1 * an.a - y1 * an.b;
            return an;
        }
        public static Point peres(Line a, Line b)
        {
            var an = new Point();
            if (a.a * b.b - a.b * b.a == 0) return an;
            an.x = (a.c * b.b - b.c * a.b) / (-a.a * b.b + a.b * b.a);
            an.y = (a.c * b.a - b.c * a.a) / (a.a * b.b - a.b * b.a);
            return an;
        }
        public static Line per(Point p,Line l)
        {
            var an = new Line();
            an.a = -l.b;
            an.b = l.a;
            an.c = 0 - p.x * an.a - p.y * an.b;
            return an;
        }
    }
}
