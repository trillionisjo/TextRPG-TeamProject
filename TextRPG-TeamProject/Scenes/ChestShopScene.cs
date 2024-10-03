using System;

class ChestShopScene : Scene
{
    enum ChestReward
    {
        Nothing,
        Gold1000,
        Gold5000,
        MountainCard,
        CherryCard,
        FairyCard,
        CatInterruptionCard,
    }

    private string warningMessage = null;
    private int keyPrice = 50;
    private Point chest;
    private int[] weights;

    public override void Start ()
    {
        chest.x = 35;
        chest.y = 7;

        weights = new int[Enum.GetValues<ChestReward>().Length];
        weights[(int)ChestReward.Nothing] = 25;
        weights[(int)ChestReward.Gold1000] = 20;
        weights[(int)ChestReward.Gold5000] = 15;
        weights[(int)ChestReward.MountainCard] = 10;
        weights[(int)ChestReward.CherryCard] = 10;
        weights[(int)ChestReward.FairyCard] = 10;
        weights[(int)ChestReward.CatInterruptionCard] = 10;
    }

    public override void Update ()
    {
        Console.Clear();
        Console.WriteLine("보물상자 상점");
        Console.WriteLine("수상한 아저씨 : 열쇠가 있으면 상자를 열 수 있는 기회가 주어지는데 어떤가?");
        WriteWarningMessage();
        Console.WriteLine();

        Console.WriteLine("[보유 골드]");
        Console.WriteLine($" {GameData.Player.Gold} G");
        Console.WriteLine();

        Console.WriteLine("[보유 열쇠]");
        Console.WriteLine($" {Inventory.ChestKeyCount} 개");

        AsciiArts.Draw(AsciiArts.ChestClosed2, chest.x, chest.y);

        string[] options = {$"열쇠 사기 ({keyPrice} G)", $"상자 자물쇠 열기", "나가기" };
        int selectedNumber = UIManager.DisplaySelectionUI(options);
        HandleInput(selectedNumber);
    }

    private void HandleInput(int selectedNumber)
    {
        switch (selectedNumber)
        {
        case 1:
            PurchaseKey();
            break;

        case 2:
            TryOpenChest();
            break;

        case 3:
            NextScene = new StartScene();
            break;
        }
    }

    private void PurchaseKey()
    {
        if (GameData.Player.Gold < keyPrice)
        {
            AudioManager.PlayOneShot(AudioName.voiceNotEnoughtGold);
            warningMessage = "돈을 내놔야지!";
            return;
        }

        AudioManager.PlayOneShot(AudioName.sfxKey);
        GameData.Player.SpendGold(keyPrice);
        Inventory.ChestKeyCount += 1; 
    }

    private void TryOpenChest()
    {
        if (Inventory.ChestKeyCount <= 0)
        {
            AudioManager.PlayOneShot(AudioName.voiceNotEnoughKey);
            warningMessage = "이 상자는 손으로는 열수 없네.";
            return;
        }

        warningMessage = null;
        Inventory.ChestKeyCount -= 1;
        UnlockChest();
        OpenChest();
    }

    private void UnlockChest()
    {
        Console.Clear();
        AsciiArts.Draw(AsciiArts.ChestClosed2, chest.x, chest.y);
        AudioManager.PlayOneShot(AudioName.sfxUnlocking);
        Console.ReadKey(true);
    }

    private void OpenChest()
    {
        Console.Clear();

        ChestReward reward = RandomChoose();

        switch (reward)
        {
        case ChestReward.Nothing:
            AsciiArts.Draw(AsciiArts.ChestOpenedWithBlank, chest.x, chest.y);
            break;

        case ChestReward.Gold1000:
            AsciiArts.Draw(AsciiArts.ChestOpenedWith1000G, chest.x, chest.y);
            GameData.Player.AddGold(1000);
            break;

        case ChestReward.Gold5000:
            AsciiArts.Draw(AsciiArts.ChestOpenedWith5000G, chest.x, chest.y);
            GameData.Player.AddGold(5000);
            break;

        case ChestReward.MountainCard:
            AsciiArts.Draw(AsciiArts.ChestOpenedWithMountainCard, chest.x, chest.y);
            CollectionData.GetCard(CardType.Mountain).Found = true;
            break;

        case ChestReward.CherryCard:
            AsciiArts.Draw(AsciiArts.ChestOpenedWithCherryCard, chest.x, chest.y);
            CollectionData.GetCard(CardType.Cherry).Found = true;
            break;

        case ChestReward.FairyCard:
            AsciiArts.Draw(AsciiArts.ChestOpenedWithFairyCard, chest.x, chest.y);
            CollectionData.GetCard(CardType.Fairy).Found = true;
            break;

        case ChestReward.CatInterruptionCard:
            AsciiArts.Draw(AsciiArts.ChestOpenedWithCatInterruptionCard, chest.x, chest.y);
            CollectionData.GetCard(CardType.CatInterruption).Found = true;
            break;
        }

        AudioManager.PlayOneShot(AudioName.sfxOpenChest);
        Console.ReadKey(true);
    }

    private ChestReward RandomChoose ()
    {
        int totalWeight = weights.Sum();

        int num = new Random().Next(0, totalWeight);
        int sum = 0;

        for (int i = 0; i < weights.Length; i++)
        {
            sum += weights[i];
            if (num < sum)
                return (ChestReward)i;
        }

        return ChestReward.Nothing;
    }

    private void WriteWarningMessage()
    {
        if (warningMessage == null)
            return;

        Console.WriteLine($"수상한 아저씨 : {warningMessage}");
    }
}
