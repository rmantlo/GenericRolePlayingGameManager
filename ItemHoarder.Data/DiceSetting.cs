using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Data
{
    public class DiceSetting
    {
        [Key]
        public int DiceID { get; set; }
        public Guid OwnerID { get; set; }
        public int CharInstanceID { get; set; }
        public int TypeOfDieOne { get; set; }
        public int NumberOfDieOne { get; set; }
        public int TypeOfDieTwo { get; set; }
        public int NumberOfDieTwo { get; set; }
        public DateTimeOffset DateOfCreation { get; set; }
        public DateTimeOffset? DateOfModification { get; set; }
    }
}
