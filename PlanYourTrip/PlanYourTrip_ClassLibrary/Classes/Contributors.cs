using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanYourTrip_ClassLibrary.Classes
{
    public class Contributors
    {
        [Key]
        public int ContributorId { get; set; }
        
        //Relacje
        public int UserI_FK { get; set; }
        [ForeignKey("UserId")]
        public Users User { get; set; }

        public ICollection<TripPlans> TripPlans { get; set; }
    }
}
