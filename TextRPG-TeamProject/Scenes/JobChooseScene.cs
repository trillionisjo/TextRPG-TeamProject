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
                    Console.WriteLine("HP:5 / MP:20 / AtkPower : 5 / DfsPower : 7\n");
                    break;
                case 2:
                    Console.WriteLine("HP:70 / MP:100 / AtkPower : 10 / DfsPower : 2\n");
                    break;
                case 3:
                    Console.WriteLine("HP:100 / MP:50 / AtkPower : 6 / DfsPower : 6\n");
                    break;
                case 4:
                    Console.WriteLine("HP:50 / MP:70 / AtkPower : 12 / DfsPower : 5\n");
                    break;
            }
        }
        HandleInput();
    }
    
            //case 1:
            //    Type = PlayerType.Knight;
            //    SetInfo(5, 20, 5, 7);
            //    break;
            //case 2:
            //    Type = PlayerType.Mage;
            //    SetInfo(70, 100 , 10, 2);
            //    break;
            // case 3:
            //    Type = PlayerType.Archer;
            //    SetInfo(100, 50 , 6, 6);
            //    break;
            //case 4:
            //    Type = PlayerType.Rogue;
            //    SetInfo(50, 70 , 12, 5);
            //    break;

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