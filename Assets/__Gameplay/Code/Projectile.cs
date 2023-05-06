using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float destroyCooldown = 1f;
    public float speed;

    float cooldown = 0;

    void Update()
    {
        transform.position += transform.right * speed * Time.deltaTime;


        // ვამოწებთ ტყვია ზედმეტი ხანი ხომ არ არსებობს
        cooldown += Time.deltaTime;
        if (cooldown >= destroyCooldown)
        {
            DestroyBullet();
        }
    }

    void DestroyBullet()
    {
        Destroy(gameObject);
    }

    private void OnBecameInvisible()
    {
        DestroyBullet();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Player")
        {
            DestroyBullet();
        }
    }
}