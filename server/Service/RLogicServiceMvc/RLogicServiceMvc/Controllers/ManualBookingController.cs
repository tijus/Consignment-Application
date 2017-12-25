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
    public class ManualBookingController : Controller
    {
        //
        // GET: /ManualBooking/
        
        [HttpGet]
        public ActionResult Index()
        {
            ManualBookingModel mbm = new ManualBookingService().GetByKeyId(0);
            ViewBag.BookingModel = mbm;
            return View("~/Views/ManualBookingView.cshtml");
        }

        public ActionResult GetManualBookingByNo(string ManualBookingNo)
        {
            MessageModel msg = new MessageModel();
            ManualBookingService bkService = new ManualBookingService();
            try
            {
                var mbm = bkService.GetByBookingNo(ManualBookingNo, "ripl");
                msg.ResultObject = mbm;
            }
            catch(Exception ex)
            {
                msg.MessageId = 1;
                msg.Message = ex.Message;
            }

            return Json(msg,JsonRequestBehavior.AllowGet); 
        }

        [HttpPost]
        public ActionResult Save(ManualBookingModel BookingModel)
        {
            MessageModel msg = new MessageModel();
            try
            {
                HttpPostedFileBase Upload = Request.Files[0];
                var fileName = "";
                if (Request.Files[0].ContentLength > 0)
                {
                    fileName = Path.GetFileName(Upload.FileName);
                    var path = Path.Combine(Server.MapPath("~/Attachments"), fileName);

                    Upload.SaveAs(path);
                }
                BookingModel.AttachmentPath = fileName;
                ManualBookingService bkService = new ManualBookingService();
                msg = bkService.Save(BookingModel, "ripl", 123);
            }
            catch(Exception ex)
            {
                msg.MessageId = 1;
                msg.Message = ex.Message;
            }
            return Json(msg, JsonRequestBehavior.AllowGet);            
        }
      


        
     }
}
