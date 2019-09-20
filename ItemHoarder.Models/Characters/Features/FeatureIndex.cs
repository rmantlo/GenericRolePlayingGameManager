using ItemHoarder.Data.RoomFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Models.Characters.Features
{
    public class FeatureIndex
    {
        public int FeatureID { get; set; }
        public GameType GameTag { get; set; }
        public string FeatureName { get; set; }
        public string Description { get; set; }
        public DateTimeOffset DateOfCreation { get; set; }
        public DateTimeOffset? DateOfModification { get; set; }
    }
}
