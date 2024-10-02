class QuestAcceptScene : Scene
{
    public override void Start()
    {
        Console.Clear();
    }

    public override void Update()
    {
        var notStartedList = QuestManager.GetQuestListByStatus(QuestStatus.NotStarted);
        string[,] table = QuestManager.GetQuestTableByStatus(QuestStatus.NotStarted);
        int selectedNum = UIManager.DisplaySelectionUI(table);

        if (selectedNum != -1)
        {
            int id = notStartedList[selectedNum].Id;
            ShowQuestInfoById(id);
        }

        else
            NextScene = new QuestScene();
    }

    public void ShowQuestInfoById(int id)
    {
        Quest quest = QuestManager.GetQuestById(id);
        Console.Clear();

        string[] texts =
        {
            $"{quest.Name}",
            $"난이도:{quest.Difficulty}",
            $"보상:{quest.Reward} Gold",
        };

        UIManager.AlignTextCenter(texts, -3);
        UIManager.AlignTextCenter(quest.DetailedDescription, 2);
        string[] options = { "수락", "나가기" };


        int selectNum = UIManager.DisplaySelectionUI(options);

        if (selectNum == 1)
            QuestManager.ActivateQuest(id);

        Console.Clear();
    }
}

