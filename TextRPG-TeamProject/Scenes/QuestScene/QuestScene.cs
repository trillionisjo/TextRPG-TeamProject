class QuestScene : Scene
{
    /// <summary>
    ///
    /// 1. 퀘스트 목록을 띄워준다. 
    ///
    /// 2. 퀘스트 
    /// </summary>
    public override void Start()
    {
        Console.Clear();
    }

    public override void Update()
    {
        UIManager.TitleBox($"     {GameData.Player.Type}     ");
        Console.WriteLine("모험가 길드에 오신 것을 진심으로 환영합니다.");
        Console.WriteLine("이곳에서는 당신을 위한 다양한 퀘스트가 준비되어 있습니다.");
        string[] option = { "퀘스트 목록 열람", "진행 중인 퀘스트 확인", "길드 문을 나선다" };
        int number = UIManager.DisplaySelectionUI(option);
        HandleInput(number);
    }


    private void HandleInput(int selectedNumber)
    {
        switch (selectedNumber)
        {
            case 1:
                if (QuestManager.GetQuestListByStatus(QuestStatus.NotStarted).Count == 0)
                {
                    Console.Clear();
                    UIManager.TitleBox("    퀘스트 목록    ");
                    Console.WriteLine("흠... 더 이상 자네에게 줄 퀘스트가 없구만");
                    UIManager.DisplaySelectionUI(new[] { "다음" });
                    Console.Clear();
                }
                else
                    NextScene = new QuestAcceptScene();

                break;
            case 2:
                if (QuestManager.GetQuestListByStatus(QuestStatus.Active).Count == 0)
                {
                    Console.Clear();
                    UIManager.TitleBox("    진행 중인 퀘스트    ");
                    Console.WriteLine("퀘스트를 한번 받아 보는건 어때?");
                    UIManager.DisplaySelectionUI(new[] {"다음"});
                    Console.Clear();
                }
                else 
                    NextScene = new ActiveQuestScene();

                break;
            case 3:
                NextScene = new StartScene();
                break;
        }
    }
}