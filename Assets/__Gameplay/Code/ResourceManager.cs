using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceManager : MonoBehaviour
{
    public Slider Oxigen;
    public Slider Energy;
    [SerializeField] private PlayerController PlayerController;
    void Start()
    {
        
    }


    void Update()
    {
        if (PlayerController.is2003)
        {
            Oxigen.value++;
            Energy.value--;
        }
        else
        {
            Oxigen.value--;
            Energy.value++;
        }
    }
}