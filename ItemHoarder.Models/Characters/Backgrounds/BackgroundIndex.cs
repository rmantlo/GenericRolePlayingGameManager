using ItemHoarder.Data.RoomFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Models.Characters.Backgrounds
{
    public class BackgroundIndex
    {
        public int BackgroundID { get; set; }
        public GameType GameTag { get; set; }
        public string BackgroundName { get; set; }
        public string BackgroundDescription { get; set; }
        public DateTimeOffset DateOfCreation { get; set; }
        public DateTimeOffset? DateOfModification { get; set; }
    }
}
