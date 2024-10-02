public enum QuestStatus
{
    None,
    NotStarted,
    Active,
    Completed,
}




public abstract class Quest
{
 
    public QuestStatus Status { get; set; }
    public int Id { get; set; }
    public string Name { get; set; }
    public int Reward { get; set; }


    public abstract void CheckQuestClear();
    
    
}