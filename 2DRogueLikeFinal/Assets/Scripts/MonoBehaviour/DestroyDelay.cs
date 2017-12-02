using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyDelay : PoolObject
{
    public float destroyDelay = 0.5f;

    private void Awake()
    {
        StartCoroutine(DelayDestroy());
    }


    IEnumerator DelayDestroy()
    {
        yield return new WaitForSeconds(destroyDelay);
        Destroy();
    }	
}
