using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectionBullets : Bullets
{  
    public float effectTime = 0;

    public float effectForce = 0;

    public float flashTime = 0.01f;

    public GameObject explosion;

    public CharacterHealth characterHealth;

    public EnemyMovement enemyMovement;

    private void Start()
    {
        PoolManager.instance.CreatePool(explosion, 20);
    }

    protected virtual IEnumerator HitEffect(float effectForce,float effectTime)
    {
        enemyMovement.isEffecting = true;

        Rigidbody2D rigidBody2D = characterHealth.GetComponent<Rigidbody2D>();
            
        if (rigidBody2D.isKinematic)
        {
            rigidBody2D.isKinematic = false;
        }
        rigidBody2D.AddForce((rigidBody2D.position - new Vector2(transform.position.x,transform.position.y)).normalized * effectForce);

        yield return new WaitForSeconds(effectTime);

        rigidBody2D.velocity = Vector2.zero;
        rigidBody2D.isKinematic = true;
        enemyMovement.isEffecting = false;
    }   

    protected void OnTriggerEnter2D(Collider2D collision)
    {

        characterHealth = collision.GetComponent<CharacterHealth>();
        if (characterHealth && characterHealth.playerIndex != playerIndex)
        {
            enemyMovement = characterHealth.GetComponent<EnemyMovement>();
            if (explosion)
                PoolManager.instance.ReuseObject(explosion, transform.position, Quaternion.identity);
            //if add effect
            characterHealth.TakeDamage(damage);                      

            if (characterHealth.playerIndex != 0 && effectTime>0 && enemyMovement.isEffecting == false)
            {
                characterHealth.StartCoroutine(HitEffect(effectForce, effectTime));
            }


            Destroy();
        }
        else if (!characterHealth)
        {
            PoolManager.instance.ReuseObject(explosion, transform.position, Quaternion.identity);
            Destroy();
        }        
    }
}
