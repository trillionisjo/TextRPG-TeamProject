
public class Program
{
    static void Main (string[] args)
    {
        GameData.InitDatas();

        Scene currentScene = new NameInputScene();
        Scene nextScene = currentScene;
        currentScene.Start();
        QuestManager.Init();

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

