using ItemHoarder.Data.RoomFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Models.Characters.Races
{
    public class RaceIndex
    {
        public int RaceID { get; set; }
        public GameType GameTag { get; set; }
        public string RaceName { get; set; }
        public string VisualDescription { get; set; }
        public DateTimeOffset DateOfCreation { get; set; }
        public DateTimeOffset? DateOfModification { get; set; }
    }
}
