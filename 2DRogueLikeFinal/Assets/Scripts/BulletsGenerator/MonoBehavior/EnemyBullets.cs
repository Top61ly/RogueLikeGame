using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullets : MonoBehaviour
{
    public int damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy") && !collision.CompareTag("Gun"))
        {
            if (collision.CompareTag("Player"))

                collision.GetComponent<PlayerHealth>().TakeDamage(damage);          
                       
            Destroy(gameObject);
        }
    }
}
