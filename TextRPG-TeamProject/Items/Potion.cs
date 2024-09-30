using System;

class Potion : Item, IConsumable
{
    public int RecoveryPower { get; set; }

    public override string StatInfo => $"회복력 {RecoveryPower}";

    public Potion(ItemId id, string name, string desc, int price, int recoveryPower) : base(id, name, desc, price)
    {
        RecoveryPower = recoveryPower;
    }

    public void Consume ()
    {
        switch (Id)
        {
        case ItemId.HPPotion:
            GameData.Player.HP += RecoveryPower;
            break;

        case ItemId.MPPotion:
            GameData.Player.MP += RecoveryPower;
            break;
        }
    }
}
