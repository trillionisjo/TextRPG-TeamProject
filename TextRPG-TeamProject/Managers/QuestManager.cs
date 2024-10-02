using System.ComponentModel;

public static class QuestManager
{
     public static List<Quest> QuestList = new List<Quest>();

     //퀘스트 id는 중복 X
     public static void Init()
     {
         QuestList.Add(new HuntQuest(1,"몬스터 사냥",1000,"몬스터 10마리를 잡아라",3 ,"어려움"));
         QuestList[0].SetDetailedDescription("이봐! 마을 근처에 미니언들이 너무 많아졌다고 생각하지 않나?\n마을주민들의 안전을 위해서라도 저것들 수를 좀 줄여야 한다고!\n모험가인 자네가 좀 처치해주게!");
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

    public static Quest GetQuestById(int id)
    {
        return QuestList.SingleOrDefault(q => q.Id == id);
    }

    public static List<Quest> GetQuestListByStatus(QuestStatus status)
    {
        return QuestList.Where(q => q.Status == status).ToList();
    }
}