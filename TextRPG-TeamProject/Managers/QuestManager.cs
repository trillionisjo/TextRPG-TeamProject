

using Newtonsoft.Json;

public static class QuestManager
{
    public static List<Quest> QuestList = new List<Quest>();

    public static int MaxActivateCount = 2;
    public static int CurrentActivateCount = 0;

    
    //퀘스트 id는 중복 X
    public static void Init()
    {
        QuestList.Add(new HuntQuest(1, "몬스터 토벌(초보)", 1000, "몬스터 3마리를 잡아라", 3, "쉬움"));
        QuestList[0].SetDetailedDescription(new string[]
        {
            "이봐!마을 근처에 미니언들이 너무 많아졌다고 생각하지 않나?",
            "마을주민들의 안전을 위해서라도 저것들 수를 좀 줄여야 한다고!",
            "모험가인 자네가 좀 처치해주게!"
        });
        QuestList.Add(new HuntQuest(2, "몬스터 토벌(고급)", 5000, "몬스터 7마리를 잡아라", 7, "어려움"));
        QuestList[1].SetDetailedDescription(new string[]
        {
            "최근 몬스터들이 너무 많아져서 골치 아파.",
            "몇 마리 잡는 건 효과가 없으니, 최소 7마리는 처리해주게나.",
            "이 정도는 가볍게 해낼 수 있겠지?"
        });
        QuestList.Add(new GatherQuest(3, "절약만이 살길!!", 1000, "돈을 모아 부자가 되어보자!", CollectionItem.Gold, 500,
            "쉬움"));
        QuestList[2].SetDetailedDescription(new string[]
        {
            "요즘 재정 상황이 좋지 않아...",
            "모험가라면 어설프게 돈을 모으는 게 아니라, 제대로 저축해야 하지 않겠어?",
            "최소 500골드는 모아야 부채도 청산하고, 장비도 업그레이드할 수 있을 거야.",
            "절약과 모험은 종이 한 장 차이지만, 넌 그걸 해낼 수 있을 거라고 믿어."
        });
        QuestList.Add(new GatherQuest(4, "최고의 상인", 3000, "상인의 길을 걷자!", CollectionItem.Gold, 3000, "중간"));
        QuestList[3].SetDetailedDescription(new string[]
        {
            "모험가, 돈을 벌기 위한 다른 방법이 필요하지 않은가?",
            "이 마을에서 부를 쌓기 위해선 단순한 모험만으로는 부족해. 상인의 눈과 손이 필요하지.",
            "최소한 3000골드를 모아, 투자를 통해 자산을 늘려보게. 목표는 부자가 아니라 최고의 상인이 되는 것!",
            "단순히 모험에서 얻는 돈이 아닌, 시장과 거래를 통해 새로운 수익원을 창출해보는 건 어떤가?",
            "세상은 위험하지만, 위험 속에서 기회를 찾는 자만이 진정한 부를 얻을 수 있지.",
            "자, 당신의 상인 감각을 시험해보게! 자산을 불려 최고의 상인으로 거듭나길 바란다."
        });

    }

    public static bool IsQuestTypeInProgress(QuestType type)
    {
        return QuestList.Any(q => q.Status == QuestStatus.Active && q.Type == type);
    }


  public static string[,] GetQuestTableByStatus(QuestStatus status)
{
    var questListByStatus = QuestManager.GetQuestListByStatus(status);

    if (questListByStatus.Count == 0)
        return null;

    int maxNameLength = questListByStatus.Max(q => UIManager.GetByteFromText(q.Name));
    int maxDescriptionLength = questListByStatus.Max(q => UIManager.GetByteFromText(q.Description));
    int maxDifficultyLength = questListByStatus.Max(q => UIManager.GetByteFromText(q.Difficulty));

    int nameColumnWidth = maxNameLength + 5; // 5칸 여유 추가
    int descriptionColumnWidth = maxDescriptionLength + 5; 
    int difficultyColumnWidth = maxDifficultyLength + 5;

    int rowCount = questListByStatus.Count;

    string[,] table = new string[rowCount, 1];

    
    // 퀘스트 데이터를 테이블에 채우기
    for (int i = 0; i < questListByStatus.Count; i++)
    {
        Quest quest = questListByStatus[i];

        int namePadding = nameColumnWidth - UIManager.GetByteFromText(quest.Name);
        int descriptionPadding = descriptionColumnWidth - UIManager.GetByteFromText(quest.Description);
        int difficultyPadding = difficultyColumnWidth - UIManager.GetByteFromText(quest.Difficulty);

        string name = quest.Name + new string(' ', namePadding); 
        string description = quest.Description + new string(' ', descriptionPadding); 
        string difficulty = quest.Difficulty + new string(' ', difficultyPadding); 

        table[i, 0] = $"{name}|{description}|{difficulty}";
    }

    return table;
}




    public static void ActivateQuest(int id)
    {
        foreach (var q in QuestList)
        {
            if (q.Id == id)
            {
                q.Status = QuestStatus.Active;
                q.StartQuest();
                QuestManager.CurrentActivateCount++;
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