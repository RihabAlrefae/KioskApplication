using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace KioskApp
{
   public class writingLogs : IDisposable
    {
       public static void writing_visit_log(string file_name, string the_Text)
       {
           if (!File.Exists(file_name))
           {
               File.Create(file_name);
           }
           try
           {
               File.AppendAllText(file_name, the_Text + Environment.NewLine);
               //File.Create(file_name).Flush(true);
               //File.Create(file_name).Close();
           }
           catch
               (Exception exec)
           {
           }
       }
       public static void writing_finantial_log(string file_name, string the_Text)
       {
           if (!File.Exists(file_name))
           {
               File.Create(file_name);
           }
           try
           {
               File.AppendAllText(file_name, the_Text + Environment.NewLine);
               //File.Create(file_name).Flush(true);
               //File.Create(file_name).Close();
               
           }
           catch
               (Exception exec)
           {

           }
       }
       public static void Write(string name, string details, Byte[] ppurge=null)
       {

           StreamWriter sw = null;
       //    DateTime dateTime = DateTimeServer.CurrentDateTime();
         //  string errorNo = dateTime.ToString("yyyyMMddHHmmssfff");
           try
           {
               sw = new StreamWriter("logs\\dispinserlog" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt", true);
             //  sw.WriteLine("Error No. " + errorNo);
               sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
               sw.WriteLine(name);
               //sw.WriteLine("Date-Time : " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
               sw.WriteLine("response:");
               
               sw.WriteLine(details);
               if (ppurge != null)
               {
                  // string[] ss = new string[ppurge.Length];
                   for (int i = 0; i < ppurge.Length; i++)
                   {
                      // ss[i] = ppurge[i].ToString();
                       sw.Write(ppurge[i].ToString()+",");
                   }
                   sw.WriteLine("");
                  // System.IO.File.WriteAllLines("dispinserlog" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt", ss);
                   //ppurge.foreach(r => sw.WriteLine(r.ToString()));
               }
               sw.WriteLine("-------------------------------------------------------------");
               //sw.WriteLine("");
               //ErrorForm ef = new ErrorForm(name, "Error No. " + errorNo + "\n Exception : " + name + "\nDate-Time : " + dateTime.ToString("yyyy-MM-dd HH:mm:ss.fff") +
               //    "\nDetails:" + details, errorNo);
               //ef.ShowDialog();
               //MessageBox.Show("An error had occured, Error No. " + errorNo);
               sw.Close();
               sw.Dispose();
           }
           catch (Exception ex)
           {
               sw.Dispose();
           }

       }
       public static void WriteWebServiceLog(string name, string details,int? start=null )
       {

           StreamWriter sw = null;
           //    DateTime dateTime = DateTimeServer.CurrentDateTime();
           //  string errorNo = dateTime.ToString("yyyyMMddHHmmssfff");
           try
           {
               
               sw = new StreamWriter("logs\\WebServiceLog" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt", true);
               //  sw.WriteLine("Error No. " + errorNo);
               if (start == 1)
               {
                   sw.WriteLine("-------------------------------------------------------------");
                   sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                   sw.WriteLine("Start the Transaction");
                   sw.WriteLine("-------------------------------------------------------------");
                   sw.WriteLine("-------------------------------------------------------------");
               }
               else if (start == 0)
               {
                   sw.WriteLine("-------------------------------------------------------------");
                   sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                   sw.WriteLine("Start the fill process Transaction");
                   sw.WriteLine("-------------------------------------------------------------");
                   sw.WriteLine("-------------------------------------------------------------");
               }
               sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
               sw.WriteLine(name);
               //sw.WriteLine("Date-Time : " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
               sw.WriteLine("response:");

               sw.WriteLine(details);
               
               sw.WriteLine("-------------------------------------------------------------");
               //sw.WriteLine("");
               //ErrorForm ef = new ErrorForm(name, "Error No. " + errorNo + "\n Exception : " + name + "\nDate-Time : " + dateTime.ToString("yyyy-MM-dd HH:mm:ss.fff") +
               //    "\nDetails:" + details, errorNo);
               //ef.ShowDialog();
               //MessageBox.Show("An error had occured, Error No. " + errorNo);
               sw.Close();
               sw.Dispose();
           }
           catch (Exception ex)
           {
               sw.Dispose();
           }

       }

       
       ~writingLogs()  { this.Dispose(); }
         public void Dispose() 
         {
             Dispose(true);
             GC.SuppressFinalize(this);
         }
        
         protected void Dispose(bool disposing)
         {
             
         }   
    }
}
