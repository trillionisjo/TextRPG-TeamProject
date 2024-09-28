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
    public float CriticalChance { get; set; }
    public float CritDmgPct { get; set; }

    protected Creature(CreatureType type)
    {
        CreatureType = type;
    }

    public void SetInfo(int hp, int attackPower, int defensePower)
    {
        HP = hp;
        AttackPower = attackPower;
        DefensePower = defensePower;
        CriticalChance = 0.15f;
        CritDmgPct =1.6f;
    }

    public void OnDamaged(int damage)
    {
        HP -= damage;

        if (HP < 0)
            HP = 0;
    }




    public virtual int GetTotalAttackPower()
    { 
        return AttackPower;
    }


    public virtual int GetTotalDefensePower()
    {
        return DefensePower;
    }



}






