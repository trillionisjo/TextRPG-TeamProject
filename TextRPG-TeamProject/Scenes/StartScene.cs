using System;

class StartScene : Scene
{
    private Scene nextScene;

    public override void Start ()
    {
        nextScene = this;
        Console.Clear();
    }

    public override Scene GetNextScene ()
    {
        return nextScene;
    }

    public override void Update ()
    {
        Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.");
        Console.WriteLine("이제 전투를 시작할 수 있습니다.");

        Console.WriteLine("1. 상태 보기");
        Console.WriteLine("2. 전투 시작");

        Console.WriteLine("원하시는 행동을 입력해주세요.");
        Console.WriteLine(">> ");

        HandleInput();
    }

    private void HandleInput()
    {
        string input = Console.ReadLine() ?? string.Empty;
        int.TryParse(input, out var number);

        switch (number)
        {
        case 1:
            nextScene = new PlayerState();
            break;

        default:
            break;
        }
    }


}
