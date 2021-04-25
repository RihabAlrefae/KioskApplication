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


namespace KioskApp
{
	/// <summary>
    /// Interaction logic for ErrorMessage.xaml
	/// </summary>
	public partial class Offline : Window
	{
        //string str = Environment.GetFolderPath(
        //               System.Environment.SpecialFolder.DesktopDirectory);
        
        public Offline()
		{
            
                //System.Timers.Timer timer = new System.Timers.Timer(1000);
                this.InitializeComponent();
            
          
			}
       private void Window_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            
            Off_line.Background = new ImageBrush(new BitmapImage(new Uri(@"D:\\backGroundImage\\HADEEDBackground.png")));
            string file_name2 = "errors log " + Convert.ToString(DateTime.Today) + ".log";
            string newfile2 = file_name2.Replace("/", "-");

            newfile2 = newfile2.Replace(":", " ");
        }
	}
}