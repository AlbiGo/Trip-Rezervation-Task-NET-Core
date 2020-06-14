using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourReservationAPI.Data;

namespace TourReservationAPI.Data
{
    public class GuideDbContext : DbContext
    {

        public GuideDbContext(DbContextOptions<GuideDbContext> options)
            : base(options)
        {
           
        }
       public DbSet<Guide> Guides { get; set; }
        public DbSet<Rezervation> Rezervations { get; set; }


    }
}
