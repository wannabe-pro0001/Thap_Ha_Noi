using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ThapHN
{
    class Program
    {
        public static CustomStack RodA = new CustomStack();
        public static CustomStack RodB = new CustomStack();
        public static CustomStack RodC = new CustomStack();

        public static void Rule()
        {
            Console.WriteLine("Luật chơi: ");
            Console.WriteLine("\t+Mục tiêu di chuyển các con số từ cột đầu tiên qua hộp thứ ba.");
            Console.WriteLine("\t+Nếu tất cả các con số nằm bên cột thứ ba, Bạn thắng!");
            Console.WriteLine("\t+Mỗi lần chỉ di chuyển được một số và số bé hơn phải nằm trên số lớn.");
            Console.WriteLine("\t+Các cột phải có số lớn dần từ trên xuống");
        }
        public static void HowToPlay()
        {
            Console.WriteLine("Nhấn các số và kí tự sau để chơi: ");
            Console.WriteLine("\t0: Bắt đầu một game mới");
            Console.WriteLine("\t1: Thực hiện di chuyển số giữa các cột.");
            Console.WriteLine("\t2: Máy tự chơi (chỉ việc xem máy chơi (*O*)).");
            Console.WriteLine("\tr: Xem lại luật chơi");
            Console.WriteLine("\t?: Xem cách chơi");
        }
        public static void InitGame(ref int level, ref int count)
        {
            Console.WriteLine("_________Trò chơi tháp hà nội!____________");
            Rule();
            Console.WriteLine();
            while ((level < 2) || (level > 9))
            {
                Console.Write("Nhập độ khó cho trò chơi (2-9): ");
                level = int.Parse(Console.ReadLine());
                if ((level < 2) || (level > 9))
                    Console.WriteLine("Trò chơi chỉ chơi được từ 2 đến 9 số! Vui lòng nhập lại!");
            }
            Console.WriteLine();
            HowToPlay();

            //Init 
            count = 0;
            RodB.RemoveAll();
            RodC.RemoveAll();
            RodA.RemoveAll();
            for (int i = level; i > 0; i--)
                RodA.Push(i);           
        }
        public static void Draw(int level)
        {
            string a = " ", b = " ", c = " ";
            CustomStack A = new CustomStack(RodA);
            CustomStack B = new CustomStack(RodB);
            CustomStack C = new CustomStack(RodC);

            Console.WriteLine("****Kết quả*****");
            for (int i = level; i > 0; i--)
            {
                if (A.count >= i)
                {
                    int temp = A.Pop().data;
                    a = temp.ToString();
                }
                if (B.count >= i)
                {
                    int temp = B.Pop().data;
                    b = temp.ToString();
                }
                if (C.count >= i)
                {
                    int temp = C.Pop().data;
                    c = temp.ToString();
                }
                Console.WriteLine("| {0} |\t| {1} |\t| {2} |", a, b, c);
            }
            Console.WriteLine("|___|\t|___|\t|___|");

            /* Output:
             * 
             *| 5 |   | 1 |    | 4 |
             *| 6 |   | 2 |    | 5 |
             *|___|   |___|    |___|
             */
        }
        public static bool Move(int start, int end)
        {
            if (start == 1)
            {
                if (RodA.IsEmpty())
                {
                    Console.WriteLine("Cột 1 không có số để chọn");
                    return false;
                }
                switch (end){
                    case 2:
                        {
                            //rod A -> rod B
                            if (!RodB.IsEmpty())
                                if (RodA.Top > RodB.Top)
                                {
                                    Console.WriteLine("Nước đi không hợp lệ");
                                    return false;
                                }                           
                            RodB.Push(RodA.Pop());
                            return true;
                        }
                    case 3:
                        {
                            //rod A -> rod C
                            if (!RodC.IsEmpty())
                                if (RodA.Top > RodC.Top)
                                {
                                    Console.WriteLine("Nước đi không hợp lệ");
                                    return false;
                                }
                            RodC.Push(RodA.Pop());
                            return true;
                        }
                }                 
            }
            if (start == 2)
            {
                if (RodB.IsEmpty())
                {
                    Console.WriteLine("Cột 2 không có số để chọn");
                    return false;
                }
                switch (end)
                {
                    case 1:
                        {
                            //rod B -> rod A
                            if (!RodA.IsEmpty())
                                if (RodB.Top > RodA.Top)
                                {
                                    Console.WriteLine("Nước đi không hợp lệ");
                                    return false;
                                }
                            RodA.Push(RodB.Pop());
                            return true;
                        }
                    case 3:
                        {
                            //rod B -> rod C
                            if (!RodC.IsEmpty())
                                if (RodB.Top > RodC.Top)
                                {
                                    Console.WriteLine("Nước đi không hợp lệ");
                                    return false;
                                }
                            RodC.Push(RodB.Pop());
                            return true;                            
                        }
                }
            }
            if (start == 3)
            {
                if (RodC.IsEmpty())
                {
                    Console.WriteLine("Cột 3 không có số để chọn");
                    return false;
                }
                switch (end)
                {
                    case 1:
                        {
                            //rod C -> rod A
                            if (!RodA.IsEmpty())
                                if (RodC.Top > RodA.Top)
                                {
                                    Console.WriteLine("Nước đi không hợp lệ");
                                    return false;
                                }
                            RodA.Push(RodC.Pop());
                            return true;
                        }
                    case 2:
                        {
                            //rod C -> rod B
                            if (!RodB.IsEmpty())
                                if (RodC.Top > RodB.Top)
                                {
                                    Console.WriteLine("Nước đi không hợp lệ");
                                    return false;
                                }
                            RodB.Push(RodC.Pop());
                            return true;
                        }
                }
            }
            Console.WriteLine("Nước đi bị trùng hoặc không tồn tại! Hãy thử lại");
            return false;
        }
        public static void Answer(int start, int end, int n, int level, ref int count)
        {
            //Tham khảo thuật toán đệ quy của thầy Vinh, sách CTDL và giải thuật 
            count++;
            if (n == 1)
            {
                Console.Clear();
                Move(start, end);
                Draw(level);
                Thread.Sleep(500);
            }
            else
            {
                //chuyển n-1 số sang cột trung gian 
                Answer(start, 6 - start - end, n-1, level,ref count);
                //Chuyển 1 đĩa từ cột start sang end
                Answer(start, end, 1, level, ref count);
                //Chuyển n-1 đĩa từ cột trung sang end
                Answer(6 - start - end, end, n - 1, level, ref count);
            }
        }
        public static bool WinCondition(int level)
        {
            if (RodC.count == level)
                return true;
            return false;
        }
        public static void GamePlay(int level)
        {
            int count;
            InitGame(ref level, ref count);
            while (true)
            {
                Draw(level);
                if (WinCondition(level))
                {
                    Console.WriteLine("***Tuyệt vời! w(^o^)W Bạn đã hoàn thành tháp Hà Nội cấp độ {0} ***", level);
                    Console.WriteLine("Tổng số bước thực hiện {0}", count);
                    Console.Write("Nhập lệnh thực hiện (nhấn 0 để chơi lại): ");
                }
                else
                {
                    Console.Write("Nhập lệnh thực hiện (nhấn 1 để di chuyển): ");
                }
                
                char key = char.Parse(Console.ReadLine());
                switch (key)
                {
                    case '?':
                        {
                            HowToPlay();
                            break;
                        }
                    case 'r':
                        {
                            Rule();
                            break;
                        }
                    case '0':
                        {
                            Console.Clear();
                            GamePlay(0);
                            return;
                        }
                    case '1':
                        {
                            if (!WinCondition(level))
                            {
                                Console.Write("Nhập cột muốn bắt đầu: ");
                                int start = int.Parse(Console.ReadLine());
                                Console.Write("Nhập cột đích: ");
                                int end = int.Parse(Console.ReadLine());
                                Console.Clear();
                                if(Move(start, end))
                                    count ++;
                            }
                            else
                                Console.WriteLine("Bạn đã thắng!");
                            break;
                        }
                    case '2':
                        {
                            Answer(1, 3, level, level, ref count);
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Không đọc được lệnh");
                            break;
                        }              
                }
            }
        }

        static void Main(string[] args)
        {
            try
            {
                Console.OutputEncoding = Encoding.Unicode;
                Console.InputEncoding = Encoding.Unicode;
                Console.Title = "Tháp hà nội";
                int level = 0;

                GamePlay(level);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error {0}", e);
            }
            Console.ReadLine();
        }
    }
}
