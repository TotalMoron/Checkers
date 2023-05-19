using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class WinTextAlterer : MonoBehaviour
{
    public TextMeshProUGUI winText;

    private PieceLogic logic;
    // Start is called before the first frame update
    void Start()
    {
        logic = cam().GetComponent<PieceLogic>();
        winText.text = (logic.whichGuy() == 1) ? "BLACK wins" : "RED wins";
        if (logic.count > 100) {winText.text = "TIE"; }
        SceneManager.UnloadScene(1);
    }

    private GameObject cam()
    {
        foreach (GameObject obj in SceneManager.GetSceneByBuildIndex(1).GetRootGameObjects())
        {
            if(obj.name == "Main Camera")
            {
                return obj;
            }
        }

        return null;
    }
}
