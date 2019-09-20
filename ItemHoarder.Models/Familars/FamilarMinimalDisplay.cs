using ItemHoarder.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Models.Familars
{
    public class FamilarMinimalDisplay
    {
        public int FamilarID { get; set; }
        public string PetName { get; set; }
        public Photo PetPhoto { get; set; }
        public string FamilarName { get; set; }
        public string FamilarType { get; set; }
        public string VisualDescription { get; set; }
    }
}
