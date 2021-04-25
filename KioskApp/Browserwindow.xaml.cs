using Ini;
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

using WpfKb;



namespace KioskApp
{
    /// <summary>
    /// Interaction logic for Browserwindow.xaml
    /// </summary>
    public partial class Browserwindow : Window
    {
        public Browserwindow()
        {
            InitializeComponent();
        }
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                IniFile ini = new IniFile(Environment.CurrentDirectory + "\\" + "kioskConfigFile.ini");

                int Timeout = Convert.ToInt32(ini.Read("timeout", "browser_timeout"));
                string URL = ini.Read("URL", "browser_URL");
                costumBrowse.Source = new Uri(URL);

                dispatcherTimer.Tick += dispatcherTimer_Tick;
                dispatcherTimer.Interval = new TimeSpan(0, 0, Timeout);
                dispatcherTimer.Start();
            }
            catch { }
        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            dispatcherTimer.Stop();
            dispatcherTimer.Tick -= dispatcherTimer_Tick;
            StartScreen con = new StartScreen();
            con.Show();
          //  StopFile();
            this.Close();
        }

        private void btnLogOut_Click(object sender, RoutedEventArgs e)
        {
            dispatcherTimer.Stop();
            dispatcherTimer.Tick -= dispatcherTimer_Tick;
            
            StartScreen cl = new StartScreen();
            cl.Show();
           // StopFile();
            this.Close();
        }

    }
}
