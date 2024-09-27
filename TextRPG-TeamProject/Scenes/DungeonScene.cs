using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
namespace TextRPG_TeamProject.Scenes;

class DungeonScene : Scene
{
    private Monster[] monsters = new Monster[new Random().Next(1, 5)]; // 1 ~ 4마리의 몬스터 배열 생성
    
    public int DungeonLevel { get; private set; }
    
    public DungeonScene(){
    
        DungeonLevel = 1;   
    
    }

    public DungeonScene(int dungeonLevel)
    {

        DungeonLevel = dungeonLevel;
    }



    public override void Start()
    {
        Console.Clear();
        SpawnRandomEnemy();
    }


    public override void Update()
    {
        //1 Lv.2 미니언 HP 15
        //2 Lv.5 대포미니언 HP 25 
        //3 LV.3 공허충 Dead

        //  [내정보] Lv.1  Chad(전사)
        // HP 100/100   
      
        UIManager.TitleBox("Battle!");
        for (int i = 0; i < monsters.Length; i++)
        {
            Console.WriteLine($"{i+1} Lv.{monsters[i].Level} {monsters[i].Name} HP {monsters[i].HP} ");
        }

        string[] options = { "싸운다", "도망간다" };
        int selectNum = UIManager.DisplaySelectionUI(options);

        switch (selectNum)
        {
            case 1:
                NextScene = new BattleScene(monsters);
                break;
            case 2:
                NextScene = new StartScene(); 
                break;           
        }

    }

    public void SpawnRandomEnemy()
    {
        for (int i = 0; i < monsters.Length; i++)
        {
            int type = new Random().Next(1, DungeonLevel+1);
            switch(type)
             {
                case 1:
                    monsters[i] = new Goblin();
                    break;
                case 2:
                    monsters[i] = new Kobold();
                    break;
                case 3:
                    monsters[i] = new Orc();
                    break;
            }              
        }


    }
    //선택지 UI를 제공, 선택된 값 반환

}




