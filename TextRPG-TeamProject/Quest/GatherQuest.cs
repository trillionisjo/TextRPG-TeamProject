using Newtonsoft.Json;

public enum CollectionItem
{
    None,
    Gold,
    Card
}


public class GatherQuest : Quest
{
    [JsonProperty] private CollectionItem collection;
    [JsonProperty] public int targetAmount;
    [JsonProperty] public int currentAmount;

    public GatherQuest(int id, string name, int reward, string description, CollectionItem collection, int targetAmount,
        string difficulty) : base(id, name, reward, description)
    {
        this.collection = collection;
        this.targetAmount = targetAmount;
        Type = QuestType.Gather;
        Difficulty = difficulty;
    }

    public override void StartQuest()
    {
        Status = QuestStatus.Active;
        if (CollectionItem.Gold == collection)
        {
            GameData.Player.OnAddGold += IncreaseTargetAmount;
            GameData.Player.OnSpendGold += DecreaseTargetAmount;
        }
    }

    public override string GetQuestProgressText()
    {
        if (collection == CollectionItem.Gold)
            return $"증가한 돈:{Math.Min(currentAmount, targetAmount)}/{targetAmount}";
        else
        {
            return $"수집한 카드 수:{Math.Min(currentAmount, targetAmount)}/{targetAmount}";
        }
    }

    public void DecreaseTargetAmount(int Amount)
    {
        currentAmount -= Amount;
    }

    public void IncreaseTargetAmount(int Amount)
    {
        currentAmount += Amount;
    }

    public override void CancelQuest()
    {
        if (CollectionItem.Gold == collection)
        {
            GameData.Player.OnAddGold -= IncreaseTargetAmount;
            GameData.Player.OnSpendGold -= DecreaseTargetAmount;
        }

        currentAmount = 0;
        Status = QuestStatus.NotStarted;
    }

    public override bool IsCompleteQuest()
    {
        if (targetAmount <= currentAmount)
        {
            currentAmount = targetAmount;
            if (CollectionItem.Gold == collection)
            {
                GameData.Player.OnAddGold -= IncreaseTargetAmount;
                GameData.Player.OnSpendGold -= DecreaseTargetAmount;
                return true;
            }
        }


        return false;
    }

    public override void Reloaded ()
    {
        if (Status == QuestStatus.Active)
        {
            GameData.Player.OnAddGold += IncreaseTargetAmount;
            GameData.Player.OnSpendGold += DecreaseTargetAmount;
        }
    }
}