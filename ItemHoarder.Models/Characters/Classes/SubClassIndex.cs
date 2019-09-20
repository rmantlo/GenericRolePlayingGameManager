using ItemHoarder.Data.RoomFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Models.Characters.Classes
{
    public class SubClassIndex
    {
        public int SubClassID { get; set; }
        public GameType GameType { get; set; }
        public string SubClassName { get; set; }
        public string SubClassDesc { get; set; }
        public DateTimeOffset DateOfCreation { get; set; }
        public DateTimeOffset? DateOfModification { get; set; }
    }
}
