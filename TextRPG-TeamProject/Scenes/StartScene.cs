using System;

class StartScene : Scene
{
    public override void Start ()
    {
        Console.Clear();
        AudioManager.PlayAudio("main_bgm.mp3");
    }
    
    public override void Update ()
    {
        UIManager.TitleBox("    스파르타 마을    ");
        Console.WriteLine("원하는 행동을 선택해 주세요.");

        string[] option = { "상태 보기","주점 입장", "던전 입장", "인벤토리", "상점","모험가 길드" };
        int number = UIManager.DisplaySelectionUI(option);
        HandleInput(number);
    }
    
    private void HandleInput(int selectedNumber)
    {
        switch (selectedNumber)
        {
        case 1:
            NextScene = new PlayerState();
            break;
                
        case 2:
            NextScene = new PubScene();
            break;
        case 3:
            NextScene = new DungeonScene();
            break;
        case 4:
            NextScene = new InventoryScene();
            break;
        case 5:
            NextScene = new ShopScene();
            break;
        case 6:
            NextScene = new QuestScene();
            break;
        default:
            break;
        }
    }


}