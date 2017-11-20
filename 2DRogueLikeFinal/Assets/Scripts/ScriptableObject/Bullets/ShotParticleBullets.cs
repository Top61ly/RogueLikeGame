using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BulletsGenerator
{
    public class ShotParticleBullets : ParticleBullets
    {
        public int numOfWidth;

        public int offset;
        
        protected override void ImmediateShoot(Transform transform)
        {
            Quaternion startQuaternion = transform.rotation * Quaternion.Euler(0, 0, -(numOfWidth-1) * offset / 2);
            for (int i = 0; i<numOfWidth;i++)
            {
                GameObject go = Instantiate(bullet, transform.position, startQuaternion) as GameObject;
                startQuaternion *= Quaternion.Euler(0, 0, offset);
                go.GetComponent<Rigidbody2D>().AddForce(go.transform.right * speed);
            }   
        }
    }
}