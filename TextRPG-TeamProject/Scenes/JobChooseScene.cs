using System;
using System.Reflection.Metadata.Ecma335;
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
        UIManager.AlignTextCenter("직업을 선택해 주세요.",-2);
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