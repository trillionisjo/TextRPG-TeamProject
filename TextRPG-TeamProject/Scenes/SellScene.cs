using System;

class SellScene : Scene
{
    private class SellEvent : IEventHandler
    {
        private int index;

        public SellEvent (int index)
        {
            this.index = index;
        }

        public void Invoke ()
        {
            IItem item = Inventory.ItemList[index];

            if (item is IEquipable equipable)
                EquipManager.UnequipItem(equipable.Slot);

            int gold = (int)(item.Price * ShopData.Rate);
            GameData.Player.AddGold(gold);
            ShopData.ItemList.Add(Inventory.ItemList[index]);
            Inventory.ItemList.RemoveAt(index);

            AudioManager.PlayOntShot("sfx-sell-item.mp3");
        }
    }

    int cursor = 0;

    public override void Update ()
    {
        Console.Clear();
        Console.WriteLine("상점 - 아이템 판매");
        Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
        Console.WriteLine();

        Console.WriteLine("[보유 골드]");
        Console.WriteLine($"{GameData.Player.Gold} G");
        Console.WriteLine();

        Console.WriteLine("[아이템 목록]");
        WriteItemList();
    }

    private void WriteItemList ()
    {
        int n = Inventory.ItemList.Count;
        string[,] table = new string[n, 4];
        var options = new Option[n + 2];

        for (int i = 0; i < Inventory.ItemList.Count; i++)
        {
            IItem item = Inventory.ItemList[i];
            string strEquipted = (item is IEquipable equipable && EquipManager.IsEquiptedItem(equipable) ? "[E]" : "");

            table[i, 0] = $"{strEquipted}{item.Name}";
            table[i, 1] = item.StatInfo;
            table[i, 2] = item.Desc;
            table[i, 3] = $"{(int)(item.Price * ShopData.Rate)} G";

            options[i] = new Option("", new SellEvent(i));
        }

        string[] paddedList = UIManager.CreatePaddedList(table);

        for (int i = 0; i < paddedList.Count(); i++)
            options[i].Text = paddedList[i];

        //options[n + 1] = new Option(UIManager.GetLineString(), null);
        options[n + 1] = new Option("나가기", new NextSceneEvent(this, new ShopScene()));

        (int x, int y) point = Console.GetCursorPosition();
        cursor = UIManager.DisplaySelectionUI(options, point.x, point.y, cursor);
    }
}
