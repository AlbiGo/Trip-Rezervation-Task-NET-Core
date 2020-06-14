using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TourReservationAPI.Data
{
    public class Guide
    {
        [Key]
        [Required]
        public Guid guideID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime Start { get; set; }
        [Required]
        public DateTime End { get; set; }
        [Required]
        public decimal Pice { get; set; }
        [Required]
        public int AgeLimit { get; set; }

        public bool validateAgeLimit(int age)
        {
            var ageVal = false;
            if (age < 100 && age > 5)
            {
                 ageVal = true;
            }
            return ageVal;
        }


    }
}
