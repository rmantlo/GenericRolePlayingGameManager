using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Models.Rooms
{
    public class RoomPlayerDisplay
    {
        public int RoomID { get; set; }
        public string RoomCreatorUsername { get; set; }
        public string RoomName { get; set; }
        public string GameType { get; set; }
        public List<string> PlayerUsernames { get; set; }
        public DateTimeOffset DateOfCreation { get; set; }
    }
}
