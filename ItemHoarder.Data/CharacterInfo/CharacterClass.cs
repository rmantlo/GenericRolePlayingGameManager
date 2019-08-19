using ItemHoarder.Data.CharacterInfo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Data
{
    public class CharacterClass
    {
        //multi class
        [Key]
        public int ClassID { get; set; }
        [Required]
        public Guid OwnerID { get; set; }
        public string ApiClassID { get; set; }
        public string ClassName { get; set; }
        public string ClassDescription { get; set; }
        public string HitDie { get; set; }
        public string SavingThrows { get; set; }
        public int FortitudeSavingThrow { get; set; }
        public int ReflexSavingThrow { get; set; }
        public int WillSavingThrow { get; set; }//maybe make this front end??
        public string Proficiencies { get; set; } //weapons, armors
        public double Strength { get; set; }
        public double Dexterity { get; set; }
        public double Constitution { get; set; }
        public double Intelligence { get; set; }
        public double Wisdom { get; set; }
        public double Charisma { get; set; }
        [DefaultValue(false)]
        public bool IsDeactivated { get; set; }
        public virtual ICollection<CharClassSubConnection> SubClasses { get; set; }
        public DateTimeOffset DateOfCreation { get; set; }
        public DateTimeOffset? DateOfModification { get; set; }
    }
}
