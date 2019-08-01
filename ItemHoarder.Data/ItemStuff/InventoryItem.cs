using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Data.ItemStuff
{
    public class InventoryItem
    {
        [Key]
        public int ItemID { get; set; }
        [Required]
        public Guid CreatorID { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public float Weight { get; set; }
        //pick random num between array nums [fragile value, resilient value]
        public string HitPoints { get; set; }
        public RarityOfItem ItemRarity { get; set; }
        public ItemClass ItemClass { get; set; }
        public string ClassType { get; set; }
        public double Damage { get; set; }
        public double DamageResiliance { get; set; }
        public bool IsEquiptable { get; set; }
        public double Strength { get; set; }
        public double Dexterity { get; set; }
        public double Constitution { get; set; }
        public double Intelligence { get; set; }
        public double Wisdom { get; set; }
        public double Charisma { get; set; }
        public DateTimeOffset DateOfCreation { get; set; }
        public DateTimeOffset? DateOfModification { get; set; }
    }
}
