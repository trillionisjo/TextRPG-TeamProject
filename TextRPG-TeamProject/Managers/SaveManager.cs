using Newtonsoft.Json;

enum LoadGameResult
{
    Success,
    FileNotFound,
    CorruptedData,
}


static class SaveManager
{
    private class SaveData
    {
        // 플레이어 관련 데이터
        [JsonProperty] public Player Player;
        [JsonProperty] public List<IItem> InventoryItems;
        [JsonProperty] public int ChestKeyCount;
        [JsonProperty] public Dictionary<Slot, IEquipable> Slots;

        // 상점 데이터
        [JsonProperty] public List<IItem> ShopItems;

        // 수집 데이터
        [JsonProperty] public Card[] Cards;

        // 던전 관련 데이터
        [JsonProperty] public int DungeonLv;
        [JsonProperty] public int HuntedMonster;

        // 퀘스트 관련 데이터
        [JsonProperty] public List<Quest> QuestList;
        [JsonProperty] public int MaxActivateCount;
        [JsonProperty] public int CurrentActivateCount;
    }

    static JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
    static string path = @"save\save.txt";

    public static void SaveGame ()
    {
        var data = new SaveData();
        data.Player = GameData.Player;
        data.InventoryItems = Inventory.ItemList;
        data.ChestKeyCount = Inventory.ChestKeyCount;
        data.Slots = EquipManager.Slots;
        data.ShopItems = ShopData.ItemList;
        data.Cards = CollectionData.Cards;
        data.DungeonLv = GameData.DungeonLv;
        data.HuntedMonster = GameData.HuntedMonster;
        data.QuestList = QuestManager.QuestList;
        data.MaxActivateCount = QuestManager.MaxActivateCount;
        data.CurrentActivateCount = QuestManager.CurrentActivateCount;

        // 폴더 경로 가져오기
        string directory = Path.GetDirectoryName(path);

        // 폴더가 없으면 생성
        if (!Directory.Exists(directory))
            Directory.CreateDirectory(directory);

        // Json으로 저장하기
        string jsonString = JsonConvert.SerializeObject(data, settings);
        File.WriteAllText(path, jsonString);
    }

    public static LoadGameResult LoadGame()
    {
        if (!File.Exists(path))
            return LoadGameResult.FileNotFound;

        string jsonString = File.ReadAllText(path);
        SaveData? data = JsonConvert.DeserializeObject<SaveData>(jsonString, settings);

        if (data == null)
            return LoadGameResult.CorruptedData;

        GameData.Player = data.Player;
        Inventory.ItemList = data.InventoryItems;
        Inventory.ChestKeyCount = data.ChestKeyCount;
        EquipManager.Slots = data.Slots;
        ShopData.ItemList = data.ShopItems;
        CollectionData.Cards = data.Cards;
        GameData.DungeonLv = data.DungeonLv;
        GameData.HuntedMonster = data.HuntedMonster;
        QuestManager.QuestList = data.QuestList;
        QuestManager.MaxActivateCount = data.MaxActivateCount;
        QuestManager.CurrentActivateCount = data.CurrentActivateCount;

        return LoadGameResult.Success;
    }
}
