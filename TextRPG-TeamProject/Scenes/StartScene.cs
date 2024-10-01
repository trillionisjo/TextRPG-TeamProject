﻿using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        HandleInput();
    }


    private void HandleInput()
    {
        string[] option = { "상태 보기", "던전 입장" ,"인벤토리" };
        int number = UIManager.DisplaySelectionUI(option);

        switch (number)
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
            

        }
    }

}