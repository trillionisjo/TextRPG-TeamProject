using System;

class PlayerState : Scene
{
    private Scene nextScene;

    public override void Start ()
    {
        nextScene = this;
        Console.Clear();
    }

    public override Scene GetNextScene ()
    {
        return nextScene;
    }

    public override void Update ()
    {
        Console.WriteLine("플레이어 스테이트 신");
        Console.WriteLine($" 직업  : {GameData.Player.Type.ToString()}");
        Console.WriteLine($" H  P  : {GameData.Player.HP}");
        Console.WriteLine($"공격력 : {GameData.Player.AttackPower}");
        Console.WriteLine($"방어력 : {GameData.Player.DefensePower}");
        Console.WriteLine("\n\n나가기 : 0");
        int outKey = int.Parse(Console.ReadLine());
        if (outKey==0)
        {
            nextScene=new StartScene();
        }
    }
}
