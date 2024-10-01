interface IEventHandler
{
    public void Invoke ();
}

class NextSceneEvent : IEventHandler
{
    private Scene currentScene;
    private Scene nextScene;
    
    public NextSceneEvent(Scene currentScene, Scene nextScene)
    {
        this.currentScene = currentScene;
        this.nextScene = nextScene;
    }

    public void Invoke ()
    {
        currentScene.NextScene = nextScene;
    }
}
