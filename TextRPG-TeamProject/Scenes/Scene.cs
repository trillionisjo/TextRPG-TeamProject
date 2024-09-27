using System;

abstract class Scene
{
    public Scene NextScene { get; protected set; }

    public Scene()
    {
        NextScene = this;
    }

    public virtual void Start () { }

    public virtual void Update () { }
}
