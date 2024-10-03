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
        UIManager.TitleBox("    캐릭터 정보    ");
        // 테이블 형식으로 출력해야 할 것 같음.
        string[] playerStateInfo = 
        {
            $" 이 름  : {GameData.Player.Name}",
            $" 직 업  : {GameData.Player.Type.ToString()}",
            $" L  V  : {GameData.Player.Level}",
            $" H  P  : {GameData.Player.HP}",
            $" M  P  : {GameData.Player.MP}",
            $" 공격력 : {GameData.Player.AttackPower}",
            $" 방어력 : {GameData.Player.DefensePower}",
            $" 소지금 : {GameData.Player.Gold}"
        };


        foreach (var p in playerStateInfo)
        {
            Console.WriteLine(p);
        }
        
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
