using System;

interface IItem
{
    ItemId Id { get; set; }
    string Name { get; set; }
    string Desc { get; set; }
    int Price { get; set; }

    /// <summary>
    ///  해당 아이템의 능력 정보 문자열을 리턴합니다.
    /// </summary>
    /// <returns>
    /// <para>예시) "공격력 +5"</para>
    /// <para>예시) "방여력 +3"</para>
    /// <para>예시) "치유력 +50"</para>
    /// </returns>
    string StatInfo { get; }
}
