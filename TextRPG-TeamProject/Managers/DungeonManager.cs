using System;

class DungeonManager
{
    static public DungeonManager Instance = new DungeonManager();

    private BattleSystem battleSystem;
    private Spawner spawner;
    private Player player = GameData.Player;
    private DungeonScene dungeonScene;


    private readonly int[] goldReward = { 100, 200, 300, 400, 500 };
    private readonly Random random = new Random();


    private DungeonManager()
    {
        
    }

    public void Init(int mobNum, DungeonScene dungeonScene)
    {
        spawner = new Spawner();
        this.dungeonScene = dungeonScene;
        GameData.AliveMonster = spawner.GenerateMonstersByLevel(GameData.DungeonLv, mobNum);
        GameData.DeathMonster = new List<Monster>();
        battleSystem = new BattleSystem();
    }

    public void EnterDungeon()
    {
        AudioManager.PlayAudio("fight_bgm.mp3");

        battleSystem.OnLoseBattle += OnDungeonUncomplete;
        battleSystem.OnWinBattle += OnDungeonComplete;

        battleSystem.StartBattle();
    }


    private void OnDungeonUncomplete()
    {
        Console.Clear();
        string text = "플레이어사망";
        UIManager.AlignTextCenter(text);

        dungeonScene.PromptRestartOrExit();
    }


    private void DropLoot()
    {
        //전리품 획득 
        int goldLoot = goldReward[GameData.DungeonLv - 1];
        int prevPlayerGold = player.Gold;
        player.AddGold(goldLoot);
        UIManager.AlignTextCenter($"소지금:{prevPlayerGold} -> {player.Gold}");

        float itemDropChance = 0.3f;

        if (itemDropChance >= random.NextDouble())
        {
            UIManager.AlignTextCenter("아이템 획득!", 1);
        }
    }


    private void OnDungeonComplete()
    {
        battleSystem.OnWinBattle -= OnDungeonComplete;

        Console.Clear();
        int lineSpacing = -2;
        UIManager.AlignTextCenter($"Lv{GameData.DungeonLv}의 던전 클리어", lineSpacing);
        DropLoot();

        GameData.DungeonLv++;

        if (GameData.DungeonLv > goldReward.Length)
            GameData.DungeonLv = goldReward.Length;

        dungeonScene.PromptTryNextDungeon();
    }
}