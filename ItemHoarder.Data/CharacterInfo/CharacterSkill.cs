using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Data.CharacterInfo
{
    public class CharacterSkill
    {
        [Key]
        public int ID { get; set; }
        public Guid OwnerID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ClassesApplied { get; set; }
        public string RacesApplied { get; set; }
        public string BackgroundsApplied { get; set; }
        public string StatApplied { get; set; }
        public double Strength { get; set; }
        public double Dexterity { get; set; }
        public double Constitution { get; set; }
        public double Intelligence { get; set; }
        public double Wisdom { get; set; }
        public double Charisma { get; set; }
        [DefaultValue(false)]
        public bool IsDeactivated { get; set; }
        public DateTimeOffset DateOfCreation { get; set; }
        public DateTimeOffset? DateOfModification { get; set; }
    }
}
