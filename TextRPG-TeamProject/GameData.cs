using System;
using System.Numerics;

static class GameData
{
    static public Player Player = new Player();
    static public List<Monster> AliveMonster;
    static public Monster[] DeathMonster;
    static public int DungeonLv = 0;

    public static void InitDatas()
    {
        if(DungeonLv == 0)
            { DungeonLv = 1; }

        // Test
        Inventory.ItemList.Add(ItemManager.Instantiate(ItemId.TraineesArmor));
        Inventory.ItemList.Add(ItemManager.Instantiate(ItemId.IronArmor));
        Inventory.ItemList.Add(ItemManager.Instantiate(ItemId.SpartanArmor));
        Inventory.ItemList.Add(ItemManager.Instantiate(ItemId.WornSword));
        Inventory.ItemList.Add(ItemManager.Instantiate(ItemId.BronzeAxe));
        Inventory.ItemList.Add(ItemManager.Instantiate(ItemId.SpartanSpear));
        Inventory.ItemList.Add(ItemManager.Instantiate(ItemId.HPPotion));
        Inventory.ItemList.Add(ItemManager.Instantiate(ItemId.MPPotion));
    }
    
}
