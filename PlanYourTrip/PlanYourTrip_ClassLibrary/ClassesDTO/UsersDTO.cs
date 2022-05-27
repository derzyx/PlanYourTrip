using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanYourTrip_ClassLibrary.ClassesDTO
{
    public class UsersDTO
    {
        public int? Id { get; set; }
        public string? Nick { get; set; } = string.Empty;
        public string? Imie { get; set; } = string.Empty;
        public string? Nazwisko { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? Haslo { get; set; }
        public string? Opis { get; set; }
        public int? IdAvatar { get; set; } = 1;
    }
}
