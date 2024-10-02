using System;

class Potion : IConsumable
{
    public ItemId Id { get; set; }
    public string Name { get; set; }
    public string Desc { get; set; }
    public int Price { get; set; }
    public int RecoveryPower { get; set; }
    public string StatInfo => $"회복력 {RecoveryPower:+#;-#;0}";

    public Potion(ItemId id, string name, string desc, int price, int recoveryPower)
    {
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
