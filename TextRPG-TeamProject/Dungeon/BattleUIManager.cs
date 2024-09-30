using System;
using System.Numerics;


class BattleUIManager
{
    Player player;

    public BattleUIManager()
    { 
        player = GameData.Player;
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
        for (int i = 0; i < GameData.DeathMonster.Length; i++)
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
            $"플레이어:{player.Name} Lv:{player.Level}({player.Exp}/{player.ExpToNextLv})",
            $"HP:{player.HP}",
            $"MP:{player.MP}",
            $"공격력:{player.GetTotalAttackPower()}{extraAttackPower}",
            $"방어력:{player.GetTotalDefensePower()}{extraDefensePower}",
            $"치명타율:{player.CriticalChance}",
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

