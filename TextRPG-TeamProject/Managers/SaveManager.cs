using Newtonsoft.Json;

static class SaveManager
{
    private class SaveData
    {
        // 플레이어 관련 데이터
        [JsonProperty] public Player Player;
        [JsonProperty] public List<IItem> InventoryItems;
        [JsonProperty] public int ChestKeyCount;

        // 상점 데이터
        [JsonProperty] public List<IItem> ShopItems;

        // 던전 관련 데이터
        [JsonProperty] public int DungeonLv;
        [JsonProperty] public int HuntedMonster;
    }

    static JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
    static string path = @"save\save.txt";

    public static void SaveGame ()
    {
        var data = new SaveData();
        data.Player = GameData.Player;
        data.InventoryItems = Inventory.ItemList;
        data.ChestKeyCount = Inventory.ChestKeyCount;
        data.ShopItems = ShopData.ItemList;
        data.DungeonLv = GameData.DungeonLv;
        data.HuntedMonster = GameData.HuntedMonster;

        // 폴더 경로 가져오기
        string directory = Path.GetDirectoryName(path);

        // 폴더가 없으면 생성
        if (!Directory.Exists(directory))
            Directory.CreateDirectory(directory);

        // Json으로 저장하기
        string jsonString = JsonConvert.SerializeObject(data, settings);
        File.WriteAllText(path, jsonString);
    }

    public static bool LoadGame()
    {
        if (!File.Exists(path))
            return false;

        string jsonString = File.ReadAllText(path);
        SaveData? data = JsonConvert.DeserializeObject<SaveData>(jsonString, settings);

        if (data == null)
            return false;

        GameData.Player = data.Player;
        Inventory.ItemList = data.InventoryItems;
        Inventory.ChestKeyCount = data.ChestKeyCount;
        ShopData.ItemList = data.ShopItems;
        GameData.DungeonLv = data.DungeonLv;
        GameData.HuntedMonster = data.HuntedMonster;

        return true;
    }
}
