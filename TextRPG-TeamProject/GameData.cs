using System;

static class GameData
{
    public static Player Player = new Knight();

    static public Player player = new Knight();
    
  public  static void InitDatas()
    {
        player.Name = "김말";
    }

    public static Armor TraineesArmor { get; }
    public static Armor IronArmor { get; }
    public static Armor SpartanArmor { get; }

    public static void InitDatas()
    {
    }
}
