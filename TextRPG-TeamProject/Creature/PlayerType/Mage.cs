class Mage : Player, ISkill
{
    public string SkillName { get; set;}
    public int ManaCost { get; set; }
    public int Damage { get; set; }


    public Mage() 
    {
        SkillName = "법사스킬";
        ManaCost = 30;
        Damage = 50;
    }


    public int UseSkill(Player caster, Monster target)
    {
        caster.MP -= ManaCost;
        int damage = Level * Damage;

        return damage;
    }
}
