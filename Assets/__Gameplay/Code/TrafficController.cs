using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TraficController : MonoBehaviour
{
    public float traficSpeed;

    public bool reverseDirection;

    void FixedUpdate()
    {
        if (!reverseDirection)
        {
            transform.Translate(traficSpeed, 0, 0);
        }
        else
        {
            transform.Translate(-traficSpeed, 0, 0);
        }
    }
}