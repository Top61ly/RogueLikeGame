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
                collision.GetComponent<EnemyHealth>().TakeDamage(damage);

                StartCoroutine(HitEffect(collision.GetComponent<Rigidbody2D>()));
            }
        }
    }

    private IEnumerator HitEffect(Rigidbody2D rb2D)
    {
        if (rb2D.isKinematic)
        {
            rb2D.isKinematic = false;
            rb2D.AddForce((rb2D.position - new Vector2(transform.position.x, transform.position.y)).normalized * effectForce);
        }
        yield return new WaitForSeconds(effectTime);
        rb2D.isKinematic = true;
        Destroy(gameObject);
    }
}
