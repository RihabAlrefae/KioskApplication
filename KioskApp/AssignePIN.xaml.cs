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
using System.Security.Cryptography;
using System.IO;
using KioskApp.HelloWord;
using System.Diagnostics;
using KioskApp.Clases;

namespace KioskApp
{
	/// <summary>
	/// Interaction logic for AssignePIN.xaml
	/// </summary>
	public partial class AssignePIN : Window
	{
     //   private KioskAppWebServiceSoapClient fromyoutube = new KioskAppWebServiceSoapClient();
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
	
		string newPINCode="";
		public AssignePIN()
		{
			this.InitializeComponent();
			
        }

		private void btn1_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			NewPIN.Password+="1";
			
		}

		private void btn2_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			NewPIN.Password+="2";
		}

		private void btn3_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			NewPIN.Password+="3";
		}

		private void btn4_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			NewPIN.Password+="4";
		}

		private void btn5_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			NewPIN.Password+="5";
		}

		private void btn6_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			NewPIN.Password+="6";
		}

		private void btn7_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			NewPIN.Password+="7";
		}

		private void btn8_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			NewPIN.Password+="8";
		}

		private void btn9_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			NewPIN.Password+="9";
		}

		private void btn0_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			NewPIN.Password+="0";
		}
		private void btnDelete_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			 try
            {
                NewPIN.Password = NewPIN.Password.Remove(NewPIN.Password.Length - 1, 1);
            }
            catch (Exception x) { };
		}

		private void btnClear_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			//PIN_code.Password="";
            NewPIN.Clear();
		}

        public string Encrypt(string pin)
        {
            string EncrptKey = "2013;[pnuLIT)WebCodeExpert";
            byte[] byKey = { };
            byte[] IV = { 18, 52, 86, 120, 144, 171, 205, 239 };
            byKey = System.Text.Encoding.UTF8.GetBytes(EncrptKey.Substring(0, 8));
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray = Encoding.UTF8.GetBytes(pin);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(byKey, IV), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            return Convert.ToBase64String(ms.ToArray());
        }

		private void btn_ok_Click(object sender, System.Windows.RoutedEventArgs e)
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
			if (NewPIN.Password.Length==4 && newPINCode=="")
				{
					newPINCode=NewPIN.Password;
					NewPIN.Clear();
                    if (_session.language == 2)
                    {
						TextBlock_1.FontFamily=new FontFamily("Tw Cen MT Condensed Extra Bold");
						TextBlock_1.FontSize = 42;
                        TextBlock_1.Text = "Repeate The PIN Code";
                    }
                    else
                    {
                        TextBlock_1.Text = "الرجاء تأكيد الرمز السري الذي ادخلته";
                    }
					NewPIN.Focus();
				}
			else if(NewPIN.Password.Length!=4)
			{
                if (_session.language == 2)
                {
					TextBlock_1.FontFamily=new FontFamily("Tw Cen MT Condensed Extra Bold");
					TextBlock_1.FontSize = 42;
                    TextBlock_1.Text = "Enter a 4 Degits Number Only";
                }
                else
                {
                    TextBlock_1.Text = "أدخل رمز مكون من 4 ارقام حصرا";
                }
				NewPIN.Clear();
				NewPIN.Focus();
			}
			if (NewPIN.Password.Length==4&&newPINCode!="")
				{
					if(NewPIN.Password==newPINCode)
					{
                        string encryptdNewPin = Encrypt(NewPIN.Password);
                        try
                        {
                            int? resault=WebServiseFunctions.UpdatePin_Class(_session.empId, encryptdNewPin);
                        }
                        catch (Exception exe) {
                            dispatcherTimer.Stop();
                            dispatcherTimer.Tick -= dispatcherTimer_Tick;
                            _session.errorecode += "// webservice not fount//";
                            StopFile();
                            ErrorMessage ss = new ErrorMessage(Clases.ErrorsMsg.ItErrorMsg_Ar, Clases.ErrorsMsg.ItErrorMsg_En);
                            ss.Show();
                           
                            this.Close();
                            return;
                        }
                        dispatcherTimer.Stop();
                        dispatcherTimer.Tick -= dispatcherTimer_Tick;
                        _session.empPath += "sucsesspin :" + Encrypt(NewPIN.Password) + " // ";
                        StopFile();
						sucsessPIN sp=new sucsessPIN();
						sp.Show();
                        
						this.Close();
                        return;
					}
					else 
					{
                        if (_session.language == 2)
                        {
							TextBlock_1.FontFamily=new FontFamily("Tw Cen MT Condensed Extra Bold");
							TextBlock_1.FontSize = 42;
                            TextBlock_1.Text = "The 2 Pin's Dos't Match.Enter Your New PIN";
                        }
                        else
                        {
                            TextBlock_1.Text = "الرمزان غير متطابقان ادخل الرمز السري";
                        }
						NewPIN.Clear();
						NewPIN.Focus();
						newPINCode="";
					}
				}
			/*else
			{
				TextBlock_1.Text="يجب ان يكون الرمز مؤلف من 4 ارقام فقط ويرجى التأكد من توافق الرمزين";
				NewPIN.Clear();
				NewPIN.Focus();
			}*/
			
		}

		private void btnLogOut_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            _session.empPath += "logout // ";
            dispatcherTimer.Stop();
            dispatcherTimer.Tick -= dispatcherTimer_Tick;
            StopFile();
			StartScreen cl=new StartScreen();
			cl.Show();
           
			this.Close();
		}

		 private void Window_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            //string str = Environment.GetFolderPath(
            //           System.Environment.SpecialFolder.DesktopDirectory);
            Assigne_PIN.Background = new ImageBrush(new BitmapImage(new Uri(@"D:\\backGroundImage\\HADEEDBackground.png")));
            if (_session.language == 2)
            {
                PlayFile(@"D:\\backgroundsounds\\assinepin_EN.mp3");
                TextBlock_1.FontFamily = new System.Windows.Media.FontFamily("Tw Cen MT Condensed Extra Bold");
                TextBlock_1.FontSize = 42;
                TextBlock_1.Text = "Please Enter Your New PIN,It Must Be 4 Degits Number ";
                btnDelete.FontFamily = new System.Windows.Media.FontFamily("Adobe Arabic");
                btnDelete.FontWeight = FontWeights.Bold;
                TextBlock_1.FontSize = 36;
                btnDelete.Content = "Delete";
                btnClear.FontFamily = new System.Windows.Media.FontFamily("Adobe Arabic");
                btnClear.FontWeight = FontWeights.Bold;
                TextBlock_1.FontSize = 36;
                btnClear.Content = "Clear";
                btn_ok.FontFamily = new System.Windows.Media.FontFamily("Adobe Arabic");
                btn_ok.FontWeight = FontWeights.Bold;
                TextBlock_1.FontSize = 36;
                btn_ok.Content = "Ok";
                btnLogOut.FontFamily = new System.Windows.Media.FontFamily("Adobe Arabic");
                btnLogOut.FontWeight = FontWeights.Bold;
                TextBlock_1.FontSize = 36;
                btnLogOut.Content = "Log Out";
            }
            else {
                PlayFile(@"D:\\backgroundsounds\\assinepin_AR.mp3");
            }
			dispatcherTimer.Tick += dispatcherTimer_Tick;
			dispatcherTimer.Interval = new TimeSpan(0,0,30);
			dispatcherTimer.Start();
        }
		private void dispatcherTimer_Tick(object sender, EventArgs e)
		{
            dispatcherTimer.Stop();
            dispatcherTimer.Tick -= dispatcherTimer_Tick;
            StopFile();
		    StartScreen con=new StartScreen();
			con.Show();
			this.Close();
        }
	}
}