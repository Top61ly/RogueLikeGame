using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletsGenerator
{
    public class BulletsAttack : MonoBehaviour
    {
        public int health = 100;

        public Transform startPosition;

        public BulletsWave[] bulletsWaveCollected = new BulletsWave[0];

        private Animator animator;

        private void Start()
        {
            animator = GetComponent<Animator>();
            for (int i = 0;i<bulletsWaveCollected.Length;i++)
            {
                bulletsWaveCollected[i].Init();
            }
        }
        

        public void Shoot()
        {
            StartCoroutine(GenerateWaves());              
        }

        IEnumerator GenerateWaves()
        {
            int index = Random.Range(0, bulletsWaveCollected.Length);
            bulletsWaveCollected[index].Shoot(this, startPosition);
            yield return new WaitForSeconds(bulletsWaveCollected[index].time);
        }

    }
}