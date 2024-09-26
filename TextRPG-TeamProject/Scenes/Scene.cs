using System;

abstract class Scene
{
    public virtual void Start () { }

    public virtual void Update () { }

    public abstract Scene GetNextScene ();
}
