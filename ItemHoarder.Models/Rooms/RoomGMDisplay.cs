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
        public string PlayerOneNotes { get; set; }
        public string PlayerTwoNotes { get; set; }
        public string PlayerThreeNotes { get; set; }
        public string PlayerFourNotes { get; set; }
        public string PlayerFiveNotes { get; set; }
        public string PlayerSixNotes { get; set; }
        public string PlayerSevenNotes { get; set; }
        public string GeneralNotes { get; set; }
        public List<string> PlayerUsernames { get; set; }
        public DateTimeOffset DateOfCreation { get; set; }
    }
}
