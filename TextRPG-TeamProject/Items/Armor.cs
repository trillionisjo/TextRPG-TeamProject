using System;

class Armor : IEquipable
{
    public ItemId Id { get; set; }
    public string Name { get; set; }
    public string Desc { get; set; }
    public int Price { get; set; }
    public int ArmorDefense { get; set; }
    public string StatInfo => $"방어력 {ArmorDefense:+#;-#;0}";

    public Slot Slot => Slot.Body;

    public Armor (ItemId id, string name, string desc, int price, int armorDefense)
    {
        Id = id;
        Name = name;
        Desc = desc;
        Price = price;
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
