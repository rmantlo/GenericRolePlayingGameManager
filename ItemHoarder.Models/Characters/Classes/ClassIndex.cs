using ItemHoarder.Data.RoomFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Models.Characters.Classes
{
    public class ClassIndex
    {
        public int ClassID { get; set; }
        public GameType GameType { get; set; }
        public string ClassName { get; set; }
        public string ClassDescription { get; set; }
        public DateTimeOffset DateOfCreation { get; set; }
        public DateTimeOffset? DateOfModification { get; set; }
    }
}
