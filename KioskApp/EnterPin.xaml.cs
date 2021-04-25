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
using System.Drawing;
using AForge.Video.DirectShow;
using WMPLib;
using System.Web.Services;
using KioskApp.HelloWord;
using System.Security.Cryptography;
using System.IO;
using System.Diagnostics;
using System.ComponentModel;
using KioskApp.Clases;

namespace KioskApp
{
	/// <summary>
	/// Interaction logic for EnterPin.xaml
	/// </summary>
	public partial class EnterPin : Window
	{
        BackgroundWorker bcworker = new BackgroundWorker();
        struct dataparameter
        {
            public int process;
            public int delay;
        }
        private dataparameter _inputparameter;
       // private KioskAppWebServiceSoapClient fromyoutube = new KioskAppWebServiceSoapClient();
        WMPLib.WindowsMediaPlayer sound1;
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
        private void StopFile()
        {
            sound1.controls.stop();
            sound1.close();
        }

        CameraImaging camimg = new CameraImaging();
       
        public Bitmap tmp;
       
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
		int trynumber=0;
		public EnterPin()
		{
			this.InitializeComponent();
            bcworker.DoWork += Worker_DoWork;
            bcworker.RunWorkerCompleted += bcworker_RunWorkerCompleted;
			// Insert code required on object creation below this point.
        }
        string resault;
        void bcworker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            
            if (e.Error != null)
            {
                ErrorMessage em = new ErrorMessage(Clases.ErrorsMsg.ItErrorMsg_Ar, Clases.ErrorsMsg.ItErrorMsg_En);
                em.Show();
                dispatcherTimer.Stop();
                dispatcherTimer.Tick -= dispatcherTimer_Tick;
                try
                {
                    camimg.videoSource.Stop();

                }
                catch (Exception ee) { }
                try
                {
                    camimg.videoSource.Stop();

                }
                catch (Exception ee) { }
                try
                {
                    camimg.Dispose();

                }
                catch (Exception ee) { }
                this.Close();
            }
           
            else if (resault=="assine")
            {
                AssignePIN AssP = new AssignePIN();
                AssP.Show();
                StopFile();
                try
                {
                    camimg.videoSource.Stop();

                }
                catch (Exception ee) { }
                try
                {
                    camimg.Dispose();

                }
                catch (Exception ee) { }
                this.Close();
            }
            else if (resault == "ok")
            {
                ChooseService cs = new ChooseService();
                cs.Show();
                StopFile();
                try
                {
                    camimg.videoSource.Stop();

                }
                catch (Exception ee) { }
                try
                {
                    camimg.Dispose();

                }
                catch (Exception ee) { }
                this.Close();
            }
            else if (resault == "no")
            {
                ErrorMessage ol = new ErrorMessage(Clases.ErrorsMsg.HrErrorMsg_Ar, Clases.ErrorsMsg.HrErrorMsg_En);
                ol.Show();
                StopFile();
                try
                {
                    camimg.videoSource.Stop();

                }
                catch (Exception ee) { }
                try
                {
                    camimg.Dispose();

                }
                catch (Exception ee) { }
                this.Close();
                return;
            }
            else if (e.Cancelled)
            {
                ErrorMessage em = new ErrorMessage(Clases.ErrorsMsg.ItErrorMsg_Ar, Clases.ErrorsMsg.ItErrorMsg_En);
                em.Show();
                dispatcherTimer.Stop();
                dispatcherTimer.Tick -= dispatcherTimer_Tick;
                try
                {
                    camimg.videoSource.Stop();

                }
                catch (Exception ee) { }
                try
                {
                    camimg.Dispose();

                }
                catch (Exception ee) { }
                this.Close();
            }
          
         
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            int? TempFlag = null;

            string encryptedPIN="";
            try
            {
                this.Dispatcher.Invoke(() =>
                {
                    encryptedPIN = Encrypt(PIN_code.Password);
                });
            }
            catch(Exception){}
                    try
                    {
                        TempFlag = WebServiseFunctions.Authenticate_Class(_session.empId, encryptedPIN);
                    }
                    catch (Exception noserverconnect)
                    {
                        TempFlag = null;
                    }
                if (TempFlag == null)
                {
                    trynumber++;
                    if (trynumber < 4)
                    {
                        try
                        {
                            this.Dispatcher.Invoke(() =>
                           {

                               Grid_1.Visibility = Visibility.Visible;
                               PIN_code.Visibility = Visibility.Visible;
                               btnLogOut.Visibility = Visibility.Visible;
                               if (_session.language == 2)
                               {
                                   AlertText.Text = "";
                                   TextBlock_1.FontFamily = new System.Windows.Media.FontFamily("Tw Cen MT Condensed Extra Bold");
                                   TextBlock_1.FontSize = 60;
                                   TextBlock_1.Text = "The PIN is Wrong";
                               }
                               else
                               {
                                   AlertText.Text = "";
                                   TextBlock_1.Text = "الرجاء التأكد من الرمز";
                               }
                               PIN_code.Clear();
                               PIN_code.Focus();
                           });
                        }
                        catch(Exception){}
                    }
                        
                    else
                    {
                        #region image1
                        //dispatcherTimer.Stop();
                        try
                        {
                            camimg.videoSource = new VideoCaptureDevice(camimg.videoDevices[0].MonikerString);
                             //System.Windows.MessageBox.Show(camimg.videoDevices[1].Name);
                            camimg.videoSource.DesiredFrameRate = 10;
                            camimg.videoSource.DesiredFrameSize = new System.Drawing.Size(800, 600);
                            camimg.videoSource.Start();
                          
                            Thread.Sleep(1000);
                            Thread.Sleep(33);

                            //sfd.Filter = "*.Jpeg|*.Jpeg";
                            tmp = (Bitmap)camimg.bitmap.Clone();
                            camimg.bitmap.Dispose();
                            string filename = "deactivate employee- " +_session.empId.ToString()+"-" +Convert.ToString(DateTime.Now) + ".Jpeg";
                            string newfile = filename.Replace("/", "-");
                            newfile = newfile.Replace(":", " ");
                            //  System.Windows.MessageBox.Show(newfile);
                            string  imgpath = System.IO.Path.Combine(@"" + _session.serverpath, newfile);
                            using (var m = new MemoryStream())
                            {
                                tmp.Save(m, System.Drawing.Imaging.ImageFormat.Jpeg);
                                var image = System.Drawing.Image.FromStream(m);
                                try
                                {
                                    image.Save(imgpath);
                                }
                                catch(Exception rr){}
                                image.Dispose();
                                m.Dispose();
                            }
                            tmp.Dispose();
                            
                            long length = new System.IO.FileInfo(imgpath).Length;
                            // System.Windows.MessageBox.Show(Convert.ToString(length));
                            int looping = 0;
                            while (length < 900 & looping < 4)
                            {
                                looping++;
                                tmp = (Bitmap)camimg.bitmap.Clone();
                                camimg.bitmap.Dispose();
                                using (var m = new MemoryStream())
                                {
                                    tmp.Save(m, System.Drawing.Imaging.ImageFormat.Jpeg);
                                    var image = System.Drawing.Image.FromStream(m);
                                    try
                                    {
                                        image.Save(imgpath);
                                    }
                                    catch (Exception rr) { }
                                    image.Dispose();
                                    m.Dispose();
                                }
                                length = new System.IO.FileInfo(imgpath).Length;
                                tmp.Dispose();
                            }
                            _session.image1 = imgpath;
                        }
                        catch (Exception ee) { tmp.Dispose();  _session.errdescroption += "// couldnt take the enter pin photo // "; }
                        try
                        {
                            camimg.videoSource.Stop();

                        }
                        catch (Exception ee) { }
                        try
                        {
                            camimg.Dispose();

                        }
                        catch (Exception ee) { }
                        #endregion
                        try
                        {
                            int? resault = WebServiseFunctions.UpdateState_Class(_session.empId);
                            _session.empPath += "(enter wrong pin 3 times) deactivate employee // ";
                        }
                        catch (Exception noserverconnect)
                        {
                            _session.errorecode += "// UpdateState webservice not found//";

                            dispatcherTimer.Stop();
                            dispatcherTimer.Tick -= dispatcherTimer_Tick;
                            resault = "no";
                        }
                        dispatcherTimer.Stop();
                        dispatcherTimer.Tick -= dispatcherTimer_Tick;
                        resault = "no";
                    }
                }
                else
                {
#region image
                 //   MessageBox.Show(TempFlag.ToString());
                    dispatcherTimer.Stop();
                    dispatcherTimer.Tick -= dispatcherTimer_Tick;
                    try
                    {
                        camimg.videoSource = new VideoCaptureDevice(camimg.videoDevices[0].MonikerString);
                      //   System.Windows.MessageBox.Show(camimg.videoDevices[1].Name);
                        camimg.videoSource.DesiredFrameRate = 10;
                        camimg.videoSource.DesiredFrameSize = new System.Drawing.Size(800, 600);
                        camimg.videoSource.Start();
   
                        Thread.Sleep(1000);
                        Thread.Sleep(33);

                        //sfd.Filter = "*.Jpeg|*.Jpeg";
                        tmp = (Bitmap)camimg.bitmap.Clone();
                     //   camimg.videoSource.Stop();
                        camimg.bitmap.Dispose();
                        string filename = "PINEnter" + _session.empId.ToString() + "-" + Convert.ToString(DateTime.Now) + ".Jpeg";
                        string newfile = filename.Replace("/", "-");
                        newfile = newfile.Replace(":", " ");
                        //  System.Windows.MessageBox.Show(newfile);
                       // string imgpath = @"\" + _session.serverpath+ "\\" + newfile;
                        string imgpath =System.IO.Path.Combine(@"\" + _session.serverpath, newfile);
                     //   tmp.Save(imgpath);
                        
                        using (var s1 = new MemoryStream())
                        {
                            tmp.Save(s1, System.Drawing.Imaging.ImageFormat.Jpeg);
                           // tmp.Dispose();
                            var image = System.Drawing.Image.FromStream(s1);
                            try
                            {
                                image.Save(imgpath);
                            }
                            catch (Exception rr) { }
                            image.Dispose();
                            s1.Dispose();
                        }
                        tmp.Dispose();
                        long length = new System.IO.FileInfo(imgpath).Length;
                        // System.Windows.MessageBox.Show(Convert.ToString(length));
                        int looping = 0;
                        while (length < 900 & looping < 4)
                        {
                            looping++;
                            tmp = (Bitmap)camimg.bitmap.Clone();
                            camimg.bitmap.Dispose();
                            //string imgpath = System.IO.Path.Combine(@"" + _session.serverpath, newfile);
                            using (var m = new MemoryStream())
                            {
                                tmp.Save(m, System.Drawing.Imaging.ImageFormat.Jpeg);
                                var image = System.Drawing.Image.FromStream(m);
                                try
                                {
                                    image.Save(imgpath);
                                }
                                catch(Exception rr){}
                                image.Dispose();
                                m.Dispose();
                            }
                            length = new System.IO.FileInfo(imgpath).Length;
                            tmp.Dispose();
                        }
                        _session.image1 = imgpath;
                    }
                    catch (Exception ee) { tmp.Dispose(); _session.errdescroption += "// couldnt take the enter pin photo // "; }
                    try
                    {
                        camimg.videoSource.Stop();

                    }
                    catch (Exception ee) { }
                    try
                    {
                        camimg.Dispose();

                    }
                    catch (Exception ee) { }
#endregion
                    if (TempFlag == 1)
                    {
                        _session.empPath += "Enter temp PIN // ";
                        resault = "assine";
                       
                    }
                    else
                    {
                        dispatcherTimer.Stop();
                        dispatcherTimer.Tick -= dispatcherTimer_Tick;
                        _session.empPath += "Enter PIN: // ";
                        resault = "ok";
                    }
                }
            if (bcworker.CancellationPending)
            {
                e.Cancel = true;
            }
        }


		private void btn1_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			PIN_code.Password+="1";
			
		}

		private void btn2_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			PIN_code.Password+="2";
		}

		private void btn3_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			PIN_code.Password+="3";
		}

		private void btn4_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			PIN_code.Password+="4";
		}

		private void btn5_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			PIN_code.Password+="5";
		}

		private void btn6_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			PIN_code.Password+="6";
		}

		private void btn7_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			PIN_code.Password+="7";
		}

		private void btn8_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			PIN_code.Password+="8";
		}

		private void btn9_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			PIN_code.Password+="9";
		}

		private void btn0_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			PIN_code.Password+="0";
		}

		private void btnDelete_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			 try
            {
                PIN_code.Password = PIN_code.Password.Remove(PIN_code.Password.Length - 1, 1);
            }
            catch (Exception x) { };
		}

		private void btnClear_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			//PIN_code.Password="";
            PIN_code.Clear();
		}
        public string Encrypt(string pin)
        {
            string EncrptKey = "2013;[pnuLIT)WebCodeExpert";
            byte[] byKey = { };
            byte[] IV = { 18, 52, 86, 120, 144, 171, 205, 239 };
            byKey = System.Text.Encoding.UTF8.GetBytes(EncrptKey.Substring(0, 8));
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray = Encoding.UTF8.GetBytes(pin);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(byKey, IV), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            return Convert.ToBase64String(ms.ToArray());
        }

		private void btn_ok_Click(object sender, System.Windows.RoutedEventArgs e)
		 {
           
		}

		private void Window_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            string empname = _session.Empname;
           
//            string str = Environment.GetFolderPath(
//System.Environment.SpecialFolder.DesktopDirectory);
            Enter_PIN.Background = new ImageBrush(new BitmapImage(new Uri(@"D:\\backGroundImage\\HADEEDBackground.Png")));

            if (_session.language == 2)
            {
                Hello.FontFamily = new System.Windows.Media.FontFamily("Tw Cen MT Condensed Extra Bold");
                Hello.FontSize = 55;
                Hello.Text = "Hello Mr/Ms. " + empname;
                PlayFile(@"D:\\backgroundsounds\\enterpin_EN.mp3");
                TextBlock_1.FontFamily = new System.Windows.Media.FontFamily("Tw Cen MT Condensed Extra Bold");
                TextBlock_1.FontSize = 55;
                TextBlock_1.Text = "Please Enter Your PIN";
                btnDelete.FontFamily = new System.Windows.Media.FontFamily("Adobe Arabic");
                btnDelete.FontWeight = FontWeights.Bold;
                btnDelete.FontSize = 36;
                btnDelete.Content = "Delete";
                btnClear.FontFamily = new System.Windows.Media.FontFamily("Adobe Arabic");
                btnClear.FontWeight = FontWeights.Bold;
                btnClear.FontSize = 36;
                btnClear.Content = "Clear";
                btn_ok.FontFamily = new System.Windows.Media.FontFamily("Adobe Arabic");
                btn_ok.FontWeight = FontWeights.Bold;
                btn_ok.FontSize = 36;
                btn_ok.Content = "Ok";
                btnLogOut.FontFamily = new System.Windows.Media.FontFamily("Adobe Arabic");
                btnLogOut.FontWeight = FontWeights.Bold;
                btnLogOut.FontSize = 36;
                btnLogOut.Content = "Log Out";
            }
            else {
                Hello.Text = "اهلا سيد/ة  " + empname;
                PlayFile(@"D:\\backgroundsounds\\enterpin_AR.mp3"); }

			AlertText.Text="";
        	
			dispatcherTimer.Tick += dispatcherTimer_Tick;
			dispatcherTimer.Interval = new TimeSpan(0,0,30);
			dispatcherTimer.Start();
        }
		private void dispatcherTimer_Tick(object sender, EventArgs e)
		{
            dispatcherTimer.Stop();
            dispatcherTimer.Tick -= dispatcherTimer_Tick;
            if (bcworker.IsBusy)
            {
                bcworker.CancelAsync();
            }
		    StartScreen con=new StartScreen();
			con.Show();
            StopFile();
			this.Close();
        }

		private void btnLogOut_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            dispatcherTimer.Stop();
            dispatcherTimer.Tick -= dispatcherTimer_Tick;
            if (bcworker.IsBusy)
            {
                bcworker.CancelAsync();
            }
			StartScreen cl=new StartScreen();
			cl.Show();
            StopFile();
			this.Close();
		}
        string filename;
        private void PIN_code_PasswordChanged(object sender, RoutedEventArgs e)
        {
            //dispatcherTimer.Stop();
            if (PIN_code.Password.Length > 3)
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
                Grid_1.Visibility = Visibility.Hidden;
                PIN_code.Visibility = Visibility.Hidden;
                btnLogOut.Visibility = Visibility.Hidden;
                if (_session.language == 1)
                {
                    AlertText.Text = "يتم الان معالجة طلبك الرجاء الانتظار";
                    TextBlock_1.Text = "  ";
                }
                else if (_session.language == 2)
                {
                    AlertText.FontFamily = new System.Windows.Media.FontFamily("Tw Cen MT Condensed Extra Bold");
                    AlertText.FontSize = 55;
                    AlertText.Text = "we are processing your request please wait";
                    TextBlock_1.Text = " ";
                }
             //   filename = "PINEnter" + Convert.ToString(DateTime.Now) + ".Jpeg";
                /////////////////////new//////////////////////

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