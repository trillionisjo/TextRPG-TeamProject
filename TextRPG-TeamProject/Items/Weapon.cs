using System;

class Weapon : Item, IEquipable
{
    public int WeaponDamage { get; set; }
    public override string StatInfo => $"공격력 {WeaponDamage:+#;-#;0}";

    public Weapon (ItemId id, string name, string desc, int price, int weaponDamage) : base(id, name, desc, price)
    {
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
