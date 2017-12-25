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
    public class ManualBookingTrackNTraceService
    {
        public ManualBookingTrackNTraceModel GetTrackDetails(int ManualBookingId, string CompanyCode)
        {
            string conString = CompanyConfigService.getDbCredentials(CompanyCode);
            SqlDataAceessLayer _dal = new SqlDataAceessLayer(conString);
            string Query = @"SELECT Tracking_Date,Tracking_Time,Location,Reason,Remarks,A.Attachment_Path,[Signature]
                            FROM    Domestic.Trn_Vehicle_Tracking A INNER JOIN  
                                    Custom.Trn_Booking_Manual B ON A.Manual_Booking_ID = B.Booking_Manual_ID LEFT OUTER JOIN
                                    Common.Master_Reason C ON A.Reason_ID = C.Reason_ID where A.Manual_Booking_ID = " + ManualBookingId ;

            DataSet Ds = _dal.ExecuteSql(Query);
            ManualBookingTrackNTraceModel mbm = new ManualBookingTrackNTraceModel();
            mbm.TrackDs = Ds;
            return mbm;
        }
    }
}