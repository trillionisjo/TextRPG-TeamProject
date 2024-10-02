using System;
using System.Drawing;

class PurchaseScene : Scene
{
    private class PurchaseEvent : IEventHandler
    {
        private int index;
        private Action notEnoughGold;

        public PurchaseEvent(int index, Action notEnoughGold)
        {
            this.index = index;
            this.notEnoughGold = notEnoughGold;
        }
        
        public void Invoke ()
        {
            if (GameData.Player.Gold < ShopData.ItemList[index].Price)
            {
                notEnoughGold?.Invoke();
                return;
            }

            ShopData.Purchase(index);
        }
    }

    private int cursor = 0;
    private int x, y;

    public override void Update ()
    {
        Console.Clear();
        Console.WriteLine("상점 - 아이템 구매");
        Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
        Console.WriteLine();

        Console.WriteLine("[보유 골드]");
        Console.WriteLine($"{GameData.Player.Gold} G");
        Console.WriteLine();

        Console.WriteLine("[아이템 목록]");
        DisplaySelectionList();
    }

    private void DisplaySelectionList()
    {
        int n = ShopData.ItemList.Count;
        string[,] table  = new string[n, 4];
        Option[] options = new Option[n + 2];

        for (int i = 0; i < n; i++)
        {
            IItem item = ShopData.ItemList[i];
            table[i, 0] = $"{item.Name}";
            table[i, 1] = $"{item.StatInfo}";
            table[i, 2] = $"{item.Desc}";
            table[i, 3] = $"{item.Price} G";

            options[i] = new Option(null, new PurchaseEvent(i, NotEnoughGold));
        }

        string[] paddedList = UIManager.CreatePaddedList(table);
        for (int i = 0; i < n; i++)
            options[i].Text = paddedList[i];

        options[n + 1] = new Option("나가기", new NextSceneEvent(this, new ShopScene()));

        (int x, int y) point = Console.GetCursorPosition();
        x = 30;
        y = point.y + n + 3;
        cursor = UIManager.DisplaySelectionUI(options, point.x, point.y, cursor);
    }

    private void NotEnoughGold ()
    {
        UIManager.PrintTextAtPosition("금액이 부족합니다!", x, y);
        Console.ReadKey(true);
    }
}
