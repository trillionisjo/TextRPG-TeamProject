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
        
        string[] texts =
        {
            $"{quest.Name}", 
            $"난이도:{quest.Difficulty}",
            $"보상:{quest.Reward} Gold",
        };

        UIManager.AlignTextCenter(texts,-6);
        UIManager.AlignTextCenter($"({quest.GetQuestProgressText()})",-4);
        UIManager.AlignTextCenter(quest.DetailedDescription);
        
        
        
        string[] options = { "보상받기","포기하기" ,"돌아가기" };
        
        int selectNum = UIManager.DisplaySelectionUI(options);

        if (selectNum == 1)
        {
            if (quest.IsCompleteQuest())
            {
                Console.Clear();
                UIManager.AlignTextCenter("퀘스트를 훌륭하게 완수했군...!",-3);
                UIManager.AlignTextCenter($"보유골드{GameData.Player.Gold - quest.Reward} -> {GameData.Player.Gold}");
                UIManager.DisplaySelectionUI(new string[]{"돌아가기" });
            }
            
            else
            {
                Console.Clear();
                UIManager.AlignTextCenter("아직 퀘스트를 클리어하지 못했어");
                UIManager.DisplaySelectionUI(new[] { "다음" });
                Console.Clear();
            }

        }

        else if(selectNum ==2)
        {
            QuestManager.CurrentActivateCount--;
            quest.CancelQuest();
        }
        
        

        Console.Clear();
    }


    
    
}