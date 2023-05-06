using UnityEngine;
using UnityEngine.SceneManagement;

public class ShootingDriver : MonoBehaviour
{
    [Header("Combat")]
    public bool canShoot = true;    // შეუძლია თუ არა სროლა

    public GameObject bulletPrefab; // ტყვიის პრეფაბი
    public float firerate = 0.66f;  // სროლის სისწრაფე
    public Transform firingPoint; // ტრანსფორმი საიდანაც ისვრის
    public float bulletSpread = 5f; // ტყვიის სპრედი
    public int hP = 5;  // დარჩენილი სიცოცხლე
}
