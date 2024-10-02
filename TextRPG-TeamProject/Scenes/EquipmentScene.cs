


class EquipmentScene : Scene
{
    private class ToggleEvent : IEventHandler
    {
        IEquipable item;
        public ToggleEvent (IEquipable item)
        {
            this.item = item;
        }

        public void Invoke ()
        {
            EquipManager.ToggleEquip(item);
        }
    }

    int cursor = 0;

    public override void Start ()
    {
        AudioManager.PlayAudio("main_bgm.mp3");
    }

    public override void Update ()
    {
        Console.Clear();
        Console.WriteLine("인벤토리 - 장착관리");
        Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
        Console.WriteLine();

        Console.WriteLine("[아이템 목록]");
        WriteItemList();
    }


    private void WriteItemList()
    {
        var equipmentList = Inventory.GetItemsByType<IEquipable>();
        int n = equipmentList.Count;
        string[,] table = new string[n, 3];
        var options = new Option[n + 3];

        for (int i = 0; i < equipmentList.Count; i++)
        {
            string strEquipted = EquipManager.IsEquiptedItem(equipmentList[i]) ? "[E]" : "";

            table[i, 0] = $"{strEquipted}{equipmentList[i].Name}";
            table[i, 1] = equipmentList[i].StatInfo;
            table[i, 2] = equipmentList[i].Desc;

            options[i] = new Option("", new ToggleEvent(equipmentList[i]));
        }

        string[] paddedList = UIManager.CreatePaddedList(table);

        for (int i = 0; i < paddedList.Count(); i++)
            options[i].Text = paddedList[i];

        options[n + 1] = new Option(UIManager.GetLineString(), null);
        options[n + 2] = new Option("나가기", new NextSceneEvent(this, new InventoryScene()));

        (int x, int y) point = Console.GetCursorPosition();
        cursor = UIManager.DisplaySelectionUI(options, point.x, point.y, cursor);
    }
}
;