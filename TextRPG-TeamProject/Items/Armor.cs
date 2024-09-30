using System;

class Armor : Item, IEquipable
{
    public int ArmorDefense { get; set; }
    public override string StatInfo => $"방어력 {ArmorDefense:+#;-#;0}";

    public Armor (ItemId id, string name, string desc, int price, int armorDefense) : base(id, name, desc, price)
    {
        ArmorDefense = armorDefense;
    }

    public void ApplyStats ()
    {
        GameData.Player.ExtraDefensePower += ArmorDefense;
    }

    public void RemoveStats ()
    {
        GameData.Player.ExtraDefensePower -= ArmorDefense;
    }
}
