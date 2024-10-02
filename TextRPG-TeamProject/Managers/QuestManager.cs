public class QuestManager
{
    public List<Quest> QuestList = new List<Quest>();

     



    public void ActivateQuest(int id)
    {
        foreach (var q in QuestList)
        {
            if (q.Id == id)
            {
                q.Status = QuestStatus.Active;
                q.CheckQuestClear();
                break;
            }
        }
    }

    
    public List<Quest> GetQuestByStatus(QuestStatus status)
    {
        return QuestList.Where(q => q.Status == status).ToList();
    }
}