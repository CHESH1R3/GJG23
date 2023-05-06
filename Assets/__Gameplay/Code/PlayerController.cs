using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Camera")]
    public Transform camTarget; // თრანსფორმი რომელსაც ცინემაშინი დაყვება

    [Header("Movement")]
    public float automaticSpeed = 20f; // ავტომატური წინსვლის სისწრაფე
    public float steeringSpeed = 10f; // მოხვევის სისწრაფე
    public float steeringAcceleration = 7.5f;  // რამდენად მალე რაზგონდება მოსახვევად
    public float drivingSpeed = 5f; // ხელით წინსვლის სისწრაფე
    public float drivingAcceleration = 7.5f;  // რამდენად მალე რაზგონდება საწინსვლოდ



    public Transform max, min; // ფლეერი კედლების იქით რომ არ გავიდეს ტრანსფორმი შევადაროთ. კოლაიდერზე უფრო ოპტიმალურია, ნაკლებ ფიზიკაზე ვინერვიულებთ

    [Header("Combat")]
    public int hP = 3;  // დარჩენილი სიცოცხლე
    public int damage = 1;  // რამდენ დემეჯს იძლევა ერთ სროლაში

    [Header("Shift")]
    public bool canShift = true;    // შეუძლია თუ არა დროში გადახტეს
    public bool is2003 = false; // დროის შემოწმების ბულიანი
    public float cooldown = 0.25f;  // რამდენი დრო ჭირდება რომ დრო დაშიფტოს

    public GameObject t2003, t2043;

    float verticalInput;    // ინპუტი რომ შევინახოთ ფლეერის
    public bool shiftInput = false; // ფლეერის ნახტომის ინპუტი
    float activeSteerSpeed; // ამ კონკრეტულ მომენტში აქსელერაცია სადამდეა მისული

    private void Start()
    {
        // თავიდანვე 2043ში რომ დაიყოს თამაში

        t2043.SetActive(true);
        t2003.SetActive(false);

        is2003 = false;
    }

    private void Update()
    {
        GetPlayerInput();
    }

    void GetPlayerInput()
    {
        verticalInput = Input.GetAxisRaw("Vertical");

        // დროში ნახტომის ინპუტის აღება
        if (Input.GetMouseButtonDown(1))
        {
            if (canShift) shiftInput = true;
        }
    }

    private void FixedUpdate()
    {
        Move();
        Steer();

        if (shiftInput)
        {
            shiftInput = false;
            Shift();
        }
    }

    void Move()
    {
        transform.position += transform.right * automaticSpeed * Time.deltaTime; // წინსვლა

        // კამერა თარგეთის დააფდეითება
        camTarget.position = new Vector3(transform.position.x, camTarget.position.y, 0);
    }

    void Steer()
    {
        // აქსელერაციის დათვლა. თუ ინპუტი არ შემოდის ვასწრაფებთ. საპირისპირო შემთხვევაში ძალიან სრიალებს
        if (verticalInput == 0) activeSteerSpeed = Mathf.Lerp(activeSteerSpeed, verticalInput, steeringAcceleration * 1.5f * Time.deltaTime);
        else activeSteerSpeed = Mathf.Lerp(activeSteerSpeed, verticalInput, steeringAcceleration * Time.deltaTime);

        // მოხვევა
        if (activeSteerSpeed > 0)
        {
            // თუ ზედა კედელს არ ეხება, ზემოთ შეუძლია მოხვევა
            if (transform.position.y <= max.position.y) transform.position += transform.up * activeSteerSpeed * steeringSpeed * Time.deltaTime;
        }
        else if (activeSteerSpeed < 0)
        {
            // თუ ქვედა კედელს არ ეხება, ქვემოთ შეუძლია მოხვევა
            if (transform.position.y >= min.position.y) transform.position += transform.up * activeSteerSpeed * steeringSpeed * Time.deltaTime;
        }
    }

    void Shift()
    {
        canShift = false;

        if (is2003) { t2043.SetActive(true); t2003.SetActive(false);  is2003 = false; }
        else { t2043.SetActive(false); t2003.SetActive(true);   is2003 = true; }

        // ნახტომის ქულდაუნზე გაშვება
        StartCoroutine(ShiftCooldown());
    }

    IEnumerator ShiftCooldown() { yield return new WaitForSeconds(cooldown); canShift = true; }

    void OnTriggerEnter2D(Collider2D other)
    {
        // თუ ობსთექლს შეეხო ეგრევე კვდება
        if (other.tag == "Obstacle") { Die(); }
    }
    void Die()
    {
        // აქ იქნება სიკვდილის და რესტარტის ფუნქცია. ამჯერად უბრალოდ სცენა დარესეტდება

        print("RIP");
        SceneManager.LoadScene(0);
    }
}