using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KioskApp.HelloWord;

namespace KioskApp
{
   public static class _session
    {
       private static KioskAppWebServiceSoapClient fromyoutube = new KioskAppWebServiceSoapClient();
       public static int empId = 0;
       public static int userid = 0;
       public static int empPIN = 0;
       public static int? KioskID=0;
       public static string empPath = "";// visit log
       public static int salary = 0;
       public static string image1 = "";
       public static string image2 = "";
       public static string image3 = "";
       public static int resualt = 0;
       public static string errorecode = ""; // error log
       public static string errdescroption = "";// error log
       public static int language = 0;
       public static string doorstatus = "";
       public static int dispensed_money = 0;
       public static int number_of_peapers = 0;
       public static int number_of_patches = 0;
       public static int retracted_peapers = 0;
       public static string tranactionid = "";
       public static double? recomp = 0;
       public static int? payRec = null;

       public static int fcst1 = 0;// عدد الاوراق بالدرج الاول
       public static int fcst2 = 0;// عدد الاوراق بالدج الثاني
       public static int fcst3 = 0;//عدد الاوراق بالدرج الثالث
       public static int fcst4 = 0;//عدد الاوراق بالدرج الرابع

       public static int dispinse11 = 0;//dispensed from 1 or 2 in every patch
       public static int dispinse22 = 0;//dispensed from 3 or 4 in every patch
       public static int dispinse33 = 0;
       public static int dispinse44 = 0;

       public static int rejected1 = 0;//rejected from 1 or 2 in every patch
       public static int rejected2 = 0;//rejected from 3 or 4 in every patch
       public static int rejected3 = 0;
       public static int rejected4 = 0;

       public static int dispinse1 = 0;//all despinsed from 1 or 2
       public static int dispinse2 = 0;// all dispensed from 3 or 4
       public static int dispinse3 = 0;//not for now
       public static int dispinse4 = 0;//not for now


       public static int rejectedbox1 = 0;//all rejected from 1 or 2
       public static int rejectedbox2 = 0;//all rejected from 3 or 4
       public static int rejectedbox3 = 0;
       public static int rejectedbox4 = 0;

       public static int paidsalary = 0;//financial log
       public static string serverpath = "";
       public static string Empname = "";
       public static long fullcash = 0;//financial log
       public static int thetruthresault = 0;
       public static int restofthecash = 0;
       /// <summary>
       /// نوع العملة في كل درج
       /// </summary>
       public static int cst1 = 0;
       public static int cst2 = 0;
       public static int cst3 = 0;
       public static int cst4 = 0;

       public static string cs1type = "";
       public static string cs2type = "";
       public static string cs3type = "";
       public static string cs4type = "";
       
       //new props
       public static int CamPortNo=6;
       public static string CamPortName ="COM6";

       public static int DispenserPortNo =4;
       public static string DispenserPortName ="COM4";

       public static string VisitLogsPath="";
       public static string FinanceLogsPath = "";



      static _session() 
      
      {
      
      
      }
    /*   public enum [esitiings]

           end enum */
    }
}
