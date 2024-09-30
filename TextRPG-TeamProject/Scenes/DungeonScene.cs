using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
namespace TextRPG_TeamProject.Scenes;

/// <summary>
/// 1. 던전 입장시 플레이어 레벨에 따라 난이도 설정
///2. 몬스터를 던전 난이도에 따라 소환.소환은 스포너 클래스를 통해 진행
///스포너 클래스 => 소환 관리
/// 2.플레이어는 던전을 확인하고 도망 혹은 전투를 실행 할 수 있음. 
/// 도망은 30% 확률로 실패 할 수 있음.
/// 
///3. 전투 페이즈
/// 배틀시스템 클래스를 통해 관련 메서드 관리 주요 기능 (전투(플레이어턴, 몬스터턴)  , 공격 판정 )
/// 베틀유아이매니저를 통해 배틀UI따로 관리
///3-1 플레이어 행동 선택  일반 공격. 스킬 사용 
///3-2 공격 판정 -> 회피(스킬 적용 x) -> 치명타 ->  정타
///3-3 몬스터턴(살아있는 몬스터가 모두 플레이어를 공격 )
///플레이어와 동일하게 진행 
/// 4. 결과 
/// 4-1 플레이어 승리시 
/// 승리 문구 출력 
/// 잡은 몬스터 수 * 던전 레벨에 따른 기본 보상 * 몬스터 평균 레벨 
/// 4-2 플레이어 패배시
/// 패배 문구 출력 후 게임 종료 => 플레이어 사망 처리(저장 기능 있을 시 플레이어 데이터를 초기화 해줘야함.)
/// </summary>


/*고민 몬스터 데이터를 gamedata에서 관리하도록 처리 ? */


class DungeonScene : Scene
{
    private BattleSystem battleSystem;        
    private Spawner spawner;                  
    private Player player = GameData.Player;   

    private float escapeChance = 0.30f;            
    private bool isPlayerTurn = true;               
    private bool isEscape = false;
    private Random random = new Random();


    private int[] goldRoot = { 100, 200, 300, 400, 500 };

    private void Init()
    {
        spawner = new Spawner();
        GameData.AliveMonster = spawner.GetMobListByLevel(GameData.DungeonLv);
        GameData.DeathMonster = new Monster[GameData.AliveMonster.Count];

        battleSystem = new BattleSystem();

        isPlayerTurn = true;
        isEscape = false;
    }


    public override void Start()
    {
        Console.Clear();
        Init();

        string[] options = { "싸운다", "도망간다" };
        int selectNum = UIManager.DisplaySelectionUI(options);
        double chance = random.NextDouble();

        if (selectNum == 2)
        {
            if (escapeChance < chance)
            {
                isEscape = true;
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


    public override void Update()
    {

        if (isEscape)
            NextScene = new StartScene();

        else
        {
            ProcessTurn();
            CheckBattleEnd();
        }
    }


    private void ProcessTurn()
    {
        if (isPlayerTurn)
            battleSystem.ProcessPlayerTurn();

        else
            battleSystem.ProcessMonsterTurn();

        isPlayerTurn = !isPlayerTurn;
    }


    private void NextDungeon() 
    {
        Console.Clear();
        Init();

        string[] options = { "싸운다", "도망간다" };
        int selectNum = UIManager.DisplaySelectionUI(options);
        double chance = random.NextDouble();

        if (selectNum == 2)
        {
            if (escapeChance < chance)
            {
                isEscape = true;
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




    private void DropLoot()
    {
        //전리품 획득 
        int goldLoot = goldRoot[GameData.DungeonLv - 1] * GameData.DeathMonster.Length;
        int prevPlayerGold = player.Gold;
        player.AddGold(goldLoot);
        UIManager.AlignTextCenter($"소지금:{prevPlayerGold} -> {player.Gold}");

        float itemDropChance = 0.3f;

        if (itemDropChance >= random.NextDouble())
        {
            UIManager.AlignTextCenter("아이템 획득!",1);
        }
    }



    private void CheckBattleEnd()
    {

        if (player.IsDead)
        {
            Console.WriteLine("플레이어가 죽었습니다");
            Environment.Exit(0);
        }

        if (GameData.AliveMonster.Count == 0)
        {
            Console.Clear();
            int lineSpacing = -2;
            UIManager.AlignTextCenter($"Lv{GameData.DungeonLv}의 던전 클리어", lineSpacing);
            DropLoot();
            GameData.DungeonLv++;
         
            string[] options = { "던전입장","나가기"};
            int selectNum = UIManager.DisplaySelectionUI(options);
            Console.Clear();
            switch (selectNum)
            {
                case 1:
                    NextDungeon();
                    break;
                case 2:
                    isEscape = true;
                    break;
            }
        }
    }

}




