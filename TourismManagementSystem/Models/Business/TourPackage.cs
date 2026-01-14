using System.ComponentModel.DataAnnotations;

namespace TourismManagementSystem.Models.Business
{
    public class TourPackage
    {
        public int TourPackageId { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int DurationDays { get; set; }

        [Required]
        public int MaxGroupSize { get; set; }

        public int AgencyId { get; set; }
        public Agency Agency { get; set; }
    }
}
