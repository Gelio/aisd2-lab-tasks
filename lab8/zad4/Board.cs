using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGreedyFish
{

    struct Point
    {
        public int x; //współrzędna wierszowa
        public int y; //współrzędna kolumnowa

        public Point(int x, int y)
        {
            this.x = x; //nr wiersza
            this.y = y; //nr kolumny
        }

        /// <summary>
        /// Sprawdza, czy pole istnieje
        /// </summary>
        /// <returns>
        /// true - jeśli pole istnieje
        /// false - wpp.
        /// </returns>
        public bool IsValid()
        {
            return this.x > -1 && this.y > -1;
        }

        public override string ToString()
        {
            return "Punkt: (" + x + ", " + y + ")";
        }
    }

    class Move
    {
        public Point from;
        public Point to;
        public int fish; //ile ryb było na polu "from"
        public int penguinId; //który pingwin się rusza

        public Move(Point from, Point to, int fish, int penguinId)
        {
            this.from = from;
            this.to = to;
            this.fish = fish;
            this.penguinId = penguinId;
        }

        public override string ToString()
        {
            return "Pingwin " + this.penguinId + ". Ruch z " + this.from.ToString() + " na " + this.to.ToString() + ". Ryby: " + this.fish;
        }
    }

    class Board
    {
        public int rows; //liczba wierszy
        public int columns; //liczba kolumn
        public int[,] grid; //siatka - w każdym polu jest opisana liczba ryb na krze (od 0 do 3 ryb)
        public List<Point> penguins = new List<Point>(); //lista z dostępnymi pingwinami i ich współrzędnymi (patrz plik PNG)



        public void SetBoard(int[,] grid, List<Point> penguins)
        {
            this.ClearPenguins();
            this.rows = grid.GetLength(0);
            this.columns = grid.GetLength(1);
            this.grid = grid;
            this.penguins = penguins;
        }

        public int GetGridAt(Point p)
        {
            return this.grid[p.x, p.y];
        }

        /// <summary>
        /// Metoda pobiera współrzędne sąsiedniego pola w wybranym kierunku
        /// </summary>
        /// <param name="x">Współrzędna wierszowa</param>
        /// <param name="y">Współrzędna kolumnowa</param>
        /// <param name="dir">
        /// Kierunek, w którym szukamy sąsiednie pole. Ustalono:
        /// 0 - lewo
        /// 1 - lewo-góra
        /// 2 - prawo-góra
        /// 3 - prawo
        /// 4 - prawo-dół
        /// 5 - lewo-dół
        /// (jak widać, numeracja jest zgodna z kierunkiem ruchu wskazówek zegara)
        /// </param>
        /// <returns>
        /// new Point(-1, -1) - jeśli nie ma takiego pola (jest to "dziura" albo szukane jest pole poza planszą)
        /// new Point(newX, newY) - jeśli jest takie pole (newX i newY to współrzędne sąsiada)
        /// </returns>
        public Point GetNeighbour(int x, int y, int dir)
        {
            int tmpX, tmpY;
            switch(dir)
            {
                case 0: //ruch w lewo
                    if(y > 0 && this.grid[x, y - 1] > 0 && !IsPenguinAtField(x, y - 1))
                    {
                        return new Point(x, y - 1);
                    }
                    return new Point(-1, -1);
                case 1: //ruch w lewo-góra
                    tmpX = x - 1;
                    tmpY = (x % 2) == 1 ? y : y - 1;
                    if(tmpX >= 0 && tmpY >= 0 && this.grid[tmpX, tmpY] > 0 && !IsPenguinAtField(tmpX, tmpY))
                    {
                        return new Point(tmpX, tmpY);
                    }
                    return new Point(-1, -1);
                case 2: //ruch w prawo-góra
                    tmpX = x - 1;
                    tmpY = (x % 2) == 1 ? y + 1 : y;
                    if(tmpX >= 0 && tmpY < this.columns && this.grid[tmpX, tmpY] > 0 && !IsPenguinAtField(tmpX, tmpY))
                    {
                        return new Point(tmpX, tmpY);
                    }
                    return new Point(-1, -1);
                case 3: //ruch w prawo
                    if(y < (this.columns - 1) && this.grid[x, y + 1] > 0 && !IsPenguinAtField(x, y + 1))
                    {
                        return new Point(x, y + 1);
                    }
                    return new Point(-1, -1);
                case 4: //ruch w prawo-dół
                    tmpX = x + 1;
                    tmpY = (x % 2) == 1 ? y + 1 : y;
                    if(tmpX < this.rows && tmpY < this.columns && this.grid[tmpX, tmpY] > 0 && !IsPenguinAtField(tmpX, tmpY))
                    {
                        return new Point(tmpX, tmpY);
                    }
                    return new Point(-1, -1);
                case 5: //ruch w lewo-dół
                    tmpX = x + 1;
                    tmpY = (x % 2) == 1 ? y : y - 1;
                    if(tmpX < this.rows && tmpY >= 0 && this.grid[tmpX, tmpY] > 0 && !IsPenguinAtField(tmpX, tmpY))
                    {
                        return new Point(tmpX, tmpY);
                    }
                    return new Point(-1, -1);
                default: throw new Exception("Nie ma takiego kierunku!");
            }
        }

        public void ClearPenguins()
        {
            this.penguins.Clear();
        }

        public void AddPenguinAt(int x, int y)
        {
            this.penguins.Add(new Point(x, y));
        }

        /// <summary>
        /// Sprawdza, czy na danym polu znajduje się pingwin
        /// </summary>
        /// <param name="x">współrzędna wierszowa</param>
        /// <param name="y">Współrzędna kolumnowa</param>
        /// <returns>
        /// true - jeśli na danym polu znajduje się pingwin
        /// false - jeśli nie ma pingwina na polu
        /// </returns>
        public bool IsPenguinAtField(int x, int y)
        {
            for(int i = 0; i < this.penguins.Count; ++i)
            {
                if(this.penguins[i].x == x && this.penguins[i].y == y)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Przesuwa pingwina na dane pole (jeśli to możliwe)
        /// </summary>
        /// <param name="id">Indeks pingwina</param>
        /// <param name="x">Współrzędna wierszowa</param>
        /// <param name="y">Współrzędna kolumnowa</param>
        /// <returns>
        /// W przypadku, gdy pole znajduje się w siatce i nie jest "dziurą", metoda zwraca liczbę ryb z pola, na którym stał pingwin
        /// Wpp. zwraca 0 (i pingwin się nie porusza)
        /// </returns>
        public int MovePenguin(int id, int x, int y)
        {
            if(x >= 0 && x < this.rows && y >= 0 && y < this.columns && this.grid[x, y] > 0 && !IsPenguinAtField(x, y))
            {
                Point oldField = this.penguins[id];
                int fishes = this.grid[oldField.x, oldField.y];
                this.grid[oldField.x, oldField.y] = 0;
                this.penguins[id] = new Point(x, y);
                return fishes;
            }
            return 0; //brak ruchu, więc brak zwrotu pozytywnej liczby ryb
        }

        /// <summary>
        /// Zlicza ryby na planszy
        /// </summary>
        /// <returns>Liczba oznaczająca sumę ryb na planszy</returns>
        public long AllFishes()
        {
            long fishes = 0;
            for(var i = 0; i < this.rows; ++i)
            {
                for(var j = 0; j < this.columns; ++j)
                {
                    fishes += this.grid[i, j];
                }
            }
            return fishes;
        }

        public override string ToString()
        {
            string res = "------------\nSiatka:\n";
            bool odd = true;

            for (int i = 0, iss = this.rows; i < iss; ++i)
            {
                for (int j = 0, jss = this.columns; j < jss; ++j)
                {
                    res += this.grid[i, j] + " ";
                }
                res += "\n" + (odd ? " " : "");

                odd = !odd;
            }

            res += "\nPingwiny na polach:\n";
            for(int i = 0; i < this.penguins.Count; ++i)
            {
                int x = this.penguins[i].x;
                int y = this.penguins[i].y;
                res += "Pingwin " + i + " na polu (" + x + ", " + y + "), z liczba ryb: " + this.grid[x, y].ToString() +"\n";
            }

            res += "------------\n";

            return res;
        }
    }
}
