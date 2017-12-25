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
    public class ManualBookingTrackService
    {
        public ManualBookingTrackModel GetByKeyId(int ManualBookingId)
        {
            ManualBookingTrackModel mbm = new ManualBookingTrackModel();
            //mbm.BookingNo = "1234";
            return mbm;
        }
        public MessageModel Save(ManualBookingTrackModel MbtModel, string CompanyCode)
        {
            MessageModel objMessage = new MessageModel();
            string conString = CompanyConfigService.getDbCredentials(CompanyCode);
            SqlDataAceessLayer _dal = new SqlDataAceessLayer(conString);

            string Query = @"INSERT INTO Domestic.Trn_Vehicle_Tracking
                             (Vehicle_ID,Trip_ID,Booking_ID,Manual_Booking_ID,Tracking_Date,Location,
                              Tracking_Time,Remarks,Reason_ID,KMs)
                              Select 0,0,0," + MbtModel.ManualBookingId.ToString() + ",'" + MbtModel.TrackingDate.ToString("yyyy-MM-dd") + "','" +
                                     MbtModel.Location + "','" + MbtModel.TrackingDate.ToString("HH:mm") + "','" + MbtModel.Remark + "'," + MbtModel .ReasonId+ ",0";


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

        public DataSet getStatus(string con)
        {
            SqlDataAceessLayer _dal = new SqlDataAceessLayer(con);
            string Query = "Select Reason_ID,Reason from Common.Master_Reason a inner join Common.Master_Reason_Category b on "+
                           "a.Master_Reason_Category_ID = b.Master_Reason_Category_ID where b.Master_Reason_Category='Vehicle Tracking' ";

            DataSet Ds = _dal.ExecuteSql(Query);

            return Ds;
        }
    }
}