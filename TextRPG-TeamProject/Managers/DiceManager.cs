using System;
static class DiceManager
{
    static Random random = new Random();


    // 턴 진행 (플레이어)
    public static int[] PlayTurn()
    {
        int[] dice = new int[5];
        bool[] holdFlags = new bool[5];

        RollDice(dice);
        DisplayDice(dice, holdFlags); // 고정되지 않은 상태로 표시

        for (int i = 0; i < 2; i++) // 2번 굴리기
        {
            Console.Write("고정할 주사위 번호를 선택하세요 (1~5, 쉼표로 구분, 아무것도 입력하지 않으면 고정 안함): ");
            string input = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(input)) // 입력이 있으면 주사위 고정
            {
                string[] inputs = input.Split(',');
                foreach (var s in inputs)
                {
                    int index = int.Parse(s.Trim()) - 1;
                    if (index >= 0 && index < 5)
                        holdFlags[index] = true;
                }
            }

            HoldDice(dice, holdFlags); // 고정된 주사위는 그대로, 나머지는 다시 굴림
            DisplayDice(dice, holdFlags); // 고정된 주사위는 색상을 변경하여 표시
        }

        return dice;
    }

    // 턴 진행 (AI)
    public static int[] PlayAITurn()
    {

        int[] dice = new int[5];
        bool[] holdFlags = new bool[5];

        RollDice(dice);
        DisplayDice(dice, holdFlags);

        for (int i = 0; i < 2; i++) // 2번 굴리기
        {
            holdFlags = AIIntelligentHoldDice(dice); // AI가 지능적으로 주사위 고정
            HoldDice(dice, holdFlags);
            DisplayDice(dice, holdFlags);
        }

        return dice;

    }

    // 주사위 굴리기
    static void RollDice(int[] dice)
    {
        for (int i = 0; i < dice.Length; i++)
        {
            dice[i] = random.Next(1, 7);
        }
    }

    // 고정되지 않은 주사위 굴리기
    static void HoldDice(int[] dice, bool[] holdFlags)
    {
        for (int i = 0; i < dice.Length; i++)
        {
            if (!holdFlags[i])
            {
                dice[i] = random.Next(1, 7);
            }
        }
    }

    // AI 지능적인 주사위 고정 로직
    static bool[] AIIntelligentHoldDice(int[] dice)
    {
        Dictionary<int, int> count = CountDice(dice);
        int maxCount = count.Values.Max(); // 가장 많이 나온 숫자의 개수
        int targetNumber = count.FirstOrDefault(x => x.Value == maxCount).Key; // 그 숫자

        bool[] holdFlags = new bool[5];

        // 가장 많이 나온 숫자를 우선적으로 고정
        for (int i = 0; i < dice.Length; i++)
        {
            if (dice[i] == targetNumber)
            {
                holdFlags[i] = true;
            }
        }

        return holdFlags;
    }

    // 주사위 표시 (색상 변경 포함)
    static void DisplayDice(int[] dice, bool[] holdFlags)
    {
        for (int i = 0; i < dice.Length; i++)
        {
            if (holdFlags[i])
            {
                Console.ForegroundColor = ConsoleColor.Green; // 고정된 주사위는 초록색으로 표시
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.White; // 고정되지 않은 주사위는 기본 색상
            }

            Console.Write("[" + dice[i] + "] ");
        }

        Console.ForegroundColor = ConsoleColor.White; // 색상 초기화
        Console.WriteLine();
    }

    // 승리 조건 분석
    public static int GetRank(int[] dice)
    {
        var count = CountDice(dice);
        var counts = count.Values.OrderByDescending(v => v).ToList();

        if (counts[0] == 5) return 1; // FIVE CARD
        if (counts[0] == 4) return 2; // FOUR CARD
        if (counts[0] == 3 && counts[1] == 2) return 3; // FULL HOUSE
        if (counts[0] == 3) return 4; // TRIPLE
        if (counts[0] == 2 && counts[1] == 2) return 5; // TWO PAIR
        if (counts[0] == 2) return 6; // ONE PAIR
        return 7; // TOP
    }

    // 주사위 숫자 카운팅
    static Dictionary<int, int> CountDice(int[] dice)
    {
        Dictionary<int, int> count = new Dictionary<int, int>();
        for (int i = 0; i < dice.Length; i++)
        {
            if (count.ContainsKey(dice[i]))
                count[dice[i]]++;
            else
                count[dice[i]] = 1;
        }
        return count;
    }

    // 랭크 이름 출력
    public static string GetRankName(int rank)
    {
        switch (rank)
        {
            case 1: return "FIVE CARD";
            case 2: return "FOUR CARD";
            case 3: return "FULL HOUSE";
            case 4: return "TRIPLE";
            case 5: return "TWO PAIR";
            case 6: return "ONE PAIR";
            default: return "TOP";
        }
    }

    // 승리자 결정
    public static int DetermineWinner(int[] playerDice, int[] ai1Dice, int[] ai2Dice)
    {
        int playerRank = GetRank(playerDice);
        int ai1Rank = GetRank(ai1Dice);
        int ai2Rank = GetRank(ai2Dice);

        // 우선, 등급 순으로 비교 (등급 숫자가 낮을수록 높은 순위)
        if (playerRank < ai1Rank && playerRank < ai2Rank)
        {
            return 0; // 플레이어 승
        }
        else if (ai1Rank < playerRank && ai1Rank < ai2Rank)
        {
            return 1; // AI1 승
        }
        else if (ai2Rank < playerRank && ai2Rank < ai1Rank)
        {
            return 2; // AI2 승
        }

        // 등급이 동일한 경우
        if (playerRank == ai1Rank && playerRank == ai2Rank)
        {
            // 플레이어, AI1, AI2가 모두 같은 등급일 경우
            return CompareHighestDice(playerDice, ai1Dice, ai2Dice); // 세 명의 주사위 숫자 비교
        }
        else if (playerRank == ai1Rank)
        {
            // 플레이어와 AI1이 같은 등급이고 AI2는 다른 등급인 경우
            int result = CompareHighestDice(playerDice, ai1Dice); // 플레이어와 AI1의 주사위 숫자 비교
            if (result == 0) return 0; // 플레이어가 이김
            else return 1; // AI1이 이김
        }
        else if (playerRank == ai2Rank)
        {
            // 플레이어와 AI2가 같은 등급이고 AI1은 다른 등급인 경우
            int result = CompareHighestDice(playerDice, ai2Dice); // 플레이어와 AI2의 주사위 숫자 비교
            if (result == 0) return 0; // 플레이어가 이김
            else return 2; // AI2가 이김
        }
        else if (ai1Rank == ai2Rank)
        {
            // AI1과 AI2가 같은 등급이고 플레이어는 다른 등급인 경우
            int result = CompareHighestDice(ai1Dice, ai2Dice); // AI1과 AI2의 주사위 숫자 비교
            if (result == 0) return 1; // AI1이 이김
            else return 2; // AI2가 이김
        }

        // 기본값 설정 (모든 경우를 처리한 후에도 남는 경우를 대비)
        return -1; // 기본적으로 무승부를 반환
    }

    // 동일한 등급일 때, 주사위 숫자를 비교하는 함수
    static int CompareHighestDice(int[] dice1, int[] dice2)
    {
        Array.Sort(dice1);
        Array.Sort(dice2);

        // 각 주사위 배열을 내림차순으로 정렬한 후, 큰 숫자부터 비교
        for (int i = dice1.Length - 1; i >= 0; i--)
        {
            if (dice1[i] > dice2[i])
                return 0; // dice1이 이김
            else if (dice1[i] < dice2[i])
                return 1; // dice2가 이김
        }
        return -1; // 무승부 (같은 숫자일 경우)
    }

    // 플레이어와 AI1, AI2 모두의 주사위를 비교하는 함수
    static int CompareHighestDice(int[] dice1, int[] dice2, int[] dice3)
    {
        Array.Sort(dice1);
        Array.Sort(dice2);
        Array.Sort(dice3);

        // 각 주사위 배열을 내림차순으로 정렬한 후, 큰 숫자부터 비교
        for (int i = dice1.Length - 1; i >= 0; i--)
        {
            if (dice1[i] > dice2[i] && dice1[i] > dice3[i])
                return 0; // dice1(플레이어)이 이김
            else if (dice2[i] > dice1[i] && dice2[i] > dice3[i])
                return 1; // dice2(AI1)이 이김
            else if (dice3[i] > dice1[i] && dice3[i] > dice2[i])
                return 2; // dice3(AI2)이 이김
        }
        return -1; // 무승부 (같은 숫자일 경우)
    }

    // 배팅 처리
    public static void Bet(int betAmount, bool isWin)
    {
        if (isWin)
        {
            GameData.Player.Gold += betAmount * 2; // 승리 시 3배 획득
        }
        else
        {
            GameData.Player.Gold -= betAmount; // 패배 시 배팅 금액 잃음
        }
    }
}