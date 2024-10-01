using System;

class PlayerState : Scene
{
    public override void Start ()
    {
        Console.Clear();
    }

    public override void Update ()
    {   
        Console.WriteLine("플레이어 스테이트 신");
        Console.WriteLine($" 직업  : {GameData.Player.Type.ToString()}");
        Console.WriteLine($" H  P  : {GameData.Player.HP}");
        Console.WriteLine($"공격력 : {GameData.Player.AttackPower}");
        Console.WriteLine($"방어력 : {GameData.Player.DefensePower}");

        UIManager.DisplaySelectionUI(new string[] { "나가기" });
        




    }
}
