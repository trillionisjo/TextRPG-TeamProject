class Knight : Player, ISkill
{
    public string SkillName { get; set;}
    public int ManaCost { get; set; }
    public int Damage { get; set; }


    public Knight()
    {
        SkillName = "전사스킬";
        ManaCost = 20;
        Damage = 30;
    }


    public int UseSkill(Player caster, Monster target)
    {
        caster.MP -= ManaCost;
        int damage = Level * Damage;

        return damage;
    }
}
