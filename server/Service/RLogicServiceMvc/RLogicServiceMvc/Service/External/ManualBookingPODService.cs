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
    public class ManualBookingPODService
    {
       
        public MessageModel Save(ManualBookingPODModel MbtModel, string CompanyCode)
        {
            MessageModel objMessage = new MessageModel();
            string conString = CompanyConfigService.getDbCredentials(CompanyCode);
            SqlDataAceessLayer _dal = new SqlDataAceessLayer(conString);

            string Query = "Select Reason_ID,Reason from Common.Master_Reason a inner join Common.Master_Reason_Category b on " +
                           "a.Master_Reason_Category_ID = b.Master_Reason_Category_ID where b.Master_Reason_Category='POD' ";

            DataSet Ds1 = _dal.ExecuteSql(Query);
            string ReasonId = "0";
            if (Ds1.Tables[0].Rows.Count == 0)
            {
                objMessage.MessageId = 2;
                objMessage.Message = "POD Status Not Found";
                return objMessage;
            }
            else
            {
                ReasonId = Ds1.Tables[0].Rows[0]["Reason_ID"].ToString();
            }


            Query = @"INSERT INTO Domestic.Trn_Vehicle_Tracking
                             (Vehicle_ID,Trip_ID,Booking_ID,Manual_Booking_ID,Tracking_Date,Location,Tracking_Time,Remarks,Reason_ID,KMs,Attachment_Path,Signature)" +
                             "Select 0,0,0," + MbtModel.ManualBookingID.ToString() + ",'" + MbtModel.DeliveryDateTime.ToString("yyyy-MM-dd") + "','','" +
                              MbtModel.DeliveryDateTime.ToString("HH:mm") + "',''," + ReasonId + ",0,'" + MbtModel.AttachmentPath + "','" + MbtModel.UploadSignature1_txtSignatureValue + "'";


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