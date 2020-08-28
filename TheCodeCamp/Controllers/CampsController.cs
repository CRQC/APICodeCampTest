using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using TheCodeCamp.Data;
using TheCodeCamp.Models;

namespace TheCodeCamp.Controllers
{
    [RoutePrefix("api/camps")]
    public class CampsController : ApiController
    {
        private readonly ICampRepository _repository;
        private readonly IMapper _mapper;

        public CampsController(ICampRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [Route()]
        public async Task<IHttpActionResult> Get()
        {
            try
            {
                var result = await _repository.GetAllCampsAsync();
                if (result == null)
                {
                    return NotFound();
                }

                //mapping
                var mappedResult = _mapper.Map<IEnumerable<CampModel>>(result);

                return Ok(mappedResult);

                //return Ok(result);
            }
            catch (Exception ex )
            {
                //Todo add logging.
                return InternalServerError(ex);
            }

            //adding a static value
            //return Ok(new { Name = "Shawn", Occupation = "Teacher" });
        }

        //if no routing is done. [Route("api/camps/{Moniker}")]
        [Route("{Moniker}")]
        public async Task<IHttpActionResult> Get(string moniker) {

            try
            {

                var result = await _repository.GetCampAsync(moniker);
                return Ok(_mapper.Map<CampModel>(result));
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
        
        }
    }
}