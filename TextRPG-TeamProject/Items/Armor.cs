using System;

class Armor : Item, IEquipable
{
    public int ArmorDefense { get; set; }
    public override string StatInfo => $"방어력 {ArmorDefense}";

    public Armor (int id, string name, string desc, int price, int armorDefense) : base(id, name, desc, price)
    {
        ArmorDefense = armorDefense;
    }

    public void ApplyStats ()
    {
        
    }

    public void RemoveStats ()
    {
        
    }
}
