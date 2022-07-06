using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Lab1
{
    /// <summary>
    /// Логика взаимодействия для FileInputWindow.xaml
    /// </summary>
    public partial class FileInputWindow : Window
    {

        #region Fields

        public MainWindow MainWindow;

        #endregion

        #region Methods

        public FileInputWindow(MainWindow MainWindowInstanse)
        {
            MainWindow = MainWindowInstanse;
            InitializeComponent();
        }

        private void LoadFileButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MainWindow.XMLManager.LoadDocument(FileInputTextBox.Text);
                Close();
            }
            catch
            {
                MessageBox.Show("Файл не найден!");
            }
        }

        #endregion

    }
}
