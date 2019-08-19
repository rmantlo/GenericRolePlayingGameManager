using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Data.SpellsAndOther
{
    public class SubClassSpells
    {
        [Key]
        public int SubClassSpellsID { get; set; }
        public int SubClassID { get; set; }
        [ForeignKey("Spell")]
        public int SpellID { get; set; }
        public virtual Spells Spell { get; set; }
        public DateTimeOffset DateOfCreation { get; set; }
    }
}
