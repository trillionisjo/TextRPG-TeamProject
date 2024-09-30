using System;
using System.Linq;
using System.Numerics;
using System.Reflection.Emit;
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
    private BattleUtilities battleUtilities;

    private List<Monster> monsters = GameData.AliveMonster;

    private Random rand = new Random();
    private double missChance = 0.10f;

    private int diedMonsterNum = 0;


    public BattleSystem()
    {
        battleUtilities = new BattleUtilities();
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

            int backButtonIndex = battleUIManager.GetMonsterOptions().Length;

            if (selectedAction == 1 && HandleAttackSelection(backButtonIndex)) break;
            if (selectedAction == 2 && HandleSkillSelection(backButtonIndex)) break;
            if (selectedAction == 3 && HandleItemSelection()) break;
        }
    }


    private bool HandleAttackSelection(int backButtonIndex)
    {
        int selectedTargetIndex = battleUIManager.ShowBattleChoices();

        if (selectedTargetIndex == backButtonIndex)
            return false;

        PerformAttack(player, monsters[selectedTargetIndex]);
        return true;

    }

    private bool HandleSkillSelection(int backButtonIndex)
    {
        int selectedTargetIndex = battleUIManager.ShowBattleChoices();

        if (selectedTargetIndex == backButtonIndex)
            return false;

     
        ISkill skill = battleUtilities.GetSkillByType(player.Type);
        if (skill.ManaCost > player.MP)
        {
            battleUIManager.ShowManaError();
            return false;
        }

        PerformSkill(player, monsters[selectedTargetIndex ],skill);
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



    public void PerformSkill(Player player, Monster target, ISkill skill)
    {
        battleUIManager.DisplayTurnUI("플레이어 턴 - 스킬사용");
        bool isSkill = true;
        AttackType type = battleUtilities.GetAttackOutcome(isSkill);
        int damage = skill.UseSkill(player, target);
        string[] texts = battleUIManager.GetSkillResultTexts(player, target, damage, skill);
        UIManager.AlignTextCenter(texts);

        ApplyDamage(target, damage, texts);
    }

    private void ApplyDamage(Creature target, int damage, string[] texts)
    {
        target.OnDamaged(damage);
        if (target.IsDead && target is Monster monster)
        {
            int lineSpacing = texts.Length;
            HandleMonsterDeath(monster, lineSpacing);
        }
    }


    public void PerformAttack(Creature attacker, Creature target)
    {

        AttackType type = battleUtilities.GetAttackOutcome();

        int damage = 0;
        damage = battleUtilities.CalculateDamage(type, attacker, target);
        string[] texts;


        if (attacker.CreatureType == CreatureType.Player)
            battleUIManager.DisplayTurnUI("플레이어 턴 - 공격 결과");

        else
            battleUIManager.DisplayTurnUI("몬스터 턴 - 공격 결과");


        texts = battleUIManager.GetAttackResultTexts(attacker, target, type, damage);
        ApplyDamage(target, damage, texts);
        UIManager.AlignTextCenter(texts);
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


