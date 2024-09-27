using System;

abstract class Scene
{
    protected Scene nextScene;

    public Scene()
    {
        nextScene = this;
    }

    public virtual void Start () { }

    public virtual void Update () { }

    public abstract Scene GetNextScene ();
}
