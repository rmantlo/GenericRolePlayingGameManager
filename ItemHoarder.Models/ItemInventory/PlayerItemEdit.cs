using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Models.ItemInventory
{
    public class PlayerItemEdit
    {
        //only can change whether or not it is equipted and current hit points
        public int ItemInstanceID { get; set; }
        public bool IsEquipted { get; set; }
        public int CurrentHitPoints { get; set; }
    }
}
