public class HuntQuest : Quest
{
    private int targetKillCount;
    private int currentKillCount;
    
    public HuntQuest(int id, string name, int reward, string description, int targetKillCount , string difficulty) : base(id, name, reward, description)
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

    public override string GetQuestProgressText()
    {
        return $"처치 수:{Math.Min(currentKillCount,targetKillCount)}/{targetKillCount}";
    }

    public override void CancelQuest()
    {
        currentKillCount = 0;
        DungeonManager.Instance.OnKillMonster -= IncreaseTargetKillCount;
        Status = QuestStatus.NotStarted;
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
            GameData.Player.AddGold(Reward);
            return true;
        }
        return false;
    }
}