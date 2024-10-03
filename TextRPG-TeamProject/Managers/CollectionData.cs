using System;

enum CardType
{
    Mountain,
    Cherry,
    Fairy,
    CatInterruption,
    Card5,
    Card6,
}

class Card
{
    public bool Found;
    public string Name;
    public string[] AsciiArt;

    public Card(bool found, string name, string[] asciiArt)
    {
        Found = found;
        Name = name;
        AsciiArt = asciiArt;
    }
}


static class CollectionData
{
    public static Card[] Cards;

    static CollectionData()
    {
        Cards = new Card[Enum.GetValues<CardType>().Length];
        Cards[(int)CardType.Mountain] = new Card(false, "록키 산맥 카드", AsciiArts.MonkeyMountain);
        Cards[(int)CardType.Cherry] = new Card(false, "둥근 체리 카드", AsciiArts.Cherry);
        Cards[(int)CardType.Fairy] = new Card(false, "퇴실 요정 카드", AsciiArts.Fairy);
        Cards[(int)CardType.CatInterruption] = new Card(false, "냥이 방해 카드", AsciiArts.CatInterruption);
        Cards[(int)CardType.Card5] = new Card(false, "카드5", AsciiArts.EmptyCard);
        Cards[(int)CardType.Card6] = new Card(false, "카드6", AsciiArts.EmptyCard);
    }

    public static Card GetCard(CardType type)
    {
        return Cards[(int)type];
    }
}
