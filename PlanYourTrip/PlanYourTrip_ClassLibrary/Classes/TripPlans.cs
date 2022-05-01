using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanYourTrip_ClassLibrary.Classes
{
    public class TripPlans
    {
        [Key]
        public int TripPlanId { get; set; }
        public string Nazwa { get; set; }
        public string Opis { get; set; }
        public string PunktyJSON { get; set; }

        //Relacje
        public int AutorId { get; set; }
        [ForeignKey("AutorId")]
        public Users Users { get; set; }

        public ICollection<Contributors> Contributors { get; set; }
    }
}
