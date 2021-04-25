using KioskApp.HelloWord;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
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
    /// Interaction logic for passfillcard.xaml
    /// </summary>
    public partial class CassettesConfig : Window
    {
       
        public CassettesConfig()
        {
            InitializeComponent();
          
        }
        Boolean resault = false;

        private void btnLogOut_Click(object sender, RoutedEventArgs e)
        {
            FillProcess fp = new FillProcess();
            fp.Show();
            this.Close();
        }
        
        //BackgroundWorker bcworker = new BackgroundWorker();
     //   KioskAppWebServiceSoapClient fromyoutube = new KioskAppWebServiceSoapClient();
        public static string cs1oldconf = "";
        public static string cs2oldconf = "";
        public static string cs3oldconf = "";
        public static string cs4oldconf = "";
        bool StartMode = false;
        private void Window_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            //string str = Environment.GetFolderPath(
            //System.Environment.SpecialFolder.DesktopDirectory);
            Cassettes_Config.Background = new ImageBrush(new BitmapImage(new Uri(@"D:\\backGroundImage\\HADEEDBackground.png")));

            //try
            //{

            //    _session.cst1 = fromyoutube.get_cassettes_types(Convert.ToInt32(_session.KioskID), out _session.cst2, out _session.cst3, out _session.cst4);
            //}
            //catch (Exception cc) { _session.errorecode += "no web service: " + cc.Message.ToString(); }

            //  cb1.SelectedItem = Convert.ToString(_session.cst1);
            ////  cb1.SelectedIndex = cb1.FindName(Convert.ToString(_session.cst1));
            //cb2.SelectedItem = Convert.ToString(_session.cst2);
            // cb3.SelectedItem = Convert.ToString(_session.cst3);
            //cb4.SelectedItem = Convert.ToString(_session.cst4);
            //cb1.Text = Convert.ToString(_session.cst1);
            //cb2.Text = Convert.ToString(_session.cst2);
            //cb3.Text = Convert.ToString(_session.cst3);
            //cb4.Text = Convert.ToString(_session.cst4);
            //cb1.SelectedIndex=

            if (Convert.ToString(_session.cs1type).StartsWith("0"))
                // cb1.Text = "500";
                cb1.SelectedIndex = 0;
            else if (Convert.ToString(_session.cs1type).StartsWith("1"))
                // cb1.Text = "1000";
                cb1.SelectedIndex = 1;
            else if (Convert.ToString(_session.cs1type).StartsWith("2"))
                // cb1.Text = "2000";
                cb1.SelectedIndex = 2;
            else
                cb1.SelectedIndex = 3;// 5000
            if (Convert.ToString(_session.cs2type).StartsWith("0"))
                cb2.SelectedIndex = 0;
            else if (Convert.ToString(_session.cs2type).StartsWith("1"))
                // cb1.Text = "1000";
                cb2.SelectedIndex = 1;
            else if (Convert.ToString(_session.cs2type).StartsWith("2"))
                // cb1.Text = "2000";
                cb2.SelectedIndex = 2;
            else
                cb2.SelectedIndex = 3;
            if (Convert.ToString(_session.cs3type).StartsWith("0"))
                cb3.SelectedIndex = 0;
            else if (Convert.ToString(_session.cs3type).StartsWith("1"))
                // cb1.Text = "1000";
                cb3.SelectedIndex = 1;
            else if (Convert.ToString(_session.cs3type).StartsWith("2"))
                cb3.SelectedIndex = 2;
            else
                cb3.SelectedIndex = 3;
            if (Convert.ToString(_session.cs4type).StartsWith("0"))
                cb4.SelectedIndex = 0;
            else if (Convert.ToString(_session.cs4type).StartsWith("1"))
                // cb1.Text = "1000";
                cb4.SelectedIndex = 1;
            else if (Convert.ToString(_session.cs4type).StartsWith("2"))
                cb4.SelectedIndex = 2;
            else
                cb4.SelectedIndex = 3;
            

            if (Convert.ToString(_session.cs1type).EndsWith("0"))
                cb1_type.SelectedIndex = 0;
            else
                cb1_type.SelectedIndex = 1;
            if (Convert.ToString(_session.cs2type).EndsWith("0"))
                cb2_type.SelectedIndex = 0;
            else
                cb2_type.SelectedIndex = 1;
            if (Convert.ToString(_session.cs3type).EndsWith("0"))
                cb3_type.SelectedIndex = 0;
            else
                cb3_type.SelectedIndex = 1;
            if (Convert.ToString(_session.cs4type).EndsWith("0"))
                cb4_type.SelectedIndex = 0;
            else
                cb4_type.SelectedIndex = 1;

            //cb1_type.SelectedIndex = Convert.ToInt32(Convert.ToString(_session.cs1type).Last());
            //cb2_type.SelectedIndex = Convert.ToInt32(Convert.ToString(_session.cs2type).Last());
            //cb3_type.SelectedIndex = Convert.ToInt32(Convert.ToString(_session.cs3type).Last());
            //cb4_type.SelectedIndex = Convert.ToInt32(Convert.ToString(_session.cs4type).Last());


            cs1oldconf = _session.cs1type;
            cs2oldconf = _session.cs2type;
            cs3oldconf = _session.cs3type;
            cs4oldconf = _session.cs4type;

            StartMode = true;
        }
      
        private void btn_ok_Click(object sender, RoutedEventArgs e)
        {
            _session.cs1type = Convert.ToString(cb1.SelectedIndex);
            _session.cs2type = Convert.ToString(cb2.SelectedIndex);
            _session.cs3type = Convert.ToString(cb3.SelectedIndex);
            _session.cs4type = Convert.ToString(cb4.SelectedIndex);
            switch (cb1.SelectedIndex)
            {
                case 0:
                    _session.cst1 = 500;
                    break;
                case 1:
                    _session.cst1 = 1000;
                    break;
                case 2:
                    _session.cst1 = 2000;
                    break;
                case 3:
                    _session.cst1 = 5000;
                    break;
                default:
                    _session.cst1 = 0;
                    break;
            }
            switch (cb2.SelectedIndex)
            {
                case 0:
                    _session.cst2 = 500;
                    break;
                case 1:
                    _session.cst2 = 1000;
                    break;
                case 2:
                    _session.cst2 = 2000;
                    break;
                case 3:
                    _session.cst2 = 5000;
                    break;
                default:
                    _session.cst2 = 0;
                    break;
            }
            switch (cb3.SelectedIndex)
            {
                case 0:
                    _session.cst3 = 500;
                    break;
                case 1:
                    _session.cst3 = 1000;
                    break;
                case 2:
                    _session.cst3 = 2000;
                    break;
                case 3:
                    _session.cst3 = 5000;
                    break;
                default:
                    _session.cst3 = 0;
                    break;
            }
            switch (cb4.SelectedIndex)
            {
                case 0:
                    _session.cst4 = 500;
                    break;
                case 1:
                    _session.cst4 = 1000;
                    break;
                case 2:
                    _session.cst4 = 2000;
                    break;
                case 3:
                    _session.cst4 = 5000;
                    break;
                default:
                    _session.cst4 = 0;
                    break;
            }

          //  fromyoutube.set_cassettes_types(Convert.ToInt32(_session.KioskID),_session.cst1,_session.cst2,_session.cst3,_session.cst4);

          
            int length1 = 0;
            int length2 = 0;
            int length3 = 0;
            int length4 = 0;

            _session.cs1type =_session.cs1type + Convert.ToString(cb1_type.SelectedIndex);
            _session.cs2type = _session.cs2type + Convert.ToString(cb2_type.SelectedIndex);
            _session.cs3type = _session.cs3type + Convert.ToString(cb3_type.SelectedIndex);
            _session.cs4type = _session.cs4type + Convert.ToString(cb4_type.SelectedIndex);

            switch (cb1_type.SelectedIndex)
            {
                case 0:
                    length1 = 60;
                    break;
                case 1:
                    length1 = 51;
                    break;
            }
            switch (cb2_type.SelectedIndex)
            {
                case 0:
                    length2 = 60;
                    break;
                case 1:
                    length2 = 51;
                    break;
            }
            switch (cb3_type.SelectedIndex)
            {
                case 0:
                    length3 = 60;
                    break;
                case 1:
                    length3 = 51;
                    break;
            }
            switch (cb4_type.SelectedIndex)
            {
                case 0:
                    length4 = 60;
                    break;
                case 1:
                    length4 = 51;
                    break;
            }
            bool open = dispinsingClass.CDM1_Open((Byte)_session.DispenserPortNo);
            if (open)
            {
                dispinsingClass.CDM1_SetLength(length1, length2, length3, length4);
                dispinsingClass.CDM1_SetBillDispenseType(1, 2, 3, 4);
                //_session.cs1type = cb1_type.SelectedIndex;
                //_session.cs2type = cb2_type.SelectedIndex;
                //_session.cs3type = cb3_type.SelectedIndex;
                //_session.cs4type = cb4_type.SelectedIndex;

                bool close = dispinsingClass.CDM1_Close();
                FillProcess fp = new FillProcess();
                fp.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show(" الرجاء التأكد أن الديسبنسر يعمل واعادة المحاولة...");
            }
        }

        private void cb1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(StartMode )
            try
            {
                switch (cb1.SelectedIndex)
                {
                    case 2:
                        try
                        {
                            cb1_type.SelectedIndex = 1;
                            cb1_type.IsEnabled = false;
                        }
                        catch (Exception) { }
                        break;
                    case 3:
                        try
                        {
                            cb1_type.SelectedIndex = 1;
                            cb1_type.IsEnabled = false;
                        }
                        catch (Exception) { }
                        break;
                    default:
                        cb1_type.IsEnabled = true;
                        break;
                }
            }
            catch(Exception){}
        }

        private void cb2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (StartMode)
            try
            {
                switch (cb2.SelectedIndex)
                {
                    case 2:
                        try
                        {
                            cb2_type.SelectedIndex = 1;
                            cb2_type.IsEnabled = false;
                        }
                        catch (Exception) { }
                        break;
                    case 3:
                        try
                        {
                            cb2_type.SelectedIndex = 1;
                            cb2_type.IsEnabled = false;
                        }
                        catch (Exception) { }
                        break;
                    default:
                        cb2_type.IsEnabled = true;
                        break;
                }
            }
            catch (Exception) { }
        }

        private void cb3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (StartMode)
            try
            {
                switch (cb3.SelectedIndex)
                {
                    case 2:
                        try
                        {
                            cb3_type.SelectedIndex = 1;
                            cb3_type.IsEnabled = false;
                        }
                        catch (Exception) { }
                        break;
                    case 3:
                        try
                        {
                            cb3_type.SelectedIndex = 1;
                            cb3_type.IsEnabled = false;
                        }
                        catch (Exception) { }
                        break;
                    default:
                        cb3_type.IsEnabled = true;
                        break;
                }
            }
            catch (Exception) { }
        }

        private void cb4_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (StartMode)
            try
            {
                switch (cb4.SelectedIndex)
                {
                    case 2:
                        try
                        {
                            cb4_type.SelectedIndex = 1;
                            cb4_type.IsEnabled = false;
                        }
                        catch (Exception) { }
                        break;
                    case 3:
                        try
                        {
                            cb4_type.SelectedIndex = 1;
                            cb4_type.IsEnabled = false;
                        }
                        catch (Exception) { }
                        break;
                    default:
                        cb4_type.IsEnabled = true;
                        break;
                }
            }
            catch (Exception) { }
        }
    }
}
