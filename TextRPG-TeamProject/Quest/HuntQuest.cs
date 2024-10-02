public class HuntQuest : Quest
{
    private int targetKillCount;
    private int currentKillCount;
    
    public HuntQuest(int id, string name, int reward, string description, int targetKillCount , string difficulty) : base(id, name, reward,
        description)
    {
        Type = QuestType.Hunt;
        this.targetKillCount = targetKillCount;
        Difficulty = difficulty;
    }
    
    public override void StartQuest()
    {
        Status = QuestStatus.Active;
        DungeonManager.Instance.OnKillMonster += IncreaseTargetKillCount;
       
    }

    public void IncreaseTargetKillCount()
    {
        currentKillCount++;
    }

    public override bool IsCompleteQuest()
    {
        if (currentKillCount >= this.targetKillCount)
        {
            currentKillCount = targetKillCount;
            DungeonManager.Instance.OnKillMonster -= IncreaseTargetKillCount;
            Status = QuestStatus.Completed;
            return true;
        }
        return false;
    }
}