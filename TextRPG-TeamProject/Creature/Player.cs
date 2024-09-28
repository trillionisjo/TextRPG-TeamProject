using System;


public enum PlayerType
{
    None,
    Knight = 1,
    Mage = 2,
    Archer = 3,
    Rogue = 4,
}

class Player : Creature
{
    public PlayerType Type { get; set; }
    public int Gold { get; set; }

    public int ExtraAttackPower { get; set; }
    public int ExtraDefensePower { get; set; }


    public Player(PlayerType type) : base(CreatureType.Player)
    {
        Level = 1;
        Type = type;
        ExtraAttackPower = 0;
        ExtraDefensePower = 0;
        Init();
    }

    public void Init()
    {
        int num = (int)Type;
        switch (num)
        {
            case 1:
                SetInfo(120, 5, 7);
                break;
            case 2:
                SetInfo(70, 10, 2);
                break;
             case 3:
                SetInfo(100, 6, 6);
                break;
            case 4:
                SetInfo(50, 10, 5);
                break;
        }
    }

    public override int GetTotalAttackPower()
    {
        return AttackPower + ExtraAttackPower;
    }

    public override int GetTotalDefensePower()
    {
        return DefensePower + ExtraDefensePower;
    }

    public int GetGold() { return Gold; }
    public void AddGold(int amount)
    {
        Gold += amount;
    }
    public bool SpendGold(int amount)
    {
        if (Gold <= 0)
        {
            return false;
        }

        else
        {
            Gold -= amount;
            return true;
        }
    }
    public void AddLevel()
    {

        AttackPower += 1;
        DefensePower += 1;
        Level++;

    }


}

