interface ISkill
{
    public string SkillName { get; set; }
    public int Damage { get; set; } 
    public int ManaCost { get; set;}

    public bool UseSkill(Player caster,Monster target);
}
