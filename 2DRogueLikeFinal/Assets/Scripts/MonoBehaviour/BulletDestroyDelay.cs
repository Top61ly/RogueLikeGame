using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDestroyDelay : PoolObject
{
    public float destroyDelay = 0.5f;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        animator.SetTrigger("Explotion");
        StartCoroutine(DelayDestroy());
    }


    IEnumerator DelayDestroy()
    {
        yield return new WaitForSeconds(destroyDelay);
        Destroy();
    }	
}
