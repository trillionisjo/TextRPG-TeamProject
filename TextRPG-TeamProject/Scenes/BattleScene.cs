using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace TextRPG_TeamProject.Scenes
{
    class BattleScene : Scene
    {
        private Monster[] monsters;
        int selectNum = 0;
        bool isPlayerTurn = true;
        Player player = GameData.player;
        int playerPreviousHP;

        public BattleScene(Monster[] monsters)
        {
            this.monsters = monsters;
        }

        public override Scene GetNextScene()
        {
            return nextScene;
        }

        public override void Start()
        {
            Console.Clear();

        }


        public override void Update()
        {
            Console.Clear();
            UIManager.TitleBox("Battle");
            string[] options = GetMonsterOptions();
            selectNum = UIManager.DisplaySelectionUI(options);

            if (selectNum != 0)
            {
                isPlayerTurn = true;
                StartBattlePhase(monsters[selectNum - 1]);
            }
        }




        public void StartBattlePhase(Monster monster)
        {
            selectNum = 0;

            Console.Clear();
            UIManager.TitleBox("Battle");


            //플레이어 턴 
            string[] texts = CreateBattleText(monster);
            UIManager.AlignTextCenter(texts);
            string[] options = { "다음" };
            UIManager.DisplaySelectionUI(options);


            //몬스터턴 
            for (int i = 0; i < monsters.Length; i++)
            {              
                texts = CreateBattleText(monsters[i]);
                UIManager.AlignTextCenter(texts);
                UIManager.DisplaySelectionUI(options);

            }


        }


        public string[] CreateBattleText(Monster monster)
        {
            int damage;
            int previousHP;
            string[] texts;
            string[] options = { "다음" };


            if (isPlayerTurn)
            {
                damage = GameData.player.AttackPower;
                previousHP = monster.HP;
                monster.OnDamaged(damage);
                texts = new string[]
                    {
                     $"{GameData.player.Name}의 공격",
                     $"Lv.{monster.Level} {monster.Name} 을(를) 맞췄습니다. [데미지 : {damage}]",
                     $"{previousHP}->{(monster.HP > 0 ? monster.HP.ToString() : "Dead")}"
                    };

                if (player.IsDead)
                {
                    Console.Clear();
                    UIManager.TitleBox("Battle!! - Result");
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine("You Lose");
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine($"Lv.{player.Level} {player.Name}");
                    Console.WriteLine($"HP{playerPreviousHP}->{player.HP}");
                    UIManager.DisplaySelectionUI(options);
                    Environment.Exit(0);
                }

                isPlayerTurn = !isPlayerTurn;


                return texts;
            }

            else
            {
                damage = monster.AttackPower;
                playerPreviousHP = player.HP;
                player.OnDamaged(damage);
                texts = new string[]
                    {
                     $"{monster.Name}의 공격",
                     $"Lv.{player.Level} {player.Name} 을(를) 맞췄습니다. [데미지 : {damage}]",
                     $"{playerPreviousHP}->{(player.HP > 0 ? player.HP.ToString() : "Dead")}"
                    };
                isPlayerTurn = !isPlayerTurn;

                return texts;
            }

            return null;

        }



        public string[] GetMonsterOptions()
        {
            string[] options = new string[monsters.Length];
            for (int i = 0; i < options.Length; i++)
            {
                options[i] = $"{i + 1} {monsters[i].Name} ";

                Console.WriteLine($"{i + 1} Lv.{monsters[i].Level} {monsters[i].Name} HP {monsters[i].HP} ");
            }
            return options;
        }







    }
}
