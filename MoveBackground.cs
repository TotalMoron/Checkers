using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackground : MonoBehaviour
{
    public GameObject cam;

    public float x;

    private float startX, startY;
    private Vector3 move;

    private bool dontExplode = true;
    // Start is called before the first frame update
    void Start()
    {
        move = new Vector3(x, 0, 0);

        startX = this.gameObject.transform.localPosition.x;
        startY = this.gameObject.transform.localPosition.y;
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.localPosition += move;

        if (this.gameObject.transform.localPosition.x > startX+20.4f || this.gameObject.transform.localPosition.y > startY+11)
        {
            if (dontExplode)
            {
                Instantiate(this.gameObject, new Vector3(startX - 10.2f, startY, 0), Quaternion.identity, cam.transform);
                Destroy(this.gameObject, Mathf.Abs(startX));
            }
            dontExplode = false;
        }
    }
}
