using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RLogicServiceMvc.Models
{
    public class ManualBookingPODModel
    {
        public int ManualBookingID { get; set; }
        public DateTime DeliveryDateTime { get; set; }
        public HttpPostedFileBase PODUpload { get; set; }
        public string UploadSignature1_txtSignatureValue { get; set; }
        public string AttachmentPath { get; set; }
    }
}
