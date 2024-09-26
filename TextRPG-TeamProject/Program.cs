
public class Program
{
    static void Main (string[] args)
    {
        Scene currentScene = new StartScene();
        Scene nextScene = null;
        
        while (true)
        {
            if (currentScene != nextScene && nextScene != null)
            {
                currentScene = nextScene;
                currentScene.Start();
            }
            currentScene.Update();
            nextScene = currentScene.GetNextScene();
        }
    }
}
