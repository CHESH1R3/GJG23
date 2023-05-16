using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public float transitionSpeed;
    public AudioSource a2003, a2043, gameOver;

    public bool is2003;

    private void Update()
    {
        Transition();
    }

    void Transition()
    {
        if (is2003)
        {
            if (a2003.volume != 1) a2003.volume = Mathf.Lerp(a2003.volume, 1, transitionSpeed * Time.deltaTime);
            if (a2043.volume != 0) a2043.volume = Mathf.Lerp(a2043.volume, 0, transitionSpeed * Time.deltaTime);
        }
        else
        {
            if (a2003.volume != 0) a2003.volume = Mathf.Lerp(a2003.volume, 0, transitionSpeed * Time.deltaTime);
            if (a2043.volume != 1) a2043.volume = Mathf.Lerp(a2043.volume, 1, transitionSpeed * Time.deltaTime);
        }
    }
}
