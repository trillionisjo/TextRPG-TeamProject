using System;

static class GameData
{

    static public Player player = new Knight();
    
  public  static void InitDatas()
    {
        player.Name = "김말";
    }

    static void InitDatas(Player player)
    {
    }


}
