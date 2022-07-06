using System;
using System.Collections.Generic;
using System.Drawing;


namespace Spline
{
  class CubicSpline
  {
    struct SplineCoefficients {
      public double a, b, c, d, x;
    }

    SplineCoefficients[] splines; // Сплайн

    public void GetSplines(List<double> x, List<double> y, int n)
    {
      splines = new SplineCoefficients[n];

      for (int i = 0; i < n; i++) {
        splines[i].x = x[i];  // x = x
        splines[i].a = y[i];  // a = f(x)
      }

      splines[0].c = 0;
      splines[n - 1].c = 0;

     // Будем искать коэффициенты с методом прогонки
      double[] alpha = new double[n - 1];
      double[] beta = new double[n - 1];
      alpha[0] = 0;
      beta[0] = 0;

      double hi, hi1, A, B, C, F;

      for (int i = 1; i < n - 1; i++) {
        hi = x[i] - x[i - 1];
        hi1 = x[i + 1] - x[i];
        A = hi;
        B = hi1;
        C = 2 * (hi + hi1);
        F = 6 * ((y[i + 1] - y[i]) / hi1 - (y[i] - y[i - 1]) / hi);
        
        alpha[i] = -B / (A * alpha[i - 1] + C);
        beta[i] = (F - A * beta[i - 1]) / (A * alpha[i - 1] + C);
      }

      // Обратный ход
      for (int i = n - 2; i > 0; i--) {
        splines[i].c = alpha[i] * splines[i + 1].c + beta[i];
      }

      // b и d
      for (int i = n - 1; i > 0; i--)
      {
        hi = x[i] - x[i-1];
        splines[i].d = (splines[i].c - splines[i - 1].c) / hi;
        splines[i].b = hi * (2.0 * splines[i].c + splines[i - 1].c) / 6.0 + (y[i] - y[i - 1]) / hi;
      }
    }

    // Значение интерполированной функции в произвольной точке
    public double GetInterpolateY(double x)
    {
      SplineCoefficients s;

      if (x <= splines[0].x) { //х ниже
        s = splines[0];
      }
      else if (x >= splines[splines.Length - 1].x) {//x выше
        s = splines[splines.Length - 1];
      }
      else  { //x внутри
        int i = 0;
        int j = splines.Length - 1;
        while (i + 1 < j) {
          int k = i + (j - i) / 2;
          if (x <= splines[k].x) {
            j = k;
          }
          else {
            i = k;
          }
        }
        s = splines[j];
      }
      double dx = x - s.x;
      return s.a + s.b*dx + s.c*dx*dx/2.0 + s.d*dx*dx*dx/6.0;
    }
  }
}
