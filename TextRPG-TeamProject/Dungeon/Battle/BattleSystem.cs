enum AttackType
{
    None,
    Miss,
    Critical,
    Normal
}

public delegate void OnWinBattle();

public delegate void OnLoseBattle();


class BattleSystem
{
    public event OnWinBattle OnWinBattle;
    public event OnLoseBattle OnLoseBattle;


    private Player player;
    private BattleUtilities battleUtilities;
    private List<Monster> monsters = GameData.AliveMonster;
    private bool isPlayerTurn = true;

    public BattleSystem()
    {
        battleUtilities = new BattleUtilities();
        player = GameData.Player;
    }

    public void ProcessMonsterTurn()
    {
        BattleUIManager.DisplayTurnUI("몬스터 턴");

        for (int i = 0; i < GameData.AliveMonster.Count; i++)
        {
            PerformAttack(GameData.AliveMonster[i], player);
        }
    }

    public void ProcessPlayerTurn()
    {
        SelectPlayerAction();
    }


    private void SelectPlayerAction()
    {
        const string playerTurnMessage = "플레이어 턴 - 행동선택";

        while (true)
        {
            BattleUIManager.DisplayTurnUI(playerTurnMessage);
            string[] options = { "일반공격", "스킬", "아이템" };
            int selectedAction = UIManager.DisplaySelectionUI(options);

            int backButtonIndex;

            switch (selectedAction)
            {
                case 1:
                    backButtonIndex = BattleUIManager.GetMonsterOptions().Length;
                    break;
                case 2:
                    backButtonIndex = BattleUIManager.GetMonsterOptions().Length;
                    break;
                case 3:
                    backButtonIndex = BattleUIManager.GetPotionOptions().Length;
                    break;
                default:
                    backButtonIndex = 0;
                    break;
            }


            if (selectedAction == 1 && HandleAttackSelection(backButtonIndex)) break;
            if (selectedAction == 2 && HandleSkillSelection(backButtonIndex)) break;
            if (selectedAction == 3 && HandleItemSelection(backButtonIndex)) break;
        }
    }


    private bool HandleAttackSelection(int backButtonIndex)
    {
        int selectedTargetIndex = BattleUIManager.ShowBattleChoices();

        if (selectedTargetIndex == backButtonIndex)
            return false;

        PerformAttack(player, monsters[selectedTargetIndex]);
        return true;
    }

    private bool HandleSkillSelection(int backButtonIndex)
    {
        int selectedTargetIndex = BattleUIManager.ShowBattleChoices();

        if (selectedTargetIndex == backButtonIndex)
            return false;


        ISkill skill = battleUtilities.GetSkillByType(player.Type);
        if (skill.ManaCost > player.MP)
        {
            BattleUIManager.ShowManaError();
            return false;
        }

        PerformSkill(player, monsters[selectedTargetIndex], skill);
        return true;
    }


    private bool HandleItemSelection(int backButtonIndex)
    {
        BattleUIManager.DisplayTurnUI("플레이어 턴 - 아이템 사용");
        int selectedTargetIndex = BattleUIManager.ShowPotionChoices();

        if (selectedTargetIndex == backButtonIndex)
            return false;


        var potionList = Inventory.GetItemsByType<Potion>();

        Potion potion = potionList[selectedTargetIndex];
        UsePotion(potion);

        return true;
    }


    public void UsePotion(Potion potion)
    {
        BattleUIManager.DisplayTurnUI("플레이어 턴 - 포션사용");
        Inventory.UsePotion(potion.Id);
        string[] texts = BattleUIManager.GetPotionUsageResultTexts(player, potion);
        UIManager.AlignTextCenter(texts, -2);


        string[] options = new string[] { "다음" };
        UIManager.DisplaySelectionUI(options);
    }


    //스킬 치명타 부분 추가.
    public void PerformSkill(Player caster, Monster target, ISkill skill)
    {
        BattleUIManager.DisplayTurnUI("플레이어 턴 - 스킬사용");
        bool isSkill = true;
        AttackType type = battleUtilities.GetAttackOutcome(isSkill);
        int damage = skill.UseSkill(caster, target);
        string[] texts = BattleUIManager.GetSkillResultTexts(caster, target, damage, skill);
        UIManager.AlignTextCenter(texts, -2);

        string[] options = new string[] { "다음" };
        UIManager.DisplaySelectionUI(options);


        ApplyDamage(target, damage);
    }


    private void ApplyDamage(Creature target, int damage)
    {
        target.OnDamaged(damage);
    }


    public void PerformAttack(Creature attacker, Creature target)
    {
        AttackType type = battleUtilities.GetAttackOutcome();

        int damage;
        damage = battleUtilities.CalculateDamage(type, attacker, target);
        string[] texts;


        if (attacker.CreatureType == CreatureType.Player)
            BattleUIManager.DisplayTurnUI("플레이어 턴 - 공격 결과");

        else
            BattleUIManager.DisplayTurnUI("몬스터 턴 - 공격 결과");


        texts = BattleUIManager.GetAttackResultTexts(attacker, target, type, damage);
        UIManager.AlignTextCenter(texts, -2);

        string[] options = { "다음" };
        UIManager.DisplaySelectionUI(options);

        ApplyDamage(target, damage);
    }


    public void StartBattle()
    {
        for (int i = 0; i < monsters.Count; i++)
        {
            monsters[i].OnDeath += HandleMonsterDeath;
        }

        while (true)
        {
            if (monsters.Count == 0)
            {
                OnWinBattle.Invoke();
                break;
            }

            else
            {
                if (player.IsDead)
                {
                    OnLoseBattle.Invoke();
                    break;
                }

                if (isPlayerTurn)
                    ProcessPlayerTurn();
                else
                    ProcessMonsterTurn();

                isPlayerTurn = !isPlayerTurn;
            }
        }
    }

    public void HandleMonsterDeath(Monster monster)
    {
        monster.OnDeath -= HandleMonsterDeath;
        GameData.AliveMonster.Remove(monster);
        GameData.DeathMonster.Add(monster);
        int prevPlayerLevel = player.Level;

        player.AddExp(monster.DropExp);
        player.AddGold(monster.DropGold);


        BattleUIManager.DisplayTurnUI("몬스터 턴 - 공격 결과");
        string[] texts =

        {
            $"{monster.Name}({monster.InstanceNumber})을 처치!", $"{monster.DropExp}의 경험치와 {monster.DropGold} Gold를 획득"
        };


        UIManager.AlignTextCenter(texts, -2);


        if (prevPlayerLevel != player.Level)
        {
            texts = texts.Concat(new string[] { $"lv {prevPlayerLevel} -> {player.Level} " }).ToArray();
            UIManager.AlignTextCenter(texts, -2);
        }

        string[] options = { "다음" };
        UIManager.DisplaySelectionUI(options);
    }
}