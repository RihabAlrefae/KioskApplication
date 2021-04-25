using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32.SafeHandles;

namespace KioskApp
{
    class dispinsingClass : IDisposable
    {
        [DllImport("UCDM_DLL.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CDM_Open")]
        private static extern bool CDM_Open(Byte PortNo);

        [DllImport("UCDM_DLL.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CDM_Dispense")]
        private static extern Byte CDM_Dispense(int num1, int num2, int num3, int num4, Byte[] pdispense);

        [DllImport("UCDM_DLL.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CDM_OpenShutter")]
        private static extern Byte CDM_OpenShutter();

        [DllImport("UCDM_DLL.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CDM_Present")]
        private static extern Byte CDM_Present();

        [DllImport("UCDM_DLL.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CDM_Status")]
        private static extern Byte CDM_Status(Byte[] pstatus);

        [DllImport("UCDM_DLL.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CDM_CloseShutter")]
        private static extern Byte CDM_CloseShutter();

        [DllImport("UCDM_DLL.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CDM_Retract")]
        private static extern Byte CDM_Retract();

        [DllImport("UCDM_DLL.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CDM_Close")]
        private static extern bool CDM_Close();

        [DllImport("UCDM_DLL.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CDM_Reset")]
        private static extern bool CDM_Reset();

        [DllImport("UCDM_DLL.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CDM_Purge")]
        private static extern Byte CDM_Purge(Byte[] ppurge);

        [DllImport("UCDM_DLL.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CDM_SetLength")]
        private static extern Byte CDM_SetLength( int num1,  int num2, int num3, int num4);

        [DllImport("UCDM_DLL.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CDM_SetBillDispenseType")]
        private static extern Byte CDM_SetBillDispenseType(int num1, int num2, int num3, int num4);
        

      //  private SafeRegistryHandle hExtHandle, hAppIdHandle;
        public static bool CDM1_Open(Byte comnumber)
        {
            var resualt = false;
            try
            {
                 resualt = CDM_Open((Byte)comnumber);
                 writingLogs.Write("Open dispenser com: CDM_Open(" + comnumber.ToString()+")", resualt.ToString() + "\r\n");
            }
            catch (Exception err) { MessageBox.Show(err.Message); writingLogs.Write("Open dispenser com error:", err.Message.ToString() + "\r\n"); }
            return resualt;
        }
       static Byte[] pur = new byte[150];
        public static Byte CDM1_Dispense(int num1, int num2, int num3, int num4, Byte[] pdispense)
        {
            var result = 0;
            var purgresult = 0;
            
            try
            {
                 result = CDM_Dispense(num1, num2, num3, num4, pdispense);
                 string sendcommand = "CDM_Dispense(" + num1 + "," + num2 + "," + num3 + "," + num4 + ")";
                 //string details = (Byte)result == 0 ? "  FirstCst:" + pdispense[0].ToString() + "  SecondCst:" + pdispense[4].ToString() + "  ThirdCst:" + pdispense[8].ToString() + "  FourthCst:" + pdispense[12].ToString() + "  FCSTReject:" + pdispense[2].ToString() + "  SCSTReject:" + pdispense[6].ToString() + "  TCSTReject:" + pdispense[6].ToString() + "  FourCSTReject:" + pdispense[10].ToString() + "\r\n" : "\r\n";
                 string details =  "  FirstCst:" + pdispense[0].ToString() + "  SecondCst:" + pdispense[4].ToString() + "  ThirdCst:" + pdispense[8].ToString() + "  FourthCst:" + pdispense[12].ToString() + "  FCSTReject:" + pdispense[2].ToString() + "  SCSTReject:" + pdispense[6].ToString() + "  TCSTReject:" + pdispense[6].ToString() + "  FourCSTReject:" + pdispense[10].ToString() + "\r\n" ;
                 writingLogs.Write("dispense: send:" + sendcommand, " receive :" + ((Byte)result).ToString() + details);
                 _session.dispinse1 += pdispense[0] + pdispense[2];
                 _session.dispinse2 += pdispense[4] + pdispense[6];
                _session.dispinse3 += pdispense[8] + pdispense[10];
                _session.dispinse4 += pdispense[12] + pdispense[14];

                _session.rejected1 = pdispense[2];
                _session.rejected2 = pdispense[6];
                _session.rejected3 = pdispense[10];
                _session.rejected4 = pdispense[14];

                _session.dispinse11 = pdispense[0];
                _session.dispinse22 = pdispense[4];
                _session.dispinse33 = pdispense[8];
                _session.dispinse44 = pdispense[12];


                _session.rejectedbox1 += _session.rejected1;
                _session.rejectedbox2 += _session.rejected2;
                _session.rejectedbox3 += _session.rejected3;
                _session.rejectedbox4 += _session.rejected4;



                if ((Byte)result != 0 & (Byte)result != 29)// & (Byte)result != 14 & (Byte)result != 11 & (Byte)result != 13)
                {

                    //_session.resualt = false;
                    _session.errorecode += "dispenser error : " + (Byte)result;
                    try
                    {
                        purgresult = CDM1_Purge(pur);
                    }
                    catch(Exception){}
                    _session.rejectedbox1 += _session.dispinse11;
                    _session.rejectedbox2 += _session.dispinse22 ;
                    _session.rejectedbox3 += _session.dispinse33;
                    _session.rejectedbox4 += _session.dispinse44;
                }
            }
            catch (Exception ee) { MessageBox.Show(ee.Message); string sendcommand = "CDM_Dispense(" + num1 + "," + num2 + "," + num3 + "," + num4 + ")"; writingLogs.Write("dispense exception: send:" + sendcommand, ee.Message.ToString() + "\r\n"); }
            return (Byte)result;
        }
        public static Byte CDM1_OpenShutter()
        {
            var resualt = 0;
            var purgresult = 0;
            try
            {
                 resualt = CDM_OpenShutter();
                writingLogs.Write("Open Shutter:", ((Byte)resualt).ToString() + "\r\n"); 
                 if ((Byte)resualt != 0)
                 {

                     //_session.resualt = false;
                     _session.errorecode += "Open Shutter error : " + (Byte)resualt;
                     try
                     {
                         purgresult = CDM1_Purge(pur);
                     }
                     catch(Exception){}
                     _session.rejectedbox1 += _session.dispinse11;
                     _session.rejectedbox2 += _session.dispinse22;
                     _session.rejectedbox3 += _session.dispinse33;
                     _session.rejectedbox4 += _session.dispinse44;
                 }
            }
            catch (Exception err) { MessageBox.Show(err.Message); writingLogs.Write("Open Shutter exception:", err.Message.ToString() + "\r\n"); }
            return (Byte)resualt;
        }
        public static Byte CDM1_Present()
        {
            var resualt = 0;
            var purgresult = 0;
            try
            {
                resualt = CDM_Present();
                writingLogs.Write("Present:", ((Byte)resualt).ToString() + "\r\n"); 
                if ((Byte)resualt != 0 )
                {

                    //_session.resualt = false;
                    _session.errorecode += "present error : " + (Byte)resualt;
                    purgresult = CDM1_Retract();
                    _session.rejectedbox1 += _session.dispinse11;
                    _session.rejectedbox2 += _session.dispinse22;
                    _session.rejectedbox3 += _session.dispinse33;
                    _session.rejectedbox4 += _session.dispinse44;
                }
            }
            catch (Exception err) { MessageBox.Show(err.Message); writingLogs.Write("Present exception:", err.Message.ToString() + "\r\n"); }
            return (Byte)resualt;
        }
        public static Byte CDM1_Status(Byte[] pstatus)
        {
            var resualt = 0;
            try
            {
                resualt = CDM_Status(pstatus);
            }
            catch (Exception err) { MessageBox.Show(err.Message); }
            return (Byte)resualt;
        }
        public static Byte CDM1_Retract()
        {
            var resualt = 0;
            try
            {
                 resualt = CDM_Retract();
            }
            catch (Exception err) { MessageBox.Show(err.Message); }
            return (Byte)resualt;
        }
        public static Byte CDM1_CloseShutter()
        {
            var resualt = 0;
            try
            {
                 resualt = CDM_CloseShutter();
                 writingLogs.Write("CDM_CloseShutter:", ((Byte)resualt).ToString() + "\r\n"); 
            }
            catch (Exception err) { MessageBox.Show(err.Message); writingLogs.Write("CDM_CloseShutter exception:", err.Message.ToString() + "\r\n"); }
            return (Byte)resualt;
        }
        public static bool CDM1_Close()
        {
            var resualt = false;
            try
            {
                 resualt = CDM_Close();
                 writingLogs.Write("CDM_Close:", (!resualt).ToString() + "\r\n");
                // MessageBox.Show((resualt).ToString());
            }
            catch (Exception err) { MessageBox.Show(err.Message); writingLogs.Write("CDM_Close exception: ", err.Message.ToString() + "\r\n"); }
            return resualt;
        }
        public static Byte CDM1_Purge(Byte[] ppurge)
        {
        var resualt = 0;
            try
            {
                resualt = CDM_Purge(ppurge);
                writingLogs.Write("CDM_Purge:", ((Byte)resualt).ToString() + "\r\n",ppurge); 
            }
            catch (Exception err) { MessageBox.Show(err.Message); writingLogs.Write("CDM_Purge exception:", err.Message.ToString() + "\r\n"); }
            return (Byte)resualt;

        }
        public static Byte CDM1_SetLength( int n1,  int n2,  int n3, int n4)
        {
            var resualt = 0;
            try
            {
                resualt = CDM_SetLength( n1,  n2, n3, n4);
            }
            catch (Exception err) { MessageBox.Show(err.Message); }
            return (Byte)resualt;
        }
        public static Byte CDM1_SetBillDispenseType(int n1, int n2, int n3, int n4)
        {
            var resualt = 0;
            try
            {
                resualt = CDM_SetBillDispenseType(n1,n2,n3,n4);
            }
            catch (Exception exep) { MessageBox.Show(exep.Message.ToString()); }
            return (Byte)resualt;
        }

        ~dispinsingClass() { this.Dispose(); }

         public void Dispose() 
           {
              Dispose(true);
              GC.SuppressFinalize(this);
           }   

   protected virtual void Dispose(bool disposing)
   {
       if (disposing)
       {
          // this.Dispose();
       }
     }
   }
 }

