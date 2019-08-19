using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Data.ItemStuff
{
    public enum RarityOfItem
    {
        Common,
        Uncommon,
        Rare,
        VeryRare,
        Legendary
    }
    public enum ItemClass
    {
        Armor,
        Shield,
        Weapon,
        Potion,
        WonderousItem,
        Accessories,
        Equipment,
        Etc
    }
    public enum ArmorType
    {
        Light,
        Padded,
        Leather,
        StuddedLeather,
        ChainShirt,
        Medium,
        Hide,
        ScaleMail,
        ChainMail,
        Breastplate,
        Heavy,
        SplintMail,
        BandedMail,
        HalfPlate,
        FullPlate,
    }
    public class Item
    {
        [Key]
        public int ItemID { get; set; }
        [Required]
        public Guid OwnerID { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Photo> ItemPhoto { get; set; }
        public float Weight { get; set; }
        //sending in array as string [fragile value, resilient value]
        public string HitPoints { get; set; }
        public RarityOfItem ItemRarity { get; set; }
        public ItemClass ItemClass { get; set; }
        public ArmorType ArmorType { get; set; } //lgiht medium heavey, etc?
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
        public DateTimeOffset DateOfCreation { get; set; }
        public DateTimeOffset? DateOfModification { get; set; }
    }
}
