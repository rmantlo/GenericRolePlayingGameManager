using ItemHoarder.Data;
using ItemHoarder.Models.Characters.Backgrounds;
using ItemHoarder.Models.Characters.Classes;
using ItemHoarder.Models.Characters.Races;
using ItemHoarder.Models.Characters.Skeleton;
using ItemHoarder.Models.Familars;
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
        public SkeletonOtherIndex Skeleton { get; set; }
        public RaceIndex Race { get; set; }
        public List<ClassIndex> Class { get; set; }
        //subclass
        public SubClassIndex SubClass { get; set; }
        //familars
        public List<FamilarMinimalDisplay> Familars { get; set; }
        //conditions
        public List<Dictionary<string, string>> Conditions { get; set; }
        public DateTimeOffset DateOfCreation { get; set; }
    }
}
