using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCam : MonoBehaviour
{
    public Transform cam;
    public float spinSpeed, delay;
    private Vector3 rotationVector, zeroes;

    private float t;

    void Start()
    {
        rotationVector = new Vector3(0, 0, spinSpeed);
        zeroes = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (cam.gameObject.GetComponent<PieceLogic>().getTurn())
        {
            t += Time.deltaTime;

            if(t < delay)
            {
                if (cam.eulerAngles.z < 270)
                    cam.Rotate(zeroes);
            }
            else
            {
                if (cam.eulerAngles.z < 270)
                    cam.Rotate(rotationVector);
                if (cam.eulerAngles.z >= 270)
                {
                    this.enabled = false;
                    t = 0;
                }
            }
        }
        else
        {
            t += Time.deltaTime;

            if (t < delay)
            {
                if (cam.eulerAngles.z > 90)
                    cam.Rotate(-zeroes);
            }
            else
            {
                if (cam.eulerAngles.z > 90)
                    cam.Rotate(-rotationVector);
                if (cam.eulerAngles.z <= 90)
                {
                    this.enabled = false;
                    t = 0;
                }
            }
        }
    }
}
