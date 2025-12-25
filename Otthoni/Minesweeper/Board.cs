using System;

namespace Minesweeper;
class Board
{
    private Tile[,] tiles;
    private int x, y;
    private bool left;
    private int toClear;
    public Board(int x, int y)
    {
        tiles=new Tile[x,y];
        for (int i=0; i<x; i++)
        {
            for (int j=0; j<y; j++)
            {
                tiles[i][j] = new Tile();
            }
        }
        this.x = x;
        this.y = y;
        this.left= true;
        this.toClear = x*y;
    }
    public fill(int mines)
    {
        Random rnd = new Random();
        while (mines>0)
        {
            int x = rnd.Next(0, this.x);
            int y = rnd.Next(0, this.y);
            if (tiles[x][y].isMine)
            {
                continue;
            }
            tiles[x][y].makeIntoMine;
            toClear--;
            for (int i=-1; i<2; i++)
            {
                for (int j=-1; j<2; j++)
                {
                    if (x+i>=0 && x+i<this.x && y+j>=0 && y+j<this.y)
                    {
                        tiles[x + i][y + j].increaseNumber();
                    }
                }
            }
            mines--;
        }
        print();
    }
    public void print()
    {
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                Console.Write(tiles[i][j].printGood());
            }
            Console.WriteLine();
        }
    }
    public void recursive(int x, int y)
    {
        if (x >= 0 && x < this.x && y >= 0 && y < this.y)
        {
            if (!tiles[x][y].isMine() && tiles[x][y].getNumber == 0)
            {
                tiles[x][y].reveal();
                toClear--;
                recursive(x - 1, y - 1);
                recursive(x - 1, y + 1);
                recursive(x + 1, y - 1);
                recursive(x + 1, y + 1);
            }
        }
    }
    public bool leftClick(int x, int y)
    {
        if (tiles[x][y].isFlagged())
        {
            Console.WriteLine("That's flagged.");
        }
        else if (tiles[x][y].isHidden())
        {
            if (tiles[x][y].reveal();)
            {
                toClear--;
                recursive(x - 1, y - 1);
                recursive(x - 1, y + 1);
                recursive(x + 1, y - 1);
                recursive(x + 1, y + 1);
                print();
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            Console.WriteLine("Already removed!");
            return true;
        }
    }
    public void safe(int x, int y)
    {
        if (tiles[x][y].isMine())
        {
            tiles[x][y].makeSafe();
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    if (x + i >= 0 && x + i < this.x && y + j >= 0 && y + j < this.y)
                    {
                        tiles[x + i][y + j].decreaseNumber();
                    }
                }
            }
            Random rnd = new Random();
            while (true)
            {
                int nx = rnd.Next(0, this.x);
                int ny = rnd.Next(0, this.y);
                if ((nx==x && ny==y) || tiles[nx][ny].isMine)
                {
                    continue;
                }
                tiles[x][y].makeIntoMine;
                for (int i = -1; i < 2; i++)
                {
                    for (int j = -1; j < 2; j++)
                    {
                        if (nx + i >= 0 && nx + i < this.x && ny + j >= 0 && ny + j < this.y)
                        {
                            tiles[nx + i][ny + j].increaseNumber();
                        }
                    }
                }
                break;
            }
        }
    }
    public void rightClick(int x, int y)
    {
        if (tiles[x][y].isHidden())
        {
            tiles[x][y].flag();
            print();
        }
        else
        {
            Console.WriteLine("Already removed!");
        }
    }
    public void gameOver()
    {
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                Console.Write(tiles[i][j].printBad());
            }
            Console.WriteLine();
        }
        Console.WriteLine();
        Console.WriteLine("xx(");
        Console.WriteLine();
    }
    public void play()
    {
        print();
        while (true)
        {
            Console.Write(">");
            string? command = Console.ReadLine();
            try
            {
                string[] coo = command.Split(' ');
                if (coo.Length != 2)
                {
                    throw new Exception();
                }
                safe(coo[0], coo[1]);
                break;
            }
            catch (Exception e)
            {
                Console.WriteLine("Try typing that again!");
            }
        }
        while (true)
        {
            if (left)
            {
                Console.WriteLine("(mining)");
            }
            else
            {
                Console.WriteLine("(flagging)");
            }
            Console.Write(">");
            string? command = Console.ReadLine();
            if (command.Trim=="s")
            {
                left = !left;
                continue;
            }
            try
            {
                string[] coo = command.Split(' ');
                if (coo.Length != 2)
                {
                    throw new Exception();
                }
                if (left)
                {
                    if (!leftClick(coo[0], coo[1]))
                    {
                        gameOver();
                        break;
                    }
                    if (toClear==0)
                    {
                        Console.WriteLine();
                        Console.WriteLine("B)");
                        Console.WriteLine();
                        break;
                    }
                }
                else
                {
                    rightClick(coo[0], coo[1]);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Try typing that again!");
            }
        }
    }
}