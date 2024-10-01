using System;

class Weapon : IEquipable
{
    public ItemId Id { get; set; }
    public string Name { get; set; }
    public string Desc { get; set; }
    public int Price { get; set; }
    public int WeaponDamage { get; set; }
    public string StatInfo => $"공격력 {WeaponDamage:+#;-#;0}";
    public Slot Slot => Slot.Hand;

    public Weapon (ItemId id, string name, string desc, int price, int weaponDamage)
    {
        Id = id;
        Name = name;
        Desc = desc;
        Price = price;
        WeaponDamage = weaponDamage;
    }

    public void ApplyStats ()
    {
        GameData.Player.ExtraAttackPower += WeaponDamage;
    }

    public void RemoveStats ()
    {
        GameData.Player.ExtraAttackPower -= WeaponDamage;
    }
}
