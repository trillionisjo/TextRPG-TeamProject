using System;
using System;

public delegate void OnDeath<T>(T creature) where T : Creature;



interface IDeathNotifier<T> where T: Creature
 {

    public event OnDeath<T> OnDeath;
}

