﻿using System;
using System.Collections.Generic;
using System.Linq;

class DiceGame :Scene
{
    public override void Start()
    {
    }

    public override void Update()
    {
        dice();
        NextScene = new DiceGame();
    }
    static Random random = new Random();
    static int playerGold = 10000; // 플레이어 초기 소지금

    static void dice()
    {
        Console.WriteLine("Welcome to the Dice Game!");

        // 게임 반복
        while (true)
        {
            Console.WriteLine("\n소지금: " + playerGold + " 골드");
            Console.Write("베팅할 금액을 입력하세요 (최대 10000 골드): ");
            int betAmount = int.Parse(Console.ReadLine());

            if (betAmount > playerGold || betAmount > 10000)
            {
                Console.WriteLine("잘못된 베팅 금액입니다. 다시 입력하세요.");
                continue;
            }

            // 플레이어 및 AI 주사위 굴리기
            Console.WriteLine("\n--- 플레이어 차례 ---");
            int[] playerDice = PlayTurn();
            Console.WriteLine("\n--- AI1 차례 ---");
            int[] ai1Dice = PlayAITurn();
            Console.WriteLine("\n--- AI2 차례 ---");
            int[] ai2Dice = PlayAITurn();

            // 승리자 결정
            int playerRank = GetRank(playerDice);
            int ai1Rank = GetRank(ai1Dice);
            int ai2Rank = GetRank(ai2Dice);

            Console.WriteLine("\n--- 결과 ---");
            Console.WriteLine("플레이어: " + GetRankName(playerRank));
            Console.WriteLine("AI1: " + GetRankName(ai1Rank));
            Console.WriteLine("AI2: " + GetRankName(ai2Rank));

            int winner = DetermineWinner(playerDice, ai1Dice, ai2Dice);

            // 결과에 따른 금액 처리
            if (winner == 0)
            {
                Console.WriteLine("플레이어가 이겼습니다!");
                Bet(betAmount, true);
            }
            else
            {
                Console.WriteLine($"AI{winner}이 이겼습니다.");
                Bet(betAmount, false);
            }

            if (playerGold <= 0)
            {
                Console.WriteLine("더 이상 소지금이 없습니다. 게임 종료.");
                break;
            }

            Console.WriteLine("다시 하시겠습니까? (y/n)");
            if (Console.ReadLine().ToLower() != "y")
                break;
        }
    }

    // 턴 진행 (플레이어)
    static int[] PlayTurn()
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
    static int[] PlayAITurn()
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
    static int GetRank(int[] dice)
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
    static string GetRankName(int rank)
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
    static int DetermineWinner(int[] playerDice, int[] ai1Dice, int[] ai2Dice)
    {
        int playerRank = GetRank(playerDice);
        int ai1Rank = GetRank(ai1Dice);
        int ai2Rank = GetRank(ai2Dice);

        if (playerRank < ai1Rank && playerRank < ai2Rank)
        {
            return 0; // 플레이어 승
        }
        else if (ai1Rank < playerRank && ai1Rank < ai2Rank)
        {
            return 1; // AI1 승
        }
        else
        {
            return 2; // AI2 승
        }
    }

    // 배팅 처리
    static void Bet(int betAmount, bool isWin)
    {
        if (isWin)
        {
            playerGold += betAmount * 2; // 승리 시 3배 획득
        }
        else
        {
            playerGold -= betAmount; // 패배 시 배팅 금액 잃음
        }
    }
}