using System;
using System.Linq;
using System.Numerics;
using System.Threading;
using static System.Net.Mime.MediaTypeNames;

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

    private List<Monster> monsters = GameData.AliveMonster;

    private Random rand = new Random();
    private double missChance = 0.10f;

    private int diedMonsterNum = 0;


    public BattleSystem()
    {
        battleUIManager = new BattleUIManager();
        player = GameData.Player;
    }

    public void ProcessMonsterTurn()
    {
        string[] options;
        int selectNum = 0;

        battleUIManager.DisplayTurnUI("몬스터 턴");

        for (int i = 0; i < GameData.AliveMonster.Count; i++)
        {
            PerformAttack(GameData.AliveMonster[i], player);

            if (i < GameData.AliveMonster.Count - 1)
            {
                options = new string[] { "다음" };
                selectNum = UIManager.DisplaySelectionUI(options);
            }
        }

        options = new string[] { "다음" };
        selectNum = UIManager.DisplaySelectionUI(options);
    }


    public void ProcessPlayerTurn()
    {
        string[] options = new string[2];
        int selectNum = 0;

        SelectPlayerAction();

        options = new string[] { "다음" };
        selectNum = UIManager.DisplaySelectionUI(options);


    }




    private void SelectPlayerAction()
    {
        bool isAction = false;
        const string playerTurnMessage = "플레이어 턴 - 행동선택";

        while (!isAction)
        {
            battleUIManager.DisplayTurnUI(playerTurnMessage);
            string[] options = { "일반공격", "스킬", "아이템" };
            int selectedAction = UIManager.DisplaySelectionUI(options);

            int backButtonIndex = GetMonsterOptions().Length + 1;
            
            switch (selectedAction)
            {
                case 1:
                    isAction = HandleAttackSelection(backButtonIndex);
                    break;
                case 2:
                    isAction = HandleSkillSelection(backButtonIndex);
                    break;
                case 3:
                    isAction = HandleItemSelection();                  
                    break;

                default:
                    break;
            }
        }
    }


    private bool HandleAttackSelection(int backButtonIndex)
    {
        int selectedTargetIndex = ShowBattleChoices();

        if (selectedTargetIndex == backButtonIndex)
            return false;

        PerformAttack(player, monsters[selectedTargetIndex - 1]);
        return true;
    }
    private bool HandleSkillSelection(int backButtonIndex)
    {
        int selectedTargetIndex = ShowBattleChoices();

        if (selectedTargetIndex == backButtonIndex)
            return false;

        bool skillPerformed = PerformSkill(player, monsters[selectedTargetIndex - 1]);

        if (!skillPerformed)
        {
            ShowManaError();
            return false;
        }

        return true;
    }


    private bool HandleItemSelection()
    {
        battleUIManager.DisplayTurnUI("플레이어 턴 - 아이템 사용");
        
        string[] options = new string[] { "포션", "스크롤", "폭탄", "나가기" };
        int selectNum = UIManager.DisplaySelectionUI(options);

        return false;

        //포션 관리 


    }


    private int ShowBattleChoices()
    {
        
        battleUIManager.DisplayTurnUI("플레이어 턴 - 대상선택");
        string[] mobList = GetMonsterOptions();
        string[] previousPage = { "뒤로가기" };
        string[] options = mobList.Concat(previousPage).ToArray();
        int selectNum = UIManager.DisplaySelectionUI(options);
        return selectNum;
    }


    private ISkill GetSkillByType(Player player)
    {
        PlayerType playerType = player.Type;
        ISkill skill;

        switch ((int)playerType)
        {
            case 1:
            case 2:
                skill = new Mage();
                break;
            case 3:
            case 4:
            default:
                skill = new Mage();
                break;
        }
        return skill;
    }

         
    public bool PerformSkill(Player player, Monster target)
    {
        battleUIManager.DisplayTurnUI("플레이어 턴 - 스킬사용");
        bool isSkill = true;
        AttackType type = DetermineAttackOutcome(isSkill);
        ISkill skill = GetSkillByType(player);

        if (player.MP > skill.ManaCost)
        {
            return skill.UseSkill(player,target);
            
        }


        else
        {
            return false;
        }
    }


    private void ShowManaError()
    {
        battleUIManager.DisplayTurnUI("플레이어 턴 - 행동선택");
        UIManager.AlignTextCenter("마나가 부족합니다");
        string[] options = { "다음" };
        UIManager.DisplaySelectionUI(options);
    }








    public void PerformAttack(Creature attacker, Creature target)
    {

        AttackType type = DetermineAttackOutcome();

        int damage = 0;
        damage = CalculateDamage(type, attacker, target);
        string[] texts;


        if(attacker.CreatureType == CreatureType.Player)
            battleUIManager.DisplayTurnUI("플레이어 턴 - 공격 결과");
        
        else
          battleUIManager.DisplayTurnUI("몬스터 턴 - 공격 결과");



        if (type == AttackType.Miss)
        {
            texts = new string[] { $"{attacker.Name}의 공격은 빗나갔다." };
        }

        else
        {
            string critical = (type == AttackType.Critical) ? "(치명타)" : "";

            texts = new string[]
            {
                    $"{target.Name}에게 {damage}만큼의 공격{critical}",
                    $"{target.Name} HP:{target.HP} -> {(target.HP - damage > 0 ? target.HP - damage : "Dead")}"
            };

            target.OnDamaged(damage);

            if (target.IsDead && target is Monster monster)
            {
                int lineSpacing = texts.Length;
                HandleMonsterDeath(monster, lineSpacing);
            }
        }

        UIManager.AlignTextCenter(texts);
    }

    public string[] GetMonsterOptions()
    {
        string[] options = new string[GameData.AliveMonster.Count];

        for (int i = 0; i < GameData.AliveMonster.Count; i++)
        {
            options[i] = $"{GameData.AliveMonster[i].Name}({GameData.AliveMonster[i].InstanceNumber})";
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

    public void HandleMonsterDeath(Monster monster, int lineSpacing = 0)
    {
        int exp = (int)monster.Grade;
        int prevPlayerLevel = player.Level;
        player.AddExp(exp);
        GameData.DeathMonster[diedMonsterNum++] = monster;
        GameData.AliveMonster.Remove(monster);

        string text = $"{exp}의 경험치를 획득";
        UIManager.AlignTextCenter(text, lineSpacing);

        if (prevPlayerLevel != player.Level)
        {
            text = $"Lv {prevPlayerLevel} -> {player.Level} ";
            UIManager.AlignTextCenter(text, lineSpacing + 1);
        }

    }


}


