using System;

class PlayerState : Scene
{
    private Scene nextScene;

    public override void Start ()
    {
        Console.Clear();
    }

    public override Scene GetNextScene ()
    {
        return nextScene;
    }

    public override void Update ()
    {
        Console.WriteLine("플레이어 스테이트 신");
        Console.ReadKey(true);
    }
}
