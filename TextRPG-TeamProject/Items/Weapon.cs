using System;

class Weapon : Item, IEquipable
{
    public int WeaponDamage { get; set; }
    public override string StatInfo => $"공격력 {WeaponDamage}";

    public Weapon (int id, string name, string desc, int price, int weaponDamage) : base(id, name, desc, price)
    {
        WeaponDamage = weaponDamage;
    }

    public void ApplyStats ()
    {
        
    }

    public void RemoveStats ()
    {

    }
}
