using ItemHoarder.Data;
using ItemHoarder.Models.Characters.Backgrounds;
using ItemHoarder.Models.Characters.Classes;
using ItemHoarder.Models.Characters.Features;
using ItemHoarder.Models.Characters.ProficiencySkills;
using ItemHoarder.Models.Characters.Races;
using ItemHoarder.Models.Characters.Skeleton;
using ItemHoarder.Models.Familars;
using ItemHoarder.Models.ItemInventory;
using ItemHoarder.Models.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Models.Characters.Instanced
{
    public class InstanceGMDisplay
    {
        public int CharInstanceID { get; set; }
        public Guid OwnerID { get; set; }
        public int? RoomID { get; set; }
        public string RoomName { get; set; }
        public List<Photo> CharacterPhoto { get; set; }
        public InstanceCharDetails CharSkeleton { get; set; }
        public RaceDetails Race { get; set; }
        public BackgroundDetails Background { get; set; }
        public List<ClassDetails> Class { get; set; }
        public SubClassDetails SubClass { get; set; }
        public List<FeatureDetails> Features { get; set; }
        public List<SkillDetails> Skills { get; set; }
        public List<PlayerItemDetails> InventoryItems { get; set; }
        public bool IsOverWeight { get; set; }
        //familars
        public List<FamilarDisplay> Familars { get; set; }
        public List<string> Conditions { get; set; }
        public List<SpellCantripDisplay> Spells { get; set; }
        //cantrips
        public List<SpellCantripDisplay> Cantrips { get; set; }
        public DateTimeOffset DateOfCreation { get; set; }
        public DateTimeOffset? DateOfModification { get; set; }
    }
}
