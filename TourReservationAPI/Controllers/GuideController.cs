using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourReservationAPI.Data;
using TourReservationAPI.Interface;


namespace TourReservationAPI.Controllers
{
    //[ApiController]
    //[Route("api/[controller]")]
    public class GuideController : ControllerBase
    {
        private readonly IRepository<Guide> guideRepository;
        private readonly GuideDbContext guideDbContext;
        public GuideController(GuideDbContext context, IRepository<Guide> repo)
        {
            guideRepository = repo;
            guideDbContext = context;
        }
    
        [HttpGet("api/getGuide")]
        public async Task<IActionResult> getGuide(string id)
        {

            try
            {
                var ID = Guid.TryParse(id, out Guid guid);
                if (ID)
                {
                    var Guide = await guideDbContext.Guides.FindAsync(guid);
                    if (Guide != null)
                    {
                        return Ok(Guide);

                    }
                    else return NotFound();

                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                throw ex;
            }
          
        }

        [HttpPost("api/AddGuide")]
        public async Task<IActionResult> AddGuide( Guide guide)
        {
            try
            {
                if (guide.validateAgeLimit(guide.AgeLimit))
                {
                    guideRepository.Add(guide);
                    var save = await guideRepository.SaveAsync(guide);
                    return CreatedAtAction("GetBlogPost", new { id = guide.guideID }, guide);
                }
                return BadRequest("Age is above limit");



            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("api/guides")]
        public IEnumerable<Guide> getAllGuides()
        {
            //return guideDbContext.Guides;
            return guideDbContext.Guides;
        }
    }
}
