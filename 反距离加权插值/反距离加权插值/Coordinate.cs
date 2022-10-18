using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 反距离加权插值
{
    class Coordinate
    {
        public Coordinate(string i, double j, double k, double l)
        {
            mark = i;
            X_point = j;
            Y_point = k;
            altitude = l;

        }

        private string Mark;
        private double X_coordinate;
        private double Y_coordinate;
        private double Altitude;
        private double DIS = 0.0;


        public string mark
        {
            get { return Mark;}
            set { Mark = value; }
        }

        public double X_point
        {
            set { X_coordinate = value; }
            get { return X_coordinate; }
        }

        public double Y_point
        {
            set { Y_coordinate = value; }
            get { return Y_coordinate; }
        }

        public double altitude
        {
            set { Altitude = value; }
            get { return Altitude; }
        }

        public double dis
        {
            set { DIS = value; }
            get { return DIS; }
        }

        public static double distance(Coordinate i, Coordinate j)
        {
            double dis = Math.Sqrt(Math.Pow((i.X_point - j.X_point), 2) + Math.Pow((i.Y_point - j.Y_point), 2));

            return dis;
        }

        public static List<result> distList(List<Coordinate> cd, Coordinate cn)
        {
            List<result> distList = new List<result>();
            for (int i = 0; i < cd.Count; i++)
            {
                double temp = distance(cd[i], cn);
                result rt = new result(cd[i].mark ,cd[i].altitude, temp);
                distList.Add(rt);
                
            }

            distList.Sort((x, y) => x.distance.CompareTo(y.distance));
            return distList;
        }




        public static double Get_Altu(List<result> rt, int n)
        {
            double x = 0.0;
            double y = 0.0;
            for (int i = 0; i < n; i++)
            {
                x += rt[i].Alt * (1 / rt[i].distance);
                y += (1 / rt[i].distance);
            }

            return x / y;
        }

        public static string Ptxt(List<result> rt, int n)
        {
            string p = "";
            for (int i = 0; i < n; i++)
            {
                p += rt[i].ID + " ";
            }

            return p;
        }


    }

    class result
    {
        public result(string s, double i, double j)
        {
            ID = s;
            Alt = i;
            distance = j;
        }

        public string ID;
        public double distance;
        public double Alt;
    }
}
