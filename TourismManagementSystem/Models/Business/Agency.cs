using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TourismManagementSystem.Models.Business
{
    public class Agency
    {
        public int AgencyId { get; set; }

        [Required, StringLength(100)]
        public string AgencyName { get; set; }

        [Required, StringLength(500)]
        public string Description { get; set; }

        [Required]
        public string ContactNumber { get; set; }

        public string ProfileImage { get; set; }

        [Required]
        public string UserId { get; set; }   // Identity User

        public virtual ICollection<TourPackage> TourPackages { get; set; }
        public bool IsApproved { get; set; }

    }
}