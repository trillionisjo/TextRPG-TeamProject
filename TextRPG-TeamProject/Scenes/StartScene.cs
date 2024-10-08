﻿using System;

class StartScene : Scene
{
    public override void Start ()
    {
        AudioManager.PlayAudio("main_bgm.mp3");
    }
    
    public override void Update ()
    {
        Console.Clear();
        Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.");
        Console.WriteLine("이제 전투를 시작할 수 있습니다.");

        string[] option = { "상태 보기","주점 입장", "전투 시작", "인벤토리", "상점", "퀘스트", "보물상자 상점", "수집품", "저장하기" };
        int number = UIManager.DisplaySelectionUI(option);
        HandleInput(number);
    }
    
    private void HandleInput(int selectedNumber)
    {
        switch (selectedNumber)
        {
        case 1:
            NextScene = new PlayerState();
            break;
                
        case 2:
            NextScene = new PubScene();
            break;
        case 3:
            NextScene = new DungeonScene();
            break;
        case 4:
            NextScene = new InventoryScene();
            break;
        case 5:
            NextScene = new ShopScene();
            break;
        case 6:
            NextScene = new QuestScene();
            break;
        case 7:
            NextScene = new ChestShopScene();
            break;
        case 8:
            NextScene = new CollectionGalleryScene();
            break;
        case 9:
            SaveGame();
            break;
        default:
            break;
        }
    }

    private void SaveGame ()
    {
        SaveManager.SaveGame();
        UIManager.AlignTextCenter("저장을 완료하였습니다");
        Console.ReadKey(true);
    }

}