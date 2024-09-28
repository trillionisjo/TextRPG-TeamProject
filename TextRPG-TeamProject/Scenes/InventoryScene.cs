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
        string[,] table = new string[Inventory.ItemList.Count, 4];

        for (int i = 0; i < Inventory.ItemList.Count; i++)
        {
            Item item = Inventory.ItemList[i];
            table[i, 0] = $"- {item.Name}";
            table[i, 1] = item.StatInfo;
            table[i, 2] = item.Desc;
            table[i, 3] = $"{item.Price} G";
        }

        UIManager.WriteTable(table);
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
