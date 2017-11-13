using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BulletsGenerator
{   
    public abstract class Bullets : ScriptableObject
    {
        public GameObject bullet;
        
        public void Init()
        {
            SpecificInit();
        }

        protected virtual void SpecificInit()
        { }

        public void Shoot(MonoBehaviour monoBehaviour,Transform transform)
        {
            ImmediateShoot(transform);
        }

        protected abstract void ImmediateShoot(Transform transform);
    }
}
