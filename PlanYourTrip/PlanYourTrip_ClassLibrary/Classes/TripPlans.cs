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
        public string Nazwa { get; set; } = "Nowy plan";
        public string? Opis { get; set; }
        public string? PunktyJSON { get; set; }
        public DateTime? DataUtworzenia { get; set; }
        public DateTime? OstatniaAktualizacja { get; set; }
        public bool Publiczny { get; set; } = false;

        //Relacje
        public int AutorId { get; set; }
        [ForeignKey("AutorId")]
        public Users? Users { get; set; }
    }
}
