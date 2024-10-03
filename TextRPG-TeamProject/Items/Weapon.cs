using Newtonsoft.Json;
using System;

class Weapon : IEquipable
{
    [JsonProperty] public Guid guid { get; set; }
    [JsonProperty] public ItemId Id { get; set; }
    [JsonProperty] public string Name { get; set; }
    [JsonProperty] public string Desc { get; set; }
    [JsonProperty] public int Price { get; set; }
    [JsonProperty] public int WeaponDamage { get; set; }
    public string StatInfo => $"공격력 {WeaponDamage:+#;-#;0}";
    public Slot Slot => Slot.Hand;


    public Weapon (ItemId id, string name, string desc, int price, int weaponDamage)
    {
        guid = Guid.NewGuid();
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
