using System;
using System.Text;

class Option
{
    public string Text;
    public IEventHandler Handler;

    public Option()
    {
        Text = null;
        Handler = null;
    }

    public Option(string text, IEventHandler handler)
    {
        Text = text;
        Handler = handler;
    }
}

static class UIManager
{
    const int padding = 3;

    /// <summary>
    /// 콘솔 창 상단 중앙에 제목 텍스트를 상하 선으로 감싸서 출력하는 메서드 
    /// </summary>
    public static void TitleBox(string text)
    {
        int byteSize = GetByteFromText(text);
        int cursorPosX = (Console.WindowWidth / 2) - (byteSize / 2);


        for (int i = 0; i < byteSize; i++)
        {
            cursorPosX = (Console.WindowWidth / 2) - (byteSize / 2);
            Console.SetCursorPosition(cursorPosX + i, 0);
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

    /// <summary>
    /// 콘솔 창 가운데에 텍스트를 중앙 정렬하여 출력하는 메서드
    /// </summary>
    public static void AlignTextCenter(string text)
    {
        int cursorPosX = (Console.WindowWidth / 2) - (GetByteFromText(text) / 2);
        int cursorPosY = Console.WindowHeight / 2;

        Console.SetCursorPosition(cursorPosX, cursorPosY);
        Console.Write(text);
        Console.SetCursorPosition(0, 0);
    }

    /// <summary>
    /// 콘솔 창 가운데에 텍스트 배열을 중앙 정렬하여 출력하는 메서드
    /// </summary>
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

    /// <summary>
    /// 콘솔 창 가운데에서 지정된 줄 간격만큼 떨어진 위치에 텍스트를 중앙 정렬하여 출력하는 메서드
    /// </summary>
    public static void AlignTextCenter(string text, int lineSpacing)
    {
        int cursorPosX = (Console.WindowWidth / 2) - (GetByteFromText(text) / 2);
        int cursorPosY = Console.WindowHeight / 2 + lineSpacing;

        Console.SetCursorPosition(cursorPosX, cursorPosY);
        Console.Write(text);

        Console.SetCursorPosition(0, 0);
    }

    /// <summary>
    /// 콘솔 창 가운데에서 지정된 줄 간격만큼 떨어진 위치에 텍스트 배열을 중앙 정렬로 출력하는 메서드
    /// </summary>
    public static void AlignTextCenter(string[] text, int lineSpacing)
    {
        int cursorPosX;
        int cursorPosY = Console.WindowHeight / 2 - text.Length / 2 + lineSpacing;

        for (int i = 0; i < text.Length; i++)
        {
            cursorPosX = (Console.WindowWidth / 2) - (GetByteFromText(text[i]) / 2);
            Console.SetCursorPosition(cursorPosX, cursorPosY++);
            Console.Write(text[i]);
        }

        Console.SetCursorPosition(0, 0);
    }

    /// <summary>
    /// 지정된 좌표 (x, y) 위치에 텍스트를 출력하는 메서드
    /// </summary>
    public static void PrintTextAtPosition(string text, int x, int y)
    {
        Console.SetCursorPosition(x, y);
        Console.Write(text);
        Console.SetCursorPosition(0, 0);
    }

    /// <summary>
    /// 주어진 텍스트의 바이트 크기를 계산하는 메서드 (영문 1바이트, 한글 2바이트)
    /// </summary>
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

    /// <summary>
    /// 2차원 배열로 주어진 옵션을 출력하고 선택할 수 있는 UI를 제공하는 메서드
    /// </summary>
    public static int DisplaySelectionUI(string[,] table)
    {
        if (table == null || table.GetLength(0) == 0 || table.GetLength(1) == 0)
        {
            return -1;
        }
        
        int rows = table.GetLength(0);
        int cols = table.GetLength(1);

        int padding = 2;
        int cursorPosY = 0;
        int selectCursorPosY = cursorPosY + 1;
        int previousCursorPosY = selectCursorPosY;
        bool isSelecting = true;

        while (isSelecting)
        {
            for (int i = 0; i < rows; i++)
            {
                Console.SetCursorPosition(1, cursorPosY + 1 + i);

                for (int j = 0; j < cols; j++)
                {
                    Console.Write(table[i, j] + "\t");
                }

                Console.WriteLine();
            }

            Console.SetCursorPosition(1, cursorPosY + 1 + rows);
            Console.WriteLine("뒤로가기");

            Console.SetCursorPosition(0, previousCursorPosY);
            Console.Write(' ');

            if (selectCursorPosY < cursorPosY + 1) selectCursorPosY = cursorPosY + rows + 1;
            else if (selectCursorPosY > cursorPosY + rows + 1) selectCursorPosY = cursorPosY + 1;

            Console.SetCursorPosition(0, selectCursorPosY);
            Console.Write('▶');

            ConsoleKey input = Console.ReadKey(true).Key;

            previousCursorPosY = selectCursorPosY;

            switch (input)
            {
                case ConsoleKey.UpArrow:
                case ConsoleKey.LeftArrow:
                    selectCursorPosY -= 1;
                    break;
                case ConsoleKey.DownArrow:
                case ConsoleKey.RightArrow:
                    selectCursorPosY += 1;
                    break;
                case ConsoleKey.Enter:
                    isSelecting = false;

                    if (selectCursorPosY == cursorPosY + rows + 1)
                        return -1;
                    break;
            }
        }

        return selectCursorPosY - cursorPosY - 1;
    }

    /// <summary>
    /// 1차원 배열로 주어진 옵션을 출력하고 선택할 수 있는 UI를 제공하는 메서드
    /// </summary>
    public static int DisplaySelectionUI(string[] options)
    {
        int cursorPosY = (int)(Console.WindowHeight * 0.7) +
                         (options.Length < 2 ? 2 + padding : options.Length > 4 ? 2 - options.Length / 2 : 2);
        int selectCursorPosY = cursorPosY + 1;
        int previousCursorPosY = selectCursorPosY;
        bool isSelecting = true;

        while (isSelecting)
        {
            Console.SetCursorPosition(0, cursorPosY);
            Console.WriteLine(new string('-', Console.WindowWidth - 1));

            for (int i = 0; i < options.Length; i++)
            {
                Console.SetCursorPosition(1, cursorPosY + 1 + i);
                Console.WriteLine(options[i]);
            }

            Console.SetCursorPosition(0, previousCursorPosY);
            Console.Write(' ');

            if (selectCursorPosY < cursorPosY + 1) selectCursorPosY = cursorPosY + options.Length;
            else if (selectCursorPosY > cursorPosY + options.Length) selectCursorPosY = cursorPosY + 1;

            Console.SetCursorPosition(0, selectCursorPosY);
            Console.Write('▶');

            ConsoleKey input = Console.ReadKey(true).Key;

            previousCursorPosY = selectCursorPosY;
            selectCursorPosY += input == ConsoleKey.UpArrow || input == ConsoleKey.LeftArrow ? -1 :
                input == ConsoleKey.DownArrow || input == ConsoleKey.RightArrow ? 1 : 0;

            if (input == ConsoleKey.Enter) isSelecting = false;
        }

        return selectCursorPosY - cursorPosY;
    }

    /// <summary>
    /// 1차원 배열을 받아와서 지정된 위치에 출력하고 선택할 수 있는 UI를 제공하는 메서드
    /// </summary>
    public static int DisplaySelectionUI(Option[] options, int x, int y, int cursorOffset)
    {
        bool looping = true;
        int count = 0;

        while (cursorOffset < 0 || cursorOffset >= options.Count() || options[cursorOffset] == null ||
               options[cursorOffset].Handler == null)
        {
            cursorOffset = (cursorOffset - 1 + options.Length) % options.Length;
            if (++count >= options.Count())
                return 0;
        }

        while (looping)
        {
            for (int i = 0; i < options.Count(); i++)
            {
                if (options[i] == null)
                    continue;

                Console.SetCursorPosition(x, y + i);
                if (cursorOffset == i)
                    Console.Write("▶");
                else
                    Console.Write(" ");

                Console.SetCursorPosition(x + 2, y + i);
                Console.Write(options[i].Text);
            }

            ConsoleKey key = Console.ReadKey(false).Key;
            switch (key)
            {
                case ConsoleKey.UpArrow:
                case ConsoleKey.LeftArrow:
                    do
                    {
                        cursorOffset = (cursorOffset - 1 + options.Length) % options.Length;
                    } while (options[cursorOffset] == null || options[cursorOffset].Handler == null);

                    break;

                case ConsoleKey.DownArrow:
                case ConsoleKey.RightArrow:
                    do
                    {
                        cursorOffset = (cursorOffset + 1) % options.Length;
                    } while (options[cursorOffset] == null || options[cursorOffset].Handler == null);

                    break;

                case ConsoleKey.Enter:
                    options[cursorOffset].Handler.Invoke();
                    looping = false;
                    break;
            }
        }

        return cursorOffset;
    }

    /// <summary>
    /// 주어진 2차원 배열을 받아와서 정렬 후 출력하는 메서드
    /// </summary>
    public static void WriteTable(string[,] table)
    {
        string[,] paddedTable = CreatePaddedTable(table);
        for (int row = 0; row < table.GetLength(0); row++)
        {
            for (int col = 0; col < table.GetLength(1); col++)
                Console.Write(paddedTable[row, col]);
            Console.WriteLine();
        }
    }

    /// <summary>
    /// 주어진 2차원 배열을 지정된 위치 (left, top)에서 정렬 후 출력하는 메서드
    /// </summary>
    public static void WriteTable(string[,] table, int left, int top)
    {
        int count = 0;
        string[,] paddedTable = CreatePaddedTable(table);
        for (int row = 0; row < table.GetLength(0); row++)
        {
            Console.SetCursorPosition(left, top + count++);
            for (int col = 0; col < table.GetLength(1); col++)
                Console.Write(paddedTable[row, col]);
        }
    }

    /// <summary>
    /// 2차원 배열을 정렬 후 반환 해주는 메서드
    /// </summary>
    public static string[,] CreatePaddedTable(string[,] table)
    {
        int rows = table.GetLength(0);
        int cols = table.GetLength(1);
        int[] maxWidths = new int[cols];
        string[,] paddedTable = new string[rows, cols];

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

        // 스페이스가 채워진 테이블 생성
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols - 1; col++)
            {
                string paddedText = PadRight(table[row, col], maxWidths[col]);
                paddedTable[row, col] = $"{paddedText} | ";
            }

            string lastColumnText = PadRight(table[row, cols - 1], maxWidths[cols - 1]);
            paddedTable[row, cols - 1] = lastColumnText;
        }

        return paddedTable;
    }

    public static string[] CreatePaddedList(string[,] table)
    {
        int rows = table.GetLength(0);
        int cols = table.GetLength(1);
        int[] maxWidths = new int[cols];
        string[] paddedList = new string[rows];

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

        // 스페이스가 채워진 테이블 생성
        for (int row = 0; row < rows; row++)
        {
            var sb = new StringBuilder();
            for (int col = 0; col < cols - 1; col++)
            {
                string paddedText = PadRight(table[row, col], maxWidths[col]);
                sb.Append($"{paddedText} | ");
            }

            string lastColumnText = PadRight(table[row, cols - 1], maxWidths[cols - 1]);
            sb.Append(lastColumnText);

            paddedList[row] = sb.ToString();
        }

        return paddedList;
    }

    public static void DrawLine(int y, int length = 110)
    {
        Console.SetCursorPosition(0, y);
        Console.Write(GetLineString(length));
    }

    public static string GetLineString(int length = 110)
    {
        var sb = new StringBuilder();
        for (int i = 0; i < length; i++)
            sb.Append('-');
        return sb.ToString();
    }

    public static string PadRight(string input, int totalWidth)
    {
        int textWidth = CalcTextWidth(input);
        return input.PadRight(input.Length + (totalWidth - textWidth));
    }

    public static int CalcTextWidth(string str)
    {
        return str.Sum(c => IsKorean(c) ? 2 : 1);
    }

    public static bool IsKorean(char ch)
    {
        return ('가' <= ch && ch <= '힣') || ('ㄱ' <= ch && ch <= 'ㅎ') || ('ㅏ' <= ch && ch <= 'ㅣ');
    }
}