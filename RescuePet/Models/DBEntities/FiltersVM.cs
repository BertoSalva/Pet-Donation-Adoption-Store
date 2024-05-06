using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Description;

namespace RescuePet.Models.DBEntities
{
    public class FiltersVM
    {
        public List<Breed> Breeds { get; set; }
        public List<Location> Locations { get; set; }
        public List<Type> Types { get; set; }

        public List<Petz> Petz { get; set; } 
        public List<Pet> Pets { get; set; }

        public List<Userz> Users { get; set; }

       
    }
}