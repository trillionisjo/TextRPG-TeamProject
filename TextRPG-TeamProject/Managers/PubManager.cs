using System;

static class PubManager
{
    public static void PubDrink()
    {
        if (GameData.Player.Gold >= 10)
        {
            Console.Clear();
            GameData.Player.SpendGold(10);
            Console.WriteLine("한잔 마시고 나니 기분이 좋아졌습니다.");

            HandleInput();
        }
        else
        {
            Console.Clear();
            Console.WriteLine("이정도 돈도 없다니! 가난뱅이 녀석! 썩 꺼져!");

            HandleInput();
        }
    }

    public static void PubRest()
    {
        if (GameData.Player.Gold >= 50)
        {
            Console.Clear();
            GameData.Player.SpendGold(50);
            GameData.Player.HP = GameData.Player.MaxHP;
            GameData.Player.MP = GameData.Player.MaxMP;
            Console.WriteLine("지친 몸을 이끌고 편히 쉬었습니다.");

            HandleInput();
        }
        else
        {
            Console.Clear();
            Console.WriteLine("이정도 돈도 없다니! 가난뱅이 녀석! 썩 꺼져!");

            HandleInput();
        }
    }

    private static void HandleInput()
    {
        string[] option = { "나가기" };
        int number = UIManager.DisplaySelectionUI(option);

        switch (number)
        {
            case 1:
                break;

            default:
                break;
        }
    }
}