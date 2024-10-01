using System;

class DungeonManager
{
    static public DungeonManager Instance
    {

        get
        {
            if (instance == null)
                instance = new DungeonManager();
           
            return instance;
        }

        private set 
        {
            instance = value;
        }
    }
    static private DungeonManager instance;

    private BattleSystem battleSystem;
    private Spawner spawner;
    private Player player = GameData.Player;
    private DungeonScene dungeonScene;


    private readonly int[] GOLD_REWARD = { 100, 200, 300, 400, 500 };
    private readonly Random RANDOM = new Random();
    private const float ESCAPE_CHANCE = 0.30f;



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
        int goldLoot = GOLD_REWARD[GameData.DungeonLv - 1];
        int prevPlayerGold = player.Gold;
        player.AddGold(goldLoot);
        UIManager.AlignTextCenter($"소지금:{prevPlayerGold} -> {player.Gold}");

        float itemDropChance = 0.3f;

        if (itemDropChance >= RANDOM.NextDouble())
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

        if (GameData.DungeonLv > GOLD_REWARD.Length)
            GameData.DungeonLv = GOLD_REWARD.Length;

        dungeonScene.PromptTryNextDungeon();
   
    }

}

