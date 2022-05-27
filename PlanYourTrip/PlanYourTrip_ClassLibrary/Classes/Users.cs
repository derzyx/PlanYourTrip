using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlanYourTrip_ClassLibrary.Classes
{
    public class Users
    {
        [Key]
        [Column(Order = 0)]
        public int Id { get; set; }
        [Required]
        [Column(Order = 1)]
        public string Nick { get; set; } = string.Empty;
        [Column(Order = 2)]
        public string Imie { get; set; } = string.Empty;
        [Column(Order = 3)]
        public string Nazwisko { get; set; } = string.Empty;
        [Required]
        [Column(Order = 4)]
        public string Email { get; set; }
        [Required]
        [Column(Order = 5)]
        public string Haslo { get; set; }
        [Column(Order = 6)]
        public string Opis { get; set; }
        [Column(Order = 7)]
        public int IdAvatar { get; set; } = 1;

        //Relacje
        public List<TripPlans>? TripPlans { get; set; }
    }
}