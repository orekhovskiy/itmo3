using System.Windows;
using System.Xml;

namespace Lab1
{
    public class XMLManager
    {

        #region Fields

        public MainWindow MainWindow;

        private XmlDocument XDoc;
        private XmlElement XRoot;
        private XmlNode Xdimension, Xcoefficients;
        string coefficients;

        #endregion

        #region Methods

        public XMLManager(MainWindow MainWindowInstance)
        {
            MainWindow = MainWindowInstance;
            XDoc = new XmlDocument();
        }

        public void LoadDocument(string documentName)
        {
            string pathToDocument = "../../saves/" + documentName;
            XDoc.Load(pathToDocument);
            ExtractData();
            SendData();
        }

        private void ExtractData()
        {
            coefficients = "";
            XRoot = XDoc.DocumentElement;
            Xdimension = XRoot.SelectSingleNode("dimension");
            Xcoefficients = XRoot.SelectSingleNode("coefficients");
            foreach (XmlNode coefficient in Xcoefficients)
            {
                coefficients += coefficient.InnerText + '\n';
            }
        }

        private void SendData()
        {
            MainWindow.DimensionTextBox.Text = Xdimension.InnerText;
            MainWindow.MatrixTextBox.Text = coefficients;
        }

        #endregion

    }
}
