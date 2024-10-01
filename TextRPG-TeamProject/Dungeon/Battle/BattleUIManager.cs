using System;
using System.Linq;
using System.Numerics;
using static System.Net.Mime.MediaTypeNames;


class BattleUIManager
{
    Player player;

    public BattleUIManager()
    {
        player = GameData.Player;
    }

    public int ShowBattleChoices()
    {

        DisplayTurnUI("플레이어 턴 - 대상선택");
        string[] mobList = GetMonsterOptions();
        string[] previousPage = { "뒤로가기" };
        string[] options = mobList.Concat(previousPage).ToArray();
        int selectNum = UIManager.DisplaySelectionUI(options);
        return selectNum - 1;
    }

    public int ShowPotionChoices()
    {

        DisplayTurnUI("플레이어 턴 - 포션선택");
        string[] potionList = GetPotionOptions();
        string[] previousPage = { "뒤로가기" };
        string[] options = potionList.Concat(previousPage).ToArray();
        int selectNum = UIManager.DisplaySelectionUI(options);
        return selectNum - 1;
    }


    public string[] GetPotionOptions()
    {
        var potionList = Inventory.GetItemsByType<Potion>();
        string[] options = new string[potionList.Count];
        for (int i = 0; i < potionList.Count; i++)
        {
            options[i] = $"{potionList[i].Name}";
        }

        return options;
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


    public string[] GetSkillResultTexts(Player caster, Monster target, int damage ,ISkill skill)
    {
       int previousMp =caster.MP + skill.ManaCost;

        string[] texts =
       {
          $"{target.Name}({target.InstanceNumber})에게 {skill.SkillName} 사용",
          $"{damage}의 피해",
          $"MP{previousMp} -> {caster.MP}"
        };

        return texts;
 }


    public string[] GetPotionUsageResultTexts(Player player, Potion potion)
    {
        int previousPower = 0;
        int currentPower = 0;
        string power= " ";

        if (potion.Id == ItemId.HpPotion)
        {
            previousPower = player.HP - potion.RecoveryPower;
            currentPower = player.HP;
            power = "HP";
        }

        else if (potion.Id == ItemId.MpPotion)
        {
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



    public string[] GetAttackResultTexts(Creature attacker, Creature target, AttackType type, int damage)
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


    public void DisplayMonsterList()
    {
        Console.ForegroundColor = ConsoleColor.Green;

        for (int i = 0; i < GameData.AliveMonster.Count; i++)
        {
            Console.WriteLine($"Lv.{GameData.AliveMonster[i].Level} {GameData.AliveMonster[i].Name}({GameData.AliveMonster[i].InstanceNumber}) HP {GameData.AliveMonster[i].HP} 공격력: {GameData.AliveMonster[i].GetTotalAttackPower()} 방어력: {GameData.AliveMonster[i].GetTotalDefensePower()} ");
        }

        Console.WriteLine();

        Console.ForegroundColor = ConsoleColor.Red;
        for (int i = 0; i < GameData.DeathMonster.Count; i++)
        {

            if (GameData.DeathMonster[i] != null)
                Console.WriteLine($"Lv.{GameData.DeathMonster[i].Level} {GameData.DeathMonster[i].Name}({GameData.DeathMonster[i].InstanceNumber}) HP {(GameData.DeathMonster[i].HP > 0 ? GameData.DeathMonster[i].HP : "Dead")} ");

        }

        Console.ForegroundColor = ConsoleColor.White;
    }

    //스탯 정렬부분 개선 필요 .
    public void DisplayPlayerStat()
    {
        string extraAttackPower = player.ExtraAttackPower > 0 ? $"({player.ExtraAttackPower})" : "";
        string extraDefensePower = player.ExtraAttackPower > 0 ? $"({player.ExtraAttackPower})" : "";

        string[] statText =
      {
        $"플레이어: {player.Name,-8} Lv: {player.Level} ({player.Exp}/{player.ExpToNextLv})",
        $"체력    : {player.HP,-8} 마나: {player.MP}",
        $"공격력  : {player.GetTotalAttackPower(),-8}{extraAttackPower} 방어력: {player.GetTotalDefensePower()}{extraDefensePower}",
        $"치명타율: {player.CriticalChance}"
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
    public void ShowManaError()
    {
        DisplayTurnUI("플레이어 턴 - 행동선택");
        UIManager.AlignTextCenter("마나가 부족합니다");
        string[] options = { "다음" };
        UIManager.DisplaySelectionUI(options);
    }

    public void DisplayTurnUI(string title)
    {
        Console.Clear();
        DisplayPlayerStat();
        UIManager.TitleBox(title);
        DisplayMonsterList();
    }



    private int GetMaxWidth(string[] texts)
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

