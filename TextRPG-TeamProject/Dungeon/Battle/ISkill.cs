interface ISkill
{
    public string SkillName { get; set; }
    public int Damage { get; set; } 
    public int ManaCost { get; set;}

    public int UseSkill(Player caster,Monster target);
}
