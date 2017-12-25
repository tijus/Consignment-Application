using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RLogicServiceMvc.Models;
using RLogicServiceMvc.Service.Internal;
using RLogicServiceMvc.Service.External;

namespace RLogicServiceMvc.Controllers
{
    public class UserAuthenticationController : Controller
    {
        //
        // GET: /UserAuthentication/

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ValidateUser(string CompanyCode, string Login, string Password)
        {
            string _con = CompanyConfigService.getDbCredentials(CompanyCode);
            MessageModel msg = new MessageModel();
            if (_con.Length > 0)
            {
                AuthenticationService AS = new AuthenticationService();
                msg = AS.ValidateUser(_con, Login, Password);
            }
            else
            {
                msg.MessageId = 1;
                msg.Message = "Invalid Login/Password.";
            }

            return Json(msg,JsonRequestBehavior.AllowGet);
        }

    }
}
