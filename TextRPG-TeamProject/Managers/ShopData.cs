using System;

static class ShopData
{
    public static float Rate => 0.85f;

    public static List<IItem> ItemList { get; set; }

    static ShopData()
    {
        ItemList = new List<IItem>();
    }
}
