using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Board : MonoBehaviour
{
    private bool tileColorChooser = true;

    private int[,] board;


    public int setRange(int size, int pieceRange)
    {
        return (pieceRange < (size/2)-1) ? (size/2)-1 : pieceRange;
    }

    public void setBoardSize(int size, int r)
    {
        board = new int[size, size];
        int tileColor = 0;

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                tileColor = (tileColorChooser == true) ? 10 : 0;
                tileColorChooser = !tileColorChooser;
                board[i,j] = tileColor;

                ChoosePieceColor(i, j, size, r);
            }
            tileColorChooser = !tileColorChooser;
        }
    }

    private void ChoosePieceColor(int r, int c, int len, int range)
    {
        if (board[r,c]/10 == 1)
        {
            if (r < range){
                setPieceColor(r, c, 1);
            }

            if (r >= len - range){
                setPieceColor(r, c, 2);
            }
        }
    }


    public void setPieceColor(int row, int col, int eger)
    {
        board[row, col] = 10+eger;
    }

    public void kill(int row, int col, GameObject gb)
    {
        setPieceColor(row, col, 0);

        Destroy(gb);
    }



    public int getTileColor(int row, int col)
    {
        return board[row, col]/10;
    }

    public int getCheckerColor(int row, int col)
    {
        return board[row, col] % 10;
    }

    public int[,] getBoard()
    {
        return board;
    }
}