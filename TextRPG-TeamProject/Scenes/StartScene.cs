﻿using System;

class StartScene : Scene
{
    public override void Start ()
    {
        Console.Clear();
        AudioManager.PlayAudio("main_bgm.mp3");
    }


    public override void Update ()
    {
        Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.");
        Console.WriteLine("이제 전투를 시작할 수 있습니다.");

        string[] option = { "상태 보기", "전투 시작", "인벤토리" };
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
            NextScene = new DungeonScene();
            break;
        case 3:
            NextScene = new InventoryScene();
            break;
        default:
            break;
        }
    }


}