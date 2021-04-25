using KioskApp.Clases;
using KioskApp.HelloWord;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
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
    /// Interaction logic for passfillcard.xaml
    /// </summary>
    public partial class passfillcard : Window
    {
        //[DllImport("dll_camera.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "StartDevice")]
        //public static extern int StartDevice();

        //[DllImport("dll_camera.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SetLed")]
        //public static extern void SetLed(bool bctrLed);
       // KioskAppWebServiceSoapClient fromyoutube = new KioskAppWebServiceSoapClient();
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        public passfillcard()
        {
            InitializeComponent();
            bcworker.DoWork += Worker_DoWork;
            bcworker.RunWorkerCompleted += bcworker_RunWorkerCompleted;
        }
        Boolean resault = false;
        void bcworker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                ErrorMessage em = new ErrorMessage(Clases.ErrorsMsg.ItErrorMsg_Ar, Clases.ErrorsMsg.ItErrorMsg_En);
                bcworker.Dispose();
                em.Show();
               
                dispatcherTimer.Stop();
                dispatcherTimer.Tick -= dispatcherTimer_Tick;
               // BarcodeReader.ReleaseDevice();
                BarcodeReader.SetBarcode(false);
                this.Close();
            }
            else if (e.Cancelled)
            {
               // ErrorMessage em = new ErrorMessage();
               // em.Show();
               //// BarcodeReader.ReleaseDevice();
                try
                {
                    if (BarcodeReader.GetDevice() == 1)
                        BarcodeReader.ReleaseLostDevice();
                }
                catch (Exception) { }
                BarcodeReader.SetBarcode(false);
                bcworker.Dispose();
                bcworker.DoWork -= Worker_DoWork;
                bcworker.RunWorkerCompleted -= bcworker_RunWorkerCompleted;
                this.Close();
            }

            BarcodeReader.SetBarcode(false);
           // BarcodeReader.ReleaseDevice();
          
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            //if (BarcodeReader.GetDevice() == 1)
            //    BarcodeReader.ReleaseLostDevice();
            startResult = BarcodeReader.StartDevice();

            if (startResult == 1)
            {

                BarcodeReader.SetBeepTime(100);
                BarcodeReader.SetBarcode(true);
                long startTicks = DateTime.Now.Ticks;
                double timeElapsed = 0;
                StringBuilder barcode = new StringBuilder(1024);
                int length = 0;

                while ((timeElapsed < 25) && (length == 0) && (!bcworker.CancellationPending))
                {
                    BarcodeReader.GetDecodeString(barcode, out length);
                    if (length > 0)
                    {
                        this.Dispatcher.Invoke(() =>
                        {
                            PINtext.Text += barcode.ToString();
                        });
                        BarcodeReader.SetBarcode(false);
                        BarcodeReader.ReleaseDevice();
                        if (BarcodeReader.GetDevice() == 1)
                            BarcodeReader.ReleaseLostDevice();

                    }
                    timeElapsed = (DateTime.Now.Ticks - startTicks) / 10000000;
                }
                if (length <= 0)
                {
                    BarcodeReader.SetBarcode(false);
                    BarcodeReader.ReleaseDevice();
                    if (BarcodeReader.GetDevice() == 1)
                        BarcodeReader.ReleaseLostDevice();
                }
            }
            
           
            if (bcworker.CancellationPending)
            {
                e.Cancel = true;
             //   resault = false;
            }
        }


        private void btnLogOut_Click(object sender, RoutedEventArgs e)
        {
            dispatcherTimer.Stop();
            dispatcherTimer.Tick -= dispatcherTimer_Tick;
           // BarcodeReader.ReleaseDevice();
            if (bcworker.IsBusy)
            {
                bcworker.CancelAsync();
                //try
                //{
                //    if (BarcodeReader.GetDevice() == 1)

                //        BarcodeReader.ReleaseLostDevice();
                //}
                //catch (Exception) { }
                BarcodeReader.SetBarcode(false);
                
            }
            //pname = Process.GetProcessesByName("RC532");
            //if (pname.Length != 0)
            //{
            //    //Thread.Sleep(500);
            //    foreach (Process worker in pname)
            //    {
            //        worker.Kill();
            //        worker.WaitForExit();
            //        worker.Dispose();
            //    }
            //}
            StartScreen cl = new StartScreen();
            bcworker.Dispose();
            cl.Show();
            BarcodeReader.SetBarcode(false);
            this.Close();


        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            dispatcherTimer.Stop();
            dispatcherTimer.Tick -= dispatcherTimer_Tick;
            if (bcworker.IsBusy)
            {
                bcworker.CancelAsync();
            }
           // BarcodeReader.ReleaseDevice();
            BarcodeReader.SetBarcode(false);
            StartScreen con = new StartScreen();
            bcworker.Dispose();
            con.Show();
            ////pname = Process.GetProcessesByName("RC532");
            ////if (pname.Length != 0)
            ////{
            ////    //Thread.Sleep(500);
            ////    foreach (Process worker in pname)
            ////    {
            ////        worker.Kill();
            ////        worker.WaitForExit();
            ////        worker.Dispose();
            ////    }
            ////}
           
            this.Close();
        }
        
        BackgroundWorker bcworker = new BackgroundWorker();
     //   Process[] pname;
        int startResult = 0;
        private void Window_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            //string str = Environment.GetFolderPath(
            //System.Environment.SpecialFolder.DesktopDirectory);
            Pass_FillCard.Background = new ImageBrush(new BitmapImage(new Uri(@"D:\\backGroundImage\\HADEEDBackground.png")));
            // pp = Process.Start("osk");
            // pname = Process.GetProcessesByName("RC532");
            //if (pname.Length == 0)
            //{
            //    Process firstProc = new Process();
            //    firstProc.StartInfo.FileName = @"532English-test\\RC532.exe";
            //    firstProc.EnableRaisingEvents = true;
            //    firstProc.StartInfo.WorkingDirectory = @"\\532English-test\\";
            //    firstProc.Start();
            //}
            if (!bcworker.IsBusy)
            {
                bcworker.WorkerSupportsCancellation = true;
                _inputparameter.delay = 100;
                _inputparameter.process = 1200;
                bcworker.RunWorkerAsync(_inputparameter);
            }

            PINtext.Focus();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 30);
            dispatcherTimer.Start();
        }
           
        struct dataparameter
        {
            public int process;
            public int delay;
        }

        private dataparameter _inputparameter;
        private void PINtext_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (PINtext.Text.Length > 9)
            {
                dispatcherTimer.Stop();
                dispatcherTimer.Tick -= dispatcherTimer_Tick;
           
                Process[] pname1 = Process.GetProcessesByName("kioskEngine");
                if (pname1.Length == 0)
                {
                    foreach (Process p in pname1)
                    {
                        p.Dispose();
                    }
                    resault = false;

                }
               
                    TextBlock_1_Copy.Text = "يتم الان معالجة طلبك الرجاء الانتظار";

               
                if ((PINtext.Text.Remove(2) == "HE" && PINtext.Text.Substring(6) == "ATMS" && Convert.ToInt32(PINtext.Text.Substring(2, 4).Length) == 4))
                {
                    try
                    {
                        _session.empId = Convert.ToInt32(PINtext.Text.Substring(2, 4));
                    }
                    catch (Exception err)
                    {
                        resault = false;
                    }
                }
                else
                {
                    resault = false;

                }
                try
                {
                    //  KioskAppWebServiceSoapClient fromyoutube = new KioskAppWebServiceSoapClient();
                    //empState = fromyoutube.getEmpState(_session.empId);

                    //  _session.empId = Convert.ToInt32(PINtext.Text.Substring(2, 4));
                    _session.userid = _session.empId;
                    _session.tranactionid = _session.empId.ToString() + _session.KioskID.ToString() + DateTime.Now.ToString("dd/mm/yyyy HH:mm", CultureInfo.CurrentCulture);
                    _session.tranactionid = _session.tranactionid.Replace(":", "");
                    _session.tranactionid = _session.tranactionid.Replace(" ", "");
                    _session.tranactionid = _session.tranactionid.Replace("/", "");
                    // 
                    if (_session.userid != 0)
                    { resault = true; }
                }
                catch (Exception ee)
                {
                    resault = false;
                    //bcworker.CancelAsync();
                    // _session.errorecode += "// webservice not fount//";
                }
                if (resault == false)
                {
                    _session.empPath += " scan fill Card Faild // ";
                    ErrorMessage em = new ErrorMessage(Clases.ErrorsMsg.HrErrorMsg_Ar, Clases.ErrorsMsg.HrErrorMsg_En);
                    bcworker.Dispose();
                    em.Show();
                   // BarcodeReader.ReleaseDevice();
                    BarcodeReader.SetBarcode(false);
                    this.Close();
                }
                else
                {
                    try
                    {
                        _session.cs1type = WebServiseFunctions.get_cassete_types_Class(Convert.ToInt32(_session.KioskID), out _session.cs2type, out _session.cs3type, out _session.cs4type);
                    }
                    catch (Exception) { }
                    EnterUserNamePass EP = new EnterUserNamePass();
                    bcworker.Dispose();
                    EP.Show();
                    _session.empPath += "scan fill Card Succesfully // ";
                    //MessageBox.Show(Convert.ToString(_session.empId));
                   // BarcodeReader.ReleaseDevice();
                    BarcodeReader.SetBarcode(false);
                    this.Close();
                    //return;
                }
                //if (!bcworker.IsBusy)
                //{
                //    bcworker.WorkerSupportsCancellation = true;
                //    _inputparameter.delay = 100;
                //    _inputparameter.process = 1200;
                //    bcworker.RunWorkerAsync(_inputparameter);
                //}
            }
        }

    }
}
