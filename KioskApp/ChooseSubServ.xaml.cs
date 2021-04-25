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
//using MohammadDayyanCalendar;
using System.Drawing;
using AForge.Video.DirectShow;
using WMPLib;
using System.IO;
using System.Diagnostics;
using KioskApp.HelloWord;
using KioskApp.Clases;

namespace KioskApp
{
	/// <summary>
	/// Interaction logic for ChooseSubServ.xaml
	/// </summary>
	public partial class ChooseSubServ : Window
	{
        //private KioskAppWebServiceSoapClient fromyoutube = new KioskAppWebServiceSoapClient();
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

        public static int servicetype = 0;
        public int printerStatus = 1;

        CameraImaging camimg = new CameraImaging();
        //Thread thrVideo;
        public Bitmap tmp;
       

        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
	
		public ChooseSubServ()
		{
			this.InitializeComponent();
			
        }


		private void Salarybtn_Click(object sender, System.Windows.RoutedEventArgs e)
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
            _session.empPath += "Salarybtn_Click // ";
            dispatcherTimer.Stop();
            dispatcherTimer.Tick -= dispatcherTimer_Tick;
            servicetype = 1;
            try { 
            camimg.videoSource = new VideoCaptureDevice(camimg.videoDevices[0].MonikerString);
            // System.Windows.MessageBox.Show(camimg.videoDevices[1].Name);
            camimg.videoSource.DesiredFrameRate = 10;
            camimg.videoSource.DesiredFrameSize = new System.Drawing.Size(800, 600);
            camimg.videoSource.Start();
            // VideoRecording();
           
            Thread.Sleep(1000);
            Thread.Sleep(33);

            //sfd.Filter = "*.Jpeg|*.Jpeg";
            tmp = (Bitmap)camimg.bitmap.Clone();
            camimg.bitmap.Dispose();
            string filename = "SubService" + _session.empId.ToString() + "-" + Convert.ToString(DateTime.Now) + ".Jpeg";
            string newfile = filename.Replace("/", "-");
            newfile = newfile.Replace(":", " ");
            //  System.Windows.MessageBox.Show(newfile);
            string imgpath = System.IO.Path.Combine(@"\" + _session.serverpath, newfile);
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
            long length = new System.IO.FileInfo(imgpath).Length;
            tmp.Dispose();
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

                _session.image2 = imgpath;

            }
            _session.image2 = imgpath;
            }
            catch (Exception ww) { tmp.Dispose(); _session.errdescroption += "// couldnt take the 'choose sub service' photo // "; }
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
            _session.fullcash = Convert.ToInt32(_session.salary);
            try
            {
                _session.payRec = WebServiseFunctions.paymentrecid_Class(_session.empId, 1);
            }
            catch (Exception error)
            {
            }


            DispinsingCash ws = new DispinsingCash();
            ws.printerStatus = this.printerStatus;
           
            try
            {
                camimg.videoSource.Stop();
            }
            catch (Exception ee) { }
            StopFile();
            ws.Show();
            Thread.Sleep(100);
            this.Close();
		}

		private void Otherbtn_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            Process[] pname = Process.GetProcessesByName("kioskEngine");
            if (pname.Length == 0)
            {
                //foreach (Process p in pname)
                //{
                //    p.Dispose();
                //}
                Offline off = new Offline();
                off.Show();
                return;

            }
            _session.empPath += "other  // ";//vist log
            dispatcherTimer.Stop();//The Interface Timer to return back to main interface
            dispatcherTimer.Tick -= dispatcherTimer_Tick;//remove event handler
            servicetype = 2;
            try
            {
                // for capturing image by camera
                camimg.videoSource = new VideoCaptureDevice(camimg.videoDevices[0].MonikerString);
                // System.Windows.MessageBox.Show(camimg.videoDevices[1].Name);
                camimg.videoSource.DesiredFrameRate = 10;
                camimg.videoSource.DesiredFrameSize = new System.Drawing.Size(800, 600);
                camimg.videoSource.Start();// start camera
                
                // VideoRecording();
              
                Thread.Sleep(1000);
                Thread.Sleep(33);

                //sfd.Filter = "*.Jpeg|*.Jpeg";
                tmp = (Bitmap)camimg.bitmap.Clone();
                camimg.bitmap.Dispose();
                string filename = "SubService" + _session.empId.ToString() + "-" + Convert.ToString(DateTime.Now) + ".Jpeg";
                string newfile = filename.Replace("/", "-");
                newfile = newfile.Replace(":", " ");
                //  System.Windows.MessageBox.Show(newfile);
                string imgpath = System.IO.Path.Combine(@"\" + _session.serverpath, newfile);
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
                long length = new System.IO.FileInfo(imgpath).Length;
                tmp.Dispose();
                // System.Windows.MessageBox.Show(Convert.ToString(length));
                int looping = 0;// if the length is small we have to try 3 times
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
                    _session.image2 = imgpath;


                }
                _session.image2 = imgpath;
            }
            catch (Exception ee) { tmp.Dispose(); _session.errdescroption += "// couldnt take the 'choose sub service' photo // "; }
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
            _session.fullcash = Convert.ToInt32(_session.recomp);
            try
            {
                _session.payRec = WebServiseFunctions.paymentrecid_Class(_session.empId, 2);
            }
            catch (Exception error)
            { }


            DispinsingCash ws = new DispinsingCash(); // inclus waiting to pay money
            ws.printerStatus = this.printerStatus;
			ws.Show();
            try
            {
                camimg.videoSource.Stop();
            }
            catch (Exception) { }
            Thread.Sleep(100);
			this.Close();
		}

		private void btnLogOut_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            _session.empPath += "choose subservice log out // ";
            dispatcherTimer.Stop();
            dispatcherTimer.Tick -= dispatcherTimer_Tick;
			StartScreen cl=new StartScreen();
			cl.Show();
            StopFile();
			this.Close();
		}

		private void Window_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            if (_session.salary == null || _session.salary == 0) {
                Salarybtn.IsEnabled = false;
            }
            if (_session.recomp == null || _session.recomp == 0)
            {
                Otherbtn.IsEnabled = false;
            }

           // string str = Environment.GetFolderPath(
           //System.Environment.SpecialFolder.DesktopDirectory);
            Sub_Service.Background = new ImageBrush(new BitmapImage(new Uri(@"D:\\backGroundImage\\HADEEDBackground.Png")));

            if (_session.language == 2)
            {
                PlayFile(@"D:\\backgroundsounds\\selectsub_EN.mp3");
                TextBlock_1.FontFamily = new System.Windows.Media.FontFamily("Tw Cen MT Condensed Extra Bold");
                TextBlock_1.FontSize = 50;
                TextBlock_1.Text = "Please Choose a Service";

                Salarybtn.FontFamily = new System.Windows.Media.FontFamily("Adobe Arabic");
                Salarybtn.FontSize = 45;
                Salarybtn.FontWeight = FontWeights.Bold;
                Salarybtn.Content = "Salary";

                Otherbtn.FontFamily = new System.Windows.Media.FontFamily("Adobe Arabic");
                Otherbtn.FontSize = 45;
                Otherbtn.FontWeight = FontWeights.Bold;
                Otherbtn.Content = "Other";

                btnLogOut.FontFamily = new System.Windows.Media.FontFamily("Adobe Arabic");
                btnLogOut.FontSize = 36;
                btnLogOut.FontWeight = FontWeights.Bold;
                btnLogOut.Content = "Log out";
            }
            else
            { PlayFile(@"D:\\backgroundsounds\\selectsub_AR.mp3"); }
        
			dispatcherTimer.Tick +=dispatcherTimer_Tick;
			dispatcherTimer.Interval = new TimeSpan(0,0,30);
			dispatcherTimer.Start();
        }
		private void dispatcherTimer_Tick(object sender, EventArgs e)
		{
            dispatcherTimer.Stop();
            dispatcherTimer.Tick -= dispatcherTimer_Tick;
			StartScreen con=new StartScreen();
			con.Show();
            StopFile();
			this.Close();
        }

	}
}