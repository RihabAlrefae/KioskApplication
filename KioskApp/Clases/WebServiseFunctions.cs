using KioskApp.HelloWord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KioskApp.Clases
{
  static  class  WebServiseFunctions
    {
      private static KioskAppWebServiceSoapClient fromyoutube = new KioskAppWebServiceSoapClient();
      public static int? Authenticate_Class(int empid, string emppin)
      {
          int? resault=null;
          try
          {
              resault = fromyoutube.Authenticate(empid, emppin);
              writingLogs.WriteWebServiceLog("Authenticate(" + empid + "," + emppin + ")", resault.ToString());
          }
          catch (Exception err) { MessageBox.Show(err.Message); writingLogs.WriteWebServiceLog("Authenticate webservice exception: Authenticate(" + empid + "," + emppin + ")", err.Message.ToString() + "\r\n"); }
          
          return resault;
      }
      public static String GetEmpName_Class(int empid, int lang)
      {
          String resault = "";
          try
          {
              resault = fromyoutube.GetEmpName(empid,lang);
              writingLogs.WriteWebServiceLog("GetEmpName(" + empid + "," + lang + ")", resault.ToString(),1);
          }
          catch (Exception err) { writingLogs.WriteWebServiceLog("GetEmpName webservice exception: GetEmpName(" + empid + "," + lang + ")", err.Message.ToString() + "\r\n",1); }
          return resault;
      }
      public static bool? getEmpState_Class(int EmpId)
      {
          bool? resault = null;
          try
          {
              resault = fromyoutube.getEmpState(EmpId);
              writingLogs.WriteWebServiceLog("getEmpState(" + EmpId + ")", resault.ToString());
          }
          catch (Exception err) { writingLogs.WriteWebServiceLog("getEmpState webservice exception: getEmpState(" + EmpId + ")", err.Message.ToString() + "\r\n"); }
          return resault;
      }
      public static int? UpdateState_Class(int EmpId)
      {
          int? resault = null;
          try
          {
             resault= fromyoutube.UpdateState(EmpId);
             writingLogs.WriteWebServiceLog("UpdateState(" + EmpId + ")", resault.ToString());
          }
          catch (Exception err) { writingLogs.WriteWebServiceLog("UpdateState webservice exception: UpdateState(" + EmpId + ")", err.Message.ToString() + "\r\n"); }
          return resault;
      }
      public static int? UpdatePin_Class(int EmpId, string newPin)
      {
          int? resault = null;
          try
          {
              resault = fromyoutube.UpdatePin(EmpId, newPin);
              writingLogs.WriteWebServiceLog("UpdatePin(" + EmpId + ","+newPin+")", resault.ToString());
          }
          catch (Exception err) { writingLogs.WriteWebServiceLog("UpdatePin webservice exception:UpdatePin(" + EmpId + "," + newPin + ")", err.Message.ToString() + "\r\n"); }
          return resault;
      }
      public static double? GetPayrool_Class(int EmpId, int serviceno)
      {
          double? resault = null;
          try
          {
              resault = fromyoutube.GetPayrool(EmpId, serviceno);
              writingLogs.WriteWebServiceLog("GetPayrool(" + EmpId + "," + serviceno + ")", resault.ToString());
          }
          catch (Exception err) { writingLogs.WriteWebServiceLog("GetPayrool webservice exception:GetPayrool(" + EmpId + "," + serviceno + ")", err.Message.ToString() + "\r\n"); }
          return resault;
      }
      public static int? updatesalstat_Class(int EmpId, int serviceno)
      {
          int? resault = null;
          try
          {
              resault = fromyoutube.updatesalstat(EmpId, serviceno);
              writingLogs.WriteWebServiceLog("updatesalstat(" + EmpId + "," + serviceno + ")", resault.ToString());
          }
          catch (Exception err) { writingLogs.WriteWebServiceLog("updatesalstat webservice exception:updatesalstat(" + EmpId + "," + serviceno + ")", err.Message.ToString() + "\r\n"); }
          return resault;
      }
      public static int updatethetrue_Class(int thetruth, int recordid, int restofthecash, int servicenumber)
      {
          int resault=-2;
          try
          {
              resault = fromyoutube.updatethetrue_resault(thetruth, recordid, restofthecash, servicenumber);
              writingLogs.WriteWebServiceLog("updatethetrue_resault(" + thetruth + "," + recordid + "," + restofthecash + "," + servicenumber + ")", resault.ToString());
          }
          catch (Exception err) { writingLogs.WriteWebServiceLog("updatethetrue_resault webservice exception:updatesalstat(" + thetruth + "," + recordid + "," + restofthecash + "," + servicenumber + ")", err.Message.ToString() + "\r\n"); }
          return resault;
      }
      public static int? paymentrecid_Class(int EmpId, int serviceno)
      {
          int? resault = null;
          try
          {
              resault = fromyoutube.paymentrecid(EmpId, serviceno);
              writingLogs.WriteWebServiceLog("paymentrecid(" + EmpId + "," + serviceno + ")", resault.ToString());
          }
          catch (Exception err) { writingLogs.WriteWebServiceLog("paymentrecid webservice exception:paymentrecid(" + EmpId + "," + serviceno + ")", err.Message.ToString() + "\r\n"); }
          return resault;
      }
      public static bool? authinticatFilluser_Class(int empid, string pass)
      {
          bool? resault = null;
          try
          {
              resault = fromyoutube.authinticatFilluser(empid, pass);
              writingLogs.WriteWebServiceLog("authinticatFilluser(" + empid + "," + pass + ")", resault.ToString(),0);
          }
          catch (Exception err) {   writingLogs.WriteWebServiceLog("authinticatFilluser webservice exception:authinticatFilluser(" + empid + "," + pass  +")", err.Message.ToString() + "\r\n",0); }
          return resault;
      }
      public static int getCasettevalue_Class(int kioskid, out int c2, out int c3, out int c4, out int r1, out int r2, out int r3, out int r4, out int config1, out int config2, out int config3, out int config4)
      {
          int resault = 0;
          c2 = 0;
          c3 = 0;
          c4 = 0;
          r1 = 0;
          r2 = 0;
          r3 = 0;
          r4 = 0;
          config4 = 0;
          config3 = 0;
          config2 = 0;
          config1 = 0;
          try
          {
              resault = fromyoutube.getCasettevalue(kioskid, out c2, out c3,out  c4, out r1, out r2, out r3, out r4,out config1, out config2,out config3,out config4);
              writingLogs.WriteWebServiceLog("getCasettevalue(" + kioskid + ")", resault.ToString() + " " + c2.ToString() + "," + c3.ToString() + "," + c4.ToString() + "," + r1.ToString() + "," + r2.ToString() + "," + r3.ToString() + "," + r4.ToString() + "," + config1.ToString() + "," + config2.ToString() + "," + config3.ToString() + "," + config4.ToString());
          }
          catch (Exception err) { writingLogs.WriteWebServiceLog("getCasettevalue webservice exception:getCasettevalue(" + kioskid + ")", err.Message.ToString() + "\r\n"); }
          return resault;
      }
      public static string get_cassete_types_Class(int kioskid, out string cst2, out string cst3, out string cst4)
      {
          string resault = "";
          cst2 = "";
          cst3 = "";
          cst4 = "";
          try
          {
              resault = fromyoutube.get_cassete_types(kioskid, out  cst2, out  cst3, out  cst4);
              writingLogs.WriteWebServiceLog("get_cassete_types(" + kioskid + ")", resault.ToString() + " " + cst2.ToString() + "," + cst3.ToString() + "," + cst4.ToString());
          }
          catch (Exception err) { writingLogs.WriteWebServiceLog("get_cassete_types webservice exception:get_cassete_types(" + kioskid + ")", err.Message.ToString() + "\r\n"); }
          return resault;
      }
      public static int? set_cassettes_types_Class(int kioskid, int cst1type, int cst2type, int cst3type, int cst4type)
      {
          int? resault = null;
          try
          {
              resault = fromyoutube.set_cassettes_types(kioskid, cst1type, cst2type, cst3type, cst4type);
              writingLogs.WriteWebServiceLog("set_cassettes_types(" + kioskid + "," + cst1type + ","+cst2type+","+cst3type+","+cst4type+")", resault.ToString());
          }
          catch (Exception err) { writingLogs.WriteWebServiceLog("set_cassettes_types webservice exception:set_cassettes_types(" + kioskid + "," + cst1type + "," + cst2type + "," + cst3type + "," + cst4type + ")", err.Message.ToString() + "\r\n"); }
          return resault;
      }

      public static int fillprocess_Class(int kioskid, int newcst1, int newcst2, int newcst3, int newcst4, int usrid, string image1, string tranid, int oldcst1, int oldcst2, int oldcst3, int oldcst4, string oldconf1, string oldconf2, string oldconf3, string oldconf4, string newconf1, string newconf2, string newconf3, string newconf4,string Notes)
      {
          int resault = -1;
          try
          {
              resault = fromyoutube.fillprocess(kioskid, newcst1, newcst2, newcst3, newcst4, usrid, image1, tranid, oldcst1, oldcst2, oldcst3, oldcst4, oldconf1, oldconf2, oldconf3, oldconf4, newconf1, newconf2, newconf3, newconf4,Notes);
              writingLogs.WriteWebServiceLog("fill without reciept==>  fillprocess(" + kioskid + "," + newcst1 + "," + newcst2 + "," + newcst3 + "," + newcst4 + "," + usrid + "," + image1 + "," + tranid + "," + oldcst1 + "," + oldcst2 + "," + oldcst3 + "," + oldcst4 + "," + oldconf1 + "," + oldconf2 + "," + oldconf3 + "," + oldconf4 + "," + newconf1 + "," + newconf2 + "," + newconf3 + "," + newconf4 +","+Notes+ ")", resault.ToString());
          }
          catch (Exception err) { writingLogs.WriteWebServiceLog("fillprocess 'the fill without reciept' webservice exception:fillprocess1(" + kioskid + "," + newcst1 + "," + newcst2 + "," + newcst3 + "," + newcst4 + "," + usrid + "," + image1 + "," + tranid + "," + oldcst1 + "," + oldcst2 + "," + oldcst3 + "," + oldcst4 + "," + oldconf1 + "," + oldconf2 + "," + oldconf3 + "," + oldconf4 + "," + newconf1 + "," + newconf2 + "," + newconf3 + "," + newconf4 + "," + Notes + ")", err.Message.ToString() + "\r\n"); }
          return resault;
      }


      public static int fillprocess1_Class(int kioskid, int newcst1, int newcst2, int newcst3, int newcst4, int usrid, string image1, string tranid, int oldcst1, int oldcst2, int oldcst3, int oldcst4, string oldconf1, string oldconf2, string oldconf3, string oldconf4, string newconf1, string newconf2, string newconf3, string newconf4)
      {
          int resault = -1;
          try
          {
              resault = fromyoutube.fillprocess1(kioskid, newcst1, newcst2, newcst3, newcst4, usrid, image1, tranid, oldcst1, oldcst2, oldcst3, oldcst4, oldconf1, oldconf2, oldconf3, oldconf4, newconf1, newconf2, newconf3, newconf4);
              writingLogs.WriteWebServiceLog("fillprocess1(" + kioskid + "," + newcst1 + "," + newcst2 + "," + newcst3 + "," + newcst4 +","+ usrid+","+ image1+","+ tranid+","+ oldcst1+","+ oldcst2+","+ oldcst3+","+ oldcst4+","+ oldconf1+","+ oldconf2+","+ oldconf3+","+ oldconf4+","+ newconf1+","+ newconf2+","+ newconf3+","+ newconf4 +")", resault.ToString());
          }
          catch (Exception err) { writingLogs.WriteWebServiceLog("fillprocess1 webservice exception:fillprocess1(" + kioskid + "," + newcst1 + "," + newcst2 + "," + newcst3 + "," + newcst4 + "," + usrid + "," + image1 + "," + tranid + "," + oldcst1 + "," + oldcst2 + "," + oldcst3 + "," + oldcst4 + "," + oldconf1 + "," + oldconf2 + "," + oldconf3 + "," + oldconf4 + "," + newconf1 + "," + newconf2 + "," + newconf3 + "," + newconf4 + ")", err.Message.ToString() + "\r\n"); }
          return resault;
      }
      public static int? updatecurrentcasettevalue_Class(int kioskid, int newcst1, int newcst2, int newcst3, int newcst4, int rej1, int rej2, int rej3, int rej4)
      {
          int? resault = null;
          try
          {
              resault = fromyoutube.updatecurrentcasettevalue(kioskid, newcst1, newcst2, newcst3, newcst4, rej1, rej2, rej3, rej4);
              writingLogs.WriteWebServiceLog("updatecurrentcasettevalue(" + kioskid + "," + newcst1 + "," + newcst2 + "," + newcst3 + "," + newcst4 + "," + rej1 + "," + rej2 + "," + rej3 + "," + rej4 + ")", resault.ToString());
          }
          catch (Exception err) { writingLogs.WriteWebServiceLog("updatecurrentcasettevalue webservice exception:updatecurrentcasettevalue("  +kioskid + "," + newcst1 + "," + newcst2 + "," + newcst3 + "," + newcst4 + "," + rej1 + "," + rej2 + "," + rej3 + "," + rej4 + ")", err.Message.ToString() + "\r\n"); }
          return resault;
      }
      public static bool insertvisit_Class(int kioskid, string tranid, int empid, int result, int paidsal, int salaryrec, int repaymentrec, string img1url = " ", string img2url = " ", string img3url = " ")
      {
          bool resault = false;
          try
          {
              resault = fromyoutube.insertvisit(kioskid,tranid, empid, result, paidsal, salaryrec, repaymentrec, img1url, img2url, img3url);
              writingLogs.WriteWebServiceLog("insertvisit(" + tranid + "," + kioskid + "," + empid + "," + result + "," + paidsal + "," + salaryrec + "," + repaymentrec + "," + img1url + "," + img2url + "," + img3url + ")", resault.ToString());
          }
          catch (Exception err) { writingLogs.WriteWebServiceLog("insertvisit webservice exception:insertvisit(" + kioskid + "," + tranid + "," + empid + "," + result + "," + paidsal + "," + salaryrec + "," + repaymentrec + "," + img1url + "," + img2url + "," + img3url + ")", err.Message.ToString() + "\r\n"); }
          return resault;
      }
      /*public static bool insertvisit_Class(int kioskid, string tranid, int empid, int result, int paidsal, int salaryrec, int repaymentrec, string img1url = " ", string img2url = " ", string img3url = " ")
      {
          bool resault = false;
          try
          {
              resault = fromyoutube.insertvisit(kioskid, tranid, empid, result, paidsal, salaryrec, repaymentrec, img1url, img2url, img3url);
              writingLogs.WriteWebServiceLog("insertvisit(" + kioskid + "," + tranid + "," + empid + "," + result + "," + paidsal + "," + salaryrec + "," + repaymentrec + "," + img1url + "," + img2url + "," + img3url + ")", resault.ToString());
          }
          catch (Exception err) { writingLogs.WriteWebServiceLog("insertvisit webservice exception:insertvisit(" + kioskid + "," + tranid + "," + empid + "," + result + "," + paidsal + "," + salaryrec + "," + repaymentrec + "," + img1url + "," + img2url + "," + img3url + ")", err.Message.ToString() + "\r\n"); }
          return resault;
      }*/

      public static bool insertvisit1_Class(int kioskid, string tranid, int empid, int result, int paidsal, int salaryrec, int repaymentrec, string img1url = " ", string img2url = " ", string img3url = " ",string Notes="")
      {
          bool resault = false;
          try
          {
              //resault = fromyoutube.insertvisit1(kioskid, tranid, empid, result, paidsal, salaryrec, repaymentrec, img1url, img2url, img3url,Notes);
              resault = fromyoutube.insertvisit(kioskid, tranid, empid, result, paidsal, salaryrec, repaymentrec, img1url, img2url, img3url);
              writingLogs.WriteWebServiceLog("insertvisit(" + tranid + "," + kioskid + "," + empid + "," + result + "," + paidsal + "," + salaryrec + "," + repaymentrec + "," + img1url + "," + img2url + "," + img3url + ")", resault.ToString());
          }
          catch (Exception err) { writingLogs.WriteWebServiceLog("insertvisit webservice exception:insertvisit(" + kioskid + "," + tranid + "," + empid + "," + result + "," + paidsal + "," + salaryrec + "," + repaymentrec + "," + img1url + "," + img2url + "," + img3url + ")", err.Message.ToString() + "\r\n"); }
          return resault;
      }

     /* public static bool insertvisit1_Class(int kioskid, string tranid, int empid, int result, int paidsal, int salaryrec, int repaymentrec, string img1url = " ", string img2url = " ", string img3url = " ", string Notes = "")
      {
          bool resault = false;
          try
          {
              resault = fromyoutube.insertvisit1(kioskid, tranid, empid, result, paidsal, salaryrec, repaymentrec, img1url, img2url, img3url, Notes);
              writingLogs.WriteWebServiceLog("insertvisit(" + kioskid + "," + tranid + "," + empid + "," + result + "," + paidsal + "," + salaryrec + "," + repaymentrec + "," + img1url + "," + img2url + "," + img3url + ")", resault.ToString());
          }
          catch (Exception err) { writingLogs.WriteWebServiceLog("insertvisit webservice exception:insertvisit(" + kioskid + "," + tranid + "," + empid + "," + result + "," + paidsal + "," + salaryrec + "," + repaymentrec + "," + img1url + "," + img2url + "," + img3url + ")", err.Message.ToString() + "\r\n"); }
          return resault;
      }*/

      public static int employeesalforrecipt_Class(int recid, out int REPAYMENT, out int TOTAL_REPAYMENT, out int OVERTIME, out int MATCHING, out int REWARD, out int ADVANCE, out int LOAN, out int WITHOUT_SALARY, out int NON_ATTENDANCE, out int SICKNESS, out int DISCOUNT, out int NET_PAY, out string MONTH1, out string YEAR1, out string NOTES)
      {
          //int rrid = 0;
         int FIX_SALARY = 0;
          REPAYMENT = 0;
          TOTAL_REPAYMENT = 0;
          OVERTIME = 0;
          MATCHING = 0;
          REWARD = 0;
          ADVANCE = 0;
          LOAN = 0;
          WITHOUT_SALARY = 0;
          NON_ATTENDANCE = 0;
          SICKNESS = 0;
          DISCOUNT = 0;
          NET_PAY = 0;
          MONTH1 = "";
          YEAR1 = "";
          NOTES = "";
          try
          {
              FIX_SALARY = fromyoutube.employeesalforrecipt(recid, out  REPAYMENT, out  TOTAL_REPAYMENT, out  OVERTIME, out  MATCHING, out  REWARD, out  ADVANCE, out  LOAN, out  WITHOUT_SALARY, out  NON_ATTENDANCE, out  SICKNESS, out  DISCOUNT, out  NET_PAY, out  MONTH1, out  YEAR1, out  NOTES);
              writingLogs.WriteWebServiceLog("employeesalforrecipt(" + recid + ")", FIX_SALARY.ToString() + " " + REPAYMENT.ToString() + "," + TOTAL_REPAYMENT.ToString() + "," + OVERTIME.ToString() + "," + MATCHING.ToString() + "," + REWARD.ToString() + "," + ADVANCE.ToString() + "," + LOAN.ToString() + "," + WITHOUT_SALARY.ToString() + "," + NON_ATTENDANCE.ToString() + "," + SICKNESS.ToString() + "," + DISCOUNT.ToString() + "," + NET_PAY.ToString() + "," + MONTH1.ToString() + "," + YEAR1.ToString() + "," + NOTES.ToString());
          }
          catch (Exception err) { writingLogs.WriteWebServiceLog("employeesalforrecipt webservice exception:employeesalforrecipt(" + recid + ")", err.Message.ToString() + "\r\n"); }
          return FIX_SALARY;
      }
      public static int employeerecompforrecipt_Class(int recid, out int SERVICE_2, out int SERVICE_3, out int SERVICE_4, out int SERVICE_5, out int SERVICE_6, out int SERVICE_7, out int SERVICE_8, out int SERVICE_9, out int SERVICE_10, out int TOTAL, out string NOTES)
      {
         int SERVICE_1 = 0;
          SERVICE_2 = 0;
          SERVICE_3 = 0;
          SERVICE_4 = 0;
          SERVICE_5 = 0;
          SERVICE_6 = 0;
          SERVICE_7 = 0;
          SERVICE_8 = 0;
          SERVICE_9 = 0;
          SERVICE_10 = 0;
          TOTAL = 0;
          NOTES = "";
          try
          {
              SERVICE_1 = fromyoutube.employeerecompforrecipt(recid, out  SERVICE_2, out  SERVICE_3, out  SERVICE_4, out  SERVICE_5, out  SERVICE_6, out  SERVICE_7, out  SERVICE_8, out  SERVICE_9, out  SERVICE_10, out  TOTAL, out  NOTES);
              writingLogs.WriteWebServiceLog("employeerecompforrecipt(" + recid + ")", SERVICE_1.ToString() + " " + SERVICE_2.ToString() + "," + SERVICE_3.ToString() + "," + SERVICE_4.ToString() + "," + SERVICE_5.ToString() + "," + SERVICE_6.ToString() + "," + SERVICE_7.ToString() + "," + SERVICE_8.ToString() + "," + SERVICE_9.ToString() + "," + SERVICE_10.ToString() + "," + TOTAL.ToString() + "," + NOTES.ToString());
          }
          catch (Exception err) { writingLogs.WriteWebServiceLog("employeerecompforrecipt webservice exception:employeerecompforrecipt(" + recid + ")", err.Message.ToString() + "\r\n"); }
          return SERVICE_1;
      }
      public static string servicesnames_Class(out string SERVICE2, out string SERVICE3, out string SERVICE4, out string SERVICE5, out string SERVICE6, out string SERVICE7, out string SERVICE8, out string SERVICE9, out string SERVICE10)
      {
          string SERVICE1 = "";
          SERVICE2 = "";
          SERVICE3 = "";
          SERVICE4 = "";
          SERVICE5 = "";
          SERVICE6 = "";
          SERVICE7 = "";
          SERVICE8 = "";
          SERVICE9 = "";
          SERVICE10 = "";
          try{
              SERVICE1 = fromyoutube.servicesnames(out  SERVICE2, out  SERVICE3, out  SERVICE4, out  SERVICE5, out  SERVICE6, out  SERVICE7, out  SERVICE8, out  SERVICE9, out  SERVICE10);
              writingLogs.WriteWebServiceLog("servicesnames()", SERVICE1.ToString() + " " + SERVICE2.ToString() + "," + SERVICE3.ToString() + "," + SERVICE4.ToString() + "," + SERVICE5.ToString() + "," + SERVICE6.ToString() + "," + SERVICE7.ToString() + "," + SERVICE8.ToString() + "," + SERVICE9.ToString() + "," + SERVICE10.ToString() );
          }
          catch (Exception err) { writingLogs.WriteWebServiceLog("servicesnames webservice exception:servicesnames()", err.Message.ToString() + "\r\n"); }
          return SERVICE1;
      }
    }
}
