using System;
class NameInputScene : Scene
{
    bool nameCheck = true;
    public override void Start()
    {
        Console.Clear();
    }

    public override void Update()
    {
        Console.WriteLine("이름을 입력해 주세요");
        while (nameCheck)
        {
            GameData.Player.Name = Console.ReadLine();
            Console.WriteLine($"당신의 이름이 {GameData.Player.Name}가 맞습니까?");
            HandleInput();

        }
    }

    private void HandleInput()
    {
        string[] option = { "맞다","아니다" };
        int number = UIManager.DisplaySelectionUI(option);

        switch (number)
        {
            case 1:
                nameCheck = false;
                NextScene = new JobChooseScene();
                break;
            case 2:
                Console.Clear ();
                Console.WriteLine("다시 이름을 입력해 주세요");
                break;

            default:
                break;
        }
    }

}
