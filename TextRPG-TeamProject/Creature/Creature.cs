using Newtonsoft.Json;
using System;


public enum CreatureType
{
    None,
    Player = 1,
    Monster = 2

}

public class Creature
{
    [JsonProperty] public CreatureType CreatureType { get; set; }
    [JsonProperty] public int HP { get; set; }
    [JsonProperty] public int MP { get; set; }

    [JsonProperty] public int MaxHP { get; set; }
    [JsonProperty] public int MaxMP { get; set; }
    [JsonProperty] public string Name { get; set; }
    public bool IsDead => HP <= 0;
    [JsonProperty] public int Level { get; set; }
    [JsonProperty] public int AttackPower { get; set; }
    [JsonProperty] public int DefensePower { get; set; }
    [JsonProperty] public float CriticalChance { get; set; }
    [JsonProperty] public float CritDmgPct { get; set; }

    protected Creature(CreatureType type)
    {
        CreatureType = type;
    }

    public void SetInfo(int hp,int attackPower, int defensePower)
    {
        MaxHP = hp;
        HP = hp;
        AttackPower = attackPower;
        DefensePower = defensePower;
        CriticalChance = 0.15f;
        CritDmgPct =1.6f;
    }


    public virtual void OnDamaged(int damage)
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






