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

    public override void Start ()
    {
        Console.CursorVisible = false;
    }

    public override void Update ()
    {
        while (true)
        {
            DisplayAsciiArt(17, 3);

            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(47, 22);
            Console.Write("PRESS ENTER TO START");

            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(false).Key;
                if (key == ConsoleKey.Enter)
                {
                    NextScene = new StartScene();
                    break;
                }
            }
            Thread.Sleep(100);
        }
    }



    private void DisplayAsciiArt(int x, int y)
    {
        
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
    }
}
