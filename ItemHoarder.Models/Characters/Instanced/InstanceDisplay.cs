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
    public class InstanceDisplay
    {
        public int CharInstanceID { get; set; }
        public Guid OwnerID { get; set; }
        public int? RoomID { get; set; }
        public string RoomName { get; set; }
        public InstanceCharDetails CharacterSkeleton { get; set; }
        public RaceDetails Race { get; set; }
        public List<ClassDetails> Class { get; set; }
        public SubClassDetails SubClass { get; set; }
        public BackgroundDetails Background { get; set; }
        public List<FeatureDetails> Features { get; set; }
        public List<SkillDetails> Skills { get; set; }
        public List<ItemIndex> InventoryItems { get; set; }
        public bool IsOverWeight { get; set; }
        //familars
        public List<FamilarMinimalDisplay> Familars { get; set; }
        public List<SpellCantripDisplay> Spells { get; set; }
        //cantrips
        public List<SpellCantripDisplay> Cantrips { get; set; }
        public List<Dictionary<string, string>> Conditions { get; set; }
        public string CharacterNotes { get; set; }
        public string SkeletonNotes { get; set; }
        public DateTimeOffset DateOfCreation { get; set; }
        public DateTimeOffset? DateOfModification { get; set; }
    }
}
