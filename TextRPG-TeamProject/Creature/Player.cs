using System;

namespace TextRPG_TeamProject.Creature
{
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
        protected PlayerType type;
        protected int gold;
        protected int level;


        protected Player(PlayerType type) : base(CreatureType.Player)
        {

        }


        public int GetGold() { return gold; }
        public void AddGold(int amount)
        { 
            gold += amount;
        }

        public bool SpendGold(int amount)
        {
            if (gold <= 0)
            {
                return false;
            }

            else
            {
                gold -= amount;
                return true;
            }
        }
        public void AddLevel() { level++;}
      
        public int GetLevel() { return level; }







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
}
