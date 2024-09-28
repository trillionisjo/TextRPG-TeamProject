using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
namespace TextRPG_TeamProject.Scenes;

/*
1. 배틀씬 함수들 BattleManager로 옮겨서 관리하기 
//-=-----------------------
2. 몬스터 이름 배열 선언하고 가져다 쓰기 중복 상관(x)
3. 죽은 몬스터와 살아있는 몬스터 층 분리 , 색변경 
 */

/// <summary>
/// 1. 던전 입장시 플레이어 레벨에 따라 난이도 설정
///2. 몬스터를 던전 난이도에 따라 소환.소환은 스포너 클래스를 통해 진행
///스포너 클래스 => 소환 관리
/// 2.플레이어는 던전을 확인하고 도망 혹은 전투를 실행 할 수 있음. 
/// 도망은 30% 확률로 실패 할 수 있음.
/// 
///3. 전투 페이즈
/// 배틀 클래스를 통해 관련 메서드 관리 주요 기능 (전투(플레이어턴, 몬스터턴), 결과 텍스트 , 공격 판정, )
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

    private int diedMonserNum = 0;
    private BattleSystem battleSystem;
    private Spawner spawner;
    private float escapeChance = 0.30f;
    private Random random = new Random();
    private bool isPlayerTurn = true;
    private Player player = GameData.Player;

    public int dungeonLevel = 1;

    private void Init()
    {
        //spawner 초기화 
        spawner = new Spawner();
        GameData.AliveMonster = spawner.GetMobListByLevel(dungeonLevel);
        GameData.DeathMonster = new Monster[GameData.AliveMonster.Count];

        //배틀 매니저 초기화 
        battleSystem = new BattleSystem();
    }



    public override void Start()
    {
        Console.Clear();

        Init();

        //던전 정보를 추리할 수 있게 

        string[] options = { "싸운다", "도망간다" };
        int selectNum = UIManager.DisplaySelectionUI(options);
        double chance = random.NextDouble();


        if (selectNum == 2)
        {
            if (escapeChance < chance)
                NextScene = new StartScene();

            else
            {
                Console.WriteLine();
                UIManager.AlignTextCenter("도망치는데 실패했다.");
                options = new string[] { "싸운다" };
                selectNum = UIManager.DisplaySelectionUI(options);
            }

        }


    }


    public override void Update()
    {
        HandlePlayerDeath();
        HandleMonsterDeath();


        if (isPlayerTurn)       
            battleSystem.ProcessPlayerTurn();
        
        else
             battleSystem.ProcessMonsterTurn();

        isPlayerTurn = !isPlayerTurn;
    }


    public void HandlePlayerDeath()
    {
        if (player.IsDead)
        {
            Console.WriteLine("플레이어가 죽었습니다");
            Environment.Exit(0);
            
        }
    }

    public void HandleMonsterDeath()
    {
        for (int i = 0; i < GameData.AliveMonster.Count; i++)
        {
            if (GameData.AliveMonster[i].IsDead)
            {
                GameData.DeathMonster[diedMonserNum] = GameData.AliveMonster[i];
                GameData.AliveMonster.Remove(GameData.AliveMonster[i]);
                diedMonserNum++;
            }
        }

        if (diedMonserNum == GameData.DeathMonster.Length)
        {
            Console.WriteLine("던전 클리어");
            Environment.Exit(0);
        }
    }



}




