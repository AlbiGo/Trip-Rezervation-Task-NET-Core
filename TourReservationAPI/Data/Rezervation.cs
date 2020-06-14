using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TourReservationAPI.Data
{
    public class Rezervation
    {
        [Key]
        [Required]
        public Guid RezervationID { get; set; }
        [Required]
        public DateTime RezervationDate { get; set; }
        [Required]
        public DateTime UpdatedDate { get; set; }
        [Required]
        public string Firstname { get; set; }
        [Required] 
        public string Lastname { get; set; }
        [Required]
        public int PersonAge { get; set; }
        [Required] 
        public Guid GuideId { get; set; }

        public virtual ICollection<Guide> Guides { get; set; }


    }


}
