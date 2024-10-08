﻿using System;
using System.Numerics;

static class GameData
{
    static public Player Player = new Player();
    static public List<Monster> AliveMonster;
    static public List<Monster> DeathMonster;
    static public int DungeonLv;
    static public int HuntedMonster;

    public static void InitDatas()
    {
        DungeonLv = 1;
        HuntedMonster = 0;
        Console.CursorVisible = false;
        // Init for Restart
        Inventory.ItemList.Clear();
        QuestManager.QuestList.Clear();
        ShopData.ItemList.Clear();
        Player.Init();
        QuestManager.Init();
        // Test
        //GameData.Player.AddGold(20000);
        Inventory.ItemList.Add(ItemManager.Instantiate(ItemId.TraineesArmor));
        //Inventory.ItemList.Add(ItemManager.Instantiate(ItemId.IronArmor));
        //Inventory.ItemList.Add(ItemManager.Instantiate(ItemId.SpartanArmor));
        Inventory.ItemList.Add(ItemManager.Instantiate(ItemId.WornSword));
        //Inventory.ItemList.Add(ItemManager.Instantiate(ItemId.BronzeAxe));
        //Inventory.ItemList.Add(ItemManager.Instantiate(ItemId.SpartanSpear));
        Inventory.ItemList.Add(ItemManager.Instantiate(ItemId.HpPotion));
        Inventory.ItemList.Add(ItemManager.Instantiate(ItemId.MpPotion));

        //ShopData.ItemList.Add(ItemManager.Instantiate(ItemId.TraineesArmor));
        ShopData.ItemList.Add(ItemManager.Instantiate(ItemId.IronArmor));
        ShopData.ItemList.Add(ItemManager.Instantiate(ItemId.SpartanArmor));
        //ShopData.ItemList.Add(ItemManager.Instantiate(ItemId.WornSword));
        ShopData.ItemList.Add(ItemManager.Instantiate(ItemId.BronzeAxe));
        ShopData.ItemList.Add(ItemManager.Instantiate(ItemId.SpartanSpear));
        ShopData.ItemList.Add(ItemManager.Instantiate(ItemId.HpPotion));
        ShopData.ItemList.Add(ItemManager.Instantiate(ItemId.HpPotion));
        ShopData.ItemList.Add(ItemManager.Instantiate(ItemId.HpPotion));
        ShopData.ItemList.Add(ItemManager.Instantiate(ItemId.HpPotion));
        ShopData.ItemList.Add(ItemManager.Instantiate(ItemId.MpPotion));
        ShopData.ItemList.Add(ItemManager.Instantiate(ItemId.MpPotion));
        ShopData.ItemList.Add(ItemManager.Instantiate(ItemId.MpPotion));
        ShopData.ItemList.Add(ItemManager.Instantiate(ItemId.MpPotion));
    }
}