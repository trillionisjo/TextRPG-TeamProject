using System;

enum ItemId
{
    // Armors
    TraineesArmor   = 101,
    IronArmor       = 102,
    SpartanArmor    = 103,

    // Weapons
    WornSword       = 201,
    BronzeAxe       = 202,
    SpartanSpear    = 203,
}

static class ItemManager
{
    public static Item Instantiate (ItemId id)
    {
        switch (id)
        {
        case ItemId.TraineesArmor:
            return new Armor(id, "수련자 갑옷", "수련에 도움을 주는 갑옷입니다", 1000, 5);

        case ItemId.IronArmor:
            return new Armor(id, "무쇠갑옷", "무쇠로 만들어져 튼튼한 갑옷입니다", 2000, 9);

        case ItemId.SpartanArmor:
            return new Armor(id, "스파르타의 갑옷", "스파르타의 전사들이 사용했다는 전설의 갑옷입니다", 3500, 15);

        case ItemId.WornSword:
            return new Weapon(id, "낡은 검", "어디선가 사용됐던거 같은 도끼입니다", 600, 2);

        case ItemId.BronzeAxe:
            return new Weapon(id, "청동도끼", "쉽게 볼 수 있는 낡은 검 입니다", 1500, 5);

        case ItemId.SpartanSpear:
            return new Weapon(id, "스파르타의 창", "스파르타의 전사들이 사용했다는 전설의 창입니다", 3000, 7);
        }

        return null;
    }
}
