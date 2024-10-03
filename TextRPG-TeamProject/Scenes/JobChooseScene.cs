using System;
using System.Reflection.Metadata.Ecma335;
using static System.Runtime.InteropServices.JavaScript.JSType;
class JobChooseScene : Scene

{
    public override void Start()
    {
        AudioManager.PlayAudio("title_bgm.mp3");
        Console.Clear();
    }


    public override void Update()
    {
        List<Player> list = new List<Player>(); 
        UIManager.TitleBox("직업 선택");
        Console.WriteLine("모험의 길을 떠날 준비가 되셨나요? 당신의 운명을 결정할 직업을 선택해 주세요.");
        foreach (PlayerType playerType in Enum.GetValues(typeof(PlayerType)))
        {
            Console.WriteLine(playerType);
            switch ((int)playerType)
            {
                case 1:
                    Console.WriteLine("HP:50 / MP:50 / AtkPower : 5 / DfsPower : 7\n");
                    break;
                case 2:
                    Console.WriteLine("HP:20 / MP:100 / AtkPower : 10 / DfsPower : 2\n");
                    break;
                case 3:
                    Console.WriteLine("HP:40 / MP:70 / AtkPower : 6 / DfsPower : 6\n");
                    break;
                case 4:
                    Console.WriteLine("HP:30 / MP:70 / AtkPower : 12 / DfsPower : 0\n");
                    break;
            }
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