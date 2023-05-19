using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamSwitch : MonoBehaviour
{
    public string hotkey;
    public GameObject secCam, self;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(hotkey))
        {
            if (secCam.activeSelf)
            {
                secCam.SetActive(false);
                self.SetActive(true);
            }
            else
            {
                secCam.SetActive(true);
                self.SetActive(false);
            }
        }

    }
}
