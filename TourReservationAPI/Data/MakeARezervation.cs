using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TourReservationAPI.Data
{
    public class MakeARezervation
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public int PersonAge { get; set; }
        public Guid GuideId { get; set; }

        public List<User> users { get; set; }
    }
}
