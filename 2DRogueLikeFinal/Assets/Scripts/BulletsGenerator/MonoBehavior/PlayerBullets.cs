using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullets : MonoBehaviour
{
    public int damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player") && !collision.CompareTag("Weapon"))
        {
            if (collision.CompareTag("Enemy"))

                collision.GetComponent<EnemyHealth>().TakeDamage(damage);

            Destroy(gameObject);
        }
    }
}
