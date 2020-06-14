using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TourReservationAPI.Data;
using TourReservationAPI.Interface;
using TourReservationAPI.Repository;

namespace TourReservationAPI.Controllers
{
    public class RezervationController : ControllerBase
    {
        private readonly IRepository<Rezervation> guideRepository;
        private readonly GuideDbContext guideDbContext;
        public RezervationController(GuideDbContext context, IRepository<Rezervation> repo)
        {
            guideRepository = repo;
            guideDbContext = context;
        }

        [HttpGet("api/getGuide{id}")]
        public async Task<IActionResult> getRezervation(string Id)
        {
            try
            {
                var ID = Guid.TryParse(Id, out Guid guid);
                if (ID)
                {
                    var rezervation = await guideDbContext.Rezervations.FindAsync(guid);
                    if (rezervation != null)
                    {
                        return Ok(rezervation);

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
        [HttpGet("api/getRezervationsPerDay")]
        public List<Rezervation> getRezervationsPerDay(string date)
        {
            try
            {
                var Rezervations = new List<Rezervation>();
                CultureInfo provider = CultureInfo.InvariantCulture;
                string format = "dd/MM/yyyy";
                DateTime dateVal = DateTime.ParseExact(date, format, provider);
                if (dateVal != null)
                {
                    Rezervations = guideDbContext.Rezervations.Where(p => p.RezervationDate.Date == dateVal.Date).OrderByDescending(p => p.RezervationDate).ToList();
                    return Rezervations;
                }
                return Rezervations;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        [HttpDelete("api/cancelRezervation")] //For the use of testing a condition is set , if the Rezervation is past 7 days , the user cannot cancel it.
        public async Task<IActionResult> cancelRezervation(string ID)
        {
            try
            {
                var idVal = Guid.TryParse(ID, out Guid guid);
                if(idVal)
                {
                    var nowDate = DateTime.Now;
                    var rezervationToBeDeleted = guideDbContext.Rezervations.Where(p => p.RezervationID == guid).ToList();
                    foreach(var rez in rezervationToBeDeleted)
                    {
                        var days = nowDate - rez.RezervationDate;
                        if(days.TotalDays < 7)
                        {
                             guideRepository.Delete(rez);
                            var save = await guideRepository.SaveAsync(rez);
                        }
                        else
                        {
                            return BadRequest("Your Rezervation is past 7 days , you cannot cancel it .Thank you.");
                        }

                    }
                    return Ok();

                }
                else return NotFound();


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("api/makeRezervation")]
        public async Task<IActionResult> makeARezervation( MakeARezervation makeARezervation)
        {
            try
            {
                var guideId = makeARezervation.GuideId;
                Guide guide = guideDbContext.Guides.Where(p=>p.guideID == guideId).FirstOrDefault();
                if (makeARezervation.PersonAge > guide.AgeLimit)
                    return BadRequest("Your age is above limit");

                foreach (var user in makeARezervation.users)
                {
                    Rezervation rez = new Rezervation();

                    int bday = user.Birthday.Year;
                    int age = DateTime.Now.Year - bday;
                    if (age > guide.AgeLimit)
                    {
                        return BadRequest("Age is above limit for your friend " + user.Firstname + user.Lastname);
                    }
                    else
                    {

                        rez.Firstname = user.Firstname;
                        rez.Lastname = user.Lastname;
                        rez.RezervationDate = DateTime.Now;
                        rez.UpdatedDate = DateTime.Now;
                        rez.GuideId = guideId;
                        rez.PersonAge = age;
                        guideRepository.Add(rez);
                        var save = await guideRepository.SaveAsync(rez);
                    }

                }
                Rezervation rez1 = new Rezervation();
                rez1.GuideId = makeARezervation.GuideId;
                rez1.Firstname = makeARezervation.Firstname;
                rez1.Lastname = makeARezervation.Lastname;
                rez1.PersonAge = makeARezervation.PersonAge;
                rez1.RezervationDate = DateTime.Now;
                rez1.UpdatedDate = DateTime.Now;
                guideRepository.Add(rez1);
                var save1 = await guideRepository.SaveAsync(rez1);

                return StatusCode(201);
                

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //[HttpDelete]

    }
}