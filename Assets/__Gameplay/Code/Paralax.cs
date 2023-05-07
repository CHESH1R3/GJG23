using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralax : MonoBehaviour
{
    public GameObject Sky;
    public GameObject Stars;
    public GameObject Fog;
    public GameObject Asphalt;
    

    public float SkySpeed;
    public float StarsSpeed;
    public float FogSpeed;
    public float AsphaltSpeed;
    public float Speed;
    

    void Start()
    {
        
    }


    void Update()
    {
        transform.Translate(Vector3.left * Speed * Time.deltaTime);
        Sky.transform.Translate(Vector3.left * SkySpeed * Time.deltaTime);
        Stars.transform.Translate(Vector3.left * StarsSpeed * Time.deltaTime);
        Fog.transform.Translate(Vector3.left * FogSpeed * Time.deltaTime);
        Asphalt.transform.Translate(Vector3.left * AsphaltSpeed * Time.deltaTime);
        


    }
}
