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
    public Transform weaponTransform;
    private List<Collider2D> colliderEnemiesList;
   
    //Control the shoot
    private float timer = 0;
    private float effectDisplayTime = 0.2f;
    private Weapon weapon;

    //Weapon Control
    private bool isWeaponInRange;
    private Transform weaponPoint;
    public Transform weaponWait;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        weapon = GetComponentInChildren<Weapon>();
    }

    void Start()
    {
        weaponPoint = transform.Find("WeaponPoint");
       // enemyLayerMask =1<<LayerMask.NameToLayer("Enemy");
        DisableEffect();
    }

    void Update()
    {
        //Update weaponTransform Transform
       // colliderEnemiesList = Physics2D.OverlapCircleAll(transform.position, checkRange, enemyLayerMask).ToList<Collider2D>();

        if (weaponTransform)
            weaponTransform.localRotation = GetTarget();

        //Shoot Control
        timer += Time.deltaTime;

        if (Input.GetMouseButton(0) && timer >= weapon.timerGap && Time.timeScale != 0)
            UseWeapon();
                
        if (timer >= weapon.timerGap * effectDisplayTime)
            DisableEffect();

        //
        if (Input.GetKeyDown(KeyCode.E))
            ChangeWeapon();
    }

    void UseWeapon()
    {
        timer = 0;
        weapon.TriggerWeapon();
    }

    void DisableEffect()
    {
        weapon.DisableEffect();
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
        weaponTransform.parent = null;
        weaponTransform.rotation = Quaternion.identity;
        weaponTransform.position = Vector3.zero;
        weaponTransform = null;         
              
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
