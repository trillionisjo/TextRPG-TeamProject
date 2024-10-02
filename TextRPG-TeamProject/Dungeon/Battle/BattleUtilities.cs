class BattleUtilities
{
    private const double MissChance = 0.10f;
    private readonly Random rand = new Random();


    public ISkill GetSkillByType(PlayerType playerType)
    {
        ISkill skill;

        switch ((int)playerType)
        {
            case 1:
                skill = new Knight();
                break;
            case 2:
                skill = new Mage();
                break;
            case 3:
                skill = new Archer();
                break;
            case 4:
                skill = new Rogue();
                break;
            default:
                skill = new Mage();
                break;
        }

        return skill;
    }

    public void CalculateSkillDamage(AttackType type, ref int damage)
    {
        switch (type)
        {
            case AttackType.Critical:
                damage = (int)(damage * GameData.Player.CritDmgPct);
                break;
            case AttackType.Normal:
                break;
        }
    }

    public int CalculateDamage(AttackType type, Creature attacker, Creature target)
    {
        int damage = 0;

        switch (type)
        {
            case AttackType.Miss:
                break;
            case AttackType.Critical:
                damage = (int)Math.Round(attacker.GetTotalAttackPower() * attacker.CritDmgPct);
                break;
            case AttackType.Normal:
                damage = attacker.GetTotalAttackPower();
                break;
        }

        int totalDamage = damage - target.GetTotalDefensePower();

        if (totalDamage < 1)
            totalDamage = 1;

        return type == AttackType.Miss ? 0 : totalDamage;
    }


    public AttackType GetAttackOutcome(bool isSkill = false)
    {
        double chance = rand.NextDouble();

        // 1. 회피 판정
        if (!isSkill && chance < MissChance)
            return AttackType.Miss;

        // 2. 치명타 판정
        if (chance < GameData.Player.CriticalChance)
            return AttackType.Critical;

        // 3. 정타 (Normal) 판정
        return AttackType.Normal;
    }
}