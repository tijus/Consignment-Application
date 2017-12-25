using RLogicServiceMvc.Models;
using RLogicServiceMvc.Service.External;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RLogicServiceMvc.Controllers
{
    public class ManualBookingPODController : Controller
    {
        //
        // GET: /ManualBookingPOD/

        public ActionResult Index()
        {
            
            return View("~/Views/ManualBookingPODView.cshtml");
        }

        [HttpPost]
        public ActionResult Save(ManualBookingPODModel BookingPODModel)
        {
            String uploadsignature = Request.Form["UploadSignature1_txtSignatureValue"];
            MessageModel msg = new MessageModel();
            try
            {
                var fileName = "";
                if (Request.Files[0].ContentLength > 0)
                {
                    fileName = Path.GetFileName(BookingPODModel.PODUpload.FileName);
                    var path = Path.Combine(Server.MapPath("~/Images"), fileName);
                    BookingPODModel.PODUpload.SaveAs(path);
                }
                BookingPODModel.AttachmentPath = fileName;
                ManualBookingPODService bkService = new ManualBookingPODService();
                msg = bkService.Save(BookingPODModel, "ripl");
            }
            catch(Exception ex)
            {
                msg.MessageId = 1;
                msg.Message = ex.Message;
            }
            return Json(msg);         
        }
        
    }
}
