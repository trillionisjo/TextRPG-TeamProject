using System;

class MainMenuScene : Scene
{
    public override void Update ()
    {
        Console.Clear();
        string[] options = {"새로 시작하기", "불러오기" };
        int selectedNumber = UIManager.DisplaySelectionUI(options);
        HandleInput(selectedNumber);
    }

    private void HandleInput(int selectedNumber)
    {
        switch (selectedNumber)
        {
        case 1:
            NewGame();
            break;

        case 2:
            LoadGame();
            break;
        }
    }

    private void NewGame ()
    {
        NextScene = new NameInputScene();
    }

    private void LoadGame()
    {
        bool success = SaveManager.LoadGame();
        if (!success)
        {
            UIManager.AlignTextCenter("불러오기를 실패하였습니다");
            Console.ReadKey(true);
            return;
        }

        NextScene = new StartScene();
    }
}
