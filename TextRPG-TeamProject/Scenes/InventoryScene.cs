using System;

class InventoryScene : Scene
{
    public override void Start()
    {
        AudioManager.PlayAudio("main_bgm.mp3");
        Console.Clear();
    }

    public override void Update()
    {
        Console.WriteLine("인벤토리");
        Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
        Console.WriteLine();

        Console.WriteLine("[아이템 목록]");
        WriteItemList();
        Console.WriteLine();

        var options = new string[] { "장착 관리", $"회복약 사용 ({Inventory.QueryItemCount(ItemId.HpPotion)})", "나가기" };
        int selectedNumber = UIManager.DisplaySelectionUI(options);
        HandleInput(selectedNumber);
    }

    protected void WriteItemList()
    {
        string[,] table = new string[Inventory.ItemList.Count, 3];

        for (int i = 0; i < Inventory.ItemList.Count; i++)
        {
            IItem item = Inventory.ItemList[i];

            if (item is IEquipable equipment)
            {
                string strEquipted = Equipment.IsEquiptedItem(equipment) ? "[E]" : "";
                table[i, 0] = $"- {strEquipted}{item.Name}";
            }
            else
                table[i, 0] = $"- {item.Name}";

            table[i, 1] = item.StatInfo;
            table[i, 2] = item.Desc;
            //table[i, 3] = $"{item.Price} G";
        }

        UIManager.WriteTable(table);
    }

    private void HandleInput(int selectedNumber)
    {
        switch (selectedNumber)
        {
            case 1:
                NextScene = new EquipmentScene();
                break;

            case 2:
                Inventory.UsePotion(ItemId.HpPotion);
                break;

            case 3:
                NextScene = new StartScene();
                break;
        }
    }
}