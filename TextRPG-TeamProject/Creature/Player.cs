using System;
using System.Numerics;


public enum PlayerType
{
    Knight = 1,
    Mage = 2,
    Archer = 3,
    Rogue = 4,
}

public class Player : Creature
{
    public event OnDeath<Player> OnDeath;
    public event Action<int> OnAddGold;
    public event Action<int> OnSpendGold;

    public PlayerType Type { get; set; }
    public int Gold { get; set; }
    public int ExpToNextLv { get; set; }
    public int Exp { get; set; }
    public int ExtraAttackPower { get; set; }
    public int ExtraDefensePower { get; set; }
    
    public Player() : base(CreatureType.Player)
    {
        Level = 1;
        Exp = 0;
        ExpToNextLv = 1;
        ExtraAttackPower = 0;
        ExtraDefensePower = 0;
    }



    public void SetInfo(int hp, int mp, int attackPower, int defensePower)
    {
        base.SetInfo(hp, attackPower, defensePower);
        MaxMP = mp;
        MP = mp;
    }

    public void Init(int number)
    {
        switch (number)
        {
            case 1:
                Type = PlayerType.Knight;
                SetInfo(5, 20, 5, 7);
                break;
            case 2:
                Type = PlayerType.Mage;
                SetInfo(70, 100 , 10, 2);
                break;
             case 3:
                Type = PlayerType.Archer;
                SetInfo(100, 50 , 6, 6);
                break;
            case 4:
                Type = PlayerType.Rogue;
                SetInfo(50, 70 , 12, 5);
                break;
        }
    }




public void AddExp(int extraExp)
    {
        Exp += extraExp;
        bool canLevelUp = Exp >= ExpToNextLv;

        while (canLevelUp)
        {
            if (Exp >= ExpToNextLv)
            {
                AddLevel();
                Exp = Exp - ExpToNextLv;
                ExpToNextLv *= 2;
            }

            else
            {
                canLevelUp = false;
            }
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
        OnAddGold?.Invoke(Gold);
    }
    public bool SpendGold(int amount)
    {
        if (Gold <= 0)
        {
            return false;
        }

        else
        {
            OnSpendGold?.Invoke(Gold);
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
  
    public void AddMp(int amount)
    {
        MP += amount;
        if (MP > MaxMP)
            MP = MaxMP;
    }
    public void AddHp(int amount)
    {
        HP += amount;
        if (HP > MaxHP)
            HP = MaxHP;
    }
    

}
