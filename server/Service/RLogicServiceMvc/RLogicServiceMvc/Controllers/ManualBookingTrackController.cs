using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RLogicServiceMvc.Models;
using RLogicServiceMvc.Service.External;
using RLogicServiceMvc.Service.Internal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RLogicServiceMvc.Controllers
{
    public class ManualBookingTrackController : Controller
    {
        //
        // GET: /ManualBookingTrack/

        public ActionResult Index()
        {
            ManualBookingTrackModel mbt = new ManualBookingTrackService().GetByKeyId(0);
            ViewBag.BookingTrackModel = mbt;
            return View("~/Views/ManualBookingTrackView.cshtml");
        }

        public ActionResult Save(ManualBookingTrackModel MbtModel)
        {
            ManualBookingTrackService mbtservice = new ManualBookingTrackService();
            MessageModel msg = mbtservice.Save(MbtModel, "ripl");
            return Json(msg); 
           
        }

        [HttpGet]
        public JObject getStatus(string CompanyCode)
        {
            string _con = CompanyConfigService.getDbCredentials(CompanyCode);

            ManualBookingTrackService mbtservice = new ManualBookingTrackService();
            DataSet Ds = mbtservice.getStatus(_con);

            string json = JsonConvert.SerializeObject(Ds);
            JObject jobj = JObject.Parse(json);
            return jobj;
        }

    }
}
