using System;

static class GameData
{
    public static Player Player = new Knight();

    static public Player player = new Knight();
    
  public  static void InitDatas()
    {
        player.Name = "김말";
    }

    static void InitDatas(Player player)
    {
    }


}
