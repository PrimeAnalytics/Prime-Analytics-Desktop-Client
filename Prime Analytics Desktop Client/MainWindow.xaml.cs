using CefSharp;
using CefSharp.Wpf;
using MahApps.Metro.Controls;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Prime_Analytics_Desktop_Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        ChromiumWebBrowser m_chromeBrowser = null;
        JavascriptObject m_object = null;

        public static string GetAppLocation()
        {
            return AppDomain.CurrentDomain.BaseDirectory;
        }
        public MainWindow()
        {
            
            InitializeComponent();

            Cef.Initialize();
            m_chromeBrowser = new ChromiumWebBrowser();
            
            m_object = new JavascriptObject();
            //m_object.SetChromeBrowser(m_chromeBrowser);
            // Register the JavaScriptInteractionObj class with JS
            m_chromeBrowser.RegisterJsObject("DesktopClient", m_object);

            m_chromeBrowser.Address="http:/admin.primeanalytics.io";

            mainGrid.Children.Add(m_chromeBrowser);


        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Cef.Shutdown();
        }

    }
}
