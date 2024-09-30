class Archer : Player, ISkill
{
    public string SkillName { get; set;}
    public int ManaCost { get; set; }
    public int Damage { get; set; }


    public Archer() 
    {
        SkillName = "궁수스킬";
        ManaCost = 20;
        Damage = 30;
    }

    public bool UseSkill(Player caster, Monster target)
    {
       int  previousMp = caster.MP;
        caster.MP -= ManaCost;
        int damage = Level * Damage;
        target.OnDamaged(damage);

        string[] texts =
        {
          $"{target.Name}({target.InstanceNumber})에게 {SkillName} 사용",
          $"{Damage}의 피해",
          $"MP{previousMp} -> {caster.MP}"
        };

        UIManager.AlignTextCenter(texts);
        string[] options = { "다음" };
        UIManager.DisplaySelectionUI(options);

        return true;
    }
}
