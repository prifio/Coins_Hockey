using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coins_hockey
{
    class geom
    {
        public static Point RandomAngel(double angl1, double angl2)
        {
            while (angl2 < angl1)
                angl2 += Math.PI * 2;
            Random rand = new Random();
            int left = (int)(angl1 * 1000);
            int right = (int)(angl2 * 1000);
            int res = rand.Next(left, right);
            double angl = (double)res / 1000;
            return new Point(Math.Cos(angl), Math.Sin(angl));
        }
        public static Point Rotate(Point p1, Point p2)
        {
            return new Point(p1.x * p2.x - p1.y * p2.y, p1.x * p2.y + p1.y + p2.x);
        }
    }

    class Point
    {
        public double x { get; set; }
        public double y { get; set; }
        public Point()
        {
            x = y = 0;
        }
        public Point(double setx, double sety)
        {
            x = setx;
            y = sety;
        }
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
