using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TurnTextAlterer : MonoBehaviour
{
    public TextMeshProUGUI winText;
    public GameObject cam;
    private PieceLogic logic;
    // Start is called before the first frame update
    void Start()
    {
        logic = cam.GetComponent<PieceLogic>();
    }

    void Update()
    {
        winText.text = (logic.whichGuy() == 1) ? "REDs turn" : "BLACKs turn";
    }
}
