﻿class Mage : Player, ISkill
{
    public string SkillName { get; set;}
    public int ManaCost { get; set; }
    public int Damage { get; set; }


    public Mage() : base(PlayerType.Mage)
    {
        SkillName = "법사스킬";
        ManaCost = 30;
        Damage = 50;
    }


    public bool UseSkill(Player caster,Monster target)
    {
        int previousMp = caster.MP;

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
