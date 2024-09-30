using System;
using System.Reflection.Metadata.Ecma335;
class JobChooseScene : Scene

{
    public override void Start()
    {
        Console.Clear();
    }


    public override void Update()
    {
        List<Player> list = new List<Player>();
        Console.WriteLine("직업을 선택해 주세요.");
        foreach (PlayerType playerType in Enum.GetValues(typeof(PlayerType)))
        {
            Console.WriteLine(playerType);
        }
        HandleInput();
    }

    private void HandleInput()
    {
        string[] option = Enum.GetNames(typeof(PlayerType));
        int number = UIManager.DisplaySelectionUI(option);

        switch (number)
        {
            case 1:
                
                GameData.Player.Init(number);
                NextScene=new StartScene();
                break;
            case 2:
                GameData.Player.Init(number);
                NextScene=new StartScene();
                break;
            case 3:
                GameData.Player.Init(number);
                NextScene=new StartScene();
                break;
            case 4:
                GameData.Player.Init(number);
                NextScene=new StartScene();
                break;

            default:
                break;
        }
    }

}