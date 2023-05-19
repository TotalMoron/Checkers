using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    public Button button;
    public TextMeshProUGUI buttonText;
    public GameObject cam;

    private PieceLogic logic;
    private FindClicked gun;

    void Start()
    {
        logic = cam.GetComponent<PieceLogic>();
        gun = cam.GetComponent<FindClicked>();
        button.onClick.AddListener(TaskOnClick);
    }

    void Update()
    {
        if (logic.shouldForfeit)
        {
            buttonText.text = "FORFEIT";
        }
        if(!logic.shouldForfeit)
        {
            buttonText.text = "END JUMP";
        }
    }

    void TaskOnClick()
    {
        if (logic.shouldForfeit) {
            SceneManager.LoadScene(2, LoadSceneMode.Additive);
        }
        else
        {
            gun.deselect();
            logic.changeTurn();
            gun.Unjump();
            logic.shouldForfeit = !logic.shouldForfeit;
        }
    }
}
