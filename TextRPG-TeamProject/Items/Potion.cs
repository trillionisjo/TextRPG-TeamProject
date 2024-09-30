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
        case ItemId.HpPotion:
            GameData.Player.HP += RecoveryPower;
            break;

        case ItemId.MpPotion:
            GameData.Player.MP += RecoveryPower;
            break;
        }

        Inventory.RemoveItem(this);
    }
}
