class Spawner
{

    public string[] MonsterName = { "스더마" , "머다자", "애서이", "부부스" };
    public string[] BossMonsterName = { "킹슬라임","고블린 정예병", "오크 주술사", "드래곤" };
    Random random = new Random();



    public List<Monster> GenerateMonstersByLevel(int level , int monsterNum)
    { 
        List<Monster> monsters = new List <Monster>();
 
        for (int i = 0; i < monsterNum; i++)
        {
            int type = random.Next(1, Math.Min(level, 5) + 1);
            MonsterGrade grade;
           
            switch (type)
            {
                case 1:
                    grade = MonsterGrade.Low;
                    break;
                case 2:
                    grade = MonsterGrade.Medium;
                    break;
                case 3:
                    grade = MonsterGrade.High;
                    break;
                case 4:
                    grade = MonsterGrade.VeryHigh;
                    break;
                case 5:
                    grade = MonsterGrade.Extreme;
                    break;
                default:
                    grade = MonsterGrade.Low;
                    break;
            }

            Monster monster = new Monster(grade);
            monster.InstanceNumber = i + 1;
            monster.Name = GetRandomMobName();
            monsters.Add(monster);
        }

        return monsters;
    }


    public string GetRandomMobName()
    {
        int index = random.Next(0, MonsterName.Length);
      
            return MonsterName[index];
 
        
    }




    //미구현
    public Monster GetBossMobByLevel(int level)
    {
        int type = random.Next(1, Math.Min(level, 5) + 1);
        MonsterGrade grade;

        switch (type)
        {
            case 1:
                grade = MonsterGrade.Low;
                break;
            case 2:
                grade = MonsterGrade.Medium;
                break;
            case 3:
                grade = MonsterGrade.High;
                break;
            case 4:
                grade = MonsterGrade.VeryHigh;
                break;
            case 5:
                grade = MonsterGrade.Extreme;
                break;
            default:
                grade = MonsterGrade.Low;
                break;
        }

        Monster eliteMob = new Monster(grade, true);
        eliteMob.Name = $"[boss]{BossMonsterName[level - 1]}";
        return eliteMob;
    }



}

