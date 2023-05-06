using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TrafficController : MonoBehaviour
{
    public Transform playerTransform;
    SpriteRenderer sprite;
    Collider2D col;


    public float traficSpeed;  // მოძრაობის სიჩქარე

    public int startingTreshold;

    public bool reverseDirection; // მოძრაობა რევერსულია თუ არა

    bool move = false; // იმოძრაოს თუ არა გეიმობჯექთმა
    bool active = false; // უჩინარი მანქანა გააქტიურდა თუ არა


    // სიკვდილის ფუნქცია
    void Die()
    {
        move = false;
    }
    

    // გეიმობჯექთის აქტივაცია ან დეაქტივაცია
    void SetActive(bool status)
    {
        if (status)
        {
            sprite = GetComponent<SpriteRenderer>();
            sprite.enabled = true;
            col.enabled = true;
            move = true;
        }
        else
        {
            sprite = GetComponent<SpriteRenderer>();
            sprite.enabled = false;
            col.enabled = false;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            Die();
        }
    }


    private void Start()
    {
        // მოწმდება გეიმობჯექთის ტიპი, რევერსულია თუ არა და შესაბამისად ენიჭება ფუნქციონალი
        if(!reverseDirection)
        {
          col= GetComponent<Collider2D>();
          SetActive(false); // თუ რევერსული არ არის გეიმობჯექთს დროებით ვაქრობთ
        }
        else
        {
            move = true;
        }
    }

    void FixedUpdate()
    {
        if (!reverseDirection)
        {
            if(transform.position.x < playerTransform.position.x - startingTreshold && !active)
            {
              SetActive(true); // როდესაც ფლეიერი გამქრალ გეიმობჯექთს გასცდება მაშინ ვააქტიურებთ
              active = true;
            }

            if (move)
            {
                transform.Translate(traficSpeed, 0, 0); // მოძრაობა წინ
            }
        }
        else if(move)
        {
            transform.Translate(-traficSpeed, 0, 0); // მოძრაობა უკან
        }
    }

}