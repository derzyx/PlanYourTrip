using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanYourTrip_ClassLibrary.Classes
{
    public class Posts
    {
        [Key]
        public int IdPost { get; set; }
        public string Tytul { get; set; }
        public string Tresc { get; set; }
        public int Polubienia { get; set; }
        public DateTime? Data { get; set; }
        public string ZdjeciaJSON { get; set; }


        //Relacje
        public List<Answers> Odpowiedzi { get; set; }

        public int AutorId { get; set; }
        [ForeignKey("AutorId")]
        public Users User { get; set; }
    }
}
