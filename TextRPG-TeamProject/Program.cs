
using TextRPG_TeamProject.Scenes;

public class Program
{
    static void Main (string[] args)
    {
        GameData.InitDatas();

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