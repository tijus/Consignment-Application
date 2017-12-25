using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RLogicServiceMvc.Models
{
    public class ManualBookingModel
    {
        public int ManualBookingId { get; set; }
        public string BookingNo { get; set; }
        public DateTime BookingDate { get; set; }
        public string VechicleNo { get; set; }
        public int VehicleId { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string FromLocation { get; set; }
        public string FromLocationId { get; set; }
        public string ToLocation { get; set; }
        public string ToLocationId { get; set; }
        public DateTime BookingEdd { get; set; }
        public int BookingNoOfPackages { get; set; }
        public Decimal BookingWeight { get; set; }
        
        public string AttachmentPath { get; set; }
    }
}