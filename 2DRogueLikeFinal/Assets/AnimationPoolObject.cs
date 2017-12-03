using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationPoolObject : PoolObject
{
    public override void OnObjectReuse()
    {
        GetComponentInChildren<Animator>().SetTrigger("Trigger");
    }
}
