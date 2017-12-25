using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RLogicServiceMvc.Service.DataAccess;
using System.Data;

namespace RLogicServiceMvc.Service.Internal
{
    public class CompanyConfigService
    {
        public static string GetParameter(String KeyName)
        {
            return System.Configuration.ConfigurationManager.AppSettings.Get(KeyName);
        }

        public static string getDbCredentials(string CompanyCode)
        {
            SqlDataAceessLayer _dal = new SqlDataAceessLayer();
            DataSet Ds = _dal.ExecuteSql("SELECT DbCredentials FROM  dbo.Company_Config where CompanyCode = '" + CompanyCode + "'");

            if (Ds.Tables[0].Rows.Count == 0)
            {
                throw new Exception("Comapny Config not found for comapny code " + CompanyCode);
            }
            
            return Ds.Tables[0].Rows[0]["DbCredentials"].ToString();            
        }

        public static int getYearCode(DateTime Date)
        {
            int Month = Date.Month;
            int yearcode;

            if (Month >= 4)
            {
                yearcode = Date.Year;
            }
            else
            {
                yearcode = Date.Year - 1;
            }
            return (yearcode - 2000);
        }
    }
}