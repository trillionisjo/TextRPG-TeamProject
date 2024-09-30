using System;

class StartScene : Scene
{
    public override void Start ()
    {
        Console.Clear();
    }

    public override void Update ()
    {
        Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.");
        Console.WriteLine("이제 전투를 시작할 수 있습니다.");

        string []option = {"상태 보기","전투 시작"};
        UIManager.DisplaySelectionUI(option);
        
    }


    private void HandleInput()
    {
        string input = Console.ReadLine() ?? string.Empty;
        int.TryParse(input, out var number);

        switch (number)
        {
        case 1:
            NextScene = new PlayerState();
            break;

        default:
            break;
        }
    }


}