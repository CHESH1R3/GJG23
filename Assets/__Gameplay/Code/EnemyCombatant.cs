using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombatant : MonoBehaviour
{
    [Header("Combat")]
    public bool isLoaded = false;   // ჩატვირთული არის თუ არა
    public bool isAimed = false;    // დამიზნებული არის თუ არა
    public bool canShoot = false;    // შეუძლია თუ არა სროლა

    PlayerController playerController;
    Transform playerTransform;

    public float aimThreshold = 1.75f; // საჭირო დისტანცია გასროლვამდე

    public GameObject bulletPrefab; // ტყვიის პრეფაბი
    public float firerate = 0.66f;  // სროლის სისწრაფე
    public Transform firingPoint; // ტრანსფორმი საიდანაც ისვრის
    public float bulletSpread = 5f; // ტყვიის სპრედი
    public int hP = 5;  // დარჩენილი სიცოცხლე
    public int damage = 1;  // რამდენ დემეჯს იძლევა ერთ სროლაში

    bool coolingDown;   // ქულდაუნი აქტიურია თუ არა
    float fireCooldown = 0; // რა ფაზაზეა ქულდაუნი

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        playerTransform = playerController.transform;
    }

    private void Update()
    {
        if (isLoaded)
        {
            CheckAim();
        }

        if (coolingDown)
        {
            Cooldown();
        }
    }
   
    void CheckAim()
    {
        if (Mathf.Abs(transform.position.y - playerTransform.position.y) < aimThreshold)
        {
            isAimed = true;
        }
        else { if (isAimed) isAimed = false; }
    }

    private void FixedUpdate()
    {
        if (isLoaded) { if (canShoot) { if (isAimed) { Shoot(); } } }
    }

    void Shoot()
    {
        canShoot = false;

        // ტყვიის შექმნა და ტრანსფორმის ამოღება
        Transform newBullet = Instantiate(bulletPrefab, firingPoint.position, firingPoint.rotation).transform;
        // ტყვიაზე ცოტა სპრედის დადება
        newBullet.eulerAngles = new Vector3(newBullet.rotation.x, newBullet.rotation.y, Random.Range(-bulletSpread, bulletSpread));

        // სროლის ქულდაუნზე გაშვება
        coolingDown = true;
    }
    void Cooldown()
    {
        fireCooldown += Time.deltaTime;
        if (fireCooldown >= firerate)
        {
            canShoot = true;
            fireCooldown = 0;
        }
    }
}