using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletsGenerator
{ 
    public class LinearWeapon : Weapon
    {
        public Bullets bullets;
        public BulletsWave bulletsWave;

        public float force = 100;

        protected override IEnumerator GenerateBullets()
        {
            for (int i = 0; i<num; i++)
            {
                GameObject go = Instantiate(bullet, transform.position, Quaternion.identity);
                go.GetComponent<Rigidbody2D>().AddForce(-go.transform.up*force);
                yield return new WaitForSeconds(timePerGeneration);
            }
        }
    }
}