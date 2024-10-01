using System;

enum Slot
{
    Hand,
    Body
}

interface IEquipable : IItem
{
    Slot Slot { get; }
    void ApplyStats ();
    void RemoveStats ();
}
