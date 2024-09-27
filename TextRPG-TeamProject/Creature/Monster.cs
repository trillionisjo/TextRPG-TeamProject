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
    public MonsterType Type { get; set; }
    protected Monster(MonsterType type) : base(CreatureType.Monster)
    {
        Type = type;
        Level = (int)type;
        Name = type.ToString();
    }

}


class Goblin : Monster
{
    public Goblin() : base(MonsterType.Goblin)
    {
        SetInfo(30, 3, 5);
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


