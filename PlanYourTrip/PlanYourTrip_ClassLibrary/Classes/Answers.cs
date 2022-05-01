using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanYourTrip_ClassLibrary.Classes
{
    public class Answers
    {
        [Key]
        public int IdAnswer { get; set; }
        public string Tresc { get; set; }
        public DateTime? Data { get; set; }


        //Relacje
        public int PostId { get; set; }
        [ForeignKey("PostId")]
        public Posts Post { get; set; }

        public int? AutorId { get; set; }
        [ForeignKey("AutorId")]
        public Users User { get; set; }
    }
}
