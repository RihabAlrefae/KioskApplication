using Ini;
using KioskApp.HelloWord;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace KioskApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private KioskAppWebServiceSoapClient fromyoutube = new KioskAppWebServiceSoapClient();
        void App_Startup(object sender, StartupEventArgs e)
        {
            Offline oll = new Offline();
            oll.Show();
            IniFile ini = null;
            if (!File.Exists(Environment.CurrentDirectory + "\\" + "kioskConfigFile.ini"))
            {
                ini = new IniFile(Environment.CurrentDirectory + "\\" + "kioskConfigFile.ini");
                ini.Write("server", "ip", "192.168.105.15");
                ini.Write("server", "savepath", "\\192.168.105.15\\d\\kioskphotos");
                ini.Write("kiosk", "kioskid", "1002");
                //ini = new IniFile("D:\\kioskConfigFile.ini");
            }
            else { ini = new IniFile(Environment.CurrentDirectory + "\\" + "kioskConfigFile.ini"); }
           
            try
            {
                _session.KioskID = fromyoutube.get_Kiosk_number(Convert.ToInt32(ini.Read("kiosk", "kioskid")));
                _session.CamPortNo = Convert.ToInt32(ini.Read("Ports", "SerialPort"));
                _session.CamPortName = "COM" + _session.CamPortNo.ToString();

                _session.DispenserPortNo = Convert.ToInt32(ini.Read("Ports", "DispenserPort"));
                _session.DispenserPortName = "COM" + _session.DispenserPortNo.ToString();
                
            }
            catch(Exception eerr){}

            try
            {
                Process[] pname = Process.GetProcessesByName("kioskEngine");
                if (pname.Length == 0)
                {
                    Process firstProc = new Process();
                    firstProc.StartInfo.FileName =  @"C:\Program Files\Qtec\KioskEngineV2.0.3\kioskEngine.exe";
                    firstProc.EnableRaisingEvents = true;

                    firstProc.Start();
                }
            }
            catch { }


            try
            {
                Process[] pname = Process.GetProcessesByName("kioskEngine");
                if (pname.Length != 0)
                {
                    foreach (Process worker in pname)
                    {
                        worker.Kill();
                        worker.WaitForExit();
                        worker.Dispose();
                    }
                }

                try
                {
                    Process firstProc = new Process();
                    firstProc.StartInfo.FileName = @"C:\Program Files\Qtec\kiosk engine\kioskEngine.exe";
                    firstProc.EnableRaisingEvents = true;

                    firstProc.Start();
                    firstProc.Dispose();
                }
                catch (Exception ee) { }


            }
            catch (Exception ee) { MessageBox.Show("can't start the engine"); return; }
            //try
            //{
            //    _session.cst1 = fromyoutube.get_cassettes_types(Convert.ToInt32(_session.KioskID), out _session.cst2, out _session.cst3, out _session.cst4);
            //}
            //catch (Exception cc) { _session.errorecode += "no web service: "+ cc.Message.ToString(); }

        }
       
    }
}
