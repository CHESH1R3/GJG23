using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyCombatant : ShootingDriver
{
    [Header("Combat")]
    public bool isLoaded = false;   // ჩატვირთული არის თუ არა
    public bool isAimed = false;    // დამიზნებული არის თუ არა

    PlayerController playerController;
    Transform playerTransform;

    RaycastHit2D upHitForward;
    RaycastHit2D upHitBackward;

    RaycastHit2D downHitForward;
    RaycastHit2D downHitBackward;


    RaycastHit2D leftHit;
    RaycastHit2D rightHit;

    public Transform raycastPointYForward;
    public Transform raycastPointYBackward;

    public Transform min;
    public Transform max;


    public float aimThreshold = 1.75f; // საჭირო დისტანცია გასროლვამდე

    bool coolingDown;   // ქულდაუნი აქტიურია თუ არა
    bool startSteering;   //  აქტიურია თუ არა
    bool paralellEnemy;
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

            if(!isAimed)
            {
                // აქ უნდა შეიცვალოს პოზიცია რომ ფლეერს დაუმიზნოს
                startSteering = true;
            }
        }

        if (coolingDown)
        {
            Cooldown(); // სროლის ქულდაუნი
        }
    }


    void SteerToPlayer()
    {
        if (!paralellEnemy)
        {
            transform.position = Vector2.Lerp(transform.position,
            new Vector2(transform.position.x, playerTransform.position.y), 0.05f);
        }
    }


    void ObsticleAvoidance()
    {
        string obsticleLocation = "";

        Vector2 distanceToBorders = new Vector2(max.position.y - transform.position.y, transform.position.y - min.position.y);

        upHitForward = Physics2D.Raycast(raycastPointYForward.position, Vector2.up);
        Debug.DrawRay(raycastPointYForward.position, Vector2.up, Color.green);

        downHitForward = Physics2D.Raycast(raycastPointYForward.position, Vector2.down);
        Debug.DrawRay(raycastPointYForward.position, Vector2.down, Color.red);


        upHitBackward = Physics2D.Raycast(raycastPointYBackward.position, Vector2.up);
        Debug.DrawRay(raycastPointYBackward.position, Vector2.up, Color.green);

        downHitBackward = Physics2D.Raycast(raycastPointYBackward.position, Vector2.down);
        Debug.DrawRay(raycastPointYBackward.position, Vector2.down, Color.red);


        leftHit = Physics2D.Raycast(transform.position, Vector2.left, 5);
        Debug.DrawRay(transform.position, Vector2.left, Color.blue);

        rightHit = Physics2D.Raycast(transform.position, Vector2.right , 5);
        Debug.DrawRay(transform.position, Vector2.right, Color.yellow);



        if (upHitForward && upHitBackward && downHitForward && downHitBackward)
        {
            obsticleLocation = "upAndDown";
        }
        else if (upHitForward && upHitBackward)
        {
            obsticleLocation = "up";
        }
        else if (downHitForward && downHitBackward)
        {
            obsticleLocation = "down";
        }
        else if(!upHitForward && !upHitBackward && !downHitForward && !downHitBackward)
        {
            obsticleLocation = "none";
        }

        //print(obsticleLocation);

        //print(distanceToBorders);



        if (rightHit)
        {
            if (obsticleLocation == "up" && transform.position.y >= min.transform.position.y)
            {
                transform.Translate(0, -0.3f, 0);
            }
            else if (obsticleLocation == "down" && transform.position.y <= max.transform.position.y)
            {
                transform.Translate(0, 0.3f, 0);
            }
            else if (obsticleLocation == "none")
            {
                //if(distanceToBorders.x > distanceToBorders.y)
                //{

                //}

                if (distanceToBorders.x > distanceToBorders.y)
                {
                    print("dabla");
                    transform.Translate(0, 0.3f, 0);
                }
                else if (distanceToBorders.x < distanceToBorders.y)
                {
                    print("magla");
                    transform.Translate(0, -0.3f, 0);
                }
            }
        }







        // მოწმდება ჰორიზონტალურად გეიმობჯექტის პარალელურად სხვა გეიმობჯექტი დგას თუ არა და შესაბამისად ისეტება ბულეან ცვლადი
        if (upHitForward || downHitForward || upHitBackward || downHitBackward )
        {
            paralellEnemy = true;
        }
        else
        {
            paralellEnemy = false;
        }
    }

   
    void CheckAim()
    {
        if (Mathf.Abs(transform.position.y - playerTransform.position.y) < aimThreshold)
        {
            isAimed = true;
        }
        else { isAimed = false; }
    }

    private void FixedUpdate()
    {
        // თუ ენემი ჩატვირთულია, სროლა შეუძლია, და დამიზნებული აქ, ისროლოს
        if (isLoaded)
        {
            if (canShoot)
            {
                if (isAimed)
                {
                    Shoot();
                }
            }
        }

        if(startSteering)
        {
            SteerToPlayer();
        }
        ObsticleAvoidance();


    }

    void Shoot()
    {
        print("SHOT");
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
        // ვითვლით უკან დროს სროლაზე
        fireCooldown += Time.deltaTime;
        if (fireCooldown >= firerate)
        {
            canShoot = true;
            fireCooldown = 0;
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "PlayerBullet")
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