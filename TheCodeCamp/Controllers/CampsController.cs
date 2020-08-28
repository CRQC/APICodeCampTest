using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using TheCodeCamp.Data;

namespace TheCodeCamp.Controllers
{
    public class CampsController : ApiController
    {
        private readonly ICampRepository _repository;

        public CampsController(ICampRepository repository)
        {
            _repository = repository;
        }


        public async Task<IHttpActionResult> Get()
        {
            try
            {
                var result = await _repository.GetAllCampsAsync();

                return Ok(result);
            }
            catch (Exception ex )
            {
                //Todo add logging.
                return InternalServerError(ex);
            }

            //adding a static value
            //return Ok(new { Name = "Shawn", Occupation = "Teacher" });
        }

    }
}