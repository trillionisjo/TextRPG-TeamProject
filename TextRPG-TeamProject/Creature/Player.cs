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
      public PlayerType PlayerType { get; set; }
      public int Gold { get; set; }


    protected Player(PlayerType type) : base(CreatureType.Player)
        {
             Level = 1;
            PlayerType = type;
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

class Knight : Player
{
    public Knight() : base(PlayerType.Knight)
    {
        SetInfo(120, 5, 10);
    }
}

class Mage : Player
{
    public Mage(PlayerType type) : base(PlayerType.Mage)
    {
        SetInfo(120, 5, 10);
    }
}

class Archer : Player
{
    public Archer(PlayerType type) : base(PlayerType.Archer)
    {
        SetInfo(120, 5, 10);
    }
}

class Rogue : Player
{
    public Rogue(PlayerType type) : base(PlayerType.Rogue)
    {
        SetInfo(120, 5, 10);
    }
}

