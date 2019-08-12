using ItemHoarder.Models.Characters.Backgrounds;
using ItemHoarder.Models.Characters.Classes;
using ItemHoarder.Models.Characters.Features;
using ItemHoarder.Models.Characters.Instanced;
using ItemHoarder.Models.Characters.ProficiencySkills;
using ItemHoarder.Models.Characters.Races;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Models.Rooms
{
    public class RoomGMDisplay
    {
        public int RoomID { get; set; }
        public string RoomName { get; set; }
        public string GameType { get; set; }
        public List<string> PlayerUsernames { get; set; }
        public RoomNoteDisplay RoomNotes { get; set; }
        public List<ClassDisplay> RoomClasses { get; set; }
        public List<RaceDisplay> RoomRaces { get; set; }
        public List<BackgroundDisplay> RoomBackgrounds { get; set; }
        public List<FeatureDisplay> RoomFeatures { get; set; }
        public List<SkillDisplay> RoomSkills { get; set; }
        public List<InstanceGMDisplay> Characters { get; set; }
        public DateTimeOffset DateOfCreation { get; set; }
        public DateTimeOffset? DateOfModification { get; set; }
    }
}
