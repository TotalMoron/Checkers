using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitGame : MonoBehaviour
{
    public Button bigB;
    // Start is called before the first frame update
    void Start()
    {
        bigB.onClick.AddListener(leave);
    }
    void leave()
    {
        Application.Quit();
        Debug.Log("iounno");
    }
}
