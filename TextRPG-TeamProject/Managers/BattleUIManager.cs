static class BattleUIManager
{
    static Player player = GameData.Player;


    static public int ShowBattleChoices()
    {
        DisplayTurnUI("플레이어 턴 - 대상선택");
        string[] mobList = GetMonsterOptions();
        string[] previousPage = { "뒤로가기" };
        string[] options = mobList.Concat(previousPage).ToArray();
        int selectNum = UIManager.DisplaySelectionUI(options);
        return selectNum - 1;
    }

    static public int ShowPotionChoices()
    {
        DisplayTurnUI("플레이어 턴 - 포션선택");
        string[] potionList = GetPotionOptions();
        string[] previousPage = { "뒤로가기" };
        string[] options = potionList.Concat(previousPage).ToArray();
        int selectNum = UIManager.DisplaySelectionUI(options);
        return selectNum - 1;
    }


    static public string[] GetPotionOptions()
    {
        var potionList = Inventory.GetItemsByType<Potion>();
        string[] options = new string[potionList.Count];
        for (int i = 0; i < potionList.Count; i++)
        {
            options[i] = $"{potionList[i].Name}";
        }

        return options;
    }


    static public string[] GetMonsterOptions()
    {
        string[] options = new string[GameData.AliveMonster.Count];
        for (int i = 0; i < GameData.AliveMonster.Count; i++)
        {
            options[i] = $"{GameData.AliveMonster[i].Name}({GameData.AliveMonster[i].InstanceNumber})";
        }

        return options;
    }


    static public string[] GetSkillResultTexts(Player caster, Monster target, int damage, ISkill skill)
    {
        int previousMp = caster.MP + skill.ManaCost;

        string[] texts =
        {
            $"{target.Name}({target.InstanceNumber})에게 {skill.SkillName} 사용",
            $"{damage}의 피해",
            $"MP{previousMp} -> {caster.MP}"
        };

        return texts;
    }


    static public string[] GetPotionUsageResultTexts(Player player, Potion potion)
    {
        int previousPower = 0;
        int currentPower = 0;
        string power = " ";

        if (potion.Id == ItemId.HpPotion)
        {
            if (player.HP + potion.RecoveryPower == player.MaxHP)
                previousPower = player.HP;
            else
                previousPower = player.HP - potion.RecoveryPower;

            currentPower = player.HP;
            power = "HP";
        }

        else if (potion.Id == ItemId.MpPotion)
        {
            if (player.MP + potion.RecoveryPower == player.MaxMP)
                previousPower = player.MP;
            else
                previousPower = player.MP - potion.RecoveryPower;

            currentPower = player.MP;
            power = "MP";
        }


        string[] texts =
        {
            $"{(potion.Id == ItemId.HpPotion ? "회복포션" : "마나포션")} 사용",
            $"{potion.RecoveryPower}만큼 회복",
            $"{power}{previousPower} -> {currentPower}"
        };

        return texts;
    }


    static public string[] GetAttackResultTexts(Creature attacker, Creature target, AttackType type, int damage)
    {
        string[] texts;
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
        }

        return texts;
    }


    static public void DisplayMonsterList()
    {
        Console.ForegroundColor = ConsoleColor.Green;

        for (int i = 0; i < GameData.AliveMonster.Count; i++)
        {
            var monster = GameData.AliveMonster[i];
            var monsterInfo = $"Lv.{monster.Level}ㅣ{monster.Name}({monster.InstanceNumber})";
            var monsterStats =
                $"HP: {monster.HP}ㅣ공격력: {monster.GetTotalAttackPower()}ㅣ방어력: {monster.GetTotalDefensePower()}";
            Console.WriteLine($"{monsterInfo} {monsterStats}");
        }


        Console.WriteLine();

        Console.ForegroundColor = ConsoleColor.Red;
        for (int i = 0; i < GameData.DeathMonster.Count; i++)
        {
            var monster = GameData.DeathMonster[i];
            var monsterHp = (monster.HP > 0) ? monster.HP.ToString() : "Dead";
            string monsterInfo = $"Lv.{monster.Level} {monster.Name} ({monster.InstanceNumber}) HP: {monsterHp}";
            Console.WriteLine(monsterInfo);
        }

        Console.ForegroundColor = ConsoleColor.White;
    }

    static public void DisplayPlayerStat()
    {
        string extraAttackPower = player.ExtraAttackPower > 0 ? $"(+{player.ExtraAttackPower})" : "";
        string extraDefensePower = player.ExtraDefensePower > 0 ? $"(+{player.ExtraDefensePower})" : "";

        string[] statText =
        {
            $"Lv:{player.Level}({player.Exp}/{player.ExpToNextLv})",
            $"HP:{player.HP}ㅣMP:{player.MP}",
            $"ATK:{player.GetTotalAttackPower()}{extraAttackPower}ㅣDEF:{player.GetTotalDefensePower()}{extraDefensePower}"
        };


        int maxTextWidth = GetMaxWidth(statText);
        int cursorX = Console.WindowWidth - maxTextWidth;
        int cursorY = 0;
        int size = 0;

        for (int i = 0; i < statText.Length; i++)
        {
            int newSize = UIManager.GetByteFromText(statText[i]);
            if (size < newSize)
                size = newSize;
        }


        for (int i = 0; i < statText.Length; i++)
        {
            Console.SetCursorPosition(cursorX, cursorY + i);
            Console.WriteLine(statText[i]);
        }
    }

    static public void ShowManaError()
    {
        DisplayTurnUI("플레이어 턴 - 행동선택");
        UIManager.AlignTextCenter("마나가 부족합니다");
        string[] options = { "다음" };
        UIManager.DisplaySelectionUI(options);
    }

    static public void DisplayTurnUI(string title)
    {
        Console.Clear();
        DisplayPlayerStat();
        UIManager.TitleBox(title);
        DisplayMonsterList();
    }


    static private int GetMaxWidth(string[] texts)
    {
        int maxWidth = 0;

        for (int i = 0; i < texts.Length; i++)
        {
            int width = UIManager.GetByteFromText(texts[i]);
            if (width > maxWidth)
                maxWidth = width;
        }

        return maxWidth;
    }
}