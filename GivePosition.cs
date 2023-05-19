using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GivePosition : MonoBehaviour
{
    private int r, c;

    private string type;

    public void give(int row, int col)
    {
        //sets row and col
        r = row;
        c = col;
    }

    public void setType(string t)
    {
        type = t;
    }

    public string getType()
    {
        return type;
    }

    public int getRow()
    {
        return r;
    }
    public int getCol()
    {
        return c;
    }
}
