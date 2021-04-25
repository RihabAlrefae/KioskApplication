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



namespace KioskApp
{
	/// <summary>
    /// Interaction logic for ErrorMessage.xaml
	/// </summary>
	public partial class ErrorMessage : Window
	{
       public string ARsubstr = " ";
       public string ENsubstr = " ";


        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        public ErrorMessage()
		{
			this.InitializeComponent();
        }

        public ErrorMessage(string ArString)
        {
            this.InitializeComponent();
            ARsubstr = ArString;

        }
        public ErrorMessage(string ArString,string EnString)
        {
            this.InitializeComponent();
            ARsubstr = ArString;
            ENsubstr = EnString;

        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            dispatcherTimer.Stop();
            dispatcherTimer.Tick -= dispatcherTimer_Tick;
            StartScreen con=new StartScreen();
			con.Show();
			this.Close();
        }

       private void Window_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            //string str = Environment.GetFolderPath(
            //System.Environment.SpecialFolder.DesktopDirectory);
            Error_Message.Background = new ImageBrush(new BitmapImage(new Uri(@"D:\\backGroundImage\\HADEEDBackground.png")));
            if (_session.language == 2)
            {
				TextBlock_1.FontFamily=new FontFamily("Tw Cen MT Condensed Extra Bold");
				TextBlock_1.FontSize = 42;
                TextBlock_1.Text = "Sorry,We Could't Fulfill Your Request Now";
                SubStr_ForErrors.FontFamily = new FontFamily("Tw Cen MT Condensed Extra Bold");
                SubStr_ForErrors.FontSize = 42;
                SubStr_ForErrors.Text = ENsubstr;
            }
            else
            {
                SubStr_ForErrors.Text = ARsubstr;
            }
         
           
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
			this.Close();
        }
	}
}