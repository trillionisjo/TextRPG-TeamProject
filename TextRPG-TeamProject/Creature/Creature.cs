using System;


public enum CreatureType
{
    None,
    Player = 1,
    Monster = 2

}

class Creature
{
    CreatureType type;
    public int HP { get; protected set; }
    public string Name { get; set; }
    public bool IsDead => HP <= 0;

    public int AttackPower { get; protected set; }
    public int DefensePower { get; protected set; }

    protected Creature(CreatureType type)
    {
        this.type = type;
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

        if (HP < 0)
            HP = 0;
    }



}






