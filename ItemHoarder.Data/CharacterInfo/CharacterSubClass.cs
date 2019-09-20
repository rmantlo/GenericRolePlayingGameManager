using ItemHoarder.Data.RoomFolder;
using ItemHoarder.Data.SpellsAndOther;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        public GameType GameType { get; set; }
        public string SubClassName { get; set; }
        public string Description { get; set; }
        public string WeaponProficiencies { get; set; }
        public string ArmorProficiencies { get; set; }
        public string ToolProficiencies { get; set; }
        public virtual ICollection<CharClassSubConnection> Classes { get; set; }
        public virtual ICollection<CharacterFeatList> Features { get; set; }
        public string AppliedSpellIDs { get; set; } //list of spells this subclass has automatically
        public string ListOfSpellIDs { get; set; } // list of spellIDs, this sub can use
        [DefaultValue(false)]
        public bool IsDeactivated { get; set; }
        public DateTimeOffset DateOfCreation { get; set; }
        public DateTimeOffset? DateOfModification { get; set; }
    }
}
