
public class Program
{
    static void Main (string[] args)
    {
        //GameData.InitDatas();  NameIputScene으로 이동
        Scene currentScene = new TitleScene();
        Scene nextScene = currentScene;
        currentScene.Start();

        while (true)
        {
            if (currentScene != nextScene)
            {

                currentScene = nextScene;
                currentScene.Start();
            }

            currentScene.Update();
            nextScene = currentScene.NextScene;

        }
    }
}

