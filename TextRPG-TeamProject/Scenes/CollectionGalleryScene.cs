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

    Point[] pos;
    int n;
    int cursor;

    public override void Start ()
    {
        //CollectionData.Cards[(int)CardType.Mountain].Found = true;
        //CollectionData.Cards[(int)CardType.Cherry].Found = true;
        //CollectionData.Cards[(int)CardType.Fairy].Found = true;
        //CollectionData.Cards[(int)CardType.CatInterruption].Found = true;

        n = CollectionData.Cards.Length;
        pos = new Point[n];
        pos[(int)CardType.Mountain] = new Point(20, 2);
        pos[(int)CardType.Cherry] = new Point(20, 0);
        pos[(int)CardType.Fairy] = new Point(20, 0);
        pos[(int)CardType.CatInterruption] = new Point(20, 0);
        pos[(int)CardType.Card5] = new Point(0, 0);
        pos[(int)CardType.Card6] = new Point(0, 0);
        cursor = 0;
    }

    public override void Update ()
    {
        Console.Clear();
        Console.WriteLine("[수집 카드 목록]");
        WriteCardList();
    }

    private void WriteCardList ()
    {
        Option[] options = new Option[n + 2];

        for (int i = 0; i < n; i++)
        {
            Card card = CollectionData.Cards[i];
            if (card.Found)
                options[i] = new Option(card.Name, new CardSelectionEvent(pos[i].x, pos[i].y, card.AsciiArt));
            else
                options[i] = new Option("???", new CardSelectionEvent(0, 0, null));
        }

        options[n + 1] = new Option("나가기", new NextSceneEvent(this, new StartScene()));

        (int x, int y) pt = Console.GetCursorPosition();
        cursor = UIManager.DisplaySelectionUI(options, pt.x, pt.y, cursor);
    }

    
}
