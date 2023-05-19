using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    private float spinX, spinY, spinZ;
    private Vector3 spinner;
    private GameObject parent;
    // Start is called before the first frame update
    void Start()
    {
        while(spinX == 0 || spinY == 0 || spinZ == 0)
        {
            spinY = Random.Range(-6.5f, 6.5f);
            spinX = Random.Range(-6.5f, 6.5f);
            spinZ = Random.Range(-6.5f, 6.5f);
        }
        parent = this.gameObject;
        spinner = new Vector3(spinX, spinY, spinZ);
    }

    // Update is called once per frame
    void Update()
    {
        parent.transform.Rotate(spinner, Space.Self);
    }
}
