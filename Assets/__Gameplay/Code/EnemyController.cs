using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public PlayerController playerController;
    Transform playerTransform;

    float speed;

    public GameObject[] enemyObjects;

    public float distanceThreshold;

    private void Awake()
    {
        enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");
    }

    private void Start()
    {
        playerTransform = playerController.transform;

        speed = playerController.automaticSpeed;
    }

    private void FixedUpdate()
    {
        // ცოტა რთულად არის აქ საქმე. გათიშულ გეიმობიექტებზე სქრიფთი გინდ იყოს გინდ არა, თუ გვინდა რომ დროდან მეორეში რომ გადახვალ
        // ენემი მანქანები კიდევ მოძრაობდნენ, გვიწევს რომ სხვა ობიექტმა ატაროს

        foreach (GameObject enemyObject in enemyObjects)
        {
            Transform enemyTransform = enemyObject.transform;

            //  ვამოწმებთ დისტანციას ენემისა და ფლეერის შორის. თუ თრეშჰოლდზე ნაკლებია, ენემი სიარულს იწყებს
            if (enemyTransform.position.x - playerTransform.position.x <= distanceThreshold)
            {
                enemyTransform.position += enemyTransform.right * speed * Time.deltaTime; // წინსვლა
            }
        }

    }
}