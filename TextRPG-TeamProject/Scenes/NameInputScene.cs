using System;
class NameInputScene : Scene
{
    bool nameCheck = true;
    string nameInput;

    public override void Start()
    {
        AudioManager.PlayAudio("title_bgm.mp3");
        Console.Clear();
    }

    public override void Update()
    {
        Console.WriteLine("이름을 입력해 주세요\n입력하지 않을시 Sparta 가 됩니다.");
        while (nameCheck)
        {
            Console.Write("이름:" );
            nameInput = Console.ReadLine();
            Console.Clear();
            Console.WriteLine($"당신의 이름이 {nameInput}가 맞습니까?\n\n");
            HandleInput();

        }
    }

    public void HandleInput()
    {
        string[] option = { "맞다", "아니다" };
        int number = UIManager.DisplaySelectionUI(option);

        switch (number)
        {
            case 1:
                if (nameInput =="")
                {
                    GameData.Player.Name = "Sparta";
                }
                else
                {
                    GameData.Player.Name = nameInput;
                }
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
