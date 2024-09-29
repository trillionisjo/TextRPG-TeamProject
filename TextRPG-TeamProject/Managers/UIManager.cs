using System;
using System.Text;

static class UIManager
{
    const int padding = 3;

    public static void TitleBox(string text)
    { 
        int byteSize =  GetByteFromText(text);
        int cursorPosX = (Console.WindowWidth / 2) - (byteSize / 2);


        for (int i = 0; i < byteSize ; i++)
        {
            
            cursorPosX = (Console.WindowWidth / 2 ) - (byteSize  / 2);
            Console.SetCursorPosition(cursorPosX+i, 0);  
            Console.Write("-");
        }
        Console.SetCursorPosition(cursorPosX, 1);
        Console.WriteLine(text);

        for (int i = 0; i < byteSize; i++)
        {
             

            cursorPosX = (Console.WindowWidth / 2) - (byteSize / 2);
            Console.SetCursorPosition(cursorPosX + i, 2);
            Console.Write("-");
        }

        Console.SetCursorPosition(0, 2);
    }


    public static void AlignTextCenter(string[] text)
    {
        int cursorPosX;
        int cursorPosY = Console.WindowHeight / 2 - text.Length / 2;

        for (int i = 0; i < text.Length; i++)
        {
            cursorPosX = (Console.WindowWidth / 2) - (GetByteFromText(text[i]) / 2);
            Console.SetCursorPosition(cursorPosX, cursorPosY++);
            Console.Write(text[i]);
        }

        Console.SetCursorPosition(0, 0);

    }
    public static void AlignTextCenter(string text, int lineSpacing)
    {
        int cursorPosX = (Console.WindowWidth / 2) - (GetByteFromText(text) / 2);
        int cursorPosY = Console.WindowHeight / 2 + lineSpacing;

        Console.SetCursorPosition(cursorPosX, cursorPosY);
        Console.Write(text);

        Console.SetCursorPosition(0, 0);
    }


    public static void AlignTextCenter(string text)
    {
        int cursorPosX = (Console.WindowWidth / 2) - (GetByteFromText(text) / 2);
        int cursorPosY = Console.WindowHeight / 2;

        Console.SetCursorPosition(cursorPosX, cursorPosY);
        Console.Write(text);
        Console.SetCursorPosition(0, 0);
    }

    public static void PrintTextAtPosition(string text, int x, int y)
    {

        Console.SetCursorPosition(x, y);
        Console.Write(text);
        //커서 포지션 초기화 
        Console.SetCursorPosition(0, 0);
    }

    public static int GetByteFromText(string text)
    {
        int byteSize = 0;
        foreach (char c in text)
        {
            if (c <= 127)
            {
                byteSize += 1;
            }
            else
            {
                byteSize += 2;
            }
        }
        return byteSize;
    }

    public static int DisplaySelectionUI(string[] options)
    {
        //선택지 수에 따라 , 여백 2줄 

        int selectNum = 0;
        //선택값을 저장하기 위한 변수 
        int cursorPosY = (int)(Console.WindowHeight * 0.7) + 2; //
        int selectCursorPosY = cursorPosY + 1;
        int previousCursorPosY = selectCursorPosY; // 이전 커서 위치 저장
        bool isSelecting = true;

        while (isSelecting)
        {

            // 옵션 출력
            for (int i = 0; i <= options.Length; i++)
            {
                Console.SetCursorPosition(1, cursorPosY + i);

                if (i == 0)
                {
                    // 상단 경계선 그리기
                    for (int j = 0; j < Console.WindowWidth - 1; j++)
                    {
                        Console.Write("-");
                    }
                }
                else
                {
                    Console.Write(options[i - 1]);  // 옵션 출력
                }
            }

            Console.CursorVisible = false; //콘솔창 커서 숨기기 


            // 이전 커서 위치의 '▶' 지우기
            Console.SetCursorPosition(0, previousCursorPosY);
            Console.Write(" ");  // 공백으로 커서를 지움


            //콘솔 좌표 설정
            if (selectCursorPosY < cursorPosY + 1)
                selectCursorPosY = cursorPosY + options.Length;
            else if (selectCursorPosY > cursorPosY + options.Length)
                selectCursorPosY = cursorPosY + 1;

            // 새 위치에 '▶' 출력
            Console.SetCursorPosition(0, selectCursorPosY);
            Console.Write('▶');

            //사용자 입력처리 
            ConsoleKeyInfo input = Console.ReadKey(true);
            switch (input.Key)
            {
                case ConsoleKey.UpArrow:
                case ConsoleKey.LeftArrow:
                    previousCursorPosY = selectCursorPosY;
                    selectCursorPosY--;
                    break;
                case ConsoleKey.RightArrow:
                case ConsoleKey.DownArrow:
                    previousCursorPosY = selectCursorPosY;
                    selectCursorPosY++;
                    break;
                case ConsoleKey.Enter:
                    selectNum = selectCursorPosY - cursorPosY;
                    isSelecting = false;
                    break;
            }

        }

        return selectNum;
    }

    public static void WriteTable (string[,] table)
    {
        int rows = table.GetLength(0);
        int cols = table.GetLength(1);
        int[] maxWidths = new int[cols];

        // 각 열의 최대너비 계산
        for (int col = 0; col < cols; col++)
        {
            int max = 0;
            for (int row = 0; row < rows; row++)
            {
                int width = CalcTextWidth(table[row, col]);
                max = Math.Max(max, width);
            }
            maxWidths[col] = max;
        }

        // 테이블 출력
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols - 1; col++)
            {
                string paddedText = PadRight(table[row, col], maxWidths[col]);
                Console.Write($"{paddedText} | ");
            }
            string lastColumnText = PadRight(table[row, cols - 1], maxWidths[cols - 1]);
            Console.WriteLine(lastColumnText);
        }
    }

    public static string PadRight (string input, int totalWidth)
    {
        int textWidth = CalcTextWidth(input);
        return input.PadRight(input.Length + (totalWidth - textWidth));
    }

    public static int CalcTextWidth (string str)
    {
        return str.Sum(c => IsKorean(c) ? 2 : 1);
    }

    public static bool IsKorean (char ch)
    {
        return ('가' <= ch && ch <= '힣') || ('ㄱ' <= ch && ch <= 'ㅎ') || ('ㅏ' <= ch && ch <= 'ㅣ');
    }
}


