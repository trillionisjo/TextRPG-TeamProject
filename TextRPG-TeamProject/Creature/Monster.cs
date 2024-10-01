using System;
using System.Threading;


public enum MonsterGrade
{
    None,
    Low = 1,             // 낮은 위험도
    Medium = 2,          // 중간 위험도
    High = 3,            // 높은 위험도
    VeryHigh = 4,        // 매우 높은 위험도
    Extreme = 5,         // 극한 위험도
}

public enum MonsterType
{
    None,
    Slime, 
    Goblin,
    Kobold,
    Orc,
    Ogre,

}



public class Monster : Creature, IDeathNotifier<Monster>
{
    public MonsterType Type { get; set; }
    public MonsterGrade Grade { get; set; }
    public int InstanceNumber { get; set; }

    public int DropExp { get; set; }
    public int DropGold { get; set; }

    public event OnDeath<Monster> OnDeath;



    public override void OnDamaged(int damage)
    {

        HP -= damage;

        if (HP <= 0)
        {
            HP = 0;
            OnDeath?.Invoke(this);
        }
    }

    public Monster() : base(CreatureType.Monster)
    { 
    
    }

    public Monster(MonsterGrade grade,bool isBoss =false) : base(CreatureType.Monster)
    {
        Grade = grade;
        Level = (int)Grade;
        DropExp = 1 *  (int)grade;
        DropGold = 100 * (int)grade;
        Init();
        
    }


     void InitBossMonster()
    {
        switch (Grade)
        {
            case MonsterGrade.Low:
                SetInfo(10, 6, 12);
                break;
            case MonsterGrade.Medium:
                SetInfo(5, 3, 6);
                break;
            case MonsterGrade.High:
                SetInfo(5, 3, 6);
                break;
            case MonsterGrade.VeryHigh:
                SetInfo(5, 3, 6);
                break;
            case MonsterGrade.Extreme:
                SetInfo(5, 3, 6);
                break;
            default:
                break;

        }
    }

    void Init()
    {
        switch (Grade)
        {
            case MonsterGrade.Low:
                SetInfo(5, 3, 2);
                break;
            case MonsterGrade.Medium:
                SetInfo(10, 6, 6);
                break;
            case MonsterGrade.High:
                SetInfo(20, 6, 8);
                break;
            case MonsterGrade.VeryHigh:
                SetInfo(30, 6, 10);
                break;
            case MonsterGrade.Extreme:
                SetInfo(100, 10, 10);
                break;
            default:
                break;

        }

    }

}





