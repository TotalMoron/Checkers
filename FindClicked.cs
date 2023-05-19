using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindClicked : MonoBehaviour
{
    public GameObject cam;
    public AudioSource alarm;

    private GivePosition pos;
    private PrintBoard spawnChecker;
    private Board board;
    private PieceLogic logic;


    private int selectedR = -2, selectedC = -2;
    private bool alreadySelected = false;
    private GameObject curSel;
    private int currentC = 0, currentR = 0;


    void Start(){spawnChecker = cam.GetComponent<PrintBoard>();
                 board = cam.GetComponent<Board>();
                 logic = cam.GetComponent<PieceLogic>();}

    void Update()
    {
        //Left Click
        if (Input.GetMouseButtonDown(0))
        {
            //raycast from main cam
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);


            if (hit.collider != null)
            {
                placeNewChecker(hit.collider);
            }
        }

        //right click
        if (Input.GetMouseButtonDown(1) && !spawnChecker.hasJumped)
        {
            deselect();
        }
    }
    

    private void placeNewChecker(Collider2D hit)
    {
        //set row n' col to the ones that were clicked
        pos = hit.GetComponent<GivePosition>();
        currentC = pos.getCol();
        currentR = pos.getRow();

        int curTurn = (logic.getTurn()) ? 1 : 2;

        //checks if the checker can be selected. If so it selects, setting selectedR and selectedC in the process
        if (logic.shouldSelect(currentR, currentC, board.getCheckerColor(currentR, currentC), hit.gameObject) && !alreadySelected)
        {
            selectionHelper(hit.gameObject);
        }

        int beforeSize = spawnChecker.allCheckers.Count;
        //checks if space is empty and places a new checker
        if (board.getCheckerColor(currentR, currentC) == 0 && logic.canMove(currentR, currentC, selectedR, selectedC, board, spawnChecker) && !stupid())
        {
            spawnChecker.printChecker(logic.whichGuy(), currentR, currentC);
            
            board.setPieceColor(currentR, currentC, curTurn);

            logic.shouldForfeit = true;

            //repeat turn if jumped
            logic.falseJump = true;
            if ((spawnChecker.allCheckers.Count-1 < beforeSize) && logic.canJump(currentR, currentC, board, spawnChecker))
            {
                spawnChecker.hasJumped = true;
                logic.shouldForfeit = false;
                logic.changeTurn();
            }
            logic.changeTurn();
            logic.falseJump = false;

            board.kill(selectedR, selectedC, curSel);
            spawnChecker.allCheckers.Remove(curSel);

            alreadySelected = false;
            
            //rotate camera
            cam.GetComponent<RotateCam>().enabled = true;

            if (spawnChecker.hasJumped)
            {
                selectionHelper(spawnChecker.findCheckerAt(currentR, currentC));
            }
        }

        //play sound
        logic.falseJump = true;
        if (board.getCheckerColor(currentR, currentC) == 0 && (!logic.canMove(currentR, currentC, selectedR, selectedC, board, spawnChecker) || stupid()))
        {
            alarm.Play();
        }
        logic.falseJump = false;
    }

    public void deselect()
    {
        alreadySelected = false;
        selectedR = -2;
        selectedC = -2;
        logic.select(curSel, 0f);
        curSel = null;
    }

    private void selectionHelper(GameObject gb)
    {
        alreadySelected = true;
        selectedR = currentR;
        selectedC = currentC;
        curSel = gb;
        logic.select(gb, .35f);
        spawnChecker.setPrev(gb);
    }

    private bool stupid()
    {
        return (spawnChecker.hasJumped && (currentC != selectedC + 1 || currentC != selectedC - 1 || currentR != selectedR + 1 || currentR != selectedR - 1));
    }

    public void Unjump()
    {
        spawnChecker.hasJumped = false;
    }
}
