using System;

abstract class Scene
{

    protected Scene nextScene;

    public virtual void Start () { }

    public virtual void Update () { }

    public Scene()
    {
        nextScene = this; 
    }


    public abstract Scene GetNextScene ();
}
