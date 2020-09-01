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
        public async Task<IHttpActionResult> Get(bool includeTalks = false)
        {
            try
            {
                var result = await _repository.GetAllCampsAsync(includeTalks);

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
        [Route("{Moniker}", Name ="GetCamp")]
        public async Task<IHttpActionResult> Get(string moniker, bool includeTalks = false) {

            try
            {
                var result = await _repository.GetCampAsync(moniker, includeTalks);
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(_mapper.Map<CampModel>(result));
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
        
        }

        [Route("searchByDate/{eventDate:datetime}")]
        [HttpGet]
        public async Task<IHttpActionResult> SearchByEventDate(DateTime eventDate, bool includeTalks = false) {

            try
            {
                var result = await _repository.GetAllCampsByEventDate(eventDate, includeTalks);
                return Ok(_mapper.Map<CampModel>(result));

            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
        
        }

        [Route()]
        public async Task<IHttpActionResult> Post(CampModel model)
        {
            try
            {
                if (await _repository.GetCampAsync(model.Moniker) != null)
                {
                    ModelState.AddModelError("Moniker", "moniker is in use");
                }

                if (ModelState.IsValid)
                {

                    var camp = _mapper.Map<Camp>(model);
                    _repository.AddCamp(camp);

                    if (await _repository.SaveChangesAsync())
                    {
                        var newModel = _mapper.Map<CampModel>(camp);
                        // add location and get camp
                        var location = Url.Link("GetCamp", new { moniker = newModel.Moniker });
                        return Created(location, newModel);

                        //a much simpler way to do the same (location and get camp)
                        //return CreatedAtRoute("GetCamp", new { moniker = newModel.Moniker }, newModel);
                        //return Ok(camp);
                    }
                } 
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }

            return BadRequest(ModelState);
        }


        [Route("{moniker}")]
        public async Task<IHttpActionResult> Put(string moniker, CampModel model) {


            try
            {
                var camp = await _repository.GetCampAsync(moniker);
                if (camp == null) 
                {
                    return NotFound();
                }
                _mapper.Map(model, camp);

                if (await _repository.SaveChangesAsync())
                {
                    return Ok(_mapper.Map<CampModel>(camp));
                }
                else
                {
                    return InternalServerError();
                }
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
        
        }
        
        [Route("{moniker}")]
        public async Task<IHttpActionResult> Delete(string moniker)
        {

            try
            {
                var camp = await _repository.GetCampAsync(moniker);
                if (camp == null)
                {
                    return NotFound();
                }

                _repository.DeleteCamp(camp);

                if (await _repository.SaveChangesAsync())
                {
                    return Ok();
                }
                else
                {
                    return InternalServerError();
                }

            }
            catch (Exception)
            {

                return InternalServerError();
            }

        }
    }
}