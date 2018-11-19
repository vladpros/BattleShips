using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson5
{

    struct Map
    {
        public char[,] ship;
        public char[,] radar;
    }

    struct Coord
    {
        public int x;
        public int y;
    }

    class Program
    {
        static void Main(string[] args)
        {
            Map user;
            user.ship = CreatUserMap();
            user.radar = Radar();
            ShowMap(user.ship);

            Map comp;
            comp.ship = CreatCompMap();
            comp.radar = Radar();
            ShowMap(comp.ship);

            Game(comp, user);
        }


        private static char[,] CreatUserMap()
        {
            char[,] ships = new char[10, 10];
            ReadFromFile(ships);
            EditMap(ships);

            return ships;
        }

        private static char[,] ReadFromFile(char[,] ships)
        {
            var sr = new StreamReader(@"C:\Users\Vladislav\source\repos\Lesson5\Lesson5\Map.txt", System.Text.Encoding.Default);
            for (int i = 0; i < 10; i++)
            {
                string textFromFile = sr.ReadLine();
                for (int j = 0; j < 10; j++)
                {
                    ships[i, j] = Convert.ToChar(textFromFile[j]);
                }
            }

            return ships;
        }

        private static char[,] EditMap(char[,] map)
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (map[i, j] == '0')
                    {
                        map[i, j] = '*';
                    }
                }
            }

            return map;
        }

        private static void ShowMap(char[,] map)
        {
            Console.Write("   ");
            for (int i = 0; i < 10; i++)
            {
                Console.Write((char)(65 + i) + " ");
            }
            Console.WriteLine('\n');
            for (int i = 0; i < 9; i++)
            {
                Console.Write(i + 1 + "  ");
                for (int j = 0; j < 10; j++)
                {
                    Console.Write(map[i, j]);
                    Console.Write(" ");
                }
                Console.WriteLine();
            }
            Console.Write("10 ");
            for (int j = 0; j < 10; j++)
            {
                Console.Write(map[9, j]);
                Console.Write(" ");
            }
            Console.WriteLine('\n');
        }

        private static char[,] CreatCompMap()
        {
            char[,] ships = new char[10, 10];
            EditMapComp(ships);
            CreatFourShip(ships);
            CreatThShip(ships);
            CreatDoubleShip(ships);
            CreatOnesShip(ships);

            return ships;
        }

        private static char[,] CreatFourShip(char[,] map)
        {
            var random = new Random();
            int x = random.Next(0, 10);
            int y = random.Next(0, 10);
            int k = random.Next(1, 3);
            while (true)
            {
                if (k == 1)
                {
                    if (x + 3 < 10)
                    {
                        for (int i = 0; i < 4; i++)
                            map[x + i, y] = '4';

                        break;
                    }
                    else
                    {
                        x = random.Next(0, 10);
                        y = random.Next(0, 10);
                        k = random.Next(1, 3);
                    }
                }
                else if (k == 2)
                {
                    if (y + 3 < 10)
                    {
                        for (int i = 0; i < 4; i++)
                            map[x, y + i] = '4';

                        break;
                    }
                    else
                    {
                        x = random.Next(0, 10);
                        y = random.Next(0, 10);
                        k = random.Next(1, 3);
                    }
                }

            }

            return map;
        }

        private static char[,] CreatThShip(char[,] map)
        {
            var random = new Random();
            int x = random.Next(0, 10);
            int y = random.Next(0, 10);
            int k = random.Next(1, 3);

            for (int lim = 0; lim < 2; lim++)
            {
                while (true)
                {
                    if (k == 1)
                    {
                        if (x + 2 < 10 && WhoNear(map, x, y) && WhoNear(map, x + 1, y) && WhoNear(map, x + 2, y))
                        {
                            for (int i = 0; i < 3; i++)
                                map[x + i, y] = '3';

                            break;
                        }
                        else
                        {
                            x = random.Next(0, 10);
                            y = random.Next(0, 10);
                            k = random.Next(1, 3);
                        }
                    }
                    if (k == 2)
                    {
                        if (y + 2 < 10 && WhoNear(map, x, y) && WhoNear(map, x, y + 1) && WhoNear(map, x, y + 2))
                        {
                            for (int i = 0; i < 3; i++)
                                map[x, y + i] = '3';

                            break;
                        }
                        else
                        {
                            x = random.Next(0, 10);
                            y = random.Next(0, 10);
                            k = random.Next(1, 3);
                        }
                    }

                }
            }

            return map;
        }

        private static char[,] CreatDoubleShip(char[,] map)
        {
            var random = new Random();
            int x = random.Next(0, 10);
            int y = random.Next(0, 10);
            int k = random.Next(1, 3);

            for (int lim = 0; lim < 3; lim++)
            {
                while (true)
                {
                    if (k == 1)
                    {
                        if (x + 1 < 10 && WhoNear(map, x, y) && WhoNear(map, x + 1, y))
                        {
                            for (int i = 0; i < 2; i++)
                                map[x + i, y] = '2';

                            break;
                        }
                        else
                        {
                            x = random.Next(0, 10);
                            y = random.Next(0, 10);
                            k = random.Next(1, 3);
                        }
                    }
                    if (k == 2)
                    {
                        if (y + 1 < 10 && WhoNear(map, x, y) && WhoNear(map, x, y + 1))
                        {
                            for (int i = 0; i < 2; i++)
                                map[x, y + i] = '2';

                            break;
                        }
                        else
                        {
                            x = random.Next(0, 10);
                            y = random.Next(0, 10);
                            k = random.Next(1, 3);
                        }
                    }
                }
            }

            return map;
        }

        private static char[,] CreatOnesShip(char[,] map)
        {
            var random = new Random();
            int x = random.Next(0, 10);
            int y = random.Next(0, 10);
            int k = random.Next(1, 2);

            for (int lim = 0; lim < 4; lim++)
            {
                while (true)
                {
                    if (WhoNear(map, x, y))
                    {
                        map[x, y] = '1';

                        break;
                    }
                    else
                    {
                        x = random.Next(0, 10);
                        y = random.Next(0, 10);
                    }
                }
            }

            return map;
        }

        private static bool WhoNear(char[,] map, int x, int y)
        {
            for (int i = -1; i <= 1; i++)
            {
                if (x + i < 10 && x + i >= 0)
                {
                    for (int j = -1; j <= 1; j++)
                    {
                        if (y + j < 10 && y + j >= 0 && map[x + i, y + j] != '*')
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        private static char[,] EditMapComp(char[,] map)
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    map[i, j] = '*';
                }
            }

            return map;
        }

        private static char[,] Radar()
        {
            char[,] radar = new char[10, 10];
            EditMapComp(radar);
            return radar;
        }

        private static Map UserShoot(Map comp, Map user)
        {
            while (true)
            {
                int[] coor = EnterCoordinates();
                if (comp.radar[coor[0], coor[1]] != '*')
                {
                    Console.WriteLine("Your shoot this yet");
                    continue;
                }
                else
                {
                    if (Shoot(comp, coor[0], coor[1]))
                    {
                        ShowMap(user.ship);
                        ShowMap(comp.radar);
                        Console.WriteLine();
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            return comp;
        }

        private static bool Shoot(Map input, int x, int y)
        {
            if (input.ship[x, y] != '*')
            {
                input.radar[x, y] = '@';
                input.ship[x, y] = '@';
                DestroyNear(input, x, y);

                return true;
            }
            else
            {
                input.radar[x, y] = '0';
                input.ship[x, y] = '0';

                return false;
            }

        }

        private static Map DestroyNear(Map input, int x, int y)
        {
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (y + j >= 0 && y + j < 10 && x + i >= 0 && x + i < 10)
                    {
                        if (input.ship[x + i, y + j] == '*')
                            input.radar[x + i, y + j] = '0';
                    }
                }

            }
            return input;
        }

        private static int[] EnterCoordinates()
        {
            var cor = new int[2];
            while (true)
            {
                try
                {
                    Console.Write("Enter coordinates [x,y]= ");
                    string input = Console.ReadLine();
                    var p = input.Split(',');
                    cor[0] = Convert.ToInt32(p[0]) - 1;
                    cor[1] = (int)p[1][0] - 65;
                    Console.WriteLine();
                    if (cor[1] > 9 || cor[1] < 0 || cor[0] > 9 || cor[0] < 0)
                    {
                        Console.WriteLine("Incorrect input");
                    }
                    else
                    {
                        break;
                    }
                }
                catch
                {
                    Console.WriteLine("Incorrect input");
                }

            }

            return cor;
        }

        private static void Game(Map comp, Map user)
        {
            List<Coord> cordcomp = CreatList();
            while (true)//tut konets igru vstavit
            {
                UserShoot(comp, user);
                CompShoot(comp, user, cordcomp);
            }
        }

        private static Map CompShoot(Map comp, Map user, List<Coord> cordcomp)
        {
            while (true)
            {
                int[] coor = EnterCoordinatesComp(comp, user, cordcomp);
                if (user.radar[coor[0], coor[1]] != '*')
                {
                    Console.WriteLine("Your shoot this yet");
                    continue;
                }
                else
                {
                    if (Shoot(user, coor[0], coor[1]))
                    {
                        Console.WriteLine();
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            return user;
        }

        private static int[] EnterCoordinatesComp(Map comp,Map user, List<Coord> cordcomp)
        {
            var rand = new Random();
            int k = rand.Next(0, cordcomp.Count());
            Coord temp = cordcomp[k];
            int[] coor = {temp.x, temp.y};
            cordcomp.RemoveAt(k);

            return coor;
        }

        private static List<Coord> CreatList()
        {
            var cordcomp = new List<Coord>();
            int x = 0;
            int y = 0;
            for (int i = 0; i < 100; i++)
            {
                Coord temp;
                temp.x = x;
                temp.y = y;
                cordcomp.Add(temp);
                if (y == 9)
                {
                    x++;
                    y = 0;
                }
                y++;
            }

            return cordcomp;
        }
    }
}
