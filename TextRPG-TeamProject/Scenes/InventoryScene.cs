using System;

class InventoryScene : Scene
{
    public override void Start ()
    {
        Console.Clear();
    }

    public override void Update ()
    {
        Console.WriteLine("인벤토리");
        Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
        Console.WriteLine();

        Console.WriteLine("[아이템 목록]");
        WriteItemList();
        Console.WriteLine();

        var options = new string[] {"장착 관리", "나가기"};
        int selectedNumber = UIManager.DisplaySelectionUI(options);
        HandleInput(selectedNumber);
    }

    private void WriteItemList()
    {
        string[][] table = new string[Inventory.ItemList.Count][];

        for (int i = 0; i < Inventory.ItemList.Count; i++)
        {
            Item item = Inventory.ItemList[i];
            table[i] = new string[4] { $"- {item.Name}", item.StatInfo, item.Desc, $"{item.Price}" };
        }

        WriteItemDetail(table);
    }

    private void WriteItemDetail (string[][] table)
    {
        for (int row = 0; row < table.Count(); row++)
        {
            for (int col = 0; col < table[row].Count() - 1; col++)
                Console.Write($"{table[row][col]} | ");
            Console.WriteLine(table[row].Last());
        }
    }

    private void HandleInput(int selectedNumber)
    {
        switch (selectedNumber)
        {
        case 1:
        case 2:
            NextScene = new StartScene();
            break;
        }
    }
}
