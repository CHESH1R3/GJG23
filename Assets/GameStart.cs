using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    public GameObject level, splash;

    private void Awake()
    {
        level.SetActive(false);
        splash.SetActive(true);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            level.SetActive(true);
            Destroy(gameObject);    
        }
    }
}
