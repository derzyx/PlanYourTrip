using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanYourTrip_ClassLibrary.Classes
{
    public class Subscription
    {
        [Key]
        public int IdSubscription { get; set; }

        public int SubscriberId { get; set; }
        [ForeignKey("SubscriberId")]
        public Users? Subscriber { get; set; }

        public int ObservedId { get; set; }
        [ForeignKey("ObservedId")]
        public Users? Observed { get; set; }
    }
}
