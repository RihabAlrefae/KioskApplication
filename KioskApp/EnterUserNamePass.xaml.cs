using AForge.Video.DirectShow;
using KioskApp.Clases;
using KioskApp.HelloWord;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
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
    /// Interaction logic for EnterUserNamePass.xaml
    /// </summary>
    public partial class EnterUserNamePass : Window
    {
        private KioskAppWebServiceSoapClient fromyoutube = new KioskAppWebServiceSoapClient();
        CameraImaging camimg = new CameraImaging();
        Thread thrVideo;
        public Bitmap tmp;
        private void VideoRecording()
        {
            camimg.videoSource.Start();
            tmp = (Bitmap)camimg.bitmap.Clone();
        }
		System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        public EnterUserNamePass()
        {
            InitializeComponent();
        }

        private void btn_ok_Click(object sender, RoutedEventArgs e)
        {
			dispatcherTimer.Stop();
            dispatcherTimer.Tick -= dispatcherTimer_Tick;
            //try
            //{
            //    camimg.videoSource = new VideoCaptureDevice(camimg.videoDevices[0].MonikerString);
            //    // System.Windows.MessageBox.Show(camimg.videoDevices[1].Name);
            //    camimg.videoSource.Start();
            //    // VideoRecording();
            //    thrVideo = new Thread(VideoRecording);
            //    //lblRecording.Visible = true;
            //    thrVideo.Start();
            //    Thread.Sleep(1000);
            //    Thread.Sleep(33);

            //    //sfd.Filter = "*.png|*.png";
            //    tmp = (Bitmap)camimg.bitmap.Clone();
            //    string filename = "PINEnter" + Convert.ToString(DateTime.Now) + ".jpg";
            //    string newfile = filename.Replace("/", "-");
            //    newfile = newfile.Replace(":", " ");
            //    //  System.Windows.MessageBox.Show(newfile);
            //    tmp.Save(newfile, System.Drawing.Imaging.ImageFormat.Jpeg);
            //    long length = new System.IO.FileInfo(newfile).Length;
            //    // System.Windows.MessageBox.Show(Convert.ToString(length));
            //    int looping = 0;
            //    while (length < 900 & looping < 4)
            //    {
            //        looping++;
            //        tmp = (Bitmap)camimg.bitmap.Clone();
            //        tmp.Save(newfile, System.Drawing.Imaging.ImageFormat.Jpeg);
            //        length = new System.IO.FileInfo(newfile).Length;
            //        // tmp.Dispose();
            //    }
            //}
            //catch (Exception ee) { }
            bool? authinticateresualt=null;
            try
            {
                authinticateresualt = WebServiseFunctions.authinticatFilluser_Class(_session.empId, passwordbox1.Password.ToString());
            }
            catch (Exception errr) {
                _session.errorecode += "// webservice not fount//";
                //MessageBox.Show(errr.Message.ToString());
                ErrorMessage off = new ErrorMessage(Clases.ErrorsMsg.ItErrorMsg_Ar, Clases.ErrorsMsg.ItErrorMsg_En);
                off.Show();
                //try
                //{
                //    camimg.videoSource.Stop();
                //}
                //catch (Exception ee) { }
                this.Close();
                return;
            }
            if (authinticateresualt==null)
            {
                _session.errorecode += "// webservice not fount//";
               // MessageBox.Show(errr.Message.ToString());
                ErrorMessage off = new ErrorMessage(Clases.ErrorsMsg.ItErrorMsg_Ar, Clases.ErrorsMsg.ItErrorMsg_En);
                off.Show();
                //try
                //{
                //    camimg.videoSource.Stop();
                //}
                //catch (Exception ee) { }
                this.Close();
                return;
            }
            if (authinticateresualt == true)
            {
                
                ///printer check 
                ///ask the end user if he wants to continue although the the printer in not ok
                ///
                #region printer check
                int status1 = -1;
                try
                {
                    SqlConnection dbConn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["kioskApp.Properties.Settings.sqlconnstring"].ConnectionString);
                    dbConn.Open();
                    string command = "Select PrintersStatus,id from PrinterStatus where checkDatetime = (Select Max(checkDatetime) from PrinterStatus)";
                    SqlCommand sqlcomm = new SqlCommand(command, dbConn);
                    SqlDataReader datareader = sqlcomm.ExecuteReader();
                    if (datareader.HasRows)
                    {
                        if (datareader.Read())
                        {
                            status1 = (datareader.GetValue(0) != DBNull.Value ? Convert.ToInt32(datareader.GetValue(0)) : 0);
                        }
                    }

                    sqlcomm.Dispose();
                    dbConn.Close();
                    datareader.Dispose();
                }
                catch (Exception exce) { _session.errorecode += "error in local DB//"; }
                if (status1 == 1)
                {
                    _session.empPath += "error in Printer  // ";
                    dispatcherTimer.Stop();
                    dispatcherTimer.Tick -= dispatcherTimer_Tick;
                    //      string ArMsg = "الرجاء مراجعة قسم تقانة المعلومات أو التوجه إلى جهاز آخر";
                    customdialog f = new customdialog("EnterUserNamePass");
                    f.Show();
                    // ErrorMessage errorm = new ErrorMessage(ArMsg);
                    //errorm.ARsubstr = "الرجاء مراجعة قسم تقانة المعلومات أو التوجه إلى جهاز آخر";
                    //errorm.ENsubstr = "Please check whith IT or visit another Kiosk";
                    // errorm.Show();
                    this.Close();
                 //   StopFile();
                    return;
                }

                else if (status1 == -1)
                {
                    _session.empPath += "error in local DB  // ";
                    dispatcherTimer.Stop();
                    dispatcherTimer.Tick -= dispatcherTimer_Tick;

                    ErrorMessage errorm = new ErrorMessage(Clases.ErrorsMsg.ItErrorMsg_Ar, Clases.ErrorsMsg.ItErrorMsg_En);
                    errorm.Show();
                    this.Close();
                    //  StopFile();
                    return;
                }
                else
                {
                    FillProcess f = new FillProcess();
                    f.Show();
                }
         

                #endregion



                this.Close();
            }
            else {
               // _session.errorecode += "// webservice not fount//";
               // MessageBox.Show("no permission");
                ErrorMessage errr = new ErrorMessage(Clases.ErrorsMsg.HrErrorMsg_Ar, Clases.ErrorsMsg.HrErrorMsg_En);
                errr.Show();
                //try
                //{
                //    camimg.videoSource.Stop();
                //}
                //catch (Exception ee) { }
                this.Close();
            }

        }

        private void btnLogOut_Click(object sender, RoutedEventArgs e)
        {
			dispatcherTimer.Stop();
            dispatcherTimer.Tick -= dispatcherTimer_Tick;
			StartScreen cl=new StartScreen();
			cl.Show();
			this.Close();
        }
		private void dispatcherTimer_Tick(object sender, EventArgs e)
		{
            dispatcherTimer.Stop();
            dispatcherTimer.Tick -= dispatcherTimer_Tick;
		    StartScreen con=new StartScreen();
			con.Show();
            //try
            //{
            //    camimg.videoSource.Stop();
            //}
            //catch (Exception ee) { }
			this.Close();
        }
     //   private string OnScreenKeyboadApplication = "OnScreenKeyboard.exe";
		 private void Window_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
//            string str = Environment.GetFolderPath(
//System.Environment.SpecialFolder.DesktopDirectory);
            Enter_UserNamePass.Background = new ImageBrush(new BitmapImage(new Uri(@"D:\\backGroundImage\\HADEEDBackground.png")));                   
           // pp = Process.Start("osk");
             dispatcherTimer.Tick +=dispatcherTimer_Tick;
			dispatcherTimer.Interval = new TimeSpan(0,0,40);
			dispatcherTimer.Start();
        }

        
    }
}
