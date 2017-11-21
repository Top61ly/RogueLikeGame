using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private Transform endPosition;
    private Transform startPosition;
    private Transform player;

    private Vector3 direction;

    private void Start()
    {
        GetComponent<Weapon>().Init();
        player = GameObject.Find("Player").transform;
        endPosition = transform.Find("EndPosition");
        startPosition = endPosition.Find("StartPosition");
    }

    private void Update()
    {
        direction = (player.position - endPosition.position).normalized;

        startPosition.position = Vector3.Lerp(endPosition.position,endPosition.position + direction,0.01f);         
    }


}
