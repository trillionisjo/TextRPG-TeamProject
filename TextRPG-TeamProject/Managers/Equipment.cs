using System;

static class Equipment
{
    public static Weapon EquiptedWeapon { get; set; } = null;
    public static Armor EquiptedArmor { get; set; } = null;

    public static void EquipItem(IEquipable item)
    {
        switch (item)
        {
            case Weapon weapon:
                EquipWeapon(weapon);
                break;

            case Armor armor:
                EquipArmor(armor);
                break;
        }
    }

    public static void EquipWeapon(Weapon weapon)
    {
        UnequipWeapon();
        weapon.ApplyStats();
        EquiptedWeapon = weapon;
    }

    public static void EquipArmor(Armor armor)
    {
        UnequipArmor();
        armor.ApplyStats();
        EquiptedArmor = armor;
    }

    public static void ToggleItem(IEquipable item)
    {
        switch (item)
        {
            case Weapon weapon:
                ToggleWeapon(weapon);
                break;

            case Armor armor:
                ToggleArmor(armor);
                break;
        }
    }

    public static void ToggleWeapon(Weapon weapon)
    {
        if (EquiptedWeapon == null)
        {
            EquipWeapon(weapon);
            return;
        }

        if (EquiptedWeapon.Id == weapon.Id)
            UnequipWeapon();
        else
            EquipWeapon(weapon);
    }

    public static void ToggleArmor(Armor armor)
    {
        if (EquiptedArmor == null)
        {
            EquipArmor(armor);
            return;
        }

        if (EquiptedArmor.Id == armor.Id)
            UnequipArmor();
        else
            EquipArmor(armor);
    }

    public static void UnequipWeapon()
    {
        EquiptedWeapon?.RemoveStats();
        EquiptedWeapon = null;
    }

    public static void UnequipArmor()
    {
        EquiptedArmor?.RemoveStats();
        EquiptedArmor = null;
    }

    public static bool IsEquiptedItem(IEquipable item)
    {
        switch (item)
        {
            case Weapon weapon:
                return IsEquiptedWeapon(weapon);

            case Armor armor:
                return IsEquiptedArmor(armor);
        }

        return false;
    }

    public static bool IsEquiptedWeapon(Weapon weapon)
    {
        if (EquiptedWeapon == null)
            return false;

        if (EquiptedWeapon.Id == weapon.Id)
            return true;

        return false;
    }

    public static bool IsEquiptedArmor(Armor armor)
    {
        if (EquiptedArmor == null)
            return false;

        if (EquiptedArmor.Id == armor.Id)
            return true;

        return false;
    }
}