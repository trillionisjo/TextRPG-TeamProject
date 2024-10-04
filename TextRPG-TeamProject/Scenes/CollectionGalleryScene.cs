using System;

class CollectionGalleryScene : Scene
{
    private class CardSelectionEvent : IEventHandler
    {
        int x, y;
        string[] asciiArt;
        public CardSelectionEvent(int x, int y, string[] asciiArt)
        {
            this.x = x;
            this.y = y;
            this.asciiArt = asciiArt;
        }
        public void Invoke ()
        {
            if (asciiArt == null)
                return;

            AsciiArts.Draw(asciiArt, x, y);
            Console.ReadKey(true);
        }
    }

    private struct Option
    {
        public string text;
        public Action <int, int, Card> draw;
    }

    int n;
    Point[] drawPos;
    string[] cardNameList;

    int cursor;

    public override void Start ()
    {
        CollectionData.Cards[(int)CardType.Mountain].Found = true;
        CollectionData.Cards[(int)CardType.Cherry].Found = true;
        CollectionData.Cards[(int)CardType.Fairy].Found = true;
        CollectionData.Cards[(int)CardType.CatInterruption].Found = true;

        n = CollectionData.Cards.Length;
        cardNameList = new string[n];

        for (int i = 0; i < n; i++)
        {
            Card card = CollectionData.Cards[i];
            if (card.Found)
                cardNameList[i] = card.Name;
            else
                cardNameList[i] = "???";
        }

        drawPos = new Point[n];
        drawPos[(int)CardType.Mountain] = new Point(30, 2);
        drawPos[(int)CardType.Cherry] = new Point(30, 4);
        drawPos[(int)CardType.Fairy] = new Point(30, 4);
        drawPos[(int)CardType.CatInterruption] = new Point(30, 0);
        drawPos[(int)CardType.Card5] = new Point(0, 0);
        drawPos[(int)CardType.Card6] = new Point(0, 0);

        cursor = 0;

        Console.Clear();
    }

    public override void Update ()
    {
        Console.SetCursorPosition(0, 0);
        Console.WriteLine("[수집 카드 목록]");
        WriteCardList();
        DrawArt();
        HandleKeyInput();
        Thread.Sleep(10);
    }

    private void WriteCardList ()
    {
        for (int i = 0; i < n; i++)
        {
            string str;
            if (cursor == i)
                str = $">> {cardNameList[i]}";
            else
                str = $"   {cardNameList[i]}";
            Console.WriteLine(str);
        }
    }

    private void HandleKeyInput()
    {
        if (!Console.KeyAvailable)
            return;

        var key = Console.ReadKey(true).Key;

        switch (key)
        {
        case ConsoleKey.LeftArrow:
        case ConsoleKey.UpArrow:
            ClearDrawingArea();
            cursor = (cursor - 1 + n) % n;
            break;

        case ConsoleKey.RightArrow:
        case ConsoleKey.DownArrow:
            ClearDrawingArea();
            cursor = (cursor + 1) % n;
            break;

        case ConsoleKey.Escape:
        case ConsoleKey.Backspace:
            NextScene = new StartScene();
            break;
        }
    }

    private void DrawArt()
    {
        Card card = CollectionData.Cards[cursor];
        if (!card.Found)
            return;

        AsciiArts.Draw(card.AsciiArt, drawPos[cursor].x, drawPos[cursor].y);
    }

    private void ClearDrawingArea()
    {
        int startX = 20;
        for (int y = 0; y < 30; y++)
        {
            for (int x = startX; x < 120; x++)
            {
                Console.SetCursorPosition(x, y);
                Console.Write(" ");
            }
        }
    }
}
