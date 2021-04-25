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
using WMPLib;


namespace KioskApp
{
	/// <summary>
	/// Interaction logic for sucsessPIN.xaml
	/// </summary>
	public partial class sucsessPIN : Window
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
		public sucsessPIN()
		{
			this.InitializeComponent();
			
        }

        private void Window_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
			dispatcherTimer.Stop();
            dispatcherTimer.Tick -= dispatcherTimer_Tick;
			StartScreen con=new StartScreen();
			con.Show();
            StopFile();
			this.Close();
        }

        private void Window_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
//            string str = Environment.GetFolderPath(
//System.Environment.SpecialFolder.DesktopDirectory);
            Sucsess_PIN.Background = new ImageBrush(new BitmapImage(new Uri(@"D:\\backGroundImage\\HADEEDBackground.png")));

            if (_session.language == 2)
            {
                PlayFile(@"D:\\backgroundsounds\\successpin_EN.mp3");
                TextBlock_1.FontFamily = new System.Windows.Media.FontFamily("Tw Cen MT Condensed Extra Bold");
                TextBlock_1.FontSize = 42;
                TextBlock_1.Text = "Succesfuly Changed";
            }
            else {
                PlayFile(@"D:\\backgroundsounds\\successpin_AR.mp3");
            }
			dispatcherTimer.Tick +=dispatcherTimer_Tick;
			dispatcherTimer.Interval = new TimeSpan(0,0,10);
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