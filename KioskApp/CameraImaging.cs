using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AForge.Video.DirectShow;
using System.Drawing;
using AForge.Video;
 

namespace KioskApp
{
    class CameraImaging
    {
        // enumerate video devices
        public FilterInfoCollection videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
        //camera
        private VideoCaptureDevice _videoSource;
        public Bitmap bitmap = new Bitmap(40,50);
        public VideoCaptureDevice videoSource
        {
            get
            {
                return _videoSource;
            }
            set
            {
                _videoSource = value;
                // set NewFrame event handler
                _videoSource.NewFrame += new NewFrameEventHandler(videoSource_NewFrame);
            }
        }
       
        public CameraImaging(VideoCaptureDevice videoDevice)
        {
            // create video source
            VideoCaptureDevice videoSource = videoDevice;
        }
        public CameraImaging()
        {
 
        }
        public void videoSource_NewFrame( object sender, NewFrameEventArgs eventArgs )
        {
            // get new frame
            
                bitmap = (Bitmap)eventArgs.Frame.Clone();
                // process the frame
        }
        ~CameraImaging() { this.Dispose(); }

         public void Dispose() 
           {
              Dispose(true);
              GC.SuppressFinalize(this);
           }   

   protected virtual void Dispose(bool disposing)
   {
       if (disposing)
       {
           bitmap.Dispose();
           videoDevices = null;
           _videoSource = null;

       }
     }
    }
}