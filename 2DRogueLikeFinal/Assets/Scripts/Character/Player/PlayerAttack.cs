using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private PlayerMovement playerMovement;

    //Check Enemey   
    public float checkRange;

    public Transform gun;
    public int enemyLayerMask;
    private List<Collider2D> colliderEnemiesList;
    //Control the shoot
    public float timerGap = 1f;

    private float timer = 0;

    private PlayerWeaponControl playerWeaponControl;
    private BulletsGenerator.BulletsAttack bulletsAttack;

    private float effectDisplayTime = 0.2f;
    public SpriteRenderer gunEffect;

    //Weapon Control
    private bool isWeaponInRange;
    private Transform weaponPoint;
    public Transform weaponWait;

    private void Awake()
    {
        playerWeaponControl = GetComponent<PlayerWeaponControl>();
        playerMovement = GetComponent<PlayerMovement>();
        bulletsAttack = GetComponentInChildren<BulletsGenerator.BulletsAttack>();
    }

    void Start()
    {
        weaponPoint = transform.Find("WeaponPoint");
       // enemyLayerMask =1<<LayerMask.NameToLayer("Enemy");
        DisableEffect();
    }

    void Update()
    {
        //Update Gun Transform
       // colliderEnemiesList = Physics2D.OverlapCircleAll(transform.position, checkRange, enemyLayerMask).ToList<Collider2D>();

        if (gun)
            gun.localRotation = GetTarget();

        //Shoot Control
        timer += Time.deltaTime;

        if (Input.GetMouseButton(0) && timer >= timerGap && Time.timeScale != 0)
            Shoot();
                
        if (timer >= timerGap * effectDisplayTime)
            DisableEffect();

        //
        if (Input.GetKeyDown(KeyCode.E))
            ChangeWeapon();
    }

    void Shoot()
    {
        timer = 0;
        gunEffect.enabled = true;
        bulletsAttack.Shoot();
    }

    void DisableEffect()
    {
        if (gunEffect)
            gunEffect.enabled = false;
    }

    Quaternion GetTarget()
    {
        int allNotDefaultlayer = ~(1 << LayerMask.NameToLayer("Default"));

        float x = Mathf.Abs(playerMovement.movement.x);
        float y = playerMovement.movement.y;

        float angle = Mathf.Atan2(y, x)*Mathf.Rad2Deg;
        
        Quaternion result = Quaternion.AngleAxis(angle,new Vector3(0,0,1));


        //float distance = 0;
        
        //foreach (Collider2D collider in colliderEnemiesList)
        //{
        //    RaycastHit2D hit = Physics2D.Raycast(transform.position, collider.transform.position - transform.position, allNotDefaultlayer);
        //    if (hit.collider != null && hit.collider == collider)
        //        continue;
        //    else
        //        colliderEnemiesList.Remove(collider);
        //}

        //foreach (Collider2D collider in colliderEnemiesList)
        //{
        //    if ((collider.transform.position - transform.position).magnitude > distance)
        //        result = Quaternion.Euler((collider.transform.position - transform.position).normalized);
        //}

        return result;
    }

    void ChangeWeapon()
    {
        gun.parent = null;
        gun.rotation = Quaternion.identity;
        gun.position = Vector3.zero;
        gun = null;
            
        
        gunEffect = weaponWait.Find("GunEffect").GetComponent<SpriteRenderer>();
         
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon"))
        {
            isWeaponInRange = true;
            weaponWait = collision.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon"))
        {
            isWeaponInRange = false;
            weaponWait = null;
        }
    }
}
