using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class raytest : MonoBehaviour
{
    public GameObject bullet;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right,10.0f,LayerMask.NameToLayer("Environment"));
            if (hit.collider != null)
                Debug.Log(hit.collider.name);
            else
                Debug.Log("Null");
        }
    }
}
