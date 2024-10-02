using System.ComponentModel;

public static class QuestManager
{
     public static List<Quest> QuestList = new List<Quest>();

     //퀘스트 id는 중복 X
     public static void Init()
     {
         QuestList.Add(new HuntQuest(1,"몬스터 사냥",1000,"몬스터 3마리를 잡아라",3 ,"쉬움"));
         QuestList[0].SetDetailedDescription(new string[]{"이봐!마을 근처에 미니언들이 너무 많아졌다고 생각하지 않나?" ,"마을주민들의 안전을 위해서라도 저것들 수를 좀 줄여야 한다고!" ,"모험가인 자네가 좀 처치해주게!"});
         QuestList.Add(new HuntQuest(2,"몬스터 사냥",5000,"몬스터 7마리를 잡아라",7 ,"어려움"));
         QuestList[1].SetDetailedDescription(new string[]{"최근 몬스터들이 너무 많아져서 골치 아파.", "몇 마리 잡는 건 효과가 없으니, 최소 7마리는 처리해주게나.", "이 정도는 가볍게 해낼 수 있겠지?"});
         
         
     }

     public static bool IsQuestTypeInProgress(QuestType type)
    {
        return QuestList.Any(q => q.Status == QuestStatus.Active && q.Type == type);
    }

     
    public static string[,] GetQuestTableByStatus(QuestStatus status)
    {
        var questListByStatus = QuestManager.GetQuestListByStatus(status);
       
        if (questListByStatus.Count != 0)
        {
            string[,] table = new string[questListByStatus.Count, 3];

            for (int i = 0; i < questListByStatus.Count; i++)
            {
                Quest quest = questListByStatus[i];
                table[i, 0] = $"- {quest.Name}";
                table[i, 1] = quest.Description;
                table[i, 2] = $"{quest.Reward}G";
            }

            return table;
        }

        return null;
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