using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace TheCodeCamp.Controllers
{
    public class CampsController : ApiController
    {
        public object Get()
        {
            return new { Name = "Shawn", Occupation = "Teacher" };
        }

    }
}