using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

 

/*고민 몬스터 데이터를 gamedata에서 관리하도록 처리 ? */


class DungeonScene : Scene
{
    private BattleSystem battleSystem;        
    private Spawner spawner;                  
    private Player player = GameData.Player;
   


    private readonly int[] GOLD_REWARD = { 100, 200, 300, 400, 500 };
    private readonly Random RANDOM = new Random();
    private const float ESCAPE_CHANCE = 0.30f;



    private void Init()
    {
        spawner = new Spawner();
        GameData.AliveMonster = spawner.GetMobListByLevel(GameData.DungeonLv);
        GameData.DeathMonster = new List<Monster>();

        battleSystem = new BattleSystem();

    }


    public override void Start()
    {
        Console.Clear();
        Init();

        battleSystem.OnLoseBattle += OnDungeonUncomplete;
        battleSystem.OnWinBattle += OnDungeonComplete;
        DecideDungeonEntry();
    }


    private void DecideDungeonEntry()
    {
        string[] options = { "싸운다", "도망간다" };
        int selectNum = UIManager.DisplaySelectionUI(options);
        double chance = RANDOM.NextDouble();

        if (selectNum == 1)
            battleSystem.StartBattle();

        else if (selectNum == 2)
        {
            if (ESCAPE_CHANCE < chance)
            {
                NextScene = new StartScene();
            }

            else
            {
                Console.Clear();
                UIManager.AlignTextCenter("도망치는데 실패했다.");
                options = new string[] { "싸운다" };
                selectNum = UIManager.DisplaySelectionUI(options);
            }
        }
    }


    private void OnDungeonUncomplete()
    {
        Console.Clear();
        string text = "플레이어사망";
        UIManager.AlignTextCenter(text);

        string[] options = new string[] { "새로하기","종료" };
        int selectNum = UIManager.DisplaySelectionUI(options);


        if (selectNum == 1)
            NextScene = new NameInputScene();

        else if (selectNum == 2)
            Environment.Exit(0);




    }



    private void OnDungeonComplete()
    {
        battleSystem.OnWinBattle -= OnDungeonComplete;

        Console.Clear();
        int lineSpacing = -2;
        UIManager.AlignTextCenter($"Lv{GameData.DungeonLv}의 던전 클리어", lineSpacing);
        DropLoot();
       
        GameData.DungeonLv++;
       
        if(GameData.DungeonLv > GOLD_REWARD.Length)
            GameData.DungeonLv = GOLD_REWARD.Length;

        AskTryNextDungeon();

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



    private void AskTryNextDungeon()
    {
        string[] options = { "던전입장", "나가기" };
        int selectNum = UIManager.DisplaySelectionUI(options);
        Console.Clear();

        switch (selectNum)
        {
            case 1:
                NextScene = new DungeonScene();
                break;
            case 2:
                NextScene = new StartScene();
                break;
        }
    }
}




