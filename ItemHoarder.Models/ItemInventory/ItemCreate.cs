using ItemHoarder.Data.ItemStuff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Models
{
    public class ItemCreate
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public float Weight { get; set; }
        //sending in array as string [fragile value, resilient value]
        public int FragileHitPoint { get; set; }
        public int ResilientHitPoint { get; set; }
        public RarityOfItem ItemRarity { get; set; }
        public ItemClass ItemClass { get; set; }
        public string ClassType { get; set; }
        public int ArmorClass { get; set; }
        public double Damage { get; set; }
        public double DamageResiliance { get; set; }
        public bool IsEquiptable { get; set; }
        public double Strength { get; set; }
        public double Dexterity { get; set; }
        public double Constitution { get; set; }
        public double Intelligence { get; set; }
        public double Wisdom { get; set; }
        public double Charisma { get; set; }
    }
}
