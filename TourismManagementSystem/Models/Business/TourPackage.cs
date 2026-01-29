using System.Collections.Generic;
using TourismManagementSystem.Models.Business;

namespace TourismManagementSystem.Models.Business
{

    public class TourPackage
    {
        public int TourPackageId { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int DurationDays { get; set; }
        public int MaxGroupSize { get; set; }

        public int AgencyId { get; set; }
        public virtual Agency Agency { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }
    }
}
