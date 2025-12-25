using System;

namespace Minesweeper;
class Tile
{
    private bool isMine;
    private int number;
    private bool isHidden;
    private bool flag;
    public Tile()
    {
        isMine = false;
        number = 0;
        isHidden = true;
        flag= false;
    }
    public void makeIntoMine()
    {
        isMine = true;
    }
    public void makeSafe()
    {
        isMine=false;
    }
    public bool isMine()
    {
        return isMine;
    }
    public int getNumber()
    {
        return number;
    }
    public void increaseNumber()
    {
        this.number++;
    }
    public void decreaseNumber()
    {
        this.number--;
    }
    public char printGood()
    {
        if (isHidden)
        {
            return '#';
        }
        if (number>0)
        {
            return (char)number;
        }
        return ' ';
    }
    public char printBad()
    {
        if (flag && isMine)
        {
            return 'f';
        }
        if (flag && !isMine)
        {
            return '!';
        }
        if (isMine)
        {
            return '*';
        }
        if (isHidden)
        {
            return '#';
        }
        if (number>0)
        {
            return (char)number;
        }
        return ' ';
    }
    public isHidden()
    {
        return isHidden;
    }
    public bool reveal()
    {
        isHidden = false;
        if (isMine)
        {
            return false;
        }
        return true;
    }
    public void flag()
    {
        flag = !flag;
    }
}