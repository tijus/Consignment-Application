using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RLogicServiceMvc.Models
{
    public class ManualBookingTrackModel
    {
        public int ManualBookingId { get; set; }
        public DateTime TrackingDate { get; set; }
        public string Location { get; set; }
       public string Remark { get; set; }
        public string Reason { get; set; }
        public int ReasonId { get; set; }
    }
}