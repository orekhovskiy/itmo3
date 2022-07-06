using System;

namespace Spline
{
 public static class Runge_Kutt
 {
    private static int funcNum; 
    // реализация метода Рунге - Кутта
    public static double[,] Runge(double x0, double y0, double end, double accur, int _funcNum) 
    {
      funcNum = _funcNum;
      double k1 = 0, k2 = 0, k3 = 0, k4 = 0;
      double step = accur;
      int n = (int)((end - x0) / step) + 1;
      
     
      double[,] points = new double[2, n];

      points[0, 0] = x0;
      points[1, 0] = y0;

      for (int i = 1; i < n; i++)
      {
        k1 = Function(x0, y0);
        k2 = Function(x0 + step / 2, y0 + step * k1 /2);
        k3 = Function(x0 + step / 2, y0 + step * k2 / 2);
        k4 = Function(x0 + step, y0 + step * k3);

        x0 += step;
        y0 = y0 + step * (k1 + 2*k2 + 2*k3 + k4) / 6;

        points[0, i] = x0; 
        points[1, i] = y0;
      }
      return points;
    }
    
    public static double Function(double x, double y)
    {
      switch (funcNum)
            {
                case 0: return Math.Sin(x); break;
                case 1: return Math.Cos(x); break;
                case 2: return Math.Tan(x); break;
                case 3: return x * x + 5 * x + 4; break;
                default:
                    return Math.Sin(x);
            }
      
    }
     
  }
}
