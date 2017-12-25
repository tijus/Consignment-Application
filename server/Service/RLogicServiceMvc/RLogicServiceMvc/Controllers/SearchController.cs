using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RLogicServiceMvc.Service.External;
using System.Data;
using RLogicServiceMvc.Service.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace RLogicServiceMvc.Controllers
{
    public class SearchController : Controller
    {
        //
        // GET: /Search/

        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public JObject getVehicle(string CompanyCode, string SearchFor)
        {
            string _con = CompanyConfigService.getDbCredentials(CompanyCode);

            SerachServices SS = new SerachServices();
            DataSet Ds = SS.getVehicle(_con, SearchFor);

            string json = JsonConvert.SerializeObject(Ds);
            JObject jobj = JObject.Parse(json);
            return jobj;
        }

        [HttpGet]
        public JObject getLocation(string CompanyCode, string SearchFor)
        {
            string _con = CompanyConfigService.getDbCredentials(CompanyCode);

            SerachServices SS = new SerachServices();
            DataSet Ds = SS.getLocation(_con, SearchFor);

            string json = JsonConvert.SerializeObject(Ds);
            JObject jobj = JObject.Parse(json);
            return jobj;
        }

        [HttpGet]
        public JObject getClient(string CompanyCode, string SearchFor)
        {
            string _con = CompanyConfigService.getDbCredentials(CompanyCode);

            SerachServices SS = new SerachServices();
            DataSet Ds = SS.getClient(_con, SearchFor);

            string json = JsonConvert.SerializeObject(Ds);
            JObject jobj = JObject.Parse(json);
            return jobj;
        }

        [HttpGet]
        public JObject getConsignorConsignee(string CompanyCode, string SearchFor)
        {
            string _con = CompanyConfigService.getDbCredentials(CompanyCode);

            SerachServices SS = new SerachServices();
            DataSet Ds = SS.getConsignorConsignee(_con, SearchFor);

            string json = JsonConvert.SerializeObject(Ds);
            JObject jobj = JObject.Parse(json);
            return jobj;
        }
    }
}
