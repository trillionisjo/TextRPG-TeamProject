using System;

class StartScene : Scene
{
    public override void Start ()
    {
        Console.Clear();
    }

    public override void Update ()
    {
        Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.");
        Console.WriteLine("이제 전투를 시작할 수 있습니다.");

        string []option = {"상태 보기","전투 시작"};
        DisplaySelectionUI(option);



    private void HandleInput()
    {
        string input = Console.ReadLine() ?? string.Empty;
        int.TryParse(input, out var number);

        switch (number)
        {
        case 1:
            NextScene = new PlayerState();
            break;

        default:
            break;
        }
    }


}
 //선택지 UI를 제공, 선택된 값 반환
 public int DisplaySelectionUI(string[] options)
 {

     int selectNum = 0;
//선택값을 저장하기 위한 변수 
     int cursorPosY = (int)(Console.WindowHeight * 0.7); //
     int selectCursorPosY = cursorPosY + 1;
     int previousCursorPosY = selectCursorPosY; // 이전 커서 위치 저장
     bool isSelecting = true;

     while (isSelecting)
     {

         // 옵션 출력
         for (int i = 0; i <= options.Length; i++)
         {
             Console.SetCursorPosition(1, cursorPosY + i);

             if (i == 0)
             {
                 // 상단 경계선 그리기
                 for (int j = 0; j < Console.WindowWidth - 1; j++)
                 {
                     Console.Write("-");
                 }
             }
             else
             {
                 Console.Write(options[i - 1]);  // 옵션 출력
             }
         }

         Console.CursorVisible = false; //콘솔창 커서 숨기기 


         // 이전 커서 위치의 '▶' 지우기
         Console.SetCursorPosition(0, previousCursorPosY);
         Console.Write(" ");  // 공백으로 커서를 지움


         //콘솔 좌표 설정
         if (selectCursorPosY < cursorPosY + 1)
             selectCursorPosY = cursorPosY + options.Length;
         else if (selectCursorPosY > cursorPosY + options.Length)
             selectCursorPosY = cursorPosY + 1;

         // 새 위치에 '▶' 출력
         Console.SetCursorPosition(0, selectCursorPosY);
         Console.Write('▶');

         //사용자 입력처리 
         ConsoleKeyInfo input = Console.ReadKey(true);
         switch (input.Key)
         {
             case ConsoleKey.UpArrow:
             case ConsoleKey.LeftArrow:
                 previousCursorPosY = selectCursorPosY;
                 selectCursorPosY--;
                 break;
             case ConsoleKey.RightArrow:
             case ConsoleKey.DownArrow:
                 previousCursorPosY = selectCursorPosY;
                 selectCursorPosY++;
                 break;
             case ConsoleKey.Enter:
                 selectNum = selectCursorPosY - cursorPosY;
                 isSelecting = false;
                 break;
         }

     }

     return selectNum;
 }