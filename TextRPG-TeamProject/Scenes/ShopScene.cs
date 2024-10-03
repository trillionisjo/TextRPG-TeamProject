using System;

class ShopScene : Scene
{
    public override void Update ()
    {
        Console.Clear();
        UIManager.TitleBox("    상점    ");
        Console.WriteLine("“이곳에서 모험에 필요한 물건을 구매하세요. 필요 없는 물건이 있다면 판매하여 골드를 모을 수도 있답니다!”");
        Console.WriteLine();

        Console.WriteLine("[보유 골드]");
        Console.WriteLine($"{GameData.Player.Gold} G");
        Console.WriteLine();

        Console.WriteLine("[아이템 목록]");
        WriteItemList();

        DisplaySelectionList();
    }

    private void WriteItemList()
    {
        string[,] table = new string[ShopData.ItemList.Count, 4];

        for (int i = 0; i < ShopData.ItemList.Count; i++)
        {
            IItem item = ShopData.ItemList[i];
            table[i, 0] = $"- {item.Name}";
            table[i, 1] = $"{item.StatInfo}";
            table[i, 2] = $"{item.Desc}";
            table[i, 3] = $"{item.Price} G";
        }

        UIManager.WriteTable(table);
    }

    private void DisplaySelectionList()
    {
        Option[] options = new Option[] {
            new Option("구매하기", new NextSceneEvent(this, new PurchaseScene())),
            new Option("판매하기", new NextSceneEvent(this, new SellScene())),
            new Option("나가기", new NextSceneEvent(this, new StartScene())),
        };

        (int x, int y) point = Console.GetCursorPosition();
        UIManager.DrawLine(point.y + 1);
        UIManager.DisplaySelectionUI(options, point.x, point.y + 2, 0);
    }
}
