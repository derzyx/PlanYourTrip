using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanYourTrip_ClassLibrary.ClassesDTO
{
    public class UserPublicDataDTO
    {
        public int? Id { get; set; }
        public string? Nick { get; set; }
        public string? Imie { get; set; }
        public string? Nazwisko { get; set; }
        public string? Opis { get; set; }
        public int? IdAvatar { get; set; } = 1;
    }
}
