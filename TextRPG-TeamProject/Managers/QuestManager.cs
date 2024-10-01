using System;

 class QuestManager
{
   public  List<Quest> QuestList = new List<Quest>(); 
    

    public  void Instantiate()
    { 
        
    }

    public  void RemoveQuestByType()
    { 
        
    }


    public void RegisterActiveQuestEventsById(int id)
    {
        foreach (Quest quest in QuestList)
        {
            if (id == quest.Id)
            {
                quest.OnQuestClear += CompleteQuest;
                break;
            }
        }
     }


    public void CompleteQuest(int id)
    {
        var potionList = GetQuestByStatus();
        foreach (Quest quest in potionList)
        {
            if (id == quest.Id)
            {
                quest.OnQuestClear -= CompleteQuest;
                break;
            }
        }
    }


    public List<Quest> GetQuestByStatus()
    { 
        List<Quest> activeQuest = new List<Quest>();

        foreach (Quest quest in QuestList)
        {
            if (quest.Status == QuestStatus.Active)
            {
                activeQuest.Add(quest);
            }
        }
        return QuestList;
    }


}