using System;
using System.Windows;

namespace Lab1
{
    public partial class MainWindow : Window
    {

        #region Fields

        public XMLManager XMLManager;
        private int elementsCount = 0;
        FileInputWindow FileInputWindow;

        #endregion

        #region Methods

        public MainWindow()
        {
            InitializeComponent();
            XMLManager = new XMLManager(this);
        }

        private void CreateFileWindow()
        {
            FileInputWindow = new FileInputWindow(this);
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            ResultTextBox.Text = "";
            int dimension;
            bool result = Int32.TryParse(DimensionTextBox.Text, out dimension);
            if (!result)
            {
                MessageBox.Show("Проверьте размерность!");
                return;
            }
            if (dimension > 20 || dimension < 1)
            {
                MessageBox.Show("Размерность не может превышать 20 или быть меньше 1");
                return;
            }
            double[,] matrix = new double[dimension, dimension + 1];
            matrix = MatrixParse(MatrixTextBox.Text, dimension);
            if (elementsCount != dimension * (dimension + 1))
            {
                MessageBox.Show("Введено неверное количество коэффициентов или не все из них являются числами!");
                return;
            }
            double[] unknowns = new double[dimension];
            double[] residuals = new double[dimension];

            unknowns = CountUnknowns(matrix, dimension);
            if (unknowns != null)
            {
                residuals = CountResiduals(matrix, unknowns, dimension);
                PrintOut("Неизвестные:\n" + ArrayToString(unknowns, dimension));
                PrintOut("Невязки:\n" + ArrayToString(residuals, dimension));
            }
            else
            {
                MessageBox.Show("Существует бесконечное множество решений");
            }
            elementsCount = 0;
        }

        private double[,] MatrixParse(string matrixInput, int dimension)
        {
            ClearSpaces( ref matrixInput);
            double[,] matrix = new double[dimension, dimension + 1];
            int spaceIndex;
            double matrixElement;
            bool resultOfParsing;
            matrixInput += ' ';
            while (matrixInput.Length != 0)
            {
                spaceIndex = matrixInput.IndexOf(' ');
                resultOfParsing = Double.TryParse(matrixInput.Substring(0, spaceIndex), out matrixElement);
                if (resultOfParsing)
                {
                    elementsCount++;
                    if (elementsCount > dimension * (dimension + 1))
                    {
                        return matrix;
                    }
                    if (elementsCount % (dimension + 1) == 0)
                    {
                        matrix[elementsCount / (dimension + 1) - 1, dimension] = matrixElement;
                    }
                    else
                    {
                        matrix[elementsCount / (dimension + 1), elementsCount % (dimension + 1) - 1] = matrixElement;
                    }
                }
                else
                {
                    return matrix;
                }
                matrixInput = matrixInput.Substring(spaceIndex + 1, matrixInput.Length - spaceIndex - 1);
            }
            return matrix;
        }

        private double[] CountUnknowns(double[,] matrix, int dimension)
        {
            double[] result = new double[dimension];
            double ratio, temp;
            double determinate = 1;
            //<Прямой ход>
            for (int i = 0; i < dimension - 1; i++)
            {
                if (matrix[i, i] == 0)
                {
                    for (int k = i + 1; k < dimension; k++)
                        if (matrix[k, i] != 0)
                        {
                            for (int j = 0; j <= dimension; j++)
                            {
                                temp = matrix[i, j];
                                matrix[i, j] = matrix[k, j];
                                matrix[k, j] = temp;
                            }
                            determinate *= Math.Pow(-1, 2 * (k - i) - 1);
                            break;
                        }
                    if (matrix[i, i] == 0) return null;
                }
                for (int k = i + 1; k < dimension; k++)
                {
                    ratio = matrix[k, i] / matrix[i, i];
                    for (int j = 0; j <= dimension; j++)
                    {
                        
                        matrix[k, j] = matrix[k, j] - ratio * matrix[i, j];
                    }
                }
            }
            //Вывод матрицы в треугольном виде
            string triangularMatrix = "";
            for (int i = 0; i < dimension; i++)
            {
                if (matrix[i, i] == 0)
                    return null;
                for (int j = 0; j <= dimension; j++)
                {
                    triangularMatrix += matrix[i, j].ToString() + " ";
                }
                triangularMatrix += "\n";
            }
            PrintOut("Матрица в треугольном виде:\n" + triangularMatrix);
            //Подсчет неизвестных
            for (int i = dimension - 1; i >= 0; i--)
            {
                double unknownValue = matrix[i, dimension];
                determinate *= matrix[i, i];
                for (int j = i + 1; j < dimension; j++)
                    unknownValue -= matrix[i, j] * result[j];
                unknownValue /= matrix[i, i];
                result[i] = unknownValue;
            }
            PrintOut("Определитель: " + determinate + "\n");
            return result;
        }

        private double[] CountResiduals(double[,] matrix, double[] unknowns, int dimension)
        {
            double[] result = new double[dimension];
            for (int i = 0; i < dimension; i++)
            {
                double rowSum = 0;
                for (int j = 0; j < dimension; j++)
                {
                    rowSum += matrix[i, j] * unknowns[j];

                }
                result[i] = Math.Abs(rowSum - matrix[i, dimension]);
            }
            return result;
        }

        private string ArrayToString(double[] values, int dimension)
        {
            string result="";
            for (int i = 0;i<dimension;i++)
            {
                result +=values[i].ToString() + '\n';
            }
            return result;
        }

        private void PrintOut(string text)
        {
            elementsCount = 0;
            ResultTextBox.Text += text;
        }

        void ClearSpaces(ref string matrixInput)
        {
            matrixInput = matrixInput.Trim();
            matrixInput = matrixInput.Replace("\r\n", " ");
            matrixInput = matrixInput.Replace("\n", " ");
            for (int i = 0; i < matrixInput.Length - 1; i++)
            {
                int spaceCount = 0;
                while (matrixInput[i + spaceCount] == ' ')
                {
                    spaceCount++;
                }
                if (spaceCount > 0)
                {
                    matrixInput = matrixInput.Remove(i, spaceCount - 1);
                }
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            DimensionTextBox.Text = "";
            MatrixTextBox.Text = "";
            ResultTextBox.Text = "";
            elementsCount = 0;
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            CreateFileWindow();
            FileInputWindow.ShowDialog();
        }

        private void RandomizeButton_Click(object sender, RoutedEventArgs e)
        {
            int dimension;
            bool result = Int32.TryParse(DimensionTextBox.Text, out dimension);
            if (!result)
            {
                MessageBox.Show("Проверьте размерность!");
                return;
            }
            if (dimension > 20 || dimension < 1)
            {
                MessageBox.Show("Размерность не может превышать 20 или быть меньше 1");
                return;
            }
            string matrix = "";
            Random value = new Random();
            for (int i = 0; i < dimension; i++)
            {
                for (int j = 0; j <= dimension; j++)
                {
                    
                    matrix += (value.Next(-50,50) * value.NextDouble()).ToString() + ' ';
                }
                matrix += "\n";

                MatrixTextBox.Text = matrix;
            }
        }

        #endregion

    }
}
