using ItemHoarder.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Models.Rooms
{
    public class RoomIndex
    {
        public int RoomID { get; set; }
        public string RoomName { get; set; }
        public string RoomCreatorUsername { get; set; }
        [ForeignKey("Photo")]
        public int PhotoID { get; set; }
        public Photo RoomPhoto { get; set; }
        public string GameType { get; set; }
        public List<string> PlayerUsernames { get; set; }
        public Photo CharInstancePhoto { get; set; }
        public string CharInstanceName { get; set; }
        public int? CharInstanceLevel { get; set; }
        public DateTimeOffset DateOfCreation { get; set; }
        public DateTimeOffset? DateOfModification { get; set; }
    }
}
