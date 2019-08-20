using ItemHoarder.Data.SpellsAndOther;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Data.CharacterInfo
{
    public class CharacterSubClass
    {
        [Key]
        public int SubClassID { get; set; }
        public Guid OwnerID { get; set; }
        public string SubClassName { get; set; }
        public string Description { get; set; }
        public virtual ICollection<CharClassSubConnection> Classes { get; set; }
        public virtual ICollection<CharacterFeatList> Features { get; set; }
        //string separated by |, player chooses one?
        public string PerksList { get; set; }
        public string ListOfSpellIDs { get; set; } // maybe list of spellIDs?
        public DateTimeOffset DateOfCreation { get; set; }
        public DateTimeOffset? DateOfModification { get; set; }
    }
}
