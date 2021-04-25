using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Threading;
using System.Windows.Threading;
using System.Windows.Media.Animation;
using DataMatrix;
using System.Runtime.InteropServices;
using System.IO.Ports;
using WMPLib;
using KioskApp.HelloWord;
using System.Diagnostics;
using System.Globalization;
using KioskApp.Clases;
using System.ComponentModel;


namespace KioskApp
{
	/// <summary>
	/// Interaction logic for ScanCard.xaml
	/// </summary>
	public partial class ScanCard : Window
	{
       // private KioskAppWebServiceSoapClient fromyoutube = new KioskAppWebServiceSoapClient();
        WMPLib.WindowsMediaPlayer sound1;
        Process[] pname1;
        private void PlayFile(String url)
        {
            sound1 = new WMPLib.WindowsMediaPlayer();
            sound1.PlayStateChange +=
                new WMPLib._WMPOCXEvents_PlayStateChangeEventHandler(Player_PlayStateChange);
            sound1.MediaError +=
                new WMPLib._WMPOCXEvents_MediaErrorEventHandler(Player_MediaError);
            sound1.URL = url;
            sound1.controls.play();
        }
        private void Player_PlayStateChange(int NewState)
        {
            if ((WMPLib.WMPPlayState)NewState == WMPLib.WMPPlayState.wmppsStopped)
            {
                sound1.close();
            }
        }

        private void Player_MediaError(object pMediaObject)
        {
            sound1.close();
        }
        private void StopFile() {
            sound1.controls.stop();
            sound1.close();
        }
        BackgroundWorker bcworker = new BackgroundWorker();
        BackgroundWorker bcworker1 = new BackgroundWorker();
        System.IO.Ports.SerialPort  myPort = new System.IO.Ports.SerialPort(_session.CamPortName, 9600, System.IO.Ports.Parity.None, 8, System.IO.Ports.StopBits.One);
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
		public ScanCard()
		{

			//System.Timers.Timer timer = new System.Timers.Timer(1000);
			this.InitializeComponent();
            bcworker.DoWork += Worker_DoWork;
            bcworker.RunWorkerCompleted += bcworker_RunWorkerCompleted;
            bcworker1.DoWork += Worker_DoWork1;
            bcworker1.RunWorkerCompleted += bcworker_RunWorkerCompleted1;
			 
        }
        Boolean resault = false;
        int startResult = 0;
        void bcworker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                bcworker.RunWorkerCompleted -= bcworker_RunWorkerCompleted;
                bcworker.Dispose();
            }
            catch(Exception){}
           
            if (e.Error != null)
            {
                ErrorMessage em = new ErrorMessage(Clases.ErrorsMsg.ItErrorMsg_Ar, Clases.ErrorsMsg.ItErrorMsg_En);
               
                em.Show();
                Thread.Sleep(300);
                try
                {
                    rh.Abort();
                }
                catch (Exception ecxe) { }
                byte[] thebytesarray11 = HexStringToByteArray("1b 30 02");
                Thread.Sleep(200);
                try
                {
                    myPort.Write(thebytesarray11, 0, thebytesarray11.Length);
                }
                catch (Exception ww) { }
                myPort.Dispose();
                myPort.Close();
                StopFile();
                dispatcherTimer.Stop();
                dispatcherTimer.Tick -= dispatcherTimer_Tick;
                //BarcodeReader.ReleaseDevice();
                BarcodeReader.SetBarcode(false);
                //if (BarcodeReader.GetDevice() == 1)
                //    BarcodeReader.ReleaseLostDevice();
                this.Close();
            }
            else if (e.Cancelled & resault)
            {
                if (empState != null && empState.Value == false)
                {
                    ErrorMessage em = new ErrorMessage(Clases.ErrorsMsg.HrErrorMsg_Ar, Clases.ErrorsMsg.HrErrorMsg_En);
                    em.Show();
                }
                else
                {
                    ErrorMessage em = new ErrorMessage(Clases.ErrorsMsg.ItErrorMsg_Ar, Clases.ErrorsMsg.ItErrorMsg_Ar);
                    em.Show();
                }
            
                Thread.Sleep(300);
                try
                {
                    rh.Abort();
                }
                catch (Exception ecxe) { }
                byte[] thebytesarray11 = HexStringToByteArray("1b 30 02");
                Thread.Sleep(200);
                try
                {
                    myPort.Write(thebytesarray11, 0, thebytesarray11.Length);
                }
                catch (Exception ww) { }
                myPort.Dispose();
                myPort.Close();
                StopFile();
               // BarcodeReader.ReleaseDevice();
                BarcodeReader.SetBarcode(false);
                //if (BarcodeReader.GetDevice() == 1)
                //    BarcodeReader.ReleaseLostDevice();
                this.Close();
            }
            if (resault == false)
            {
                _session.empPath += "Scan Card Faild // ";
                if (empState != null && empState.Value == false)
                {
                    ErrorMessage em = new ErrorMessage(Clases.ErrorsMsg.HrErrorMsg_Ar, Clases.ErrorsMsg.HrErrorMsg_En);
                    em.Show();
                }
                else
                {
                    ErrorMessage em = new ErrorMessage(Clases.ErrorsMsg.ItErrorMsg_Ar, Clases.ErrorsMsg.ItErrorMsg_Ar);
                    em.Show();
                }
            
              
               
                Thread.Sleep(300);
                try
                {
                    rh.Abort();
                }
                catch (Exception ecxe) { }
                byte[] thebytesarray11 = HexStringToByteArray("1b 30 02");
                Thread.Sleep(200);
                try
                {
                    myPort.Write(thebytesarray11, 0, thebytesarray11.Length);
                }
                catch (Exception ww) { }
                myPort.Dispose();
                myPort.Close();
                StopFile();
                dispatcherTimer.Stop();
                dispatcherTimer.Tick -= dispatcherTimer_Tick;
               // BarcodeReader.ReleaseDevice();
                BarcodeReader.SetBarcode(false);
                //if (BarcodeReader.GetDevice() == 1)
                //    BarcodeReader.ReleaseLostDevice();
                this.Close();
            }
            else
            {
                EnterPin EP = new EnterPin();
              
                EP.Show();
                _session.empPath += "Scan Card Succesfully // ";

                Thread.Sleep(400);
                try
                {
                    rh.Abort();
                }
                catch (Exception ecxe) { }
                byte[] thebytesarray121 = HexStringToByteArray("1b 30 02");
                Thread.Sleep(200);
                try
                {
                    myPort.Write(thebytesarray121, 0, thebytesarray121.Length);
                }
                catch (Exception ww) { }
                myPort.Dispose();
                myPort.Close();
                StopFile();
                Thread.Sleep(200);
                dispatcherTimer.Stop();
                dispatcherTimer.Tick -= dispatcherTimer_Tick;
                //BarcodeReader.ReleaseDevice();
                BarcodeReader.SetBarcode(false);
                //if (BarcodeReader.GetDevice() == 1)
                //    BarcodeReader.ReleaseLostDevice();
                this.Close();
                return;
            }
           
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            
                try
                {
                   // KioskAppWebServiceSoapClient fromyoutube = new KioskAppWebServiceSoapClient();
                    //empState = fromyoutube.getEmpState(_session.empId);
                    try
                    {
                        empState = WebServiseFunctions.getEmpState_Class(_session.empId);
                        _session.Empname = WebServiseFunctions.GetEmpName_Class(_session.empId, _session.language);
                    }
                    catch {
                        resault = false;
                        bcworker.CancelAsync();
                        ErrorMessage em = new ErrorMessage(Clases.ErrorsMsg.ItErrorMsg_Ar, Clases.ErrorsMsg.ItErrorMsg_En);

                        em.Show();
                        Close();
                        return;
                    
                    }
                    _session.tranactionid = _session.empId.ToString() + _session.KioskID.ToString() + DateTime.Now.ToString("dd/mm/yyyy HH:mm:ss", CultureInfo.CurrentCulture);
                    _session.tranactionid = _session.tranactionid.Replace(":", "");
                    _session.tranactionid = _session.tranactionid.Replace(" ", "");
                    _session.tranactionid = _session.tranactionid.Replace("/", "");
                  // 
                    if (_session.Empname != null && empState!=null &&  empState == true)
                    { resault = true; }
                }
                catch (Exception ee)
                {
                    resault = false;
                    bcworker.CancelAsync();
                   
                    // _session.errorecode += "// webservice not fount//";
                }
                if (bcworker.CancellationPending)
                {
                    e.Cancel = true;
                    resault = false;
                    ErrorMessage em = new ErrorMessage(Clases.ErrorsMsg.ItErrorMsg_Ar, Clases.ErrorsMsg.ItErrorMsg_En);

                    em.Show();
                    Close();
                }
                bcworker.DoWork -= Worker_DoWork;
        }
        void bcworker_RunWorkerCompleted1(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                bcworker1.RunWorkerCompleted -= bcworker_RunWorkerCompleted1;

                bcworker1.Dispose();
            }
            catch(Exception){}
            try
            {
                if(rh!=null)
                rh.Abort();
            }
            catch (Exception ecxe) { }
            byte[] thebytesarray121 = HexStringToByteArray("1b 30 02");
            Thread.Sleep(200);
            try
            {
                if (!myPort.IsOpen)
                    myPort.Open();
                myPort.Write(thebytesarray121, 0, thebytesarray121.Length);
            }
            catch (Exception ww) { }
            if (e.Error != null)
            {
                ErrorMessage em = new ErrorMessage(Clases.ErrorsMsg.ItErrorMsg_Ar, Clases.ErrorsMsg.ItErrorMsg_En);
                
                em.Show();

                dispatcherTimer.Stop();
                dispatcherTimer.Tick -= dispatcherTimer_Tick;
               // BarcodeReader.ReleaseDevice();
                BarcodeReader.SetBarcode(false);
                //if (BarcodeReader.GetDevice() == 1)
                //    BarcodeReader.ReleaseLostDevice();

                this.Close();
            }
            else if (e.Cancelled)
            {
                ErrorMessage em = new ErrorMessage(Clases.ErrorsMsg.ItErrorMsg_Ar, Clases.ErrorsMsg.ItErrorMsg_En);
               
                em.Show();
                //BarcodeReader.ReleaseDevice();
                BarcodeReader.SetBarcode(false);
                //if (BarcodeReader.GetDevice() == 1)
                //    BarcodeReader.ReleaseLostDevice();
                this.Close();
            }
            
           
        }

        private void Worker_DoWork1(object sender, DoWorkEventArgs e)
        {
            try
            {
                if(rh!=null)
                rh.Abort();
            }
            catch (Exception ecxe) { }
            if (myPort.IsOpen == false) //if not open, open the port
                try
                {
                    myPort.Open();
                }
                catch (Exception ex) { }
            byte[] thebytesarray1 = HexStringToByteArray("1b 30 02");
            Thread.Sleep(200);
            try
            {
                myPort.Write(thebytesarray1, 0, thebytesarray1.Length);
            }
            catch (Exception ww) { }

            startResult = BarcodeReader.StartDevice();

            if (startResult == 1)
            {
               
                if (myPort.IsOpen == true)
                {
                    rh = new Thread(new ThreadStart(flashing));
                    rh.IsBackground = true;
                    rh.Start();
                }
                   
                BarcodeReader.SetBeepTime(100);
                BarcodeReader.SetBarcode(true);
                long startTicks = DateTime.Now.Ticks;
                double timeElapsed = 0;
                StringBuilder barcode = new StringBuilder(1024);
                int length = 0;

                while ((timeElapsed < 25) && (length == 0))
                {
                    BarcodeReader.GetDecodeString(barcode, out length);
                    if (length > 0)
                    {
                        try
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
                        catch(Exception){}

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
                resault = false;
            }
            bcworker1.DoWork -= Worker_DoWork1;
        }

          Thread rh;
        private byte[] HexStringToByteArray(string s)
          {
              s = s.Replace(" ", "");
              byte[] buffer = new byte[s.Length / 2];
              for (int i = 0; i < s.Length; i += 2)
                  buffer[i / 2] = (byte)Convert.ToByte(s.Substring(i, 2), 16);
              return buffer;
          }

        private string ByteArrayToHexString(byte[] data)
          {
              StringBuilder sb = new StringBuilder(data.Length * 3);
              foreach (byte b in data)
                  sb.Append(Convert.ToString(b, 16).PadLeft(2, '0').PadRight(3, ' '));
              return sb.ToString().ToUpper();
          }
        private void Window_Loaded(object sender, System.Windows.RoutedEventArgs e)
          {
              //string str = Environment.GetFolderPath(
              //    System.Environment.SpecialFolder.DesktopDirectory);
              if (!bcworker1.IsBusy)
              {
                  bcworker1.WorkerSupportsCancellation = true;
                  _inputparameter.delay = 100;
                  _inputparameter.process = 1200;
                  bcworker1.RunWorkerAsync(_inputparameter);
              }
              if (_session.language == 2)
              {
                  PlayFile(@"D:\\backgroundsounds\\scancard_EN.mp3");
                  TextBlock_1.FontFamily = new FontFamily("Tw Cen MT Condensed Extra Bold");
                  TextBlock_1.FontSize = 55;
                  TextBlock_1.Text = "Please Put Your Card In Front Of The Scanner";
              }
              else
              {
                  PlayFile(@"D:\\backgroundsounds\\scancard_AR.mp3");
              }
              //pname1 = Process.GetProcessesByName("RC532");
              //if (pname1.Length == 0)

              //{
              //    Process firstProc = new Process();
              //    firstProc.StartInfo.FileName = @"532English-test\\RC532.exe";
              //    firstProc.EnableRaisingEvents = true;
              //    firstProc.StartInfo.WorkingDirectory=@"\\532English-test\\";
              //    //firstProc.StartInfo.UseShellExecute = true;
              //   // firstProc.StartInfo.Verb = "runas";
              //    firstProc.Start();
              //}
                      
            PINtext.Focus();
            Scan_Card.Background = new ImageBrush(new BitmapImage(new Uri(@"D:\\backGroundImage\\HADEEDBackground.png")));
			dispatcherTimer.Tick += dispatcherTimer_Tick;
			dispatcherTimer.Interval = new TimeSpan(0,0,30);
			dispatcherTimer.Start();
        }
        private void flashing()
       {
          // Thread.Sleep(1100);
           byte[] thebytesarray;
           for (int i = 1; i > 0; )
           {
               thebytesarray = HexStringToByteArray("1b 32 02");
               Thread.Sleep(200);
               try
               {
                   myPort.Write(thebytesarray, 0, thebytesarray.Length);
               }catch(Exception m1){}
               Thread.Sleep(50);
               thebytesarray = HexStringToByteArray("1b 32 01");
               Thread.Sleep(200);
               try
               {
                   myPort.Write(thebytesarray, 0, thebytesarray.Length);
               }
               catch (Exception m1) { }
           }
       }
		private void dispatcherTimer_Tick(object sender, EventArgs e)
		{
            dispatcherTimer.Stop();
            dispatcherTimer.Tick -= dispatcherTimer_Tick;
            //BarcodeReader.ReleaseDevice();
            ////pname1 = Process.GetProcessesByName("RC532");
            ////if (pname1.Length != 0)
            ////{
            ////    //Thread.Sleep(500);
            ////    foreach (Process worker in pname1)
            ////    {
            ////        try
            ////        {
            ////            worker.Kill();
            ////            worker.WaitForExit();
            ////            worker.Dispose();
            ////        }
            ////        catch (Exception exec)
            ////        {
            ////            //MessageBox.Show(exec.Message.ToString()); 
            ////        }
            ////    }
            //}
            if (bcworker.IsBusy)
            {
                bcworker.CancelAsync();
            }

           
             StartScreen con = new StartScreen();
            
             con.Show();
            
			
            byte[] thebytesarray111 = HexStringToByteArray("1b 30 02");
            Thread.Sleep(200);
            try
            {
                myPort.Write(thebytesarray111, 0, thebytesarray111.Length);
            }
            catch (Exception ww) { }
            myPort.Dispose();
            myPort.Close();
            StopFile();
          //  BarcodeReader.ReleaseDevice();
            BarcodeReader.SetBarcode(false);
            //if (BarcodeReader.GetDevice() == 1)
            //    BarcodeReader.ReleaseLostDevice();
			this.Close();        
        }
        bool? empState = null;
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
                //pname1 = Process.GetProcessesByName("RC532");
                //if (pname1.Length != 0)
                //{
                //   // Thread.Sleep(500);
                //    foreach (Process worker in pname1)
                //    {
                //        try
                //        {
                //            worker.Kill();
                //            worker.WaitForExit();
                //            worker.Dispose();
                //        }
                //        catch (Exception exec)
                //        {
                            
                //        }
                //    }
                //}
                Process[] pname = Process.GetProcessesByName("kioskEngine");
                if (pname.Length == 0)
                {
                    foreach (Process p in pname)
                    {
                        p.Dispose();
                    }
                    resault = false;

                }
                if (_session.language == 1)
                {
                    TextBlock_1_Copy.Text = "يتم الان معالجة طلبك الرجاء الانتظار";
                    TextBlock_1.Text = "  ";
                }
                else if (_session.language == 2)
                {
                    TextBlock_1_Copy.FontFamily = new FontFamily("Tw Cen MT Condensed Extra Bold");
                    TextBlock_1_Copy.FontSize = 55;
                    TextBlock_1_Copy.Text = "we are processing your request please wait";
                    TextBlock_1.Text = " ";
                }
                //pname1 = Process.GetProcessesByName("RC532");
                //if (pname1.Length == 0)
                //{
                //    Process firstProc = new Process();
                //    firstProc.StartInfo.FileName = @"532English-test\\RC532.exe";
                //    firstProc.EnableRaisingEvents = true;
                //    firstProc.StartInfo.WorkingDirectory = @"\\532English-test\\";
                //    //firstProc.StartInfo.UseShellExecute = true;
                //    // firstProc.StartInfo.Verb = "runas";
                //    firstProc.Start();
                //}
                //pname1 = Process.GetProcessesByName("RC532");
                //if (pname1.Length != 0)
                //{
                //    Thread.Sleep(500);
                //    foreach (Process worker in pname1)
                //    {
                //        try
                //        {
                //            worker.Kill();
                //            worker.WaitForExit();
                //            worker.Dispose();
                //        }
                //        catch (Exception exec)
                //        {

                //        }
                //    }
                //}
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
                if (!bcworker.IsBusy)
                {
                    bcworker.WorkerSupportsCancellation = true;
                    _inputparameter.delay = 100;
                    _inputparameter.process = 1200;
                    bcworker.RunWorkerAsync(_inputparameter);
                }
            }
        }
	}
}