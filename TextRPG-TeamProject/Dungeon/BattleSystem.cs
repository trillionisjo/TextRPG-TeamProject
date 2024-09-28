using System;
using System.Threading;

public enum AttackType
{
    None,
    Miss,
    Critical,
    Normal
}

class BattleSystem
{

    private Player player;
    private BattleUIManager battleUIManager;
    private double missChance = 0.10f;
    private Random rand = new Random();


    public BattleSystem()
    {
        battleUIManager = new BattleUIManager();
        player = GameData.Player;
    }



    public void ProcessPlayerTurn()
    {
        //공격 선택
        battleUIManager.ShowTurnUI("플레이어 턴 - 행동선택");
        string[] options = { "일반공격", "스킬" };
        int selectNum = UIManager.DisplaySelectionUI(options);

        //선택 결과에 따라 회피 , 치명타, 기본 중 공격 결과 도츨 
        AttackType type = DetermineAttackOutcome();


        //대상 선택 
        battleUIManager.ShowTurnUI("플레이어 턴 - 대상선택");
        options = GetMonsterOptions();
        selectNum = UIManager.DisplaySelectionUI(options);
        Monster monster = GameData.AliveMonster[selectNum - 1];

        //공격 수행
        battleUIManager.ShowTurnUI("플레이어 턴 - 공격 결과");
        PerformAttack(player, monster, type);
        options = new string[] { "다음" };
        selectNum = UIManager.DisplaySelectionUI(options);

    }
    public void ProcessMonsterTurn()
    {
        string[] options;
        int selectNum = 0;
        //몬스터턴
        battleUIManager.ShowTurnUI("몬스터 턴");

        for (int i = 0; i < GameData.AliveMonster.Count; i++)
        {
            AttackType type = DetermineAttackOutcome();
            battleUIManager.ShowTurnUI("몬스터 턴 - 공격 결과");
            PerformAttack(GameData.AliveMonster[i], player, type);


            if (i < GameData.AliveMonster.Count - 1)
            {
                options = new string[] { "다음" };
                selectNum = UIManager.DisplaySelectionUI(options);
            }
        }

        options = new string[] { "다음" };
        selectNum = UIManager.DisplaySelectionUI(options);


    }

    public void PerformAttack(Creature attacker, Creature target, AttackType type)
    {
        int damage = 0;
        damage = CalculateDamage(type, attacker, target);
        string critical = (type == AttackType.Critical) ? "(치명타)" : "";
        string[] texts = { $"{target.Name}에게 {damage}만큼의 공격{critical}", $"{target.Name} HP:{target.HP} -> {(target.HP - damage > 0 ? target.HP - damage : "Dead")}" };
        UIManager.AlignTextCenter(texts);
        target.OnDamaged(damage);
    }

    
    public string[] GetMonsterOptions()
    {
        string[] options = new string[GameData.AliveMonster.Count];

        for (int i = 0; i < GameData.AliveMonster.Count; i++)
        {
            options[i] = $" {GameData.AliveMonster[i].Name}({GameData.AliveMonster[i].InstanceNumber})";
        }

        return options;
    }


    private int CalculateDamage(AttackType type, Creature attacker, Creature target)
    {
        int damage = 0;

        switch (type)
        {
            case AttackType.Miss:
                break;
            case AttackType.Critical:
                damage = (int)Math.Round(attacker.GetTotalAttackPower() * attacker.CritDmgPct);
                break;
            case AttackType.Normal:
                damage = attacker.GetTotalAttackPower();
                break;
            default:
                break;
        }

        int totalDamage = damage - target.GetTotalDefensePower();

        if (totalDamage < 1)
            totalDamage = 1;

        return type == AttackType.Miss ? 0 : totalDamage;
    }

    public AttackType DetermineAttackOutcome(bool isSkill = false)
    {
        double chance = rand.NextDouble();

        // 1. 회피 판정
        if (!isSkill && chance < missChance)
            return AttackType.Miss;

        // 2. 치명타 판정
        if (chance < player.CriticalChance)
            return AttackType.Critical;

        // 3. 정타 (Normal) 판정
        return AttackType.Normal;
    }




}

