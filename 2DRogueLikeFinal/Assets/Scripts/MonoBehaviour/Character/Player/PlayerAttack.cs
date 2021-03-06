﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private PlayerMovement playerMovement;

    //Check Enemey   
    public float checkRange;

    public float searchRadius;

    public int enemyLayerMask;
    public Transform weaponTransform;

    public Vector2 direction;

    private List<Collider2D> colliderEnemiesList;
   
    //Control the shoot
    private float timer = 0;
    private float effectDisplayTime = 0.2f;
    public Weapon weapon;
    private Animator weaponAnimator;

    //Weapon Control
    public Transform Item;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        weapon = GetComponentInChildren<Weapon>();
        weaponAnimator = weapon.GetComponent<Animator>();
    }

    void Start()
    {
        weapon.Init();
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

        if (direction.magnitude <= 0)
        {
            Quaternion result = new Quaternion();

            Vector2 movement = playerMovement.movement.normalized;

            float angle = Mathf.Atan2(movement.y,Mathf.Abs(movement.x));

            result = Quaternion.AngleAxis(angle, new Vector3(0, 0, 1));

            return result;
        }
        else
        {

            float x = Mathf.Abs(direction.x);
            float y = direction.y;

            float angle = Mathf.Atan2(y, x) * Mathf.Rad2Deg;

            Quaternion result = Quaternion.AngleAxis(angle, new Vector3(0, 0, 1));

            return result;
        }
    }

    void ChangeWeapon()
    {
        //Play the weapon change audio

        //----------------
        if (Item)
        {
            Item.GetComponent<Item>().Use(transform);
        }       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            Item = collision.transform;
            var script = Item.GetComponent<Item>();
            if (script.itemGuiText)
            {
                script.MarkedAsUsable();
            }
            else
            {
                script.Init();
                Debug.Log(script.itemGuiText);
                script.MarkedAsUsable();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            var script = Item.GetComponent<Item>();
            if (script.itemGuiText)
                script.MarkedAsNormal();
            Item = null;
        }
    }
}
