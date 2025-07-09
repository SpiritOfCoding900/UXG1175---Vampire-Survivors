using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletTimer = 2f;
    private int bulletDamage;

    //initialize bullet properties
    public void InitializeBullet (int damageAmount, float speed, float size)
    {
        bulletDamage = damageAmount;
        transform.localScale = Vector3.one * size;
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, bulletTimer); //destroy all bullets to prevent clutter.
    }

    void OnTriggerEnter2D(Collider2D other) // create a function for bullet behaviour
    {
        if (other.CompareTag ("Player"))
        {
            Player HP = other.GetComponent<Player>();
            if (HP != null)
            {
                HP.TakeDamage(bulletDamage);
            }
            else
            {
                Debug.Log("No damage taken by player");
            }

            Destroy(gameObject);
        }
    }
}
