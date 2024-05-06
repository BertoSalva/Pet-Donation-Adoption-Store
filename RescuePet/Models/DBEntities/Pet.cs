using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RescuePet.Models.DBEntities
{
    public class Pet
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int petId { get; set; } 
        public string adoptionStatus { get; set; }

        public string User { get; set; }

        public string Name { get; set; }

        public string Story { get; set; }

        public string PostedBy { get; set; }

        public string Location { get; set; }

        public string Breed { get; set; }


        public Decimal Weight { get; set; }

        public int Age { get; set; }

        public string Img { get; set; }

        public string Gender { get; set; }

    }
}