using System;
using System.Linq;
using System.Numerics;
using System.Reflection.Emit;
using System.Threading;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

public enum AttackType
{
    None,
    Miss,
    Critical,
    Normal
}
public delegate void OnEndBattle();

class BattleSystem
{

    public event OnEndBattle End;


    private Player player;
    private BattleUIManager battleUIManager;
    private BattleUtilities battleUtilities;

    private List<Monster> monsters = GameData.AliveMonster;

    private Random rand = new Random();
    private double missChance = 0.10f;

    private int diedMonsterNum = 0;
    private bool isPlayerTurn =true;

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
            if (selectedAction == 3 && HandleItemSelection(backButtonIndex)) break;
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


    private bool HandleItemSelection(int backButtonIndex)
    {
        battleUIManager.DisplayTurnUI("플레이어 턴 - 아이템 사용");
        int selectedTargetIndex = battleUIManager.ShowPotionChoices();

        if (selectedTargetIndex == backButtonIndex)
            return false;

        var potionList = Inventory.GetItemsByType<Potion>();
        Potion potion = potionList[selectedTargetIndex];

        UsePotion(potion);
        return true;
    }


    public void UsePotion(Potion potion)
    {
        battleUIManager.DisplayTurnUI("플레이어 턴 - 포션사용");
        Inventory.UsePotion(potion.Id);
        string[] texts  = battleUIManager.GetPotionUsageResultTexts(player, potion);
        UIManager.AlignTextCenter(texts);
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


    public void EndBattle()
    {
    
       

        End?.Invoke();
    }


    public void StartBattle()
    {

        for (int i = 0; i < monsters.Count; i++)
        {
            monsters[i].OnDeath += TriggerMonsterDeath;
        }

        while (true)
        {

            if (monsters.Count == 0)
            {
                EndBattle();
                break;
            }

            else
            {
                if(isPlayerTurn)
                     ProcessPlayerTurn();
                else
                     ProcessMonsterTurn();

                isPlayerTurn = !isPlayerTurn;
            }

        }


    }


    public  void TriggerMonsterDeath(Monster monster)
    {
        monster.OnDeath -= TriggerMonsterDeath;
        GameData.AliveMonster.Remove(monster);
        GameData.DeathMonster.Add(monster);
        int prevPlayerLevel = player.Level;
   
        player.AddExp(monster.DropExp);
        player.AddGold(monster.DropGold);

        string [] texts = { $"{monster.Name}({monster.InstanceNumber})을 처치!",$"{monster.DropExp}의 경험치와 {monster.DropGold} Gold를 획득" };
        UIManager.AlignTextCenter(texts);

        if (prevPlayerLevel != player.Level)
        {
            texts = texts.Concat(new string [] {$"lv {prevPlayerLevel} -> {player.Level} "}).ToArray();
            UIManager.AlignTextCenter(texts);
        }

        string [] options = new string[] { "다음" };
        int selectNum = UIManager.DisplaySelectionUI(options);

    }





}


