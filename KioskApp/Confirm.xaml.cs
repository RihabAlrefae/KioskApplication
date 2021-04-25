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
using System.Windows.Forms;
using System.Drawing;
using AForge.Video;
using AForge.Video.DirectShow;
using System.IO;
using WMPLib;
using KioskApp.HelloWord;

namespace KioskApp
{
	/// <summary>
	/// Interaction logic for Confirm.xaml
	/// </summary>
	public partial class Confirm : Window
	{

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
      //  private KioskAppWebServiceSoapClient fromyoutube = new KioskAppWebServiceSoapClient();
        private void Player_MediaError(object pMediaObject)
        {
            sound1.close();
        }
        private void StopFile()
        {
            sound1.controls.stop();
            sound1.close();
        }

        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
		public Confirm()
		{
			this.InitializeComponent();
			
        }
       
        BitmapImage BitmapToImageSource(Bitmap bitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;
                BitmapImage bitmapimage = new BitmapImage();
                bitmapimage.BeginInit();
                bitmapimage.StreamSource = memory;
                bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapimage.EndInit();

                return bitmapimage;
            }
        }
		private void Window_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
//            string str = Environment.GetFolderPath(
//System.Environment.SpecialFolder.DesktopDirectory);
            Confirm_Screen.Background = new ImageBrush(new BitmapImage(new Uri(@"D:\\backGroundImage\\HADEEDBackground.png")));

           // thrashOldCamera();
            if (_session.language == 2)
            {
                PlayFile(@"D:\\backgroundsounds\\confirm_EN.mp3");
                TextBlock_1.FontFamily = new System.Windows.Media.FontFamily("Tw Cen MT Condensed Extra Bold");
                TextBlock_1.Text = "Thank you for using our services, Please check the cash.";
                TextBlock_1.FontSize = 50;
                //oK.FontFamily = new System.Windows.Media.FontFamily("Adobe Arabic");
                //oK.FontWeight = FontWeights.Bold;
                //oK.FontSize = 36;
                //oK.Content = "Ok";
            }
            else {
                PlayFile(@"D:\\backgroundsounds\\confirm_AR.mp3");
            }
			dispatcherTimer.Tick +=dispatcherTimer_Tick;
			dispatcherTimer.Interval = new TimeSpan(0,0,30);
			dispatcherTimer.Start();
            //try
            //{
            //    if (ChooseSubServ.servicetype == 1)
            //    {
            //        fromyoutube.insertvisit(Convert.ToInt32(_session.KioskID), _session.tranactionid, _session.empId, _session.image1, _session.image2, _session.image3, _session.resualt, _session.paidsalary, Convert.ToInt32(_session.payRec), null);
            //    }
            //    else if (ChooseSubServ.servicetype == 2)
            //    {
            //        fromyoutube.insertvisit(Convert.ToInt32(_session.KioskID), _session.tranactionid, _session.empId, _session.image1, _session.image2, _session.image3, _session.resualt, _session.paidsalary, null, Convert.ToInt32(_session.payRec));
            //    }
            //}
            //catch (Exception err) { }
        }
        private void Window_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            StopFile();
            dispatcherTimer.Stop();
            dispatcherTimer.Tick -= dispatcherTimer_Tick;
            StartScreen con = new StartScreen();
            con.Show();
            
            this.Close();
        }
		private void dispatcherTimer_Tick(object sender, EventArgs e)
		{
            dispatcherTimer.Stop();
            dispatcherTimer.Tick -= dispatcherTimer_Tick;
		    StartScreen con=new StartScreen();
            //try
            //{
            //    camimg.videoSource.Stop();
            //}
            //catch (Exception) { }
            try
            {
                this.Close();
                con.Show();
            }
            catch(Exception err){}
        }

        

	}
}