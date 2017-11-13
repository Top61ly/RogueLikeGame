using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BulletsGenerator
{
    public abstract class Weapon:MonoBehaviour
    {
        public int num;

        public float time;
        
        public GameObject bullet;

        protected float timePerGeneration;

        protected virtual void Start()
        {
            timePerGeneration = time / num;
            StartCoroutine(GenerateBullets());
            Destroy(gameObject, time);
        }

        protected abstract IEnumerator GenerateBullets();
    }
}
