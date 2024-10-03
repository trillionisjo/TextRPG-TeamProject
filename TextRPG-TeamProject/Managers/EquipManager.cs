static class EquipManager
{
    public static Dictionary<Slot, IEquipable> Slots;
    public static event Action<Slot, IEquipable> ItemEquippted;
    public static event Action<Slot> ItemUnequippted;

    static EquipManager()
    {
        Slots = new Dictionary<Slot, IEquipable>();
        foreach (Slot slot in Enum.GetValues(typeof(Slot)))
            Slots[slot] = null;
    }

    public static void EquipItem (IEquipable item)
    {
        UnequipItem(item.Slot);
        Slots[item.Slot] = item;
        item.ApplyStats();

        ItemEquippted?.Invoke(item.Slot, item);
    }

    public static void UnequipItem (Slot slot)
    {
        if (Slots[slot] == null)
            return;

        Slots[slot].RemoveStats();
        Slots[slot] = null;

        ItemUnequippted?.Invoke(slot);
    }

    public static void ToggleEquip (IEquipable item)
    {
        if (Slots[item.Slot] == null)
        {
            EquipItem(item);
            return;
        }

        if (Slots[item.Slot] == item)
            UnequipItem(item.Slot);
        else
            EquipItem(item);
    }

    public static bool IsEquiptedItem (IEquipable item)
    {
        if (Slots[item.Slot] == null)
            return false;
        return Slots[item.Slot].guid == item.guid;
    }
}
