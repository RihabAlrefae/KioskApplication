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
using Ini;
//using KioskApp.HelloWord;
using System.Diagnostics;
using KioskApp.Clases;
using System.Data.SqlClient;


namespace KioskApp
{
	/// <summary>
	/// Interaction logic for ChooseService.xaml
	/// </summary>
	public partial class ChooseService : Window
	{
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
            try
            {
                sound1.controls.stop();
                sound1.close();
            }
            catch(Exception err){}
        }

        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();

		public ChooseService()
		{
			this.InitializeComponent();
			
        }

        private void finentialService_Click(object sender, System.Windows.RoutedEventArgs e)
        {

            int status1 = -1;
            try {
            SqlConnection dbConn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["kioskApp.Properties.Settings.sqlconnstring"].ConnectionString);
            dbConn.Open();
            string command = "Select PrintersStatus,id from PrinterStatus where checkDatetime = (Select Max(checkDatetime) from PrinterStatus)";
            SqlCommand sqlcomm = new SqlCommand(command, dbConn);
            SqlDataReader datareader = sqlcomm.ExecuteReader();
            if (datareader.HasRows)
            {
                if (datareader.Read())
                {
                    status1 =(datareader.GetValue(0)!=DBNull.Value ? Convert.ToInt32(datareader.GetValue(0)):0);
                }
            }

            sqlcomm.Dispose();
            dbConn.Close();
            datareader.Dispose();
                 }
            catch (Exception exce) {  _session.errorecode += "error in local DB//"; }
            if (status1==1)
            {
                _session.empPath += "error in Printer  // ";
                dispatcherTimer.Stop();
                dispatcherTimer.Tick -= dispatcherTimer_Tick;
          //      string ArMsg = "الرجاء مراجعة قسم تقانة المعلومات أو التوجه إلى جهاز آخر";
                customdialog f = new customdialog("ChooseService");
                f.Show();
               // ErrorMessage errorm = new ErrorMessage(ArMsg);
                //errorm.ARsubstr = "الرجاء مراجعة قسم تقانة المعلومات أو التوجه إلى جهاز آخر";
                //errorm.ENsubstr = "Please check whith IT or visit another Kiosk";
               // errorm.Show();
                this.Close();
                StopFile();
                return;
            }
            else if(status1==-1)
            {
                _session.empPath += "error in local DB  // ";
                dispatcherTimer.Stop();
                dispatcherTimer.Tick -= dispatcherTimer_Tick;
                
                ErrorMessage errorm =new ErrorMessage(Clases.ErrorsMsg.ItErrorMsg_Ar, Clases.ErrorsMsg.ItErrorMsg_En);
                errorm.Show();
                this.Close();
                StopFile();
                return;
            }
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
            if (_session.fcst1 == 0 && _session.fcst2 == 0 && _session.fcst3 == 0&&_session.fcst4==0)
            {
                _session.empPath += "empty casete all  // ";
                dispatcherTimer.Stop();
                dispatcherTimer.Tick -= dispatcherTimer_Tick;
                ErrorMessage errorm = new ErrorMessage(Clases.ErrorsMsg.FinanceErrorMsg_Ar, Clases.ErrorsMsg.FinanceErrorMsg_En);
                errorm.Show();
                this.Close();
                StopFile();
            }
            else
            {
                _session.empPath += "Financial services // ";
                dispatcherTimer.Stop();
                dispatcherTimer.Tick -= dispatcherTimer_Tick;
                ChooseSubServ css = new ChooseSubServ();
                css.Show();
                StopFile();
                this.Close();
            }
        }

        private void OtherService_Click(object sender, System.Windows.RoutedEventArgs e)
        {
          
            Process[] pname = Process.GetProcessesByName("kioskEngine");
            if (pname.Length == 0)
            {
                Offline off = new Offline();
                off.Show();
                return;

            }
            dispatcherTimer.Stop();
            dispatcherTimer.Tick -= dispatcherTimer_Tick;
            _session.empPath += "other services // ";
            //browserwindow bw = new browserwindow();
            //bw.Show();
            Browserwindow bw = new Browserwindow();
            bw.Show();
            this.Close();
          
        	// TODO: Add event handler implementation here.
        }

        private void btnLogOut_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _session.empPath += "choose service logout // ";
            dispatcherTimer.Stop();
            dispatcherTimer.Tick -= dispatcherTimer_Tick;
        	StartScreen cl=new StartScreen();
			cl.Show();
            StopFile();
			this.Close();
        }

       private void Window_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            IniFile ini = new IniFile(Environment.CurrentDirectory + "\\" + "kioskConfigFile.ini");
            int EnableOtherServices = Convert.ToInt32(ini.Read("OtherServiec", "ISEnabled")); 
           // EnableOtherServices1=ini.Read("OtherServiec", "ISEnabled").ToString();

            this.OtherService.IsEnabled = (EnableOtherServices==1);

            double? salary=0;
            double? recomp=0 ;
            try
            {
                salary = WebServiseFunctions.GetPayrool_Class(_session.empId, 1);
               
            }
            catch (Exception exx) { salary = 0; }
            _session.salary = Convert.ToInt32(salary);
            
            try
            {
                recomp = WebServiseFunctions.GetPayrool_Class(_session.empId, 2);
            }
            catch (Exception exx) { recomp = 0; }
            try
            {
                _session.fcst1 = WebServiseFunctions.getCasettevalue_Class(Convert.ToInt32(_session.KioskID), out  _session.fcst2, out _session.fcst3, out  _session.fcst4, out _session.rejectedbox1, out _session.rejectedbox2, out _session.rejectedbox3, out _session.rejectedbox4, out _session.cst1, out _session.cst2, out _session.cst3, out _session.cst4);
            }
            catch (Exception ee) {
                _session.errorecode += "// webservice not fount//";

                ErrorMessage errm = new ErrorMessage(Clases.ErrorsMsg.ItErrorMsg_Ar, Clases.ErrorsMsg.ItErrorMsg_En);
                errm.Show();
                StopFile();
                this.Close();
               
                return;
            }
            _session.recomp = recomp;
            if ((salary == null || salary==0) && (recomp == null || recomp==0)) {

                finentialService.IsEnabled = false;
            }
            //string str = Environment.GetFolderPath(
            //           System.Environment.SpecialFolder.DesktopDirectory);
            Choose_Service.Background = new ImageBrush(new BitmapImage(new Uri(@"D:\\backGroundImage\\HADEEDBackground.png")));

            if (_session.language == 2)
            {
                PlayFile(@"D:\\backgroundsounds\\select_EN.mp3");
                TextBlock_1.FontFamily = new System.Windows.Media.FontFamily("Tw Cen MT Condensed Extra Bold");
                TextBlock_1.FontSize = 50;
                TextBlock_1.Text = "Please Choose a Service";

                finentialService.FontFamily = new System.Windows.Media.FontFamily("Adobe Arabic");
                finentialService.FontWeight = FontWeights.Bold;
                finentialService.FontSize = 45;
                finentialService.Content = "Financial  Services";

                OtherService.FontFamily = new System.Windows.Media.FontFamily("Adobe Arabic");
                OtherService.FontWeight = FontWeights.Bold;
                OtherService.FontSize = 45;
                OtherService.Content = "Other Services";

                btnLogOut.FontFamily = new System.Windows.Media.FontFamily("Adobe Arabic");
                btnLogOut.FontWeight = FontWeights.Bold;
                btnLogOut.FontSize = 36;
                btnLogOut.Content = "Log Out";
            }
            else {
                PlayFile(@"D:\\backgroundsounds\\select_AR.mp3");
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
            StopFile();
			this.Close();
        }

       
	}
}