using Newtonsoft.Json;
using RLogicServiceMvc.Models;
using RLogicServiceMvc.Service.External;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RLogicServiceMvc.Controllers
{
    public class ManualBookingTrackNTraceController : Controller
    {
        //
        // GET: /ManualBookingTrackNTrace/

        public ActionResult Index()
        {
            return View("~/Views/ManualBookingTrackTraceView.cshtml");
        }
        public ActionResult GetTrackDetails(int ManualBookingId, string CompanyCode)
        {
            MessageModel msg = new MessageModel();
            ManualBookingTrackNTraceModel MBT = new ManualBookingTrackNTraceModel();
            ManualBookingTrackNTraceService bkService = new ManualBookingTrackNTraceService();
            

                MBT = bkService.GetTrackDetails(ManualBookingId, "ripl");
                var result = MBT.TrackDs;

                var json = JsonConvert.SerializeObject(result, Formatting.Indented);
            return Json(json, JsonRequestBehavior.AllowGet);
        }
    }
}
