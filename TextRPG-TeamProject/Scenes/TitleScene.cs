using System;


class TitleScene : Scene
{
    string[] asciiArt =
@"          ____                                  __
         /\  _`\                               /\ \__
         \ \,\L\_\    _____      __      _ __  \ \ ,_\     __
          \/_\__ \   /\ '__`\  /'__`\   /\`'__\ \ \ \/   /'__`\
            /\ \L\ \ \ \ \L\ \/\ \L\.\_ \ \ \/   \ \ \_ /\ \L\.\_
            \ `\____\ \ \ ,__/\ \__/.\_\ \ \_\    \ \__\\ \__/.\_\
             \/_____/  \ \ \/  \/__/\/_/  \/_/     \/__/ \/__/\/_/
                        \ \_\
                         \/_/
  ______    ____       __   __      ______             ____        ____     ____
 /\__  _\  /\  _`\    /\ \ /\ \    /\__  _\           /\  _`\     /\  _`\  /\  _`\
 \/_/\ \/  \ \ \L\_\  \ `\`\/'/'   \/_/\ \/           \ \ \L\ \   \ \ \L\ \\ \ \L\_\
    \ \ \   \ \  _\L   `\/ > <        \ \ \   _______  \ \ ,  /    \ \ ,__/ \ \ \L_L
     \ \ \   \ \ \L\ \    \/'/\`\      \ \ \ /\______\  \ \ \\ \    \ \ \/   \ \ \/, \
      \ \_\   \ \____/    /\_\\ \_\     \ \_\\/______/   \ \_\ \_\   \ \_\    \ \____/
       \/_/    \/___/     \/_/ \/_/      \/_/             \/_/\/ /    \/_/     \/___/".Split('\n');

    int startTime;
    int animationSpeed;
    bool looping;

    struct Option { public string text; public Action action; }
    Option[] options;
    int cursorOffset;

    string msg;

    public override void Start ()
    {
        Console.CursorVisible = false;
        animationSpeed = 100;
        startTime = Environment.TickCount;
        looping = true;

        options = new Option[] {
            new Option { text = "새로 시작하기", action = NewStart },
            new Option { text = "불러오기", action = LoadGame },
            new Option { text = "끝내기", action = ExitGame },
        };
        cursorOffset = 0;

        msg = "";

        AudioManager.PlayAudio("title_bgm.mp3");

        DisplayAsciiArt(17, 3);
    }

    public override void Update ()
    {
        while (looping)
        {
            int endTick = Environment.TickCount;
            if (endTick - startTime >= animationSpeed)
            {
                DisplayAsciiArt(17, 3);
                startTime = endTick;
            }

            WriteOptions(50, 22);
            WriteWarningMsg(1, 28);
            PeekKeyAndHandleIt();
        }
    }

    private void PeekKeyAndHandleIt ()
    {
        if (!Console.KeyAvailable)
            return;

        var key = Console.ReadKey(false).Key;
        switch (key)
        {
        case ConsoleKey.UpArrow:
        case ConsoleKey.LeftArrow:
            cursorOffset = (cursorOffset - 1 + options.Length) % options.Length;
            break;

        case ConsoleKey.DownArrow:
        case ConsoleKey.RightArrow:
            cursorOffset = (cursorOffset + 1) % options.Length;
            break;

        case ConsoleKey.Enter:
            options[cursorOffset].action.Invoke();
            break;
        }
    }

    private void NewStart()
    {
        NextScene = new NameInputScene();
        looping = false;
    }

    private void LoadGame()
    {
        var result = SaveManager.LoadGame();
        switch (result)
        {
        case LoadGameResult.Success:
            NextScene = new StartScene();
            looping = false;
            break;

        case LoadGameResult.FileNotFound:
            msg = "저장된 파일이 없습니다!";
            break;

        case LoadGameResult.CorruptedData:
            msg = "파일이 잘못 되었습니다!";
            break;
        }
    }

    private void ExitGame()
    {
        Environment.Exit(0);
    }

    private void WriteOptions(int x, int y)
    {
        for (int i = 0; i < options.Length; i++)
        {
            Console.SetCursorPosition(x, y + i);
            if (cursorOffset == i)
                Console.Write("▶");
            else
                Console.Write("  ");

            Console.SetCursorPosition(x + 2, y + i);
            Console.Write(options[i].text);
        }
    }

    private void WriteWarningMsg(int x, int y)
    {
        Console.SetCursorPosition(x, y);
        Console.Write(msg);
    }

    private void DisplayAsciiArt(int x, int y)
    {
        var prevColor = Console.ForegroundColor;
        var rand = new Random();
        var glowingChars = new List<(int, int)>();

        for (int i = 0; i < 15; i++)
        {
            int row = rand.Next(asciiArt.Length);
            int col = rand.Next(asciiArt[row].Length);
            if (asciiArt[row][col] != ' ')
                glowingChars.Add((row, col));
        }

        for (int i = 0; i < asciiArt.Length; i++)
        {
            Console.SetCursorPosition(x, y + i);
            for (int j = 0; j < asciiArt[i].Length; j++)
            {
                if (glowingChars.Contains((i, j)))
                    Console.ForegroundColor = ConsoleColor.Yellow;
                else
                    Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(asciiArt[i][j]);
            }
            Console.WriteLine();
        }

        Console.ForegroundColor = prevColor;
    }
}
