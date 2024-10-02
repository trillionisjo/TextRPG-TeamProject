class QuestListScene() : Scene
{
    public override void Start()
    {
        Console.Clear();
    }

    public override void Update()
    {
        var notStartedList = QuestManager.GetQuestListByStatus(QuestStatus.NotStarted);
        string[,] table = GetNotStartedTable();
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
            $"{quest.DetailedDescription}",
            $"보상:{quest.Reward} Gold",
        };

        UIManager
        string[] options = { "수락", "나가기" };
        int selectNum = UIManager.DisplaySelectionUI(options);

        
        if (selectNum == 1)
            QuestManager.ActivateQuest(id);

        Console.Clear();
    }


    protected string[,] GetNotStartedTable()
    {
        var notStartedList = QuestManager.GetQuestListByStatus(QuestStatus.NotStarted);
        if (notStartedList.Count != 0)
        {
            string[,] table = new string[notStartedList.Count, 3];

            for (int i = 0; i < notStartedList.Count; i++)
            {
                Quest quest = notStartedList[i];
                table[i, 0] = $"- {quest.Name}";
                table[i, 1] = quest.Description;
                table[i, 2] = $"{quest.Reward}G";
            }

            return table;
        }

        return null;
    }
}