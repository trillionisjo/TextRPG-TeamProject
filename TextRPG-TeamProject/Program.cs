
using TextRPG_TeamProject.Scenes;

public class Program
{
    static void Main (string[] args)
    {
        Scene currentScene = new DungeonScene();
        Scene nextScene = currentScene;
        currentScene.Start();
        GameData.InitDatas();

        while (true)
        {
            if (currentScene != nextScene )
           {
                currentScene = nextScene;
                currentScene.Start();
            }

            currentScene.Update();
            nextScene = currentScene.GetNextScene();
        }
    }
}
