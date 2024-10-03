using Newtonsoft.Json;

public enum QuestStatus
{
    None,
    NotStarted,
    Active,
    Completed,
}
public enum QuestType
{
    None,
    Hunt,  // 사냥 퀘스트 (적 또는 특정 몬스터 사냥)
    Trade, // 거래 퀘스트 (아이템을 교환하거나 거래)
    Gather // 수집 퀘스트 (자원, 아이템 등 수집)
}



public abstract class Quest
{
    [JsonProperty] public string[] DetailedDescription { get; set; }
    [JsonProperty] public string Difficulty { get; set; }
    [JsonProperty] public QuestType Type { get; set; }
    [JsonProperty] public QuestStatus Status { get; set; }
    [JsonProperty] public int Id { get; set; }
    [JsonProperty] public string Name { get; set; }
    [JsonProperty] public int Reward { get; set; }
    [JsonProperty] public string Description { get; set; }
    
    public Quest(int id, string name , int reward ,string description)
    {
        Id = id;
        Name = name;
        Reward = reward;
        Description = description;
        Status = QuestStatus.NotStarted;
    }
    
    /// <summary>
    /// 퀘스트 시작을 위한 메서드
    /// 
    public abstract void StartQuest();
    
    /// <summary>
    /// 조건에 따라 퀘스트를 클리어 처리하고 보상을 지급하는 메서드
    /// 
    public abstract bool IsCompleteQuest();
    /// <summary>
    /// 퀘스트 진행도를 return 해주기 위한 메서드 
    /// 
    public abstract string GetQuestProgressText();

    public abstract void CancelQuest();
    public virtual void SetDetailedDescription(string[] text)
    {
        DetailedDescription = text;
    }
    public abstract void Reloaded ();
}