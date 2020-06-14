using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourReservationAPI.Data;
using TourReservationAPI.Repository;
using TourReservationAPI.Interface;
using Microsoft.AspNetCore.Mvc;

namespace TourReservationAPI.Services
{
    public class GuideService
    {
        private readonly IGuideRepository<Guide> guideRepository ;
        private readonly GuideDbContext guideDbContext;


        public GuideService(GuideDbContext context, IGuideRepository<Guide> repo)
        {
            guideRepository = repo;
            guideDbContext = context;

        }
        public async Task<Guide> AddGuid( Guide guide)
        {
            try
            {
                if (guide.validateAgeLimit(guide.AgeLimit))
                {
                    guideRepository.Add(guide);
                    var save = await guideRepository.SaveAsync(guide);
                }
                return (guide);



            }
            catch (Exception)
            {
                throw;
            }
           

            
        }

        public IEnumerable<Guide> getAllGuides()
        {
            var guides = guideDbContext.Guides.OrderByDescending(p => p);
            return guides;
        }
    }
}
