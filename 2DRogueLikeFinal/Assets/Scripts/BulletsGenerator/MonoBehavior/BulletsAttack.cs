using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletsGenerator
{
    public class BulletsAttack : MonoBehaviour
    {
        public int health = 100;

        public Transform bulletsAttack;

        public BulletsWave[] bulletsWaveCollected = new BulletsWave[0];

        private bool isCoroutineOver = false;

        private Animator animator;

        private void Start()
        {
            animator = GetComponent<Animator>();
            for (int i = 0;i<bulletsWaveCollected.Length;i++)
            {
                bulletsWaveCollected[i].Init();
            }
            StartCoroutine(GenerateWaves());
        }



        public void Shoot()
        {
            if (isCoroutineOver)           
                StartCoroutine(GenerateWaves());
                       
        }

        IEnumerator GenerateWaves()
        {
            isCoroutineOver = false;
            int index = Random.Range(0, bulletsWaveCollected.Length);
            bulletsWaveCollected[index].Shoot(this, transform);
            yield return new WaitForSeconds(bulletsWaveCollected[index].time);
            isCoroutineOver = true;
        }

    }
}