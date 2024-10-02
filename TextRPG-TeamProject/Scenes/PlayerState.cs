using System;

class PlayerState : Scene
{
    public override void Start ()
    {
        Console.Clear();
        AudioManager.PlayAudio("main_bgm.mp3");
    }

    public override void Update ()
    {   
        Console.WriteLine("플레이어 스테이트 신");
        Console.WriteLine($" 이름  : {GameData.Player.Name}");
        Console.WriteLine($" 직업  : {GameData.Player.Type.ToString()}");
        Console.WriteLine($" LeveL : {GameData.Player.Level}");
        Console.WriteLine($" H  P  : {GameData.Player.HP} / {GameData.Player.MaxHP}");
        Console.WriteLine($" M  P  : {GameData.Player.MP} / {GameData.Player.MaxMP}");
        Console.WriteLine($"공격력 : {GameData.Player.AttackPower+GameData.Player.ExtraAttackPower}({GameData.Player.AttackPower}+{GameData.Player.ExtraAttackPower})");
        Console.WriteLine($"방어력 : {GameData.Player.DefensePower+GameData.Player.ExtraDefensePower}({GameData.Player.ExtraDefensePower}+{GameData.Player.ExtraDefensePower})");
        Console.WriteLine($"소지금 : {GameData.Player.Gold} gold");
        HandleInput();
    }

    private void HandleInput()
    {
        string[] option = { "나가기" };
        int number = UIManager.DisplaySelectionUI(option);

        switch (number)
        {
            case 1:
                NextScene = new StartScene();
                break;

            default:
                break;
        }
    }
}
