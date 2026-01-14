using System.ComponentModel.DataAnnotations;

namespace TourismManagementSystem.Models.Business
{
    public class TourImage
    {
        public int TourImageId { get; set; }

        [Required]
        public string ImagePath { get; set; }

        public int TourPackageId { get; set; }
        public virtual TourPackage TourPackage { get; set; }
    }
}
