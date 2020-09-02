using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Http;
using AutoMapper;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using TheCodeCamp.Data;
using TheCodeCamp.Models;



namespace TheCodeCamp.Controllers
{
    public class OperationsController : ApiController
    {
        [HttpOptions]
        [Route("api/refreshingconfig")]
        public IHttpActionResult RefreshAppSettings() 
        {
            try
            {
                ConfigurationManager.RefreshSection("AppSettings");
                return Ok();
            }
            catch (Exception ex )
            {
                return InternalServerError(ex);
            }
        }


        //// GET: Operations
        //public ActionResult Index()
        //{
        //    return View();
        //}
    }
}