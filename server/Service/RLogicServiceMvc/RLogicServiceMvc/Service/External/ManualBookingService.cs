using RLogicServiceMvc.Models;
using RLogicServiceMvc.Service.DataAccess;
using RLogicServiceMvc.Service.Internal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace RLogicServiceMvc.Service.External
{
    public class ManualBookingService
    {
        public ManualBookingModel GetByKeyId(int ManualBookingId)
        {
            ManualBookingModel mbm = new ManualBookingModel();
            //mbm.BookingNo = "1234";
            return mbm;
        }

        private ManualBookingModel GenerateManualBookingModel(DataRow Row)
        {
            ManualBookingModel mbm = new ManualBookingModel();
            
            mbm.BookingNo = Row["LR_No"].ToString();
            mbm.BookingDate = Convert.ToDateTime(Row["LR_Date"].ToString());
            mbm.FromLocation = Row["From_Service_Network"].ToString();
            mbm.ToLocation = Row["To_Service_Network"].ToString();
            mbm.VechicleNo = Row["Vehicle_No"].ToString();
            mbm.CustomerName = Row["Customer_Name"].ToString();
            mbm.BookingWeight = Decimal.Parse(Row["Weight"].ToString());
            mbm.BookingNoOfPackages = int.Parse(Row["NoOfArticles"].ToString());
            mbm.ManualBookingId = int.Parse(Row["Booking_Manual_ID"].ToString());
            mbm.AttachmentPath = Row["Attachment_Path"].ToString();
            return mbm;
        }

        public ManualBookingModel GetByBookingNo(string BookingNo, string CompanyCode)
        {
            string conString = CompanyConfigService.getDbCredentials(CompanyCode);
            SqlDataAceessLayer _dal = new SqlDataAceessLayer(conString);
            int YearCode = CompanyConfigService.getYearCode(DateTime.Now);
            string Query = @"Select Booking_Manual_ID,LR_No,LR_Date,From_Service_Network,To_Service_Network,Vehicle_No,Customer_Name,
                             Weight,NoOfArticles,Attachment_Path From Custom.Trn_Booking_Manual 
                             where Year_Code = " + YearCode .ToString()+ " and LR_No = '" + BookingNo + "'";

            DataSet Ds = _dal.ExecuteSql(Query);
            var mbm = GenerateManualBookingModel(Ds.Tables[0].Rows[0]);
            return mbm;
        }

        public MessageModel Save(ManualBookingModel BookingModel, string CompanyCode, int UserId)
        {
            string conString = CompanyConfigService.getDbCredentials(CompanyCode);
            MessageModel objMessage = new MessageModel();
            SqlDataAceessLayer _dal = new SqlDataAceessLayer(conString);
            int YearCode = CompanyConfigService.getYearCode(BookingModel.BookingDate);
            string Query = @"INSERT INTO [Custom].[Trn_Booking_Manual]
                            (Year_Code,LR_No,LR_Date,From_Service_Network_ID,From_Service_Network,To_Service_Network_ID,
                            To_Service_Network,Vehicle_ID,Vehicle_No,Customer_ID,Customer_Name,Weight,NoOfArticles,Is_Active,
                            Created_On,Created_By,Updated_On,Updated_By,Attachment_Path)
                            Select " + YearCode .ToString()+ ",'" + BookingModel.BookingNo + "','" + BookingModel.BookingDate + "'," 
                                         + BookingModel.FromLocationId + ",'" + BookingModel.FromLocation + "'," 
                                         + BookingModel.ToLocationId + ",'" + BookingModel.ToLocation + "'," 
                                         + BookingModel.VehicleId + ",'" + BookingModel.VechicleNo + "'," 
                                         + BookingModel.CustomerId + ",'" + BookingModel.CustomerName + "'," 
                                         + BookingModel.BookingWeight + "," + BookingModel.BookingNoOfPackages
                                         + ",1,getdate()," + UserId + ",getdate()," + UserId + ",'" + BookingModel.AttachmentPath+"'"
                                         + "; Select 1 ";

            try
            {
                DataSet Ds = _dal.ExecuteSql(Query);
                objMessage.MessageId = 0;
                objMessage.Message = "Save Sucessfully";
            }
            catch (Exception ex)
            {
                objMessage.MessageId = 1;
                objMessage.Message = ex.Message;
            }
            return objMessage;
        }
    }
}