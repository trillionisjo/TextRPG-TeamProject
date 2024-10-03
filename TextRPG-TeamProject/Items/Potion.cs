using Newtonsoft.Json;

class Potion : IConsumable
{
    [JsonProperty] public Guid guid { get; set; }
    [JsonProperty] public ItemId Id { get; set; }
    [JsonProperty] public string Name { get; set; }
    [JsonProperty] public string Desc { get; set; }
    [JsonProperty] public int Price { get; set; }
    [JsonProperty] public int RecoveryPower { get; set; }
    public string StatInfo => $"회복력 {RecoveryPower:+#;-#;0}";

    public Potion(ItemId id, string name, string desc, int price, int recoveryPower)
    {
        guid = Guid.NewGuid();
        Id = id;
        Name = name;
        Desc = desc;
        Price = price;
        RecoveryPower = recoveryPower;
    }

    public void Consume ()
    {
        switch (Id)
        {
        case ItemId.HpPotion:
            GameData.Player.AddHp(RecoveryPower);
            break;

        case ItemId.MpPotion:
            GameData.Player.AddMp(RecoveryPower);
            break;
        }
    }
}
