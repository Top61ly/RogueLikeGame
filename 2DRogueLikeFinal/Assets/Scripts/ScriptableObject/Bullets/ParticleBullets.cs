using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BulletsGenerator
{
    public abstract class ParticleBullets : Bullets
    {
        public int numOfWave = 1;

        public float wholeTime = 1;

        public int speed = 100;

        protected WaitForSeconds wait;
        
        public new void Init()
        {
            wait = new WaitForSeconds(wholeTime / numOfWave);
            SpecificInit();
        }

        public new void Shoot(MonoBehaviour monoBehaviour,Transform transform)
        {
            monoBehaviour.StartCoroutine(ShootCoroutine(transform));
        }

        protected IEnumerator ShootCoroutine(Transform transform)
        {
            for (int i = 0; i<numOfWave; i++)
            {
                ImmediateShoot(transform);
                
                yield return wait;
            }
        }
    }
}
