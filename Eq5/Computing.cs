using System;
using System.Collections.Generic;

namespace Eq5
{
    class Computing
    {
        public List<double> A;
        public static byte Power;
        private static int _f;
        private static double[,] _intervals = new double[5, 2];
        public static double[] Coefficients;
        public static List<string> Roots = new List<string>();

        public Computing(List<double> A, byte power)
        {
            this.A = A;
            Power = power;
        }

        public static void GetSolveSecondPowEq()
        {
            double D;
            Roots.Clear();
            var a = Coefficients[2];
            var b = Coefficients[1];
            var c = Coefficients[0];
            D = b * b - 4 * a * c;
            if (D<0)
            {
                var x1 = (-b / (2 * a)).ToString() +" + "+ (Math.Sqrt(Math.Abs(D))/(2*a)).ToString() + "i";
                var x2 = (-b / (2 * a)).ToString() +" - "+ (Math.Sqrt(Math.Abs(D)) / (2 * a)).ToString() + "i";
                Roots.Add(x1);
                Roots.Add(x2);
            }
            else
            { 
                var x1 = ((-b + Math.Sqrt(D)) / (2 * a)).ToString();
                var x2 = ((-b - Math.Sqrt(D)) / (2 * a)).ToString();
                Roots.Add(x1);
                Roots.Add(x2);
            }
        }


        public static void GetSolveThirdPowEq()
        {
            Roots.Clear();
            var a = Coefficients[2]/Coefficients[3];
            var b = Coefficients[1]/Coefficients[3];
            var c = Coefficients[0]/Coefficients[3];
            var Q = (a*a-3*b)/9;
            var R = (2*a*a*a-9*a*b+27*c)/54;
            if (R*R<Q*Q*Q)
            {
                var t = Math.Acos(R / Math.Sqrt(Math.Pow(Q, 3))) / 3;
                var x1 = -2 * Math.Sqrt(Q) * Math.Cos(t) - a / 3;
                var x2 = -2 * Math.Sqrt(Q) * Math.Cos(t + (2 * Math.PI / 3)) - a / 3;
                var x3 = -2 * Math.Sqrt(Q) * Math.Cos(t - (2 * Math.PI / 3)) - a / 3;
                Roots.Add(x1.ToString());
                Roots.Add(x2.ToString());
                Roots.Add(x3.ToString());
            }

            if (Math.Pow(R,2)>=Math.Pow(Q,3))
            {
                var A = -1 * Math.Sign(R) * Math.Pow(Math.Abs(R) + Math.Sqrt(Math.Pow(R, 2) - Math.Pow(Q, 3)),1.0/3);
                var B = 0.0;
                if (A != 0)
                    B = Q / A;
                var x1 = (A + B) - a / 3;
                var x2 = -(A + B) / 2 - a / 3 + (Math.Sqrt(3) * (A - B) / 2).ToString() + "i";
                if (A == B)
                    x2 = (- A - a / 3 ).ToString();
                var x3 = x2;
                Roots.Add(x1.ToString());
                Roots.Add(x2);
                Roots.Add(x3);
            }
        }

        public static void GetSolveFourthPowEq()
        {
            
        }

        public static double[,] GetIntervals(double a, double b) 
        {

            _f = 0;
            double begin, end;
            while (a <= b)
            {
                begin = a;
                end = begin + 0.1;
                if (F(begin) * F(end) <= 0)
                {
                    _intervals[_f, 0] = Math.Floor(begin);
                    if (begin * end < 0)
                        _intervals[_f, 0] = Math.Ceiling(begin);
                    _intervals[_f, 1] = Math.Ceiling(end);
                    _f++;
                }
                a+=0.1;
            }
            return _intervals;
        }
        
        private static double F(double X)
        {
            double result = 0;
            var power = 0;
            foreach (var coef in Coefficients)
            {
                result += coef * Math.Pow(X, power);
                power++;
            }
            return result;

        }

        #region Метод половинного деления
        public static void HalfDivision(double eps)
        {
            Roots.Clear();
            for (int i = 0; i < _f; i++)
            {
                var a = _intervals[i,0];
                var b = _intervals[i,1];
                double c;
                for (;;)
                {
                    var fa = F(a);
                    c = (a + b) / 2;
                    var fc = F(c);
                    if (Math.Abs(fc) > eps || Math.Abs(b - a) >= eps)
                        if (fa * fc > 0)
                            a = c;
                        else
                            b = c;
                    else
                    {
                        Roots.Add(Math.Round(c,7).ToString());
                        break;
                    }
                }
            }
        }
        #endregion
        public double FxP(double x)
        {
            switch (Power)
            {
                case 1: return A[1] * x + A[0];
                case 2: return A[2] * Math.Pow(x, 2) + A[1] * x + A[0];
                case 3: return A[3] * Math.Pow(x, 3) + A[2] * Math.Pow(x, 2) + A[1] * x + A[0];
                case 4: return A[4] * Math.Pow(x, 4) + A[3] * Math.Pow(x, 3) + A[2] * Math.Pow(x, 2) + A[1] * x + A[1];
                case 5: return A[5] * Math.Pow(x, 5) + A[4] * Math.Pow(x, 4) + A[3] * Math.Pow(x, 3) + A[2] * Math.Pow(x, 2) + A[1] * x + A[0];
                default: return 0;
            }
        }
    }
}
