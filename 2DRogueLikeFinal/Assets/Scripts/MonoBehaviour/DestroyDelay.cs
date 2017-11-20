using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyDelay : MonoBehaviour
{
    public float destroyDelay = 0.5f;

	void Start ()
    {
        Destroy(gameObject, destroyDelay);		
	}	
}
