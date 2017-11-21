using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDestroyBullets : Bullets
{
    public int damage;

    public float effectTime;

    public float effectForce;    

    public GameObject explosion;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            var characterHealth = collision.GetComponent<CharacterHealth>();
            if (characterHealth.playerIndex != playerIndex)
            {
                characterHealth.TakeDamage(damage, transform.position, effectTime, effectForce);
                if (explosion)
                    Instantiate(explosion, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
        else
        {
            if (explosion)
                Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
