 class QuestScene :Scene
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
        
        
        Console.Write(QuestManager.QuestList[0].Name);
        Console.Write(QuestManager.QuestList[0].Type);
        Console.Write(QuestManager.QuestList[0].Status);

        
        if (QuestManager.IsQuestCompletedById(1))
        {
            Console.WriteLine("퀘스트 클리어");
        }

    }

    public override void Update()
    {
        UIManager.TitleBox($"{GameData.Player.Type} 길드");
        UIManager.AlignTextCenter("길드에 오신 것을 환영합니다.",-2);
        UIManager.AlignTextCenter("여기서 퀘스트를 진행하실 수 있습니다.",-1);
        string[] option = { "퀘스트 목록","진행중인 퀘스트" ,"나가기"};
        int number = UIManager.DisplaySelectionUI(option);
        NextScene = new StartScene();
    }
    
    

    private void HandleInput(int selectedNumber)
    {
        switch (selectedNumber)
        {
            case 1:
                NextScene = new EquipmentScene();
                break;

            case 2:
                Inventory.UsePotion(ItemId.HpPotion);
                break;

            case 3:
                NextScene = new StartScene();
                break;
        }
    }
    
    
}