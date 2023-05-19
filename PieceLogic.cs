using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PieceLogic : MonoBehaviour
{
    //false is black, true is red
    private bool currentPlayer = false;
    public bool shouldForfeit;

    public int count;

    //1 is red, 2 is black
    public int whichGuy()
    {
        return (currentPlayer)? 1 : 2;
    }
    public int whichGuy(bool torf)
    {
        return (torf) ? 1 : 2;
    }

    public void changeTurn()
    {
        currentPlayer = !currentPlayer;
        count++;
    }

    public bool getTurn()
    {
        return currentPlayer;
    }


    public bool shouldSelect(int row, int col, int color, GameObject selected)
    {
        if (whichGuy() == color && (selected.GetComponent<GivePosition>().getType().Equals("checker") || selected.GetComponent<GivePosition>().getType().Equals("king")))
        {
            return true;
        }

        return false;
    }

    public void select(GameObject selected, float scale)
    {
        selected.transform.localScale = new Vector3(2f + scale, 2f + scale, 1);
    }


    public bool canJump(int row, int col, Board board, PrintBoard printedBoard)
    {
        bool toReturn = false;
        //black
        if (!currentPlayer || (printedBoard.findCheckerAt(row, col) != null && printedBoard.findCheckerAt(row, col).GetComponent<GivePosition>().getType().Equals("king")))
        {
            toReturn = (inBounds(row, col, -2, -2, printedBoard.size) && board.getCheckerColor(row - 2, col - 2) == 0 && canMove(row - 2, col - 2, row, col, board, printedBoard)) ? true : toReturn;
            toReturn = (inBounds(row, col, -2, 2, printedBoard.size) && board.getCheckerColor(row - 2, col + 2) == 0 && canMove(row - 2, col + 2, row, col, board, printedBoard)) ? true : toReturn;
        }
        //red
        if (currentPlayer || (printedBoard.findCheckerAt(row, col) != null && printedBoard.findCheckerAt(row, col).GetComponent<GivePosition>().getType().Equals("king")))
        {
            toReturn = (inBounds(row, col, 2, -2, printedBoard.size) && board.getCheckerColor(row + 2, col - 2) == 0 && canMove(row + 2, col - 2, row, col, board, printedBoard)) ? true : toReturn;
            toReturn = (inBounds(row, col, 2, 2, printedBoard.size) && board.getCheckerColor(row + 2, col + 2) == 0 && canMove(row + 2, col + 2, row, col, board, printedBoard)) ? true : toReturn;
        }

        return toReturn;
    }
    
    public bool canMove(int row, int col, int sr, int sc, Board board, PrintBoard printedBoard)
    {
        bool toReturn = false;
        //black logic
        if (!currentPlayer || printedBoard.findCheckerAt(sr, sc).GetComponent<GivePosition>().getType().Equals("king"))
        {
            toReturn = (isAtSelected(row, col, 1, 1, sr, sc, toReturn) || isAtSelected(row, col, 1, -1, sr, sc, toReturn));

            if (toReturn == true) { return toReturn; }
            toReturn = step(row, col, 1, 1, sr, sc, board, printedBoard, toReturn);
            if (toReturn == true) { return toReturn; }
            toReturn = step(row, col, 1, -1, sr, sc, board, printedBoard, toReturn);
        }

        //red logic
        if (currentPlayer || printedBoard.findCheckerAt(sr, sc).GetComponent<GivePosition>().getType().Equals("king"))
        {
            toReturn = (isAtSelected(row, col, -1, 1, sr, sc, toReturn) || isAtSelected(row, col, -1, -1, sr, sc, toReturn));

            if (toReturn == true) { return toReturn; }
            toReturn = step(row, col, -1, 1, sr, sc, board, printedBoard, toReturn);
            if (toReturn == true) { return toReturn; }
            toReturn = step(row, col, -1, -1, sr, sc, board, printedBoard, toReturn);
        }
        return toReturn;
    }

    public bool falseJump = false;

    private bool step(int row, int col, int rd, int cd, int sr, int sc, Board board, PrintBoard printedBoard, bool before)
    {
        if (inBounds(row + rd, col + cd, rd, cd, printedBoard.size))
        {
            if (board.getCheckerColor(row + rd, col + cd) == whichGuy(!currentPlayer) && isAtSelected(row+rd, col+cd, rd, cd, sr, sc, before))
            {
                if (!falseJump) { 
                    printedBoard.killCheckerAt(row + rd, col + cd);
                    printedBoard.hasJumped = false;
                }
                return true;
            }
        }
        return before;
    }
    private bool isAtSelected(int row, int col, int rd, int cd, int sr, int sc, bool r)
    {
        if (row+rd == sr && col+cd == sc)
        {
            r = true;
        }
        return r;
    }

    private bool inBounds(int r, int c, int rd, int cd, int size)
    {
        if (r+rd > -1 && r+rd<size && c+cd > -1 && c+cd < size) { return true;}

        return false;
    }

    void Update()
    {
        if (count > 100)
        {
            SceneManager.LoadScene(2);
        }
    }
}
