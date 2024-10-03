using System;
internal class PubScene : Scene
{
    public override void Start()
    {
        Console.Clear();
        AudioManager.PlayAudio("pub_bgm.mp3");
    }


    public override void Update()
    {
        UIManager.TitleBox("주점");
        UIManager.AlignTextCenter("주점에 오신것을 환영합니다.",-3);
        UIManager.AlignTextCenter("저희 주점에서는 휴식이 가능합니다.",-2);
        HandleInput();
    }
    private void HandleInput()
    {
        string[] option = { "한잔 마시기   -10gold", "휴식하기   -50gold","도박하기","나가기" };
        int number = UIManager.DisplaySelectionUI(option);

        switch (number)
        {
            case 1:
                PubManager.PubDrink();
                NextScene = new PubScene();
                break;
            case 2:
                PubManager.PubRest();
                NextScene = new PubScene();
                break;
            case 3:
                Console.Clear ();
                Console.WriteLine("구현중");
                Console.ReadLine();
                NextScene = new PubScene();
                break;
            case 4:
                NextScene = new StartScene();
                break;

            default:
                break;
        }
    }

}
