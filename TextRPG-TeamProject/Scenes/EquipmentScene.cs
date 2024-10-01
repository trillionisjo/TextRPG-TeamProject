


class EquipmentScene : Scene
{
    #region private classes
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

    private class ExitEvent : IEventHandler
    {
        private Action action;

        public ExitEvent(Action action)
        {
            this.action = action;
        }

        public void Invoke ()
        {
            action.Invoke();
        }
    }
    #endregion

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
        string[,] table = new string[equipmentList.Count(), 3];
        var options = new (string text, IEventHandler handler)?[equipmentList.Count + 3];

        for (int i = 0; i < equipmentList.Count; i++)
        {
            string strEquipted = EquipManager.IsEquiptedItem(equipmentList[i]) ? "[E]" : "";

            table[i, 0] = $"{strEquipted}{equipmentList[i].Name}";
            table[i, 1] = equipmentList[i].StatInfo;
            table[i, 2] = equipmentList[i].Desc;

            options[i] = (text: "", handler: new ToggleEvent(equipmentList[i]));
        }

        string[] paddedList = UIManager.CreatePaddedList(table);

        for (int i = 0; i < paddedList.Count(); i++)
            options[i] = (text: paddedList[i], handler: options[i]?.handler);

        options[options.Count() - 2] = (text: "----------------------------------------------------------------------------------------------", handler: null);
        options[options.Count() - 1] = (text: "나가기", handler: new ExitEvent(ExitScene));

        (int x, int y) point = Console.GetCursorPosition();
        DiaplaySelectionUI(options, point.x, point.y);
    }

    private void DiaplaySelectionUI ((string text, IEventHandler handler)?[] options, int x, int y)
    {
        bool looping = true;

        while (looping)
        {
            for (int i = 0; i < options.Count(); i++)
            {
                if (!options[i].HasValue)
                    continue;

                Console.SetCursorPosition(x, y + i);
                if (cursor == i)
                    Console.Write("▶");
                else
                    Console.Write(" ");

                Console.SetCursorPosition(x + 2, y + i);
                Console.Write(options[i].Value.text);
            }

            ConsoleKey key = Console.ReadKey(false).Key;
            switch (key)
            {
            case ConsoleKey.UpArrow:
                do
                {
                    cursor = (cursor - 1 + options.Length) % options.Length;
                } while (!options[cursor].HasValue || options[cursor].Value.handler == null);
                break;

            case ConsoleKey.DownArrow:
                do
                {
                    cursor = (cursor + 1) % options.Length;
                } while (!options[cursor].HasValue || options[cursor].Value.handler == null);
                break;

            case ConsoleKey.Enter:
                options[cursor]?.handler.Invoke();
                looping = false;
                break;
            }
        }
    }

    void ExitScene ()
    {
        NextScene = new InventoryScene();
    }
}
