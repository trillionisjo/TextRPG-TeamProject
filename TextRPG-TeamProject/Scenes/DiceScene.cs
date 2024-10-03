using System;

class DiceScene : Scene
{
    public bool diceCheck;
    public override void Start()
    {
        Console.Clear();
    }

    public override void Update()
    {
        Dice();
    }
    public void Dice()
    {
        string betAmountstring = null;
        diceCheck = true;
        int numChk;

        // 게임 반복
        while (diceCheck)
        {
            Console.Clear();
            Console.WriteLine("소지금: " + GameData.Player.Gold + " 골드");
            Console.Write("베팅할 금액을 입력하세요 (최대 10000 골드): ");

            
            betAmountstring = Console.ReadLine();
            if (betAmountstring == "") 
            {
                Console.WriteLine("ㅇㅇ");
                return;
            }
            bool isNum = int.TryParse(betAmountstring, out numChk);
            if (!isNum)
            {
                Console.WriteLine("숫자가 아닌것을 입력");
                return;
            }
            int betAmount = int.Parse(betAmountstring);

            if (betAmount > GameData.Player.Gold || betAmount > 10000)
            {
                Console.WriteLine("잘못된 베팅 금액입니다. 다시 입력하세요.");
                continue;
            }
            Console.Clear() ;
            Console.WriteLine($"배팅금액 : {betAmount}");
            // 플레이어 및 AI 주사위 굴리기
            Console.WriteLine("\n--- 플레이어 차례 ---");
            int[] playerDice = DiceManager.PlayTurn();
            Console.WriteLine("\n--- AI1 차례 ---");
            int[] ai1Dice = DiceManager.PlayAITurn();
            Console.WriteLine("\n--- AI2 차례 ---");
            int[] ai2Dice = DiceManager.PlayAITurn();

            // 승리자 결정
            int playerRank = DiceManager.GetRank(playerDice);
            int ai1Rank = DiceManager.GetRank(ai1Dice);
            int ai2Rank = DiceManager.GetRank(ai2Dice);

            Console.WriteLine("\n--- 결과 ---");
            Console.WriteLine("플레이어: " + DiceManager.GetRankName(playerRank));
            Console.WriteLine("AI1: " + DiceManager.GetRankName(ai1Rank));
            Console.WriteLine("AI2: " + DiceManager.GetRankName(ai2Rank));

            int winner = DiceManager.DetermineWinner(playerDice, ai1Dice, ai2Dice);

            // 결과에 따른 금액 처리
            if (winner == 0)
            {
                Console.WriteLine("플레이어가 이겼습니다!");
                DiceManager.Bet(betAmount, true);
            }
            else
            {
                Console.WriteLine($"AI{winner}이 이겼습니다.");
                DiceManager.Bet(betAmount, false);
            }

            if (GameData.Player.Gold <= 0)
            {
                Console.WriteLine("더 이상 소지금이 없습니다. 게임 종료.");
                Thread.Sleep(3000);
                NextScene = new PubScene();
                break;
            }

            Console.WriteLine("다시 하시겠습니까?");

            HandleInput();
        }
        void HandleInput()
        {
            string[] option = { "한판 더!", "이제 그만 할 때가 됫군" };
            int number = UIManager.DisplaySelectionUI(option, 4);

            switch (number)
            {
                case 1:
                    break;

                case 2:
                    diceCheck=false;
                    NextScene = new PubScene();
                    break;


                default:
                    break;
            }
        }
    }

}