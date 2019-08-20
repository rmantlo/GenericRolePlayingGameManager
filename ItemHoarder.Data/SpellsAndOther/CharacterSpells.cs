using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Data.SpellsAndOther
{
    public class CharacterSpells
    {
        [Key]
        public int ID { get; set; }
        public int CharacterID { get; set; }
        [ForeignKey("Spell")]
        public int SpellID { get; set; }
        public virtual Spells Spell { get; set; }
        public DateTimeOffset DateOfCreation { get; set; }
    }
}
