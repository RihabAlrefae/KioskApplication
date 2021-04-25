using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using KioskApp.HelloWord;
using System.Threading;
using System.Drawing;
using AForge.Video.DirectShow;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using KioskApp.Clases; 

namespace KioskApp
{
	/// <summary>
	/// Interaction logic for FillProcess.xaml
	/// </summary>
	public partial class FillProcess : Window
	{
      //  private KioskAppWebServiceSoapClient fromyoutube = new KioskAppWebServiceSoapClient();
        
		//System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
		public FillProcess()
		{
			this.InitializeComponent();
			
			// Insert code required on object creation below this point.
		}
        CameraImaging camimg = new CameraImaging();
       // Thread thrVideo;
        public Bitmap tmp;
        public int printerStatus = 1;
        
		private void btnLogOut_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			//dispatcherTimer.Stop();
			StartScreen cl=new StartScreen();
			cl.Show();
			this.Close();

		}
       
		private void btn_ok_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            byte[] file_byte=null;
            string imgpath = null;
            #region image
            try
            {
                camimg.videoSource = new VideoCaptureDevice(camimg.videoDevices[0].MonikerString);
                // System.Windows.MessageBox.Show(camimg.videoDevices[1].Name);
                camimg.videoSource.DesiredFrameRate = 10;
                camimg.videoSource.DesiredFrameSize = new System.Drawing.Size(800, 600);
                camimg.videoSource.Start();
             
                Thread.Sleep(1000);
                tmp = (Bitmap)camimg.bitmap.Clone();
                camimg.bitmap.Dispose();
                file_byte = ImageToByteArray(tmp);
                string filename = "fill" + Convert.ToString(DateTime.Now) + ".Jpeg";
                string newfile = filename.Replace("/", "-");
                newfile = newfile.Replace(":", " ");
                imgpath = System.IO.Path.Combine(@"\" + _session.serverpath, newfile);
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
           
                long length = new System.IO.FileInfo(imgpath).Length;
                tmp.Dispose();
                int looping = 0;
                while (length < 900 & looping < 4)
                {
                    looping++;
                    tmp = (Bitmap)camimg.bitmap.Clone();
                    camimg.bitmap.Dispose();
                    // imgpath = System.IO.Path.Combine("D:\\", newfile);
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
            }
            catch (Exception ee) { tmp.Dispose(); }

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

            if (CassettesConfig.cs1oldconf == "" || CassettesConfig.cs2oldconf == "" || CassettesConfig.cs3oldconf == "" || CassettesConfig.cs4oldconf == "")
            {
                CassettesConfig.cs1oldconf = _session.cs1type;
                CassettesConfig.cs2oldconf = _session.cs2type;
                CassettesConfig.cs3oldconf = _session.cs3type;
                CassettesConfig.cs4oldconf = _session.cs4type;
            }
            int recordid = 0;
            try
            {
                if (printerStatus == 0)
                {
                    recordid = WebServiseFunctions.fillprocess_Class(Convert.ToInt32(_session.KioskID), Convert.ToInt32(newcst1.Text), Convert.ToInt32(newcst2.Text), Convert.ToInt32(newcst3.Text), Convert.ToInt32(newcst4.Text), _session.empId, imgpath, _session.tranactionid, Convert.ToInt32(firstcasette1.Text), Convert.ToInt32(secondcasette2.Text), Convert.ToInt32(thirdcasette3.Text), Convert.ToInt32(forthcasette4.Text), CassettesConfig.cs1oldconf, CassettesConfig.cs2oldconf, CassettesConfig.cs3oldconf, CassettesConfig.cs4oldconf, _session.cs1type, _session.cs2type, _session.cs3type, _session.cs4type,"the user choose to fill without reciept");
                }
                else
                {
                    recordid = WebServiseFunctions.fillprocess1_Class(Convert.ToInt32(_session.KioskID), Convert.ToInt32(newcst1.Text), Convert.ToInt32(newcst2.Text), Convert.ToInt32(newcst3.Text), Convert.ToInt32(newcst4.Text), _session.empId, imgpath, _session.tranactionid, Convert.ToInt32(firstcasette1.Text), Convert.ToInt32(secondcasette2.Text), Convert.ToInt32(thirdcasette3.Text), Convert.ToInt32(forthcasette4.Text), CassettesConfig.cs1oldconf, CassettesConfig.cs2oldconf, CassettesConfig.cs3oldconf, CassettesConfig.cs4oldconf, _session.cs1type, _session.cs2type, _session.cs3type, _session.cs4type);

                }
                WebServiseFunctions.updatecurrentcasettevalue_Class(Convert.ToInt32(_session.KioskID), Convert.ToInt32(newcst1.Text), Convert.ToInt32(newcst2.Text), Convert.ToInt32(newcst3.Text), Convert.ToInt32(newcst4.Text), 0, 0, 0, 0);
                WebServiseFunctions.set_cassettes_types_Class(Convert.ToInt32(_session.KioskID), _session.cst1, _session.cst2, _session.cst3, _session.cst4);
            }
            catch (Exception look)
            {
                _session.errorecode += "// webservice not fount//";
                ErrorMessage eror = new ErrorMessage(Clases.ErrorsMsg.ItErrorMsg_Ar, Clases.ErrorsMsg.ItErrorMsg_En);
                eror.Show();
                try
                {
                    camimg.videoSource.Stop();
                }
                catch (Exception) { }
                this.Close();
                return;
            }
            StartScreen cl = new StartScreen();
            cl.Show();
            try
            {
                if (printerStatus == 1)
                {
                    fillprocessrpt1 fillp = new fillprocessrpt1();
                    fillp.SetParameterValue("recid", recordid);
                    fillp.SetParameterValue("knumber", Convert.ToInt32(_session.KioskID));
                    fillp.SetParameterValue("oldcst1", Convert.ToInt32(firstcasette1.Text));
                    fillp.SetParameterValue("oldcst2", Convert.ToInt32(secondcasette2.Text));
                    fillp.SetParameterValue("oldcst3", Convert.ToInt32(thirdcasette3.Text));
                    fillp.SetParameterValue("oldcst4", Convert.ToInt32(forthcasette4.Text));
                    fillp.SetParameterValue("newcst1", Convert.ToInt32(newcst1.Text));
                    fillp.SetParameterValue("newcst2", Convert.ToInt32(newcst2.Text));
                    fillp.SetParameterValue("newcst3", Convert.ToInt32(newcst3.Text));
                    fillp.SetParameterValue("newcst4", Convert.ToInt32(newcst4.Text));
                    fillp.SetParameterValue("empid", Convert.ToInt32(_session.userid));
                    fillp.SetParameterValue("type1", Convert.ToInt32(_session.cst1));
                    fillp.SetParameterValue("type2", Convert.ToInt32(_session.cst2));
                    fillp.SetParameterValue("type3", Convert.ToInt32(_session.cst3));
                    fillp.SetParameterValue("type4", Convert.ToInt32(_session.cst4));
                    if (Convert.ToString(CassettesConfig.cs1oldconf).StartsWith("0"))
                        fillp.SetParameterValue("csconf1", Convert.ToInt32("500"));
                    else if (Convert.ToString(CassettesConfig.cs1oldconf).StartsWith("1"))
                        fillp.SetParameterValue("csconf1", Convert.ToInt32("1000"));
                    else if (Convert.ToString(CassettesConfig.cs1oldconf).StartsWith("2"))
                        fillp.SetParameterValue("csconf1", Convert.ToInt32("2000"));
                    else
                        fillp.SetParameterValue("csconf1", Convert.ToInt32("5000"));

                    if (Convert.ToString(CassettesConfig.cs2oldconf).StartsWith("0"))
                        fillp.SetParameterValue("csconf2", Convert.ToInt32("500"));
                    else if (Convert.ToString(CassettesConfig.cs2oldconf).StartsWith("1"))
                        fillp.SetParameterValue("csconf2", Convert.ToInt32("1000"));
                    else if (Convert.ToString(CassettesConfig.cs2oldconf).StartsWith("2"))
                        fillp.SetParameterValue("csconf2", Convert.ToInt32("2000"));
                    else
                        fillp.SetParameterValue("csconf2", Convert.ToInt32("5000"));

                    if (Convert.ToString(CassettesConfig.cs3oldconf).StartsWith("0"))
                        fillp.SetParameterValue("csconf3", Convert.ToInt32("500"));
                    else if (Convert.ToString(CassettesConfig.cs3oldconf).StartsWith("1"))
                        fillp.SetParameterValue("csconf3", Convert.ToInt32("1000"));
                    else if (Convert.ToString(CassettesConfig.cs3oldconf).StartsWith("1"))
                        fillp.SetParameterValue("csconf3", Convert.ToInt32("2000"));
                    else
                        fillp.SetParameterValue("csconf3", Convert.ToInt32("5000"));


                    if (Convert.ToString(CassettesConfig.cs4oldconf).StartsWith("0"))
                        fillp.SetParameterValue("csconf4", Convert.ToInt32("500"));
                    else if (Convert.ToString(CassettesConfig.cs4oldconf).StartsWith("1"))
                        fillp.SetParameterValue("csconf4", Convert.ToInt32("1000"));
                    else if (Convert.ToString(CassettesConfig.cs4oldconf).StartsWith("2"))
                        fillp.SetParameterValue("csconf4", Convert.ToInt32("2000"));
                    else
                        fillp.SetParameterValue("csconf4", Convert.ToInt32("5000"));

                    // ErrorReciept fillp = new ErrorReciept();

                    fillp.PrintToPrinter(2, false, 0, 0);
                    fillp.Close();
                    fillp.Dispose();
                }
                //recompReciept rr = new recompReciept();
                //rr.SetParameterValue("recid", _session.payRec);
                //rr.SetParameterValue("tranid", _session.tranactionid);
            }
            catch (Exception exc)
            { _session.errdescroption += "\\ error printing \\"; }
            //StartScreen cl=new StartScreen();
            //cl.Show();
            try
            {
                camimg.videoSource.Stop();
            }
            catch (Exception) { }
			this.Close();

		}
        private byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                return ms.ToArray();
            }
        }
		 private void Window_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
             int firstcasette=0;
             int secondcasette=0;
             int thirdcasette=0;
             int fourthcasette=0;
             int rejected1 = 0;
             int rejected2 = 0;
             int rejected3 = 0;
             int rejected4 = 0;

             //int firstconf=0;
             //int secondconf=0;
             //int thirdconf=0;
             //int fourthconf=0;
             int blabla = 0;
             if (_session.cst1 != 0 || _session.cst2 != 0 || _session.cst3 != 0 || _session.cst4 != 0)
             {
                 try
                 {
                     firstcasette = WebServiseFunctions.getCasettevalue_Class(Convert.ToInt32(_session.KioskID), out secondcasette, out thirdcasette, out fourthcasette, out rejected1, out rejected2, out rejected3, out rejected4, out blabla, out blabla, out blabla, out blabla);
                 }
                 catch (Exception eww) { _session.errdescroption += "// " + eww.Message.ToString() + "//"; }
             }
             else if (_session.cst1 == 0 || _session.cst2 == 0 || _session.cst3 == 0 || _session.cst4 == 0)
             {
                 try
                 {
                     firstcasette = WebServiseFunctions.getCasettevalue_Class(Convert.ToInt32(_session.KioskID), out secondcasette, out thirdcasette, out fourthcasette, out rejected1, out rejected2, out rejected3, out rejected4, out _session.cst1, out _session.cst2, out _session.cst3, out _session.cst4);
                 }
                 catch (Exception eww) { _session.errdescroption += "// " + eww.Message.ToString() + "//"; }
             }


             firstcasette1.Text = Convert.ToString(firstcasette);
             secondcasette2.Text = Convert.ToString(secondcasette);
             thirdcasette3.Text = Convert.ToString(thirdcasette);
             forthcasette4.Text = Convert.ToString(fourthcasette);
             reject1.Text = Convert.ToString(rejected1);
             reject2.Text = Convert.ToString(rejected2);
             reject3.Text =  Convert.ToString(rejected3);
             reject4.Text =  Convert.ToString(rejected4);

             //TextBlock_type1.Text =_session.cst1.ToString() + ":" ;
             //TextBlock_type2.Text =  _session.cst2.ToString() + ":";
             //TextBlock_type3.Text =  _session.cst3.ToString() + ":";
             //TextBlock_type4.Text =  _session.cst4.ToString() + ":";
             if (Convert.ToString(_session.cs1type).StartsWith("0"))
                 TextBlock_type1.Text = "500" + ":";
             else if (Convert.ToString(_session.cs1type).StartsWith("1"))
                 TextBlock_type1.Text = "1000" + ":";
            else if (Convert.ToString(_session.cs1type).StartsWith("2"))
                TextBlock_type1.Text = "2000" + ":";
             else
                TextBlock_type1.Text = "5000" + ":";

            if (Convert.ToString(_session.cs2type).StartsWith("0"))
                 TextBlock_type2.Text = "500" + ":";
             else if (Convert.ToString(_session.cs2type).StartsWith("1"))
                 TextBlock_type2.Text = "1000" + ":";
             else
                 TextBlock_type2.Text = "2000" + ":";
             if (Convert.ToString(_session.cs3type).StartsWith("0"))
                 TextBlock_type3.Text = "500" + ":";
             else if (Convert.ToString(_session.cs3type).StartsWith("1"))
                 TextBlock_type3.Text = "1000" + ":";
              else
                 TextBlock_type3.Text = "2000" + ":";
             if (Convert.ToString(_session.cs4type).StartsWith("0"))
                 TextBlock_type4.Text = "500" + ":";
             else if (Convert.ToString(_session.cs4type).StartsWith("1"))
                 TextBlock_type4.Text = "1000" + ":";
             else
                 TextBlock_type4.Text = "2000" + ":";
             
//            string str = Environment.GetFolderPath(
//System.Environment.SpecialFolder.DesktopDirectory);
            fill_process.Background = new ImageBrush(new BitmapImage(new Uri(@"D:\\backGroundImage\\HADEEDBackground.Png")));

        }

         private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
         {
             CassettesConfig cc = new CassettesConfig();
             cc.Show();
             this.Close();
         }
        
      
	}
}