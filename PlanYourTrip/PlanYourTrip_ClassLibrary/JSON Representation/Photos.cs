using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanYourTrip_ClassLibrary.JSON_Representation
{
    public class Photos
    {
        public Photo[] photo; 
    }

    public class Photo
    {
        public int id;
        public string photoURL;
    }
}
