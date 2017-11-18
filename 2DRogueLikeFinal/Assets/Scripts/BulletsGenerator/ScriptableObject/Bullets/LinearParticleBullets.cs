using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace BulletsGenerator
{
    public class LinearParticleBullets : ParticleBullets
    {
        public int numOfWidth;

        public float offset;    
        
        protected override void ImmediateShoot(Transform transform)
        {
            GenerateWidth(transform);
        }

        private void GenerateWidth(Transform transform)
        {
            Vector3 startPosition = new Vector3(transform.position.x - (numOfWidth-1) * offset/2, transform.position.y, transform.position.z);
            for (int i = 0; i < numOfWidth; i++)
            {
                GameObject go = Instantiate(bullet, new Vector3(startPosition.x, startPosition.y, startPosition.z), Quaternion.identity, transform) as GameObject;
                startPosition.x += offset;
                go.GetComponent<Rigidbody2D>().AddForce(-Vector2.left * speed);
            }
        }
    }
}