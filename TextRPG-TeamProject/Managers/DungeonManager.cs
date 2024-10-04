using System;

class DungeonManager
{
    private static DungeonManager instance;

    public static DungeonManager Instance
    {
        get
        {
            if (instance == null)
                instance = new DungeonManager();
            return instance;
        }
    }
    
    public event Action OnKillMonster;
    public static BattleSystem BattleSystem = new BattleSystem();
    private Spawner spawner;
    private Player player = GameData.Player;
    private DungeonScene dungeonScene;
   

    private readonly int[] goldReward = { 100, 200, 300, 400, 500 };
    private readonly Random random = new Random();


    private DungeonManager()
    {
    }


    public void NotifyKill()
    {
        OnKillMonster?.Invoke();
    }


    public void Init(int mobNum, DungeonScene dungeonScene)
    {
        spawner = new Spawner();
        this.dungeonScene = dungeonScene;
        GameData.AliveMonster = spawner.GenerateMonstersByLevel(GameData.DungeonLv, mobNum);
        GameData.DeathMonster = new List<Monster>();
    }

    public void EnterDungeon()
    {
        AudioManager.PlayAudio("fight_bgm.mp3");
        
        BattleSystem.OnLoseBattle -= OnDungeonUncomplete;
        BattleSystem.OnLoseBattle += OnDungeonUncomplete;
        BattleSystem.OnWinBattle -= OnDungeonComplete;
        BattleSystem.OnWinBattle += OnDungeonComplete;
        BattleSystem.Init();
        BattleSystem.StartBattle();
    }


    private void OnDungeonUncomplete()
    {
        Console.Clear();
        AudioManager. PlayOntShot("playerDie.mp3");
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
        Console.Clear();
        int lineSpacing = -2;
        UIManager.AlignTextCenter($"Lv{GameData.DungeonLv}의 던전 클리어", lineSpacing);
        DropLoot();

        GameData.DungeonLv++;

        if (GameData.DungeonLv > goldReward.Length)
            GameData.DungeonLv = goldReward.Length;

        dungeonScene.PromptTryNextDungeon();
    }

    public void PrintRandomEscapeFailMessage(int damage)
    {
        string[] texts =
        {
            $"도망치는데 실패했습니다...! {damage}의 데미지를 입고 전투가 계속됩니다.",
            $"도망치려 했으나 발이 엉켜버렸습니다...!, 그 틈을 타 {damage}의 데미지를 입었습니다." ,
            $"도망치려 했지만, 적이 순식간에 뒤쫓아왔습니다!, {damage}의 데미지를 입고 말았습니다." 
        };

       Console.WriteLine(texts[random.Next(0,3)]); 
    }



}