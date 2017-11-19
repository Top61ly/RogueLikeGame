using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullets : MonoBehaviour
{
    public int damage;

    public float effectTime;

    public float effectForce;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player") && !collision.CompareTag("Weapon"))
        {
            if (collision.CompareTag("Enemy"))
            {
                var enemyHealth = collision.GetComponent<EnemyHealth>();
                enemyHealth.TakeDamage(damage,transform.position,effectTime,effectForce);
            }
            Destroy(gameObject);
        }
    }
}
