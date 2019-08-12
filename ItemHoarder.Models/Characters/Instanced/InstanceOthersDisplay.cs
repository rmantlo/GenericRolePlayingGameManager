using ItemHoarder.Data;
using ItemHoarder.Models.Characters.Backgrounds;
using ItemHoarder.Models.Characters.Classes;
using ItemHoarder.Models.Characters.Races;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Models.Characters.Instanced
{
    public class InstanceOthersDisplay
    {
        public int CharInstanceID { get; set; }
        public Guid OwnerID { get; set; }
        public int? RoomID { get; set; }
        public string RoomName { get; set; }
        public string CharacterName { get; set; }
        public TypeOfGender Gender { get; set; }
        public string VisualDescription { get; set; }
        public RaceDisplay Race { get; set; }
        public ClassDisplay Class { get; set; }
        public BackgroundDisplay Background { get; set; }
        public DateTimeOffset DateOfCreation { get; set; }
    }
}
