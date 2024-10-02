using System.ComponentModel;

public static class QuestManager
{
     public static List<Quest> QuestList = new List<Quest>();

     public static void Init()
     {
         QuestList.Add(new HuntQuest(1,"몬스터 사냥",1000,"몬스터 10마리를 잡아라",3));
         QuestList.Add(new HuntQuest(1,"몬스터 사냥",1000,"몬스터 10마리를 잡아라",10));
         QuestList.Add(new HuntQuest(1,"몬스터 사냥",1000,"몬스터 10마리를 잡아라",10));
         QuestList.Add(new HuntQuest(1,"몬스터 사냥",1000,"몬스터 10마리를 잡아라",10));


     }

     public static bool IsQuestTypeInProgress(QuestType type)
    {
        return QuestList.Any(q => q.Status == QuestStatus.Active && q.Type == type);
    }


    public static bool IsQuestCompletedById(int id)
    {
        foreach (var q in QuestList)
        {
            if (q.Id == id)
            {
                return q.IsCompleteQuest();
            }
        }

        return false;
    }
    
    public static void ActivateQuest(int id)
    {
        foreach (var q in QuestList)
        {
            if (q.Id == id)
            {
                q.Status = QuestStatus.Active;
                q.StartQuest();
                break;
            }
        }
    }

     public static List<Quest> GetQuestByStatus(QuestStatus status)
    {
        return QuestList.Where(q => q.Status == status).ToList();
    }
}