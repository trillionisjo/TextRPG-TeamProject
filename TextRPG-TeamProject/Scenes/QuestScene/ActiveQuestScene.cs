class ActiveQuestScene : Scene
{
    public override void Start()
    {
        Console.Clear();
    }

    public override void Update()
    {
        var activeList = QuestManager.GetQuestListByStatus(QuestStatus.Active);
        string[,] activeTable = QuestManager.GetQuestTableByStatus(QuestStatus.Active);


        UIManager.TitleBox("    진행 중인 퀘스트    ");
        int selectedNum = UIManager.DisplaySelectionUI(activeTable);

        if (selectedNum != -1)
        {
            int id = activeList[selectedNum].Id;
            DisplayQuestProgress(id);
        }

        else
            NextScene = new QuestScene();
    }


    public void DisplayQuestProgress(int id)
    {
        Quest quest = QuestManager.GetQuestById(id);
        Console.Clear();

        UIManager.TitleBox($"{quest.Name}");
        string[] texts =
        {
            $"난이도:{quest.Difficulty}",
            $"보상:{quest.Reward} Gold",
        };

        foreach (var text in texts)
        {
            Console.WriteLine(text);
        }

        
        Console.WriteLine(quest.GetQuestProgressText());
        Console.WriteLine();
       
        foreach (var text in quest.DetailedDescription)
        {
            Console.WriteLine(text);
        }


        string[] options = { "보상받기", "포기하기", "돌아가기" };

        int selectNum = UIManager.DisplaySelectionUI(options);

        if (selectNum == 1)
        {
            if (quest.IsCompleteQuest())
            {
                Console.Clear();
                Console.WriteLine("퀘스트를 훌륭하게 완수했군...!");
                Console.WriteLine($"보유골드{GameData.Player.Gold - quest.Reward} -> {GameData.Player.Gold}");
                UIManager.DisplaySelectionUI(new string[] { "돌아가기" });
            }

            else
            
            
            
            
            {
                Console.Clear();
                Console.WriteLine("아직 퀘스트를 클리어하지 못했어");
                UIManager.DisplaySelectionUI(new[] { "다음" });
                Console.Clear();
            }
        }

        else if (selectNum == 2)
        {
            QuestManager.CurrentActivateCount--;
            quest.CancelQuest();
        }


        Console.Clear();
    }
}