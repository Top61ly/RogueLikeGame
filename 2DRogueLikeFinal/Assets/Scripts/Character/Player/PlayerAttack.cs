using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private PlayerMovement playerMovement;

    //Check Enemey   
    public float checkRange;

    public int enemyLayerMask;
    public List<Collider2D> colliderEnemiesList;
    //Control the shoot
    public float timerGap = 1f;

    private float timer = 0;
    private BulletsGenerator.BulletsAttack bulletsAttack;

    private float effectDisplayTime = 0.2f;
    public SpriteRenderer gunEffect;



    private void Awake()
    {
        playerMovement = GetComponentInChildren<PlayerMovement>();
        bulletsAttack = GetComponentInChildren<BulletsGenerator.BulletsAttack>();
    }

    void Start()
    {
        enemyLayerMask =1<<LayerMask.NameToLayer("Enemy");
        DisableEffect();
    }

    void Update()
    {
        //Update Gun Transform
        colliderEnemiesList = Physics2D.OverlapCircleAll(transform.position, checkRange, enemyLayerMask).ToList<Collider2D>();

       // transform.rotation = GetTarget();

        //Shoot Control
        timer += Time.deltaTime;


        if (Input.GetMouseButton(0) && timer >= timerGap && Time.timeScale != 0)

            Shoot();

        if (timer >= timerGap * effectDisplayTime)

            DisableEffect();
    }

    void Shoot()
    {
        timer = 0;
        gunEffect.enabled = true;
        bulletsAttack.Shoot();
    }

    void DisableEffect()
    {
        gunEffect.enabled = false;
    }

    Quaternion GetTarget()
    {
        int allNotDefaultlayer = ~(1 << LayerMask.NameToLayer("Default"));
        
        Quaternion result = Quaternion.Euler(playerMovement.movement);
        float distance = 0;


        foreach (Collider2D collider in colliderEnemiesList)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, collider.transform.position - transform.position, allNotDefaultlayer);
            if (hit.collider != null && hit.collider == collider)
                continue;
            else
                colliderEnemiesList.Remove(collider);
        }

        foreach (Collider2D collider in colliderEnemiesList)
        {
            if ((collider.transform.position - transform.position).magnitude > distance)
                result = Quaternion.Euler((collider.transform.position - transform.position).normalized);
        }

        return result;
    }
}
