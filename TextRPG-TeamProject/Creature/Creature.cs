using System;


namespace TextRPG_TeamProject.Creature
{
    enum CreatureType
    {
        None,
        Player = 1,
        Monster = 2

    }

    class Creature
    {
        CreatureType type;
        protected int hp;
        protected int attackPower;
        protected int defensePower;
        protected string name;

        protected Creature(CreatureType type)
        {
            this.type = type;
        }

        public void SetInfo(int hp, int attackPower, int defensePower)
        {
            this.hp = hp;
            this.attackPower = attackPower;
            this.defensePower = defensePower;
        }
        public int GetHp() { return hp; }
        public bool IsDead() { return hp <= 0; }
        public int GetAttack() { return attackPower; }
        public void OnDamaged(int damage)
        {
            hp -= damage;

            if (hp < 0)
                hp = 0;
        }



    }





}
