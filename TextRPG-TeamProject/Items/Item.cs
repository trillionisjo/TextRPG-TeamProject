using System;

abstract class Item
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Desc { get; set; }
    public int Price { get; set; }

    /// <summary>
    ///  해당 아이템의 능력 정보 문자열을 리턴합니다.
    /// </summary>
    /// <returns>
    /// <para>예시) "공격력 5"</para>
    /// <para>예시) "방여력 3"</para>
    /// <para>예시) "치유력 50"</para>
    /// </returns>
    public abstract string StatInfo { get; }

    public Item (int id, string name, string desc, int price)
    {
        Id = id;
        Name = name;
        Desc = desc;
        Price = price;
    }
}
