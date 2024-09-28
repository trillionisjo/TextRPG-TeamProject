using System;

static class GameData
{

    static public Player Player = new Player(PlayerType.Mage);
    static public List<Monster> AliveMonster;
    static public Monster [] DeathMonster;
    
    public  static void InitDatas()
    {
        Player.Name = "김말";
    }

    public static Armor TraineesArmor { get; }
    public static Armor IronArmor { get; }
    public static Armor SpartanArmor { get; }
}
