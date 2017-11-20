using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BulletsGenerator
{
    public class BulletsWave : ScriptableObject
    {
        public string description;

        public Bullets[] bullets = new Bullets[0];
      
        [HideInInspector]
        public float time = 0;

        public void Init()
        {
            for (int i = 0; i < bullets.Length ; i++)
            {
                ParticleBullets particleBullets = bullets[i] as ParticleBullets;
                if (particleBullets)
                {
                    particleBullets.Init();
                    time = time > particleBullets.wholeTime ? time : particleBullets.wholeTime; 
                }
                else
                    bullets[i].Init();
            }
        }

        public void Shoot(MonoBehaviour monoBehaviour,Transform transform)
        {
            for (int i = 0;i<bullets.Length;i++)
            {
                ParticleBullets particleBullets = bullets[i] as ParticleBullets;
                if (particleBullets)
                    particleBullets.Shoot(monoBehaviour, transform);
                else
                    bullets[i].Shoot(monoBehaviour,transform);
            }
        }
    }
}
