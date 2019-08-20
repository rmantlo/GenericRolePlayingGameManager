using ItemHoarder.Data;
using ItemHoarder.Models.BattleInstances;
using ItemHoarder.Models.Characters.Backgrounds;
using ItemHoarder.Models.Characters.Classes;
using ItemHoarder.Models.Characters.Features;
using ItemHoarder.Models.Characters.Instanced;
using ItemHoarder.Models.Characters.ProficiencySkills;
using ItemHoarder.Models.Characters.Races;
using ItemHoarder.Models.Chests;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Models.Rooms
{
    public class RoomGMDisplay
    {
        public int RoomID { get; set; }
        public string RoomCreatorUsername { get; set; }
        public string RoomName { get; set; }
        public string GameType { get; set; }
        [ForeignKey("Photo")]
        public int PhotoID { get; set; }
        public Photo RoomPhoto { get; set; }
        public List<string> PlayerUsernames { get; set; }
        public RoomNoteDisplay RoomNotes { get; set; }
        public List<ClassDisplay> RoomClasses { get; set; }
        public List<RaceDisplay> RoomRaces { get; set; }
        public List<BackgroundDisplay> RoomBackgrounds { get; set; }
        public List<FeatureDisplay> RoomFeatures { get; set; }
        public List<SkillDisplay> RoomSkills { get; set; }
        public List<InstanceGMDisplay> Characters { get; set; }
        public List<BattleGMDisplay> BattleInstances { get; set; }
        public List<ChestDetails> Chests { get; set; }
        public DateTimeOffset DateOfCreation { get; set; }
        public DateTimeOffset? DateOfModification { get; set; }
    }
}
