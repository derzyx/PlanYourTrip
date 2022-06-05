using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanYourTrip_ClassLibrary.ClassesDTO
{
    public class PlansFilterDTO
    {
        public string? SortType { get; set; } = "desc";

        public string? DateMin { get; set; } = string.Empty;

        public string? DateMax { get; set; } = string.Empty;
    }
}
