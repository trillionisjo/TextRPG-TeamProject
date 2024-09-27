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
        Console.ReadKey(true);
    }
}
