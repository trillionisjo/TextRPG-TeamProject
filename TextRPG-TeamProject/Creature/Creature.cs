using System;


public enum CreatureType
{
    None,
    Player = 1,
    Monster = 2

}

class Creature
{
    public CreatureType CreatureType { get; set; }
    public int HP { get; set; }
    public string Name { get; set; }
    public bool IsDead => HP <= 0;
    public int Level { get; set; }
    public int AttackPower { get; set; }
    public int DefensePower { get; set; }

    protected Creature(CreatureType type)
    {
        CreatureType = type;
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






