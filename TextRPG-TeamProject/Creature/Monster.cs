using System;


    public enum MonsterType
    {
        None,
        Goblin = 1,
        Kobold = 2,
        Orc = 3
    }
class Monster : Creature
    {
      public MonsterType type { get; protected set; }
      protected Monster(MonsterType type) : base(CreatureType.Monster)
        {
             Name = type.ToString();
        }

    }


     class Goblin : Monster
    {
        public Goblin() : base(MonsterType.Goblin)
        {
            SetInfo(20, 3, 3);
        }

    }

    class Kobold : Monster
    {
        public Kobold() : base(MonsterType.Kobold)
        {
            SetInfo(30, 3, 5);
        }

    }

    class Orc : Monster
    {
        public Orc() : base(MonsterType.Orc)
        {
            SetInfo(35, 5, 7);
        }

    }


