using System;

abstract class Scene
{
    public Scene NextScene { get; set; }

    public Scene()
    {
        NextScene = this;
    }

    public virtual void Start () { }

    public virtual void Update () { }
}
