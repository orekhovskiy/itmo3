using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Spline
{
  public partial class Form1 : Form
  {
        //Для рисования графика
        Graphics graph;
        Font font = new Font("Arial", 8);
        Font font1 = new Font("Arial", 6);
        //начало координат
        Point X0Y0;
        int scale;

        //точки для отрисовки
        List<Point> pointsSpl;
        //точки для отрисовки эталонной функции
        List<Point> pointsTrue;
        //точки для интерполирования
        List<double> x = new List<double>();
        List<double> y = new List<double>();

        CubicSpline spl = new CubicSpline();
        //параматры отрисовки
        double x0 = 0, y0 = 0, end = 200, accur = 0.1;
              
        public Form1()
        {
          InitializeComponent();          
        }

        /**
         * Инициализация при создании формы
         * */
        private void Form1_Load(object sender, EventArgs e)
        {
            //X0Y0 = new Point(0, panel1.Height - 3);
            X0Y0 = new Point(0, panel1.Height / 2);
        }

        public void DrawGraph()
        {

          //получение контекста и установка параметров
          graph = panel1.CreateGraphics();
          Pen dashed_pen = new Pen(Brushes.DarkGray, 1);
          dashed_pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;

          // получаем масштаб
          scale = trackBar1.Value;         
          graph.FillRectangle(new SolidBrush(Color.Black), X0Y0.X, X0Y0.Y, 3, 3);

          //Отрисовка делений
          drawSplits(scale);

            //отрисовка графика
            if (checkBox1.Checked)
            try
            {
                pointsTrue = GetTrueFunc(scale);
                //отрисовка эталоного графика sin (x)
                Pen line_true = new Pen(Color.Green);
                for (int i = 0; i < pointsTrue.Count - 1; i++)
                {
                    graph.DrawLine(line_true, new Point(pointsTrue[i].X, pointsTrue[i].Y), 
                        new Point(pointsTrue[i + 1].X, pointsTrue[i + 1].Y));
                }
            }
            catch (Exception exc) { MessageBox.Show("Ошибка при отрисовке эталонной функции"); }
            
            if (checkBox2.Checked)
            try
            {
                pointsSpl = GetSplimeFunc(scale);
                //отрисовка графика по заданным точкам
                Pen line_splime = new Pen(Color.Red);
                for (int i = 0; i < pointsSpl.Count - 1; i++)
                {
                    graph.DrawLine(line_splime, new Point(pointsSpl[i].X, pointsSpl[i].Y), 
                        new Point(pointsSpl[i + 1].X, pointsSpl[i + 1].Y));
                }
            }
            catch (Exception exc) { MessageBox.Show("Что-то пошло не так при отрисовке графика.\nСкорее всего вы накосячили со вводом("); }

            if (checkBox3.Checked)
            try
            {
                //отрисовка точек
                Pen lineB = new Pen(Color.Blue);
                int radius = 4;
                for (int i = 0; i < x.Count; i++)
                {
                    graph.DrawEllipse(lineB, X0Y0.X + (float)(x[i] * 30 * scale) - radius / 2,
                         X0Y0.Y - (float)(y[i] * 10 * scale) - radius / 2, radius, radius);
                }
            }
            catch (Exception exc){ MessageBox.Show("Что-то пошло не так при отрисовке точек.\nСкорее всего вы накосячили со вводом("); }

        }

        private List<Point> GetTrueFunc(int scale)
        {
          List<Point> points = new List<Point>();

          for (double i = 0; i < end; i+=accur) {
            int X = X0Y0.X + (int)(i*30*scale);
            int Y = X0Y0.Y - (int)(Runge_Kutt.Function(i , 1) * 10 * scale);

            points.Add(new Point(X, Y));
          }

          return points;
        }

        private List<Point> GetSplimeFunc(int scale)
        {
            // Методом Рунге-Кутты находим точки            
            double[,] arr = Runge_Kutt.Runge(x0, y0, end, accur, comboBox1.SelectedIndex);
            //и заполняем ими массив для интерполирования
            int n = arr.Length / 2; //так как хранятся и X и Y
            x.Clear(); y.Clear();
            for (int i = 0; i < n; i++)
            {
                x.Add(arr[0, i]);
                y.Add(arr[1, i]);
            }

          //получаем интерполированную функцию
          spl.GetSplines(x, y, x.Count);
          List<Point> points = new List<Point>();
          //по найденной функции ищем точки для построения
          for (double i = x0; i < end; i += accur)
          {
            int X = X0Y0.X + (int)(i * 30 * scale);
            int Y = X0Y0.Y - (int)(spl.GetInterpolateY(i) * 10 * scale);

            points.Add(new Point(X, Y));
          }

          return points;
        }

        private void panel1_Resize(object sender, EventArgs e)
        {
            X0Y0 = new Point(0, panel1.Height / 2);
            panel1.Refresh();
            DrawGraph();
        }

        //расчет значения  функции
        private void button5_Click(object sender, EventArgs e)
        {
            double _x;
            double _y;
            if (! Double.TryParse(textBox1.Text,out _x))
                MessageBox.Show("Некорректный ввод");
            else
            {
                spl.GetSplines(x, y, x.Count);
                _y = spl.GetInterpolateY(_x);
                label3.Text = "Y = " + _y;

                Pen lineB = new Pen(Color.Blue);
                int radius = 4;
                    graph.DrawEllipse(lineB, X0Y0.X + (float)(_x * 30 * scale) - radius / 2,
                         X0Y0.Y - (float)(_y * 10 * scale) - radius / 2, radius, radius);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            panel1.Refresh();
            DrawGraph();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            panel1.Refresh();
            DrawGraph();
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            panel1.Refresh();
            DrawGraph();
        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            draw_graph_button_Click(null, null);
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            draw_graph_button_Click(null, null);
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            draw_graph_button_Click(null, null);
        }

        private void textBox5_Leave(object sender, EventArgs e)
        {
            draw_graph_button_Click(null, null);
        }

        /**
         * Отрисовка делений на осях
         * */
        private void drawSplits(int scale)
        {
              Pen axis_pen = new Pen(Brushes.DarkGray, 1);
              //Строим ось Х
              graph.DrawLine(axis_pen, new Point(0, X0Y0.Y), new Point(panel1.Width, X0Y0.Y));
              graph.DrawString("X", font, new SolidBrush(Color.Black), panel1.Width - 14, X0Y0.Y - 14);
              //Строим ось Y
              graph.DrawLine(axis_pen, new Point(X0Y0.X, 0), new Point(X0Y0.X, panel1.Height));
              graph.DrawString("Y", font, new SolidBrush(Color.Black), X0Y0.X + 10, 0);

              Pen line = new Pen(Color.Gray);
              int c = 0;
              for (int i = panel1.Height / 2; i > 20; i -= 10 * scale)
              {
                graph.DrawLine(line, X0Y0.X, i, X0Y0.X + 4, i);
                graph.DrawString("" + c, font1, new SolidBrush(Color.Black), X0Y0.X + 11, i - 5);
                c++;
              }
              c = 0;
              for (int i = panel1.Height / 2; i < panel1.Height; i += 10 * scale)
              {
                graph.DrawLine(line, X0Y0.X, i, X0Y0.X + 4, i);
                graph.DrawString("-" + c, font1, new SolidBrush(Color.Black), X0Y0.X + 11, i - 5);
                c++;
              }
              c = 0;
              for (int i = 0; i < panel1.Width - 20; i += 30 * scale)
              {
                graph.DrawLine(line, i, X0Y0.Y, i, X0Y0.Y - 7);
                graph.DrawString("" + c, font1, new SolidBrush(Color.Black), i , X0Y0.Y - 12);
                c++;
              }
        }

        /**
         * Изменение масштаба
         * */
        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            panel1.Refresh();
            DrawGraph();
        }

        /**
         * Входная точка
         */
        private void draw_graph_button_Click(object sender, EventArgs e)
        {
            try { x0 = Convert.ToDouble(textBox2.Text); }
                catch { MessageBox.Show("Ошибка ввода X0!"); return; }
            try { y0 = Convert.ToDouble(textBox3.Text); }
                catch { MessageBox.Show("Ошибка ввода Y0!"); return; }
            try { end = Convert.ToDouble(textBox4.Text); }
                catch { MessageBox.Show("Ошибка ввода x_end!"); return; }
            try { accur = Convert.ToDouble(textBox5.Text); }
                catch { MessageBox.Show("Ошибка ввода точности!"); return; }
                
            panel1.Refresh();
            DrawGraph();
        }
  }
} 