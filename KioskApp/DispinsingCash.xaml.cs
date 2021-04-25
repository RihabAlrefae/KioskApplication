using AForge.Video.DirectShow;
using KioskApp.HelloWord;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
using System.Data.OracleClient;
using System.ComponentModel;
using DispenserAlgorithm;
using System.Windows.Threading;
using System.Data.SqlClient;
using System.Configuration;
using System.Security.Cryptography;
using KioskApp.Clases;

namespace KioskApp
{
    /// <summary>
    /// Interaction logic for DispinsingCash.xaml
    /// </summary>
    public partial class DispinsingCash : Window
    {

        // WMPLib.WindowsMediaPlayer sound1;
        CameraImaging camimg = new CameraImaging();
       // Thread thrVideo;
        public Bitmap tmp;
        public int printerStatus = 1;
      //  private KioskAppWebServiceSoapClient fromyoutube = new KioskAppWebServiceSoapClient();
        System.Windows.Threading.DispatcherTimer dispatcherTimer1 = new System.Windows.Threading.DispatcherTimer();
        System.IO.Ports.SerialPort myPort = new System.IO.Ports.SerialPort(_session.CamPortName, 9600, System.IO.Ports.Parity.None, 8, System.IO.Ports.StopBits.One);
        System.IO.Ports.SerialPort dispensport = new System.IO.Ports.SerialPort(_session.DispenserPortName, 9600, System.IO.Ports.Parity.None, 8, System.IO.Ports.StopBits.One);
        Thread rh;
        Thread rh1;
        private int fromhundreds;
        private int fromfivehunderds;
        private int fromalldrawers;
        int retractflag = 0;
        WMPLib.WindowsMediaPlayer sound1;
        private void PlayFile(String url)
        {
            sound1 = new WMPLib.WindowsMediaPlayer();
            sound1.PlayStateChange +=
                new WMPLib._WMPOCXEvents_PlayStateChangeEventHandler(Player_PlayStateChange);
            sound1.MediaError +=
                new WMPLib._WMPOCXEvents_MediaErrorEventHandler(Player_MediaError);
            sound1.URL = url;
            try
            {
                sound1.controls.play();
            }
            catch(Exception ){}
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
        public DispinsingCash()
        {
            InitializeComponent();
            bcworker.DoWork += Worker_DoWork;
            bcworker.RunWorkerCompleted += bcworker_RunWorkerCompleted;
        }
        void bcworker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null){}
            else if (e.Cancelled) { }
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
            bcworker.Dispose();
            //bcworker1.Dispose();
            bcworker.DoWork -= Worker_DoWork;
            bcworker.RunWorkerCompleted -= bcworker_RunWorkerCompleted;
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                camimg.videoSource = new VideoCaptureDevice(camimg.videoDevices[0].MonikerString);
                // System.Windows.MessageBox.Show(camimg.videoDevices[1].Name);
                camimg.videoSource.DesiredFrameRate = 10;
                camimg.videoSource.DesiredFrameSize = new System.Drawing.Size(800, 600);
                camimg.videoSource.Start();
                // VideoRecording();
               
                Thread.Sleep(1000);
                Thread.Sleep(33);
                tmp = (Bitmap)camimg.bitmap.Clone();
                camimg.bitmap.Dispose();
               // string filename = "confirm" + Convert.ToString(DateTime.Now) + ".Jpeg";
               // string newfile = filename.Replace("/", "-");
              //  newfile = newfile.Replace(":", " ");
                //  System.Windows.MessageBox.Show(newfile);
               // string imgpath = System.IO.Path.Combine(@"\" + _session.serverpath, newfile);
                using (var m = new MemoryStream())
                {
                    tmp.Save(m, System.Drawing.Imaging.ImageFormat.Jpeg);
                    var image = System.Drawing.Image.FromStream(m);
                    try
                    {
                        image.Save(imgpath);
                    }
                    catch (Exception rr) { }
                    try
                    {
                        image.Dispose();
                        m.Dispose();
                    }
                    catch (Exception tt)
                    {
                        // MessageBox.Show(tt.Message);
                    }
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
                    // string imgpath = System.IO.Path.Combine(@"" + _session.serverpath, newfile);
                    using (var m = new MemoryStream())
                    {
                        tmp.Save(m, System.Drawing.Imaging.ImageFormat.Jpeg);
                        var image = System.Drawing.Image.FromStream(m);
                        try
                        {
                            image.Save(imgpath);
                        }
                        catch (Exception rr) { }
                        try
                        {
                            image.Dispose();

                            m.Dispose();
                        }
                        catch (Exception tt)
                        {
                            //MessageBox.Show(tt.Message); 
                        }

                    }
                    length = new System.IO.FileInfo(imgpath).Length;
                    tmp.Dispose();
                }
                _session.image3 = imgpath;
            }
            catch (Exception ee)
            {
                tmp.Dispose();
                _session.errdescroption += "// couldnt take the 'take cash' photo // ";
            }
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
        }

        private byte[] HexStringToByteArray(string s)
        {
            s = s.Replace(" ", "");
            byte[] buffer = new byte[s.Length / 2];
            for (int i = 0; i < s.Length; i += 2)
                buffer[i / 2] = (byte)Convert.ToByte(s.Substring(i, 2), 16);
            return buffer;
        }

        private string ByteArrayToHexString(byte[] data)
        {
            StringBuilder sb = new StringBuilder(data.Length * 3);
            foreach (byte b in data)
                sb.Append(Convert.ToString(b, 16).PadLeft(2, '0').PadRight(3, ' '));
            return sb.ToString().ToUpper();
        }


        //public bool finalnumber(int cash)
        //{
        //    bool result = false;
        //    int rest_cash = cash;
        //    fromhundreds = 0;
        //    // rest_cash = rest_cash - (cash % 500);
        //    fromfivehunderds = (rest_cash % 1500) / 500;
        //    if (fromfivehunderds > 0 & _session.fcst4 == 0 & _session.fcst3 == 0) { _session.errorecode += " // empty 3&4 caseete //"; return false; }
        //    rest_cash = rest_cash - (rest_cash % 1500);
        //    fromalldrawers = rest_cash / 1500;
        //    fromfivehunderds = fromfivehunderds + fromalldrawers;
        //    int finalnumber = fromfivehunderds + fromalldrawers;
        //    //if (_session.fcst1 + _session.fcst2 < fromalldrawers & _session.fcst3 + _session.fcst4 > fromfivehunderds + fromalldrawers * 2)
        //    //{
        //    //    finalnumber =fromfivehunderds = fromfivehunderds + fromalldrawers * 2;
        //    //    fromalldrawers=0;
        //    //}
        //    //else 
        //    if (fromfivehunderds - fromalldrawers==0)
        //    {
        //        if ((_session.fcst3 + _session.fcst4 < fromfivehunderds) & _session.fcst1 + _session.fcst2 >= fromalldrawers + (fromfivehunderds / 2))
        //        {
        //            fromalldrawers = fromalldrawers + (fromfivehunderds / 2);
        //            fromfivehunderds = 0;
        //        }
        //    }
        //   // if (_session.fcst1 + _session.fcst2)
        //    if (_session.fcst1 + _session.fcst2 < fromalldrawers || _session.fcst3 + _session.fcst4 < fromfivehunderds)
        //    {
        //        _session.errorecode += " // no money  //";
        //        return false;
        //    }

        //    if (finalnumber > 40)
        //    {
               
        //        if (_session.language == 1)
        //        {
        //            TextBlock_1.Text = "الرجاء الانتظار لانتهاء جميع الدفعات ";
        //        }
        //        else
        //        {
        //            TextBlock_1.FontFamily = new System.Windows.Media.FontFamily("Tw Cen MT Condensed Extra Bold");
        //            TextBlock_1.FontSize = 42;
        //            TextBlock_1.Text = "please wait for all the Payments ";
        //        }

        //        result = teest(finalnumber);
        //    }
        //    else
        //    {
               
        //        result = dispence(fromalldrawers, 0, fromfivehunderds, 0);
        //        if (result == true )
        //        {
        //            _session.resualt = 2;
        //            _session.thetruthresault = 2;
        //            //try { fromyoutube.updatesalstat(_session.empId, ChooseSubServ.servicetype); }
        //            //catch (Exception err)
        //            //{
        //            //    //MessageBox.Show(err.Message);
        //            //    _session.empPath = _session.empPath + "// we couldnt update salary status // ";
        //            //}
        //        }

        //    }

        //   // _session.thetruthresault = 2;
        //    _session.number_of_peapers = finalnumber;
        //    return result;
        //}
        public bool newalgorithm(int cash)
        {
            bool d = false;
            Note ncs1 = Note.OLD_500;
            Note ncs2 = Note.OLD_500;
            Note ncs3 = Note.OLD_500;
            Note ncs4 = Note.OLD_500;

            int batchLimit = 40;//عدد الاوراق بالدفعة
            int cassettesCount = 4;//عدد الدروج
            Cassette[] cassettes = new Cassette[cassettesCount];//مصفوفة الدروج
            switch (_session.cst1)
            {
                case 500:
                    ncs1 = Note.OLD_500;
                    break;
                case 1000:
                    ncs1 = Note.OLD_1000;
                    break;
                case 2000:
                    ncs1 = Note._2000;
                    break;
            }
            switch (_session.cst2)
            {
                case 500:
                    ncs2 = Note.OLD_500;
                    break;
                case 1000:
                    ncs2 = Note.OLD_1000;
                    break;
                case 2000:
                    ncs2 = Note._2000;
                    break;
            }
            switch (_session.cst3)
            {
                case 500:
                    ncs3 = Note.NEW_500;
                    break;
                case 1000:
                    ncs3 = Note.NEW_1000;
                    break;
                case 2000:
                    ncs3 = Note._2000;
                    break;
            }
            switch (_session.cst4)
            {
                case 500:
                    ncs4 = Note.NEW_500;
                    break;
                case 1000:
                    ncs4 = Note.NEW_1000;
                    break;
                case 2000:
                    ncs4 = Note._2000;
                    break;
            }
            cassettes[0] = new Cassette(ncs1, _session.fcst1);
            cassettes[1] = new Cassette(ncs2, _session.fcst2);
            cassettes[2] = new Cassette(ncs3, _session.fcst3);
            cassettes[3] = new Cassette(ncs4, _session.fcst4);

            SalaryPayable salaryPayable = Dispenser.IsSalaryPayable(cassettes, cash, batchLimit);
            if (salaryPayable.IsPayable)
            {
                int counter = 1;
                int updated = -5;


                try
                {
                    updated = WebServiseFunctions.updatethetrue_Class(4, Convert.ToInt32(_session.payRec), _session.paidsalary, Convert.ToInt32(ChooseSubServ.servicetype));
                }
                catch (Exception) { _session.errorecode += " //  network/webservice error"; return false; }
                //  Console.WriteLine("Batches: " + salaryPayable.Batches.Count);
                if (updated > 0)
                {
                    foreach (Cassette[] batch in salaryPayable.Batches)// عدد الباتشات
                    {
                        // int counter = 2;
                        if (salaryPayable.Batches.Count > 1)
                        {
                            if (_session.language == 1)
                            {
                                TextBlock_1.Text = "الرجاء الانتظار لانتهاء جميع الدفعات ";
                                TextBlock_1_Copy.Text = counter.ToString();// +" من " + (therealnumberofpatches + thefirst).ToString();
                                TextBlock_1_Copy1.Text = " من ";
                                TextBlock_1_Copy2.Text = salaryPayable.Batches.Count.ToString();
                            }
                            else
                            {
                                TextBlock_1.FontFamily = new System.Windows.Media.FontFamily("Tw Cen MT Condensed Extra Bold");
                                TextBlock_1.FontSize = 42;
                                TextBlock_1.Text = "please wait for all the Payments ";
                                TextBlock_1_Copy.Text = salaryPayable.Batches.Count.ToString();
                                TextBlock_1_Copy1.FontFamily = new System.Windows.Media.FontFamily("Tw Cen MT Condensed Extra Bold");
                                TextBlock_1_Copy1.FontSize = 42;// +" من " + (therealnumberofpatches + thefirst).ToString();
                                TextBlock_1_Copy1.Text = " from ";
                                TextBlock_1_Copy2.Text = counter.ToString();
                            }
                        }
                        int[] fc = new int[batch.Length];
                        for (int i = 0; i < batch.Length; i++)//حسب عدد الدروج
                        {

                            // Console.Write(batch[i].Count);
                            fc[i] = Convert.ToInt32(batch[i].Count);

                            if (i < batch.Length - 1)
                            {
                            }
                            // Console.Write(", ");
                            else
                            {
                                d = dispence(fc[0], fc[1], fc[2], fc[3]);
                                if (!d) { return d; }
                            }
                        }
                        counter++;
                    }
                }
              //  else { _session.errorecode += " //  the cash can't be paid"; }
            }
           // else { _session.errorecode += "webservice error"; }
            else { _session.errorecode += " //  the cash can't be paid"; }
            return d;
        }
        DispatcherTimer counterOne;
        int counterOneTime;
        int thefirst= 0;
         int firstpatchflag =0;
       
        Byte[] oo = new byte[150];
        Byte[] ucdmstatus = new byte[150];
        public bool dispence(int first, int second, int third, int fourth)
        {

            Byte d = 0;
            bool o = false;
            //using (var ddd = new dispinsingClass())
            //{
            o = dispinsingClass.CDM1_Open((Byte)_session.DispenserPortNo);

            // MessageBox.Show(dispensport.IsOpen.ToString());
            if (o == true)
            {
                //oo = new byte[90];
                d = dispinsingClass.CDM1_Dispense(first, second, third, fourth, oo);
                if (d == 29)
                {
                    d = dispinsingClass.CDM1_Dispense(first, second, third, fourth, oo);
                }
                if (d != 0  && d!=65)
                {
                  //  _session.resualt = false;
                    _session.errorecode += " dispenser error :" + d.ToString();
                    insertErrorCodeNotification(d.ToString(), _session.errorecode);
                    bool c = dispinsingClass.CDM1_Close();
                    this.Dispose();
                    return false;
                }


                Byte pp = dispinsingClass.CDM1_Present();
                if (pp != 0)// && pp!=65
                {
                    _session.errorecode += " present error :" + pp.ToString();
                    insertErrorCodeNotification(pp.ToString(), _session.errorecode);
                    bool c = dispinsingClass.CDM1_Close();
                    IDisposable eee = null;
                    //  eee.Dispose();
                    //  dispinsingClass dss = new dispinsingClass();
                    // dss.Dispose();
                    this.Dispose();
                    return false;

                }
                Byte os = dispinsingClass.CDM1_OpenShutter();
                if (os != 0)
                {
                    _session.errorecode += " open shutter error :" + os.ToString();
                    insertErrorCodeNotification(os.ToString(), _session.errorecode);
                    bool c = dispinsingClass.CDM1_Close();
                    this.Dispose();
                    return false;
                }

               

                rh1 = new Thread(new ThreadStart(beepsound));
                rh1.IsBackground = true;
                rh1.Start();
                counterOne = new DispatcherTimer();
                counterOne.Tick += new EventHandler(counterOne_Tick);
                counterOne.Interval = new TimeSpan(0, 0, 1);

                counterOneTime = 10;
                counterOne.Start();
                // ucdmstatus = new byte[90];
                // Thread.Sleep(2500);
                long StartTicks = DateTime.Now.Ticks;
                double timeElapsed = 0;
                bool banknote = true;
             //   while((timeElapsed<=10) && (banknote==true))
                while(true)
                {
                    Byte ss = dispinsingClass.CDM1_Status(ucdmstatus);
                    if (ucdmstatus[8] == 0)
                    {
                        banknote=false;
                        break;
                    }
                    timeElapsed=(DateTime.Now.Ticks-StartTicks)/10000000;
                }
                //Thread.Sleep(5000);
                //Byte ss = dispinsingClass.CDM1_Status(ucdmstatus);
                
                if (banknote == false)
                {
                    try
                    {
                        if(rh1!=null)
                        rh1.Abort();
                    }
                    catch (Exception) { }
                    counterOne.Stop();
                    //alarm_text.Text = "";
                    if (ucdmstatus[11] == 0)//check the exit sensor
                    {
                        _session.resualt = 2;
                        _session.thetruthresault = 2;

                        _session.paidsalary += (oo[0] * _session.cst1) + (oo[4] * _session.cst2) + (oo[8] * _session.cst3) + (oo[12] * _session.cst4);
                        //dispatcherTimer2.Stop();
                        _session.restofthecash -= (oo[0] * _session.cst1) + (oo[4] * _session.cst2) + (oo[8] * _session.cst3) + (oo[12] * _session.cst4);
                        Byte cs = dispinsingClass.CDM1_CloseShutter();
                        bool c = dispinsingClass.CDM1_Close();
                        IDisposable eee = null;
                        //  eee.Dispose();
                       // dispinsingClass dss = new dispinsingClass();
                      //  dss.Dispose();
                        this.Dispose();
                    }
                    else//if the checkout sensor is clear but the exit sensor not 
                    {
                        _session.errorecode += "// the check out sensor is clear but the exit sensor is not //";
                        insertErrorCodeNotification(ucdmstatus[11].ToString(), _session.errorecode);
                        Byte cs = dispinsingClass.CDM1_CloseShutter();
                        bool c = dispinsingClass.CDM1_Close();
                        IDisposable eee = null;
                        //  eee.Dispose();
                      //  dispinsingClass dss = new dispinsingClass();
//dss.Dispose();
                        this.Dispose();
                        return false;
                    }
                   
                    if (timeElapsed >= 30)
                    {
                        _session.thetruthresault = 3;
                        _session.resualt = 3;
                        try {
                            int? resault = WebServiseFunctions.updatesalstat_Class(_session.empId, ChooseSubServ.servicetype); }
                        catch (Exception err)
                        {
                            //  MessageBox.Show(err.Message); d
                            _session.empPath = _session.empPath + "// we couldnt update salary status // ";
                        }
                        _session.errdescroption += "// time out //";
                        _session.errorecode += "// time out //";
                        retractflag = 1;

                        return false;
                    }
                    return true;
                }
                else
                {
                    try
                    {
                        if(rh1!=null)
                        rh1.Abort();
                    }
                    catch(Exception){}
                    counterOne.Stop();
                    //alarm_text.Text = "";
                    _session.thetruthresault = 3;
                    _session.resualt = 3;
                  //  Byte cs = dispinsingClass.CDM1_CloseShutter();
                    Byte retract = dispinsingClass.CDM1_Retract();
                    //  Boolean cs = CDM_CloseShutter();
                    _session.empPath += "retract  // ";
                    _session.errdescroption += "// retract //";
                    retractflag = 1;
                    try {int? resault= WebServiseFunctions.updatesalstat_Class(_session.empId, ChooseSubServ.servicetype); }
                    catch (Exception err)
                    {
                        //  MessageBox.Show(err.Message); d
                        _session.empPath = _session.empPath + "// we couldnt update salary status // ";
                    }
                    _session.rejectedbox1 = _session.rejectedbox1 + _session.dispinse11;
                    _session.rejectedbox2 = _session.rejectedbox2 + _session.dispinse22;
                    _session.rejectedbox3 =_session.rejectedbox3  + _session.dispinse33;
                    _session.rejectedbox4 =_session.rejectedbox4  + _session.dispinse44;
                    bool c = dispinsingClass.CDM1_Close();
                   // dispinsingClass dss = new dispinsingClass();
                  //  dss.Dispose();
                    this.Dispose();
                    return false;
                }

            }
            else
            {
                ///
                /// 03-2018
                ///
                if (_session.paidsalary != 0)
                {
                    if (ChooseSubServ.servicetype == 1)
                    {
                        if (_session.paidsalary == _session.salary)
                        {
                            try
                            {
                                int updated = WebServiseFunctions.updatethetrue_Class(1, Convert.ToInt32(_session.payRec), _session.restofthecash, Convert.ToInt32(ChooseSubServ.servicetype));
                            }
                            catch (Exception newexc)
                            {
                                //  MessageBox.Show(newexc.Message.ToString());
                            }
                        }
                        else
                        {
                            try
                            {
                                int updated = WebServiseFunctions.updatethetrue_Class(2, Convert.ToInt32(_session.payRec), _session.restofthecash, Convert.ToInt32(ChooseSubServ.servicetype));
                            }
                            catch (Exception newexc)
                            {
                                //  MessageBox.Show(newexc.Message.ToString());
                            }
                        }
                    }
                    else if (ChooseSubServ.servicetype == 2)
                    {
                        if (_session.paidsalary == _session.recomp)
                        {
                            try
                            {
                                int updated = WebServiseFunctions.updatethetrue_Class(1, Convert.ToInt32(_session.payRec), _session.restofthecash, Convert.ToInt32(ChooseSubServ.servicetype));
                            }
                            catch (Exception newexc)
                            {
                                //  MessageBox.Show(newexc.Message.ToString());
                            }
                        }
                        else
                        {
                            try
                            {
                                int updated = WebServiseFunctions.updatethetrue_Class(2, Convert.ToInt32(_session.payRec), _session.restofthecash, Convert.ToInt32(ChooseSubServ.servicetype));
                            }
                            catch (Exception newexc)
                            {
                                //  MessageBox.Show(newexc.Message.ToString());
                            }
                        }
                    }

                }
                else
                {
                    try
                    {
                        int updated = WebServiseFunctions.updatethetrue_Class(0, Convert.ToInt32(_session.payRec), _session.restofthecash, Convert.ToInt32(ChooseSubServ.servicetype));
                    }
                    catch (Exception newexc)
                    {
                        //  MessageBox.Show(newexc.Message.ToString());
                    }
                }
                _session.empPath += "dispense faild :the dispernser is not connected // ";
                ///
                /// 03-2018
                ///
                //Byte cs = dispinsingClass.CDM1_CloseShutter();
                ///
                //bool c = dispinsingClass.CDM1_Close();
                _session.errorecode += " dispense faild:the dispernser is not connected //";
                return false;
            }
        }
        /// <summary>
        /// العداد مشان الريتراكت 
        /// طلب شادي اني اعملها 
        /// بعدين طلب اني شيلها :(
        /// </summary>
        private void counterOne_Tick(object sender, EventArgs e)
        {
            if (counterOneTime > 0)
            {
                counterOneTime--;
                //alarm_text.Text = Convert.ToString( counterOneTime);
            }
            else
                counterOne.Stop();
        }
     
        BackgroundWorker bcworker = new BackgroundWorker();
        struct dataparameter
        {
            public int process;
            public int delay;
        }
        private dataparameter _inputparameter;
        string filename = "";
        string newfile = "";
        string imgpath = "";
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           

            try
            {
                if(rh!=null)
                rh.Abort();
            }
            catch (Exception ecxe)
            {
                // MessageBox.Show(ecxe.Message);
            }
            myPort = new System.IO.Ports.SerialPort(_session.CamPortName, 9600, System.IO.Ports.Parity.None, 8, System.IO.Ports.StopBits.One);
            if (myPort.IsOpen == false) //if not open, open the port
                try
                {
                    myPort.Open();
                }
                catch (Exception ex)
                {
                    myPort.Close();
                    //MessageBox.Show(ex.Message);
                }
            byte[] thebytesarray1 = HexStringToByteArray("1b 30 02");
            Thread.Sleep(200);
            try
            {
                myPort.Write(thebytesarray1, 0, thebytesarray1.Length);
            }
            catch (Exception ww)
            {
                // MessageBox.Show(ww.Message); 
            }
            if (myPort.IsOpen == true)
            {
                rh = new Thread(new ThreadStart(dispenserflashing));
                rh.IsBackground = true;
                rh.Start();
            }
            //string str = Environment.GetFolderPath(
            //System.Environment.SpecialFolder.DesktopDirectory);
            Wait_Screen.Background = new ImageBrush(new BitmapImage(new Uri(@"D:\\backGroundImage\\HADEEDBackground.png")));

            if (_session.language == 2)
            {
                TextBlock_1.FontFamily = new System.Windows.Media.FontFamily("Tw Cen MT Condensed Extra Bold");
                TextBlock_1.FontSize = 42;
                TextBlock_1.Text = "Please Wait The Money Is Dispensing, Get Your Receipt & Check The Cash.";
                //btnLogOut.Content = "Log out";
            }
            dispatcherTimer1.Tick +=dispatcherTimer1_Tick;
            dispatcherTimer1.Interval = new TimeSpan(0, 0, 3);
            dispatcherTimer1.Start();
        }
        private void dispenserflashing()
        {
            byte[] thebytesarray;
            for (int i = 1; i > 0; )
            {
                thebytesarray = HexStringToByteArray("1b 33 02");
                Thread.Sleep(200);
                try
                {
                    myPort.Write(thebytesarray, 0, thebytesarray.Length);
                }
                catch (Exception m1)
                {
                    // MessageBox.Show(m1.Message);
                }
                Thread.Sleep(50);
                thebytesarray = HexStringToByteArray("1b 33 01");
                Thread.Sleep(200);
                try
                {
                    myPort.Write(thebytesarray, 0, thebytesarray.Length);
                }
                catch (Exception m1)
                {
                    // MessageBox.Show(m1.Message); 
                }
            }

        }
        
        private void dispatcherTimer1_Tick(object sender, EventArgs e)
        {
            dispatcherTimer1.Stop();
            dispatcherTimer1.Tick -= dispatcherTimer1_Tick;
            bool visitresalt = false;
            // dispence();
            bool thenumber1 = true;
            _session.restofthecash = Convert.ToInt32(_session.fullcash);
            if (ChooseSubServ.servicetype == 1)
            {
                thenumber1 = newalgorithm(_session.salary);
                try
                {
                    _session.payRec = WebServiseFunctions.paymentrecid_Class(_session.empId, 1);
                }
                catch (Exception error)
                { }
            }
            if (ChooseSubServ.servicetype == 2)
            {
                thenumber1 = newalgorithm(Convert.ToInt32(_session.recomp));
                try
                {
                    _session.payRec = WebServiseFunctions.paymentrecid_Class(_session.empId, 2);
                }
                catch (Exception error)
                { }
            }
            string file_name = "visits log " + Convert.ToString(DateTime.Today) + ".log";
            string newfile = file_name.Replace("/", "-");

            newfile = newfile.Replace(":", " ");
            try
            {
                writingLogs.writing_visit_log("D:\\visitlogsfolder\\" + newfile, _session.tranactionid + "," + _session.empId + "," + _session.KioskID + "," + Convert.ToString(_session.empPath) + "," + _session.number_of_patches + "," + _session.number_of_peapers + "," + Convert.ToString(DateTime.Now) + "\n");
            }
            catch (Exception eee)
            {
              //  MessageBox.Show(eee.Message);
            }

            // 2 log (finantial)
            string file_name2 = "finantial log " + Convert.ToString(DateTime.Today) + ".log";
            string newfile2 = file_name2.Replace("/", "-");

            newfile2 = newfile2.Replace(":", " ");
            try
            {
                using (MD5 md5Hash = MD5.Create())
                {
                    string hash = GetMd5Hash(md5Hash, Convert.ToString(_session.fullcash));
                    string hash1 = GetMd5Hash(md5Hash, Convert.ToString(_session.paidsalary));
                    writingLogs.writing_finantial_log("D:\\finantialogsfolder\\" + newfile2, _session.tranactionid + "," + _session.empId + "," + _session.KioskID + "," + _session.number_of_peapers + ", full cash:" + hash + ", paid cash:" + hash1 + "," + Convert.ToString(DateTime.Now) + "\n");
                }
            }
            catch(Exception){}
            filename = "confirm" + _session.empId.ToString() + "-" + Convert.ToString(DateTime.Now) + ".Jpeg";
             newfile = filename.Replace("/", "-");
            newfile = newfile.Replace(":", " ");
            //  System.Windows.MessageBox.Show(newfile);
             imgpath = System.IO.Path.Combine(@"\" + _session.serverpath, newfile);
            _session.image3 = imgpath;
            if (!bcworker.IsBusy)
            {
                bcworker.WorkerSupportsCancellation = true;
                _inputparameter.delay = 100;
                _inputparameter.process = 1200;
                bcworker.RunWorkerAsync(_inputparameter);
            }
            
                _session.fcst2 = _session.fcst2 - _session.dispinse2 ;
               // _session.fcst1 = 0;
            
                _session.fcst1 = _session.fcst1 - _session.dispinse1;
            
           
                _session.fcst4 = _session.fcst4 - _session.dispinse4 ;
               // _session.fcst3 = 0;
           
                _session.fcst3 = _session.fcst3 - _session.dispinse3;
            
            //_session.rejectedbox1 = _session.rejectedbox1 + _session.rejected1;
            //_session.rejectedbox2 = _session.rejectedbox2 + _session.rejected2;

            try
            {
                int? resault=WebServiseFunctions.updatecurrentcasettevalue_Class(Convert.ToInt32(_session.KioskID), _session.fcst1, _session.fcst2, _session.fcst3, _session.fcst4, _session.rejectedbox1, _session.rejectedbox2, _session.rejectedbox3, _session.rejectedbox4);
            }
            catch (Exception newexe)
            {
                //  MessageBox.Show(newexe.Message);
                // MessageBox.Show("error");
            }
            try
            {
                if (rh != null)
                rh.Abort();
            }
            catch (Exception ecxe)
            {
                // MessageBox.Show(ecxe.Message); 
            }
            if (myPort.IsOpen == false) //if not open, open the port
                try
                {
                    myPort.Open();
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message); 
                }
            byte[] thebytesarray1 = HexStringToByteArray("1b 30 02");
            Thread.Sleep(200);
            try
            {
                myPort.Write(thebytesarray1, 0, thebytesarray1.Length);
            }
            catch (Exception ww)
            {
                // MessageBox.Show(ww.Message); 
            }
            if (myPort.IsOpen == true)
            {
                rh = new Thread(new ThreadStart(printerflashing));
                try
                {
                    rh.IsBackground = true;
                    rh.Start();
                }
                catch (Exception edd)
                {
                    //   MessageBox.Show(edd.Message); 
                }
            }
            if (thenumber1 == true)
            {
                _session.thetruthresault = 1;
                _session.resualt = 1;
                try {int? resault= WebServiseFunctions.updatesalstat_Class(_session.empId, ChooseSubServ.servicetype); }
                catch (Exception err)
                {
                    //MessageBox.Show(err.Message);
                    _session.empPath = _session.empPath + "// we couldnt update salary status: " +err.Message.ToString()+ " // ";
                }
                _session.paidsalary = Convert.ToInt32(_session.fullcash);
                
                try
                {
                    int updated = WebServiseFunctions.updatethetrue_Class(_session.thetruthresault, Convert.ToInt32(_session.payRec), _session.restofthecash, Convert.ToInt32(ChooseSubServ.servicetype));
                }
                catch (Exception newexc)
                {
                    _session.errdescroption += " fromyoutube.updatethetrue_resault :" + newexc.Message.ToString();
                  //  MessageBox.Show(newexc.Message.ToString());
                }
                
                if (ChooseSubServ.servicetype == 2) // service typwe ==1 :salary  , 2:others
                {
                    try
                    {
                        if(printerStatus==0)
                            visitresalt = WebServiseFunctions.insertvisit1_Class(Convert.ToInt32(_session.KioskID), _session.tranactionid, _session.empId, _session.resualt, _session.paidsalary, 0, Convert.ToInt32(_session.payRec), _session.image1, _session.image2, _session.image3,"The Employee has chosen to not receive a receipt");
                       else
                       visitresalt = WebServiseFunctions.insertvisit_Class(Convert.ToInt32(_session.KioskID), _session.tranactionid, _session.empId, _session.resualt, _session.paidsalary, 0, Convert.ToInt32(_session.payRec), _session.image1, _session.image2, _session.image3);
                    }
                    catch (Exception eee) 
                    {
                        //MessageBox.Show(eee.Message.ToString()); 
                    }
                    SqlConnection pendingtransconn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["kioskApp.Properties.Settings.sqlconnstring"].ConnectionString);
                     pendingtransconn.Open();
                   // string command = "insert into pending_trans as t values("+_session.tranactionid+","+Convert.ToInt32(_session.KioskID)+","+_session.empId+","+DateTime.Now+","+_session.resualt+","+ _session.image1+","+ _session.image2+","+ _session.image3+","+ _session.paidsalary+",0,"+Convert.ToInt32(_session.payRec)+")";
                     string command = "insert into pending_trans(TRANSICATION_ID,EMPLOYEEID,RESULT,IMG_1,IMG_2,IMG_3,PAIDSALARY,SALRECID,RECOMPRECID) values('" + _session.tranactionid + "'," + _session.empId + "," + _session.resualt + ",'" + _session.image1 + "','" + _session.image2 + "','" + _session.image3 + "'," + _session.paidsalary + "," + Convert.ToInt32(_session.payRec) + ",0)";
                    SqlCommand sqlcomm = new SqlCommand(command, pendingtransconn);
                    int q =sqlcomm.ExecuteNonQuery();
                    if (!visitresalt)
                    {
                        _session.errdescroption += "// the visit didn't insert: // " +_session.empId.ToString();
                //        MessageBox.Show("the visit didn't insert");
                    }
                    if(printerStatus==1)
                    for (int i = 0; i < 3; i++)
                    {
                        try
                        {
                            int s1 = 0;
                            int s2 = 0;
                            int s3 = 0;
                            int s4 = 0;
                            int s5 = 0;
                            int s6 = 0;
                            int s7 = 0;
                            int s8 = 0;
                            int s9 = 0;
                            int s10 = 0;
                            int tt = 0;
                            string n = "";
                            s1 = WebServiseFunctions.employeerecompforrecipt_Class(Convert.ToInt32(_session.payRec), out s2, out s3, out s4, out s5, out s6, out s7, out s8, out s9, out s10, out tt, out n);

                            string sr1 = "";
                            string sr2 = "";
                            string sr3 = "";
                            string sr4 = "";
                            string sr5 = "";
                            string sr6 = "";
                            string sr7 = "";
                            string sr8 = "";
                            string sr9 = "";
                            string sr10 = "";
                            sr1 = WebServiseFunctions.servicesnames_Class(out sr2, out sr3, out sr4, out sr5, out sr6, out sr7, out sr8, out sr9, out sr10);


                            recompReciept rr = new recompReciept();
                            // rr.SetDataSource(dt.Tables[0]);
                            // rr.SetDatabaseLogon("sa", "JamesClerkMaxwell");
                            rr.SetParameterValue("empid", _session.empId);
                            rr.SetParameterValue("s1", s1);
                            rr.SetParameterValue("s2", s2);
                            rr.SetParameterValue("s3", s3);
                            rr.SetParameterValue("s4", s4);
                            rr.SetParameterValue("s5", s5);
                            rr.SetParameterValue("s6", s6);
                            rr.SetParameterValue("s7", s7);
                            rr.SetParameterValue("s8", s8);
                            rr.SetParameterValue("s9", s9);
                            rr.SetParameterValue("s10", s10);
                            rr.SetParameterValue("total", tt);
                            rr.SetParameterValue("sr1", sr1);
                            rr.SetParameterValue("sr2", sr2);
                            rr.SetParameterValue("sr3", sr3);
                            rr.SetParameterValue("sr4", sr4);
                            rr.SetParameterValue("sr5", sr5);
                            rr.SetParameterValue("sr6", sr6);
                            rr.SetParameterValue("sr7", sr7);
                            rr.SetParameterValue("sr8", sr8);
                            rr.SetParameterValue("sr9", sr9);
                            rr.SetParameterValue("sr10", sr10);
                            rr.SetParameterValue("nots", n);


                            // rr.SetParameterValue("recid", _session.payRec);
                            rr.SetParameterValue("tranid", _session.tranactionid);
                            rr.PrintToPrinter(1, false, 0, 0); rr.Dispose();// print the receipt of recom
                            rr.Close();
                            Thread.Sleep(300);
                            break;
                        }
                        catch (Exception rtt)
                        {
                            //  MessageBox.Show(rtt.Message); 
                            _session.errdescroption += "// " + i.ToString() + " error printing :" + rtt.Message.ToString() + "// ";
                        }
                    }
                }
                if (ChooseSubServ.servicetype == 1)
                {
                    
                    try
                    {
                        if (printerStatus == 0)
                            visitresalt = WebServiseFunctions.insertvisit1_Class(Convert.ToInt32(_session.KioskID), _session.tranactionid, _session.empId, _session.resualt, _session.paidsalary, Convert.ToInt32(_session.payRec), 0, _session.image1, _session.image2, _session.image3, "The Employee has chosen to not receive a receipt");
                        else
                        visitresalt = WebServiseFunctions.insertvisit_Class(Convert.ToInt32(_session.KioskID), _session.tranactionid, _session.empId, _session.resualt, _session.paidsalary, Convert.ToInt32(_session.payRec), 0, _session.image1, _session.image2, _session.image3);
                    }
                    catch (Exception eee) { 
                      //  MessageBox.Show(eee.Message.ToString()); 
                    }
                    SqlConnection pendingtransconn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["kioskApp.Properties.Settings.sqlconnstring"].ConnectionString);
                    pendingtransconn.Open();
                    //string command = "insert into pending_trans as t values(" + _session.tranactionid + "," + Convert.ToInt32(_session.KioskID) + "," + _session.empId + "," + DateTime.Now + "," + _session.resualt + "," + _session.image1 + "," + _session.image2 + "," + _session.image3 + "," + _session.paidsalary + "," + Convert.ToInt32(_session.payRec) + ",0)";
                    string command = "insert into pending_trans(TRANSICATION_ID,EMPLOYEEID,RESULT,IMG_1,IMG_2,IMG_3,PAIDSALARY,SALRECID,RECOMPRECID) values('" + _session.tranactionid + "'," + _session.empId + "," + _session.resualt + ",'" + _session.image1 + "','" + _session.image2 + "','" + _session.image3 + "'," + _session.paidsalary + "," + Convert.ToInt32(_session.payRec) + ",0)";
                    SqlCommand sqlcomm = new SqlCommand(command, pendingtransconn);
                    int q = sqlcomm.ExecuteNonQuery();
                    if (!visitresalt)
                    {
                        _session.errdescroption += "// the visit didn't insert // ";
                       // MessageBox.Show("the visit didn't insert");
                    }
                    if(printerStatus ==1)
                    for (int i = 0; i < 3; i++) // printing salary loop
                    {
                        try
                        {
                            int FIX_SALARY = 0;
                            int REPAYMENT = 0;
                            int TOTAL_REPAYMENT = 0;
                            int OVERTIME = 0;
                            int MATCHING = 0;
                            int REWARD = 0;
                            int ADVANCE = 0;
                            int LOAN = 0;
                            int WITHOUT_SALARY = 0;
                            int NON_ATTENDANCE = 0;
                            int SICKNESS = 0;
                            int DISCOUNT = 0;
                            int NET_PAY = 0;
                            string MONTH1 = "";
                            string YEAR1 = "";
                            string NOTES = "";

                            FIX_SALARY = WebServiseFunctions.employeesalforrecipt_Class(Convert.ToInt32(_session.payRec), out REPAYMENT, out TOTAL_REPAYMENT, out OVERTIME, out MATCHING, out REWARD, out ADVANCE, out LOAN, out WITHOUT_SALARY, out NON_ATTENDANCE, out SICKNESS, out DISCOUNT, out NET_PAY, out MONTH1, out YEAR1, out NOTES);
                            salary1 sr = new salary1();
                            sr.SetParameterValue("empid", Convert.ToInt32(_session.empId));
                            sr.SetParameterValue("fixsal", FIX_SALARY);
                            sr.SetParameterValue("rep", REPAYMENT);
                            sr.SetParameterValue("totalrep", TOTAL_REPAYMENT);
                            sr.SetParameterValue("overtime", OVERTIME);
                            sr.SetParameterValue("matching", MATCHING);
                            sr.SetParameterValue("reward", REWARD);
                            sr.SetParameterValue("advance", ADVANCE);
                            sr.SetParameterValue("loan", LOAN);
                            sr.SetParameterValue("withoutsal", WITHOUT_SALARY);
                            sr.SetParameterValue("nonattach", NON_ATTENDANCE);
                            sr.SetParameterValue("sickniss", SICKNESS);
                            sr.SetParameterValue("diskount", DISCOUNT);
                            sr.SetParameterValue("netpay", NET_PAY);
                            sr.SetParameterValue("mm", MONTH1);
                            sr.SetParameterValue("yy", YEAR1);
                            sr.SetParameterValue("nots", NOTES);

                            // sr.SetParameterValue("recid", _session.payRec);
                            sr.SetParameterValue("tranid", _session.tranactionid);
                            
                            sr.PrintToPrinter(1, false, 0, 0);// print salay receipt
                            sr.Dispose();
                            sr.Close();

                            Thread.Sleep(300);
                            break;
                        }
                        catch (Exception ee)
                        {
                            //  MessageBox.Show(ee.Message); 
                            _session.errdescroption += "// " + i.ToString() + " error printing:" + ee.Message.ToString() + " // ";
                        }
                    }
                }
                _session.empPath += "print reciept // ";
                Confirm con = null;
                try
                {
                    con = new Confirm();
                }
                catch (Exception ee)
                {
                    //   MessageBox.Show(ee.Message); 
                }
                Thread.Sleep(400);
                //try
                //{
                con.Show();
                //}
                //catch (Exception pp) { return; }
                try
                {
                    if (rh != null)
                    rh.Abort();
                }
                catch (Exception ecxe)
                {
                    // MessageBox.Show(ecxe.Message);
                }
                byte[] thebytesarray111 = HexStringToByteArray("1b 30 02");
                Thread.Sleep(200);
                try
                {
                    myPort.Write(thebytesarray111, 0, thebytesarray111.Length);
                }
                catch (Exception ww)
                {
                    // MessageBox.Show(ww.Message); 
                }
                try
                {
                    myPort.Dispose();
                    myPort.Close();
                }
                catch (Exception) { }
                try
                {
                    camimg.videoSource.Stop();
                }
                catch (Exception qq)
                {
                    // MessageBox.Show(qq.Message); 
                }
              //  this.Dispose();
                //try
                //{
                //    if (ChooseSubServ.servicetype == 1)
                //    {
                //      //  MessageBox.Show("insert visit 1");
                //        fromyoutube.insertvisit(Convert.ToInt32(_session.KioskID), _session.tranactionid, _session.empId, _session.resualt, _session.paidsalary, Convert.ToInt32(_session.payRec), 0, _session.image1, _session.image2, _session.image3);
                //    }
                //    else if (ChooseSubServ.servicetype == 2)
                //    {
                //       // MessageBox.Show("insert visit 2");
                //        fromyoutube.insertvisit(Convert.ToInt32(_session.KioskID), _session.tranactionid, _session.empId, _session.resualt, _session.paidsalary, 0, Convert.ToInt32(_session.payRec), _session.image1, _session.image2, _session.image3);
                //    }
                //}
                //catch (Exception err) { MessageBox.Show(err.Message.ToString()); }
                try {
                    if (rh1 != null)
                    rh1.Abort(); 
                }
                catch (Exception) { }
                try
                {
                    counterOne.Tick -= counterOne_Tick;
                }
                catch (Exception) { }
                this.Close();
            }
            else
            {
                if (ChooseSubServ.servicetype == 1)
                {
                    try
                    {
                        if (printerStatus == 0)
                            visitresalt = WebServiseFunctions.insertvisit1_Class(Convert.ToInt32(_session.KioskID), _session.tranactionid, _session.empId, _session.resualt, _session.paidsalary, 0, Convert.ToInt32(_session.payRec), _session.image1, _session.image2, _session.image3, "The Employee has chosen to not receive a receipt");
                        else
                        visitresalt = WebServiseFunctions.insertvisit_Class(Convert.ToInt32(_session.KioskID), _session.tranactionid, _session.empId, _session.resualt, _session.paidsalary, Convert.ToInt32(_session.payRec), 0, _session.image1, _session.image2, _session.image3);
                    }
                    catch (Exception eee) { 
                       // MessageBox.Show(eee.Message.ToString());
                    }
                    SqlConnection pendingtransconn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["kioskApp.Properties.Settings.sqlconnstring"].ConnectionString);
                    pendingtransconn.Open();
                    string command = "insert into pending_trans(TRANSICATION_ID,EMPLOYEEID,RESULT,IMG_1,IMG_2,IMG_3,PAIDSALARY,SALRECID,RECOMPRECID) values('" + _session.tranactionid + "'," + _session.empId + "," + _session.resualt + ",'" + _session.image1 + "','" + _session.image2 + "','" + _session.image3 + "'," + _session.paidsalary + "," + Convert.ToInt32(_session.payRec) + ",0)";
                    SqlCommand sqlcomm = new SqlCommand(command, pendingtransconn);
                    int q = sqlcomm.ExecuteNonQuery();
                    if (!visitresalt)
                    {
                        _session.errdescroption += "// the visit didn't insert // ";
                       // MessageBox.Show("the visit didn't insert");
                    }
                }
                else if (ChooseSubServ.servicetype == 2)
                {
                    try{
                        if (printerStatus == 0)
                            visitresalt = WebServiseFunctions.insertvisit1_Class(Convert.ToInt32(_session.KioskID), _session.tranactionid, _session.empId, _session.resualt, _session.paidsalary, 0, Convert.ToInt32(_session.payRec), _session.image1, _session.image2, _session.image3, "The Employee has chosen to not receive a receipt");
                        else
                        visitresalt = WebServiseFunctions.insertvisit_Class(Convert.ToInt32(_session.KioskID), _session.tranactionid, _session.empId, _session.resualt, _session.paidsalary, 0, Convert.ToInt32(_session.payRec), _session.image1, _session.image2, _session.image3);
                     }
                    catch (Exception eee) { //MessageBox.Show(eee.Message.ToString());
                    }
                    SqlConnection pendingtransconn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["kioskApp.Properties.Settings.sqlconnstring"].ConnectionString);
                    pendingtransconn.Open();
                   // string command = "insert into pending_trans as t values(" + _session.tranactionid + "," + Convert.ToInt32(_session.KioskID) + "," + _session.empId + "," + DateTime.Now + "," + _session.resualt + "," + _session.image1 + "," + _session.image2 + "," + _session.image3 + "," + _session.paidsalary + ",0," + Convert.ToInt32(_session.payRec) + ")";
                    string command = "insert into pending_trans(TRANSICATION_ID,EMPLOYEEID,RESULT,IMG_1,IMG_2,IMG_3,PAIDSALARY,SALRECID,RECOMPRECID) values('" + _session.tranactionid + "'," + _session.empId + "," + _session.resualt + ",'" + _session.image1 + "','" + _session.image2 + "','" + _session.image3 + "'," + _session.paidsalary + "," + Convert.ToInt32(_session.payRec) + ",0)";
                    SqlCommand sqlcomm = new SqlCommand(command, pendingtransconn);
                    int q = sqlcomm.ExecuteNonQuery();
                    if (!visitresalt)
                    {
                        _session.errdescroption += "// the visit didn't insert // ";
                       // MessageBox.Show("the visit didn't insert");
                    }
                }
                try
                {
                    int updated = WebServiseFunctions.updatethetrue_Class(_session.thetruthresault, Convert.ToInt32(_session.payRec), _session.restofthecash, Convert.ToInt32(ChooseSubServ.servicetype));
                }
                catch (Exception newexc)
                {
                  //  MessageBox.Show(newexc.Message.ToString());
                }
                //_session.resualt = false;
                if(printerStatus ==1)
                for (int i = 0; i < 3; i++) // error printing loop
                {
                    try
                    {
                        ErrorReciept err = new ErrorReciept();
                        //err.SetDatabaseLogon("sa", "JamesClerkMaxwell");
                        if (retractflag != 1)
                        {
                            err.SetParameterValue("errorcode", _session.errorecode);
                            err.SetParameterValue("thenotes", "يرجى المحاولة لاحقا أو التوجه الى جهاز خدمة ذاتية أخر.");
                            insertErrorCodeNotification(_session.errorecode, "يرجى المحاولة لاحقا أو التوجه الى جهاز خدمة ذاتية أخر.");
                        }
                        else 
                        {
                            err.SetParameterValue("errorcode","لم يتم استلام الاوراق النقدية خلال الفترة المحددة");
                            err.SetParameterValue("thenotes", " ");
                            insertErrorCodeNotification(_session.errorecode, "لم يتم استلام الاوراق النقدية خلال الفترة المحددة");
                        }
                        if (ChooseSubServ.servicetype == 2)
                            err.SetParameterValue("message","في حال حصول اي مشكلة يرجى مراجعة قسم المالية");
                        if (ChooseSubServ.servicetype == 1)
                            err.SetParameterValue("message", "في حال حصول اي مشكلة يرجى مراجعة قسم الموارد البشرية");
                        err.SetParameterValue("tranid", _session.tranactionid);
                        err.SetParameterValue("empid", _session.empId);
                        err.SetParameterValue("paidcash", _session.paidsalary);
                        err.PrintToPrinter(1, false, 0, 0);
                        err.Close();
                        err.Dispose();
                        Thread.Sleep(300);
                        break;
                    }
                    catch (Exception ewe)
                    {
                        //MessageBox.Show(ewe.Message); 
                        _session.errdescroption += "\\ " + i.ToString() + "error printing ErrorReciept:" + ewe.Message.ToString() + "\\";
                        insertErrorCodeNotification(_session.errorecode, "error printing ErrorReciept:" + ewe.Message.ToString());
                    }
                }
                _session.empPath += "print error reciept // ";
                ErrorMessage err111 = new ErrorMessage(Clases.ErrorsMsg.FinanceErrorMsg_Ar, Clases.ErrorsMsg.FinanceErrorMsg_En);
                if(_session.errorecode!="0")
                {
                    insertErrorCodeNotification(_session.errorecode, "");
                }
                try
                {
                    if(rh!=null)
                    rh.Abort();
                }
                catch (Exception ecxe)
                {
                    //  MessageBox.Show(ecxe.Message);
                }
                byte[] thebytesarray11 = HexStringToByteArray("1b 30 02");
                Thread.Sleep(200);
                try
                {
                    myPort.Write(thebytesarray11, 0, thebytesarray11.Length);
                }
                catch (Exception ww)
                {
                    //  MessageBox.Show(ww.Message); 
                }
                myPort.Dispose();
                myPort.Close();
                try
                {
                    camimg.videoSource.Stop();
                }
                catch (Exception ww)
                {
                    // MessageBox.Show(ww.Message);
                }
                this.Dispose();
                try {
                    if (rh1 != null)
                    rh1.Abort(); 
                }
                catch (Exception) { }
                try
                {
                    counterOne.Tick -= counterOne_Tick;
                }
                catch(Exception){}
                this.Close();
                err111.Show();
            }
        }
        private void printerflashing()
        {
            byte[] thebytesarray;
            for (int i = 1; i > 0; )
            {
                thebytesarray = HexStringToByteArray("1b 31 02");
                Thread.Sleep(200);
                try
                {
                    myPort.Write(thebytesarray, 0, thebytesarray.Length);
                }
                catch (Exception m1)
                {
                    //  MessageBox.Show(m1.Message); 
                }
                Thread.Sleep(50);
                thebytesarray = HexStringToByteArray("1b 31 01");
                Thread.Sleep(200);
                try
                {
                    myPort.Write(thebytesarray, 0, thebytesarray.Length);
                }
                catch (Exception m1)
                {
                    // MessageBox.Show(m1.Message); 
                }
            }
        }


        bool insertErrorCodeNotification(string errorcode,string errordesc)
        {
            try
            {
                string ConnStr = System.Configuration.ConfigurationManager.ConnectionStrings["KioskApp.Properties.Settings.sqlconnstring"].ConnectionString; ;

                string query = "insert into DispenserErrors (ErrorNo,ErrorDescription,Notes,CheckDateTime,status) values (" + errorcode + ",'" + errordesc + "','','"+DateTime.Now.ToString()+"',0)";
                SqlConnection Conn = new SqlConnection(ConnStr);
                SqlCommand cmd = new SqlCommand(query, Conn);
                Conn.Open();
                int affected = cmd.ExecuteNonQuery();
                Conn.Close();
                Conn.Dispose();
                return affected > 0;
            }
            catch { return false; }

        }
        static string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        ~DispinsingCash() { this.Dispose(); }
        public void Dispose()
        {
            //Dispose(true);
            GC.SuppressFinalize(this);
        }
         private void beepsound()
        {
            Thread.Sleep(1000);
            for (int i = 1; i > 0; )
            {
                PlayFile("beep-02.mp3");
                Thread.Sleep(400);
            }
        }


    }

}
