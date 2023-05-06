using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraficController : MonoBehaviour
{
    // Start is called before the first frame update

    public float traficSpeed;

    public bool reverseDirection;

    void Start()
    {
        
    }

    // Update is called once per frame
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
