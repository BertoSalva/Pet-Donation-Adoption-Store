using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RescuePet.Models.DBEntities
{
    public class Breed
    {

        public int BreedID { get; set; }

        public int TypeID { get; set; }

        public string BreedName { get; set; }    
    }
}