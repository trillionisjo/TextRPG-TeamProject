using Newtonsoft.Json;
using System;

class Armor : IEquipable
{
    [JsonProperty] public Guid guid { get; set; }
    [JsonProperty] public ItemId Id { get; set; }
    [JsonProperty] public string Name { get; set; }
    [JsonProperty] public string Desc { get; set; }
    [JsonProperty] public int Price { get; set; }
    [JsonProperty] public int ArmorDefense { get; set; }
    public string StatInfo => $"방어력 {ArmorDefense:+#;-#;0}";

    public Slot Slot => Slot.Body;


    public Armor (ItemId id, string name, string desc, int price, int armorDefense)
    {
        guid = Guid.NewGuid();
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
