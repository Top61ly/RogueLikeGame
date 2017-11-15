using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class raytest : MonoBehaviour {

    // Use this for initialization
    void Start()
    {

        int a = 1 << 8;

        a = ~a;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up,Mathf.Infinity,a);
        if (hit.collider != null)
        {
            Debug.Log(hit.collider.name);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
