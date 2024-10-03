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
            ShopData.Sell(index);
        }
    }

    int cursor = 0;

    public override void Update ()
    {
        Console.Clear();
        UIManager.TitleBox("상점 - 아이템 판매");
        Console.WriteLine("필요 없는 아이템을 판매하고 골드를 모아보세요! 쓸모 없는 물건도 골드가 될 수 있답니다.");
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
