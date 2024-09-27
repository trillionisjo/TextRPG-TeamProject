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

    protected int attackPower;
    protected int defensePower;

    protected Creature(CreatureType type)
    {
        this.type = type;
    }

    public void SetInfo(int hp, int attackPower, int defensePower)
    {
        HP = hp;
        this.attackPower = attackPower;
        this.defensePower = defensePower;
    }

    public int GetAttack() { return attackPower; }
    public void OnDamaged(int damage)
    {
        HP -= damage;

        if (HP < 0)
            HP = 0;
    }



}






