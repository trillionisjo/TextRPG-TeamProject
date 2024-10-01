using System;


/*고민 몬스터 데이터를 gamedata에서 관리하도록 처리 ? */


class DungeonScene : Scene
{
    private Spawner spawner;
    private Player player = GameData.Player;

    private readonly Random RANDOM = new Random();
    private const float ESCAPE_CHANCE = 0.30f;

    private int monsterNum;



    public override void Start()
    {
        Init();
    }


    public void Init()
    {
        Console.Clear();
        monsterNum = RANDOM.Next(1, 4);
        AudioManager.PlayAudio("dungeon_bgm.mp3");
        DungeonManager.Instance.Init(monsterNum, this);
        DecideDungeonEntry();
    }


    private void DecideDungeonEntry()
    {
        UIManager.TitleBox($"던전LV:{GameData.DungeonLv}");
        Console.ForegroundColor = ConsoleColor.Red;
        UIManager.AlignTextCenter($"{monsterNum}마리의 적이 느껴집니다...",-2);
        Console.ForegroundColor = ConsoleColor.White;

        string[] options = { "싸운다", "도망간다" };
        int selectNum = UIManager.DisplaySelectionUI(options);
        double chance = RANDOM.NextDouble();

        if (selectNum == 1)
            DungeonManager.Instance.EnterDungeon();


        else if (selectNum == 2)
        {
            if (ESCAPE_CHANCE < chance)
            {
                NextScene = new StartScene();
            }

            else
            {
                Console.Clear();
                int damage = (int)(player.HP * 0.1f);
                player.OnDamaged(damage);
                string[] texts = { "도망치는데 실패했다...", $"{damage}의 데미지를 입었다." };
                UIManager.AlignTextCenter(texts, -2);
                options = new string[] { "싸운다" };
                selectNum = UIManager.DisplaySelectionUI(options);
                DungeonManager.Instance.EnterDungeon();
            }
        }
    }



    public void PromptRestartOrExit()
    {
        string[] options = { "재시작", "게임종료" };
        int selectNum = UIManager.DisplaySelectionUI(options);
        Console.Clear();

        switch (selectNum)
        {
            case 1:
                NextScene = new NameInputScene();
                break;
            case 2:
                Environment.Exit(0);
                break;
        }
    }


    public void PromptTryNextDungeon()
    {
        AudioManager.PlayAudio("dungeon_bgm.mp3");
        string[] options = { "다음 던전", "나가기" };
        int selectNum = UIManager.DisplaySelectionUI(options);
        Console.Clear();

        switch (selectNum)
        {
            case 1:
                Init();
                break;
            case 2:
                NextScene = new StartScene();
                break;
        }
    }

}




