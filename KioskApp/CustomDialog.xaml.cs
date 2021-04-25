using AForge.Video.DirectShow;
using KioskApp.Clases;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace KioskApp
{
    /// <summary>
    /// Interaction logic for CustomDialog.xaml
    /// </summary>
    public partial class customdialog : Window
    {
        public customdialog()
        {
            this.InitializeComponent();
        }
        string currscreen = "";
        public customdialog(string currscreen1)
        {
            this.InitializeComponent();
           currscreen = currscreen1;
        }

        string ArMsg = "لا يمكنك طباعة الإيصال , هل تريد الاستمرار", EnMsg = "The receipt could not be printed,do you want to continue? ";
        public customdialog(string ArMsg1,string EnMsg1)
        {
            this.InitializeComponent();
            ArMsg = ArMsg1; EnMsg = EnMsg1;
        }
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dispatcherTimer.Interval=new TimeSpan (0,0,30);
            dispatcherTimer.Tick +=dispatcherTimer_Tick;
            dispatcherTimer.Start();

            this.Background = new ImageBrush(new BitmapImage(new Uri(@"D:\\backGroundImage\\HADEEDBackground.Png")));

            if (_session.language == 2)
            {
                TextBlock_1.Text = EnMsg;
                TextBlock_1.FontFamily = new System.Windows.Media.FontFamily("Tw Cen MT Condensed Extra Bold");
                TextBlock_1.FontSize = 50;
            }

        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            dispatcherTimer.Stop();
            dispatcherTimer.Tick -= dispatcherTimer_Tick;
            StartScreen con = new StartScreen();
            con.Show();
            //StopFile();
            this.Close();
        }
        private void btn_ok_Click(object sender, RoutedEventArgs e)
        {
            Process[] pname = Process.GetProcessesByName("kioskEngine");
            if (pname.Length == 0)
            {
                foreach (Process p in pname)
                {
                    p.Dispose();
                }
                Offline off = new Offline();
                off.Show();
                return;

            }
            _session.empPath += "choose to recieve cash without reciept  // ";
            dispatcherTimer.Stop();
            dispatcherTimer.Tick -= dispatcherTimer_Tick;
      
            _session.fullcash = Convert.ToInt32(_session.salary);
            try
            {
                _session.payRec = WebServiseFunctions.paymentrecid_Class(_session.empId, 1);
            }
            catch (Exception error)
            {
            }

            if(currscreen=="ChooseService")
            { 
                ChooseSubServ ws = new ChooseSubServ();
                ws.printerStatus = 0;
                ws.Show();
            }
            else if (currscreen == "EnterUserNamePass")
            {
                FillProcess fp = new FillProcess();
                fp.printerStatus = 0;
                fp.Show();

            }
            Thread.Sleep(100);
            this.Close();

        }

        private void btnLogOut_Click(object sender, RoutedEventArgs e)
        {
            _session.empPath = " did not receive salary because of printer failure //";
            StartScreen f = new StartScreen();
            dispatcherTimer.Stop();
            dispatcherTimer.Tick -= dispatcherTimer_Tick;
            f.Show();
            this.Close();
        }
    }
}
