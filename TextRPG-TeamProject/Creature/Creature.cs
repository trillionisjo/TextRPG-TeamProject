using System;


public enum CreatureType
{
    None,
    Player = 1,
    Monster = 2

}

class Creature
{
    public CreatureType Type { get; protected set; }
    public int HP { get; protected set; }
    public string Name { get; set; }
    public bool IsDead => HP <= 0;
    public int Level { get; protected set; }
    public int AttackPower { get; protected set; }
    public int DefensePower { get; protected set; }

    protected int attackPower;
    protected int defensePower;

    protected Creature(CreatureType type)
    {
        Type = type;
    }

    public void SetInfo(int hp, int attackPower, int defensePower)
    {
        HP = hp;
        AttackPower = attackPower;
        DefensePower = defensePower;
    }

    public void OnDamaged(int damage)
    {
        HP -= damage;

        HP -= damage;

        if (HP < 0)
            HP = 0;
    }





}






