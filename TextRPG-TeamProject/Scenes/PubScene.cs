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
        UIManager.TitleBox("          주점          ");
        Console.WriteLine("반갑습니다! 주점에 오신 걸 환영해요, 용감한 모험가님!");
        Console.WriteLine("저희 주점에서는 편안한 휴식과 흥미로운 놀이를 제공하고 있답니다. 맘껏 즐기세요!");
        HandleInput();
    }
    private void HandleInput()
    {
        string[] option = { "한잔 마시며 여유를 즐긴다 (-10 골드)", "체력을 회복하며 휴식을 취한다 (-50 골드)", "도박을 통해 운을 시험해본다", "다른 모험을 위해 주점을 떠난다" };
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
