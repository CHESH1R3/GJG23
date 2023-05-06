using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyCombatant : ShootingDriver
{
    [Header("Combat")]
    public bool isLoaded = false;   // ჩატვირთული არის თუ არა
    public bool isAimed = false;    // დამიზნებული არის თუ არა

    PlayerController playerController;
    Transform playerTransform;

    public float aimThreshold = 1.75f; // საჭირო დისტანცია გასროლვამდე

    bool coolingDown;   // ქულდაუნი აქტიურია თუ არა
    float fireCooldown = 0; // რა ფაზაზეა ქულდაუნი

    public float distanceThreshold = 17.5f;
    public float randomModifier = 2.5f;


    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        playerTransform = playerController.transform;

        distanceThreshold += Random.Range(-randomModifier, randomModifier);
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
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bullet")
        {
            hP--;
            if (hP == 0)
            {
                DestroyCombatant();
            }
        }
        else if (other.tag == "Obstacle")
        {
            DestroyCombatant();
        }
    }
    public void DestroyCombatant()
    {
        // აქ იქნება სიკვდილის და რესტარტის ფუნქცია. ამჯერად უბრალოდ სცენა დარესეტდება

        Destroy(gameObject);
    }
}