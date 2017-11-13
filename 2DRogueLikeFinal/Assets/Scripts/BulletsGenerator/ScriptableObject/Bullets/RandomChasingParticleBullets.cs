using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletsGenerator
{
    public class RandomChasingParticleBullets : ParticleBullets
    {
        private Transform _playerTransform;

        protected override void SpecificInit()
        {
            base.SpecificInit();
            _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }

        protected override void ImmediateShoot(Transform transform)
        {
            for (int i = 0; i<numOfWave;i++)
            {
                GameObject go = Instantiate(bullet, transform.position, Quaternion.identity);
                Vector2 direction = _playerTransform.position - transform.position;
                go.GetComponent<Rigidbody2D>().AddForce(direction * speed);
            }
        }
    }
}