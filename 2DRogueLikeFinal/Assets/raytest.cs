using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class raytest : MonoBehaviour
{
    //Check Enemey   

    public float timerGap = 1f;
    public float timer = 0;

    private BulletsGenerator.BulletsAttack bulletsAttack;

    private float effectDisplayTime = 0.2f;
    public SpriteRenderer gunEffect;
    

    private void Awake()
    {
        bulletsAttack = GetComponent<BulletsGenerator.BulletsAttack>();
    }

    void Start()
    {
        DisableEffect();
	}
	
	void Update ()
    {
        //Update Gun Transform

        //Shoot Control
        timer += Time.deltaTime;


        if (Input.GetMouseButton(0) && timer >= timerGap&&Time.timeScale!=0)

            Shoot();

        if (timer >= timerGap * effectDisplayTime)

            DisableEffect();
	}

    void Shoot()
    {
        timer = 0;
        Debug.Log("shoot  "+timer);
        gunEffect.enabled = true;
        bulletsAttack.Shoot();
    }

    void DisableEffect()
    {
        gunEffect.enabled = false;
    }
}
