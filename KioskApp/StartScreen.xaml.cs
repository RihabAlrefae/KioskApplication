using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Security.Cryptography;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Threading;
using System.Windows.Threading;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.IO.Ports;
using Ini;
using KioskApp.HelloWord;
using System.IO;
using System.Diagnostics;


namespace KioskApp
{
	/// <summary>
	/// Interaction logic for StartScreen.xaml
	/// </summary>
	public partial class StartScreen : Window
	{
        
       
		public StartScreen()
		{
			this.InitializeComponent();
        }
        private void btnArabic_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            
            Process[] pname = Process.GetProcessesByName("kioskEngine");
            if (pname.Length == 0)
            {
                foreach(Process p in pname)
                {
                    p.Dispose();
                }
                Offline off = new Offline();
                off.Show();
                return;

            }
            
            _session.language = 1;
            _session.empPath += "Arabic//";
            ScanCard sc = new ScanCard();
            sc.Show();
			this.Close();
        }

        private void fillClick_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            fillClick_Copy.IsEnabled = true;
        }

        private void btnEnglish_Click(object sender, RoutedEventArgs e)
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
            _session.language = 2;
            _session.empPath += "English//";
            ScanCard sc = new ScanCard();
            sc.Show();
            this.Close();
        }
        //static string GetMd5Hash(MD5 md5Hash, string input)
        //{

        //    // Convert the input string to a byte array and compute the hash.
        //    byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

        //    // Create a new Stringbuilder to collect the bytes
        //    // and create a string.
        //    StringBuilder sBuilder = new StringBuilder();

        //    // Loop through each byte of the hashed data 
        //    // and format each one as a hexadecimal string.
        //    for (int i = 0; i < data.Length; i++)
        //    {
        //        sBuilder.Append(data[i].ToString("x2"));
        //    }

        //    // Return the hexadecimal string.
        //    return sBuilder.ToString();
        //}
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            IniFile ini = null;
            if (!File.Exists(Environment.CurrentDirectory+"\\"+"kioskConfigFile.ini"))
            {
                ini = new IniFile(Environment.CurrentDirectory + "\\" + "kioskConfigFile.ini");
                ini.Write("server", "ip", "192.168.105.15");
                ini.Write("server", "savepath", "\\192.168.105.15\\d\\kioskphotos");
                ini.Write("kiosk","kioskid","1002");
                //ini = new IniFile("D:\\kioskConfigFile.ini");
            }
            else { ini = new IniFile(Environment.CurrentDirectory+"\\"+"kioskConfigFile.ini"); }
          //  MessageBox.Show(ini.FilePath.ToString());
            //string str = Environment.GetFolderPath(
                        // System.Environment.SpecialFolder.DesktopDirectory);
            Start_Name.Background = new ImageBrush(new BitmapImage(new Uri(@"D:\\backGroundImage\\HADEEDBackground.png")));


           //string file_name = "visits log " + Convert.ToString(DateTime.Today) + ".log";
           //string newfile = file_name.Replace("/", "-");
         
           //newfile = newfile.Replace(":", " ");
           //try
           //{
           //    writingLogs.writing_visit_log("D:\\visitlogsfolder\\" + newfile, _session.tranactionid + "," + _session.empId + "," + _session.KioskID + "," + Convert.ToString(_session.empPath) + "," + _session.number_of_patches + "," + _session.number_of_peapers +"," +Convert.ToString(DateTime.Now) + "\n");
           //}
           //catch (Exception eee)
           //{
           //    MessageBox.Show(eee.Message);
           //}

           // // 2 log (finantial)
           //string file_name2 = "finantial log " + Convert.ToString(DateTime.Today) + ".log";
           //string newfile2 = file_name2.Replace("/", "-");

           //newfile2 = newfile2.Replace(":", " ");
           //try
           //{
           //    using (MD5 md5Hash = MD5.Create())
           //    {
           //        string hash = GetMd5Hash(md5Hash, Convert.ToString(_session.fullcash));
           //        string hash1 = GetMd5Hash(md5Hash, Convert.ToString(_session.paidsalary));
           //        writingLogs.writing_finantial_log("D:\\finantialogsfolder\\" + newfile2, _session.tranactionid + "," + _session.empId + "," + _session.KioskID + "," + _session.number_of_peapers + ", full cash:" + hash + ", paid cash:" + hash1 + "," + Convert.ToString(DateTime.Now) + "\n");
           //    }
           //}
           //catch (Exception eee)
           //{
           //    MessageBox.Show(eee.Message);
           //}

            //3 log error log

           string file_name3 = "error log " + Convert.ToString(DateTime.Today) + ".log";
           string newfile3 = file_name3.Replace("/", "-");

           newfile3 = newfile3.Replace(":", " ");
           try
           {
               writingLogs.writing_finantial_log("D:\\Errorlog\\" + newfile3, _session.KioskID + "," + _session.errorecode + "," + _session.errdescroption + Convert.ToString(DateTime.Now) + "\n");
           }
           catch (Exception eee)
           {
               MessageBox.Show(eee.Message);
           }
           try
           {
               _session.empId = 0;
               _session.empPIN = 0;
               _session.empPath = "";
               _session.salary = 0;
               _session.fullcash = 0;
               _session.image1 = "";
               _session.errorecode = "";
               _session.errdescroption = "";
               _session.language = 0;
               _session.doorstatus = "";
               _session.dispensed_money = 0;
               _session.number_of_peapers = 0;
               _session.number_of_patches = 0;
               _session.retracted_peapers = 0;
               _session.tranactionid = "";
               _session.dispinse1 = 0;
               _session.dispinse2 = 0;
               _session.dispinse3 = 0;
               _session.dispinse4 = 0;
               _session.serverpath = ini.Read("server", "savepath");
               _session.Empname = "";
               _session.paidsalary = 0;
               _session.dispinse22 = 0;
               _session.dispinse11 = 0;
               _session.resualt = 0;
               _session.thetruthresault = 0;
               _session.restofthecash = 0;
               if (_session.KioskID == null || _session.KioskID == 0)
               {
                  _session.KioskID= Convert.ToInt32(ini.Read("kiosk", "kioskid"));
                  // KioskAppWebServiceSoapClient fromyoutube = new KioskAppWebServiceSoapClient();
                  // _session.KioskID = fromyoutube.get_Kiosk_number(Convert.ToInt32(ini.Read("kiosk", "kioskid")));
               }
               if (_session.cst1 == 0 || _session.cst2 == 0 || _session.cst3 == 0 || _session.cst4 == 0)
               {
                   try
                   {
                       //KioskAppWebServiceSoapClient fromyoutube = new KioskAppWebServiceSoapClient();
                     //  _session.cst1 = fromyoutube.get_cassettes_types(Convert.ToInt32(_session.KioskID), out _session.cst2, out _session.cst3, out _session.cst4);
                   }
                   catch (Exception cc) { _session.errorecode += "no web service: " + cc.Message.ToString(); }
               }
           }
           catch (Exception noserverconnect) { 
           //Offline ol = new Offline();
           //ol.Show();
           //this.Close();
           }
         //  Process[] pname1 = Process.GetProcessesByName("RC532");
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
           fillClick.IsEnabled = false;
           fillClick_Copy.IsEnabled = false;
         
           //MessageBox.Show(_session.empId.ToString());
        }


        private void fillClick_Copy_MouseDown(object sender, MouseButtonEventArgs e)
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
            passfillcard sc = new passfillcard();
            sc.Show();
            this.Close();
        }

        private void fillClick_Copy1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            fillClick.IsEnabled = true;
        }
		
	}
}