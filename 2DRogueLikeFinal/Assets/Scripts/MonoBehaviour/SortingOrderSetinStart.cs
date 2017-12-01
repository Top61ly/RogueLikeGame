using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingOrderSetinStart : MonoBehaviour
{    
	void Start ()
    {
        var spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sortingOrder = (int)Camera.main.WorldToScreenPoint(spriteRenderer.bounds.min).y * -1;
    }
}
