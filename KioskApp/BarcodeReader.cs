using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace KioskApp
{
    class BarcodeReader
    {

        [DllImport("dll_camera.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "GetAppHandle")]
        public static extern void GetAppHandle(IntPtr hWnd);

        [DllImport("dll_camera.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "StartDevice")]
        public static extern int StartDevice();

        [DllImport("dll_camera.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ReleaseDevice")]
        public static extern void ReleaseDevice();

        [DllImport("dll_camera.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ReleaseLostDevice")]
        public static extern void ReleaseLostDevice();

        [DllImport("dll_camera.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "GetDevice")]
        public static extern int GetDevice();

        [DllImport("dll_camera.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SetLed")]
        public static extern void SetLed(bool bctrLed);

        [DllImport("dll_camera.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SetBeep")]
        public static extern void SetBeep(bool bctrlBeep);

        [DllImport("dll_camera.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SetBeepTime")]
        public static extern void SetBeepTime(int beepTime);

        [DllImport("dll_camera.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "setBarcode")]
        public static extern void SetBarcode(bool bbarcode);

        [DllImport("dll_camera.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "setDMable")]
        public static extern void SetDMable(bool bbarcode);

        [DllImport("dll_camera.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "setQRable")]
        public static extern void SetQRable(bool bqr);

        [DllImport("dll_camera.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "GetDecodeString")]
        public static extern void GetDecodeString(StringBuilder aa, out int len);

    }
}
