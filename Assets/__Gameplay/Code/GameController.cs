using UnityEngine;

public class GameController : MonoBehaviour
{
    void Update()
    {
        // თამაშის დაპაუზება და გაშვება
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
        }
    }
}