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
            "",
            $"보상:{quest.Reward} Gold",
        };

        UIManager.AlignTextCenter(texts, -6);
        UIManager.AlignTextCenter(quest.DetailedDescription );
        string[] options = { "수락", "나가기" };


        int selectNum = UIManager.DisplaySelectionUI(options);

        if (selectNum == 1)
        {
            if (QuestManager.CurrentActivateCount >= QuestManager.MaxActivateCount)
            {
                Console.Clear();
                UIManager.AlignTextCenter($"퀘스트는 {QuestManager.MaxActivateCount}개 이상 받을 수 없다네... 진행 중인 퀘스트({QuestManager.CurrentActivateCount} / {QuestManager.MaxActivateCount})");
                UIManager.DisplaySelectionUI(new[] {"다음"});
                Console.Clear();
                return;
                
            }

            QuestManager.ActivateQuest(id);
            
        }

        Console.Clear();
    }
}

