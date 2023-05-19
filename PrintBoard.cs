using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PrintBoard : MonoBehaviour
{
    public GameObject Wsquare, Bsquare, cam, RC, BC;
    public Transform topCorn;

    private GameObject prevCheck;

    public float squareSpace;
    public int size, pieceRange;
    public bool hasJumped = false;

    private Board checkerBoard;

    private GivePosition currentPos;

    public List<GameObject> allCheckers;

    public void setPrev(GameObject gb)
    {
        prevCheck = gb;
    }

    void Start()
    {
        size = (size%2 == 0) ? size : size + 1;

        //get n' set checkerboard
        checkerBoard = cam.GetComponent<Board>();
        checkerBoard.setRange(size, pieceRange);
        checkerBoard.setBoardSize(size, pieceRange);

        printCheckerBoard();
    }

    void printSquare(int squarNum, int row, int col)
    {
        //0 is white, 1 is black
        GameObject clone = (squarNum == 0) ? Instantiate(Wsquare, new Vector3(row * squareSpace, col * -squareSpace, 0), Quaternion.identity, topCorn)
            : Instantiate(Bsquare, new Vector3(row * squareSpace, col * -squareSpace, 0), Quaternion.identity, topCorn);

        addPosition(squarNum, row, col, clone);

        //say if square (only for black squares)
        if (squarNum != 0)
        {
            currentPos = clone.GetComponent<GivePosition>();
            currentPos.setType("square");
        }
    }

    public void printChecker(int checkNum, int row, int col)
    {
        //1 is red, 2 is black
        GameObject clone = (checkNum == 1) ? Instantiate(RC, new Vector3(row * squareSpace, col * -squareSpace, -.25f), Quaternion.identity, topCorn)
            : Instantiate(BC, new Vector3(row * squareSpace, col * -squareSpace, -.25f), Quaternion.identity, topCorn);

        addPosition(checkNum, row, col, clone);

        //say if checker or king
        currentPos = clone.GetComponent<GivePosition>();
        if ((checkNum == 1 && row == 7) || (prevCheck != null && prevCheck.GetComponent<GivePosition>().getType().Equals("king")))
        {
            currentPos.setType("king");
            clone.transform.GetChild(0).gameObject.transform.localPosition = new Vector3(0, 0, -.1f);
        } 
        else if((checkNum == 2 && row == 0) || (prevCheck != null && prevCheck.GetComponent<GivePosition>().getType().Equals("king")))
        {
            currentPos.setType("king");
            clone.transform.GetChild(0).gameObject.transform.localPosition = new Vector3(0, 0, -.1f);
        }
        else
        {
            currentPos.setType("checker");
        }
        

        allCheckers.Add(clone);
    }

    private void addPosition(int n, int row, int col, GameObject gb)
    {
        if (n != 0 && gb != null)
        {
            currentPos = gb.GetComponent<GivePosition>();
            currentPos.give(row, col);
        }
    }

    void printCheckerBoard()
    {
        for (int i = 0; i < checkerBoard.getBoard().Length/size; i++)
        {
            for (int j = 0; j < checkerBoard.getBoard().Length/size; j++)
            {
                printSquare(checkerBoard.getTileColor(i, j), i, j);

                //checks for empty squares
                if (checkerBoard.getCheckerColor(i, j) != 0){
                    printChecker(checkerBoard.getCheckerColor(i, j), i, j);
                }
            }
        }
    }


    public GameObject findCheckerAt(int row, int col)
    {
        foreach (GameObject checker in allCheckers)
        {
            if (checker != null && checker.GetComponent<GivePosition>().getRow() == row && checker.GetComponent<GivePosition>().getCol() == col)
            {
                return checker;
            }
        }
        return null;
    }

    public void killCheckerAt(int row, int col)
    {
        foreach (GameObject checker in allCheckers)
        {
            if (checker != null)
            {
                if (checker.GetComponent<GivePosition>().getRow() == row && checker.GetComponent<GivePosition>().getCol() == col)
                {
                    checkerBoard.kill(row, col, checker);
                    allCheckers.Remove(checker);
                    break;
                }
            }
        }
    }
}