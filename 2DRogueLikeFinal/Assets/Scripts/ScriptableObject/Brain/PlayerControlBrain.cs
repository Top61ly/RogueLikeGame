using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[CreateAssetMenu(menuName = "myCreate/Brains/Player Control Brain")]
public class PlayerControlBrain : Brain
{

    private PlayerMovement playerMovement;

    private PlayerAttack playerAttack;

    public float searchRadius = 5f;

    public override void Initialize(CharacterThinker thinker)
    {
        playerMovement = thinker.GetComponent<PlayerMovement>();
        playerAttack = thinker.GetComponent<PlayerAttack>();
    }

    public override void Think(CharacterThinker thinker)
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        playerMovement.movement = new Vector2(h, v);

        playerMovement.direction = GetTarget(thinker);

        playerAttack.direction = GetTarget(thinker);

        //playerMovement.direction = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public Vector2 GetTarget(CharacterThinker thinker)
    {

        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll( thinker.transform.position, searchRadius, LayerMask.GetMask("Enemy"));

        List<Collider2D> collider2dList = new List<Collider2D>();

        for (int i = 0; i < collider2Ds.Length; i++)
        {
            // Physics2D.Raycast()
            RaycastHit2D hit = Physics2D.Raycast(thinker.transform.position, (collider2Ds[i].transform.position - thinker.transform.position).normalized, searchRadius, LayerMask.GetMask("Environment"));
            if (!hit.collider)
            {
                collider2dList.Add(collider2Ds[i]);
            }
        }

        if (collider2dList.Count <= 0)
        {            
            return playerMovement.movement.normalized;
        }
        else
        {
            Vector3 target = collider2dList[0].transform.position;
            float distance = Vector3.Distance(thinker.transform.position, target);

            for (int i = 1; i < collider2dList.Count; i++)
            {
                if (Vector3.Distance(thinker.transform.position, collider2dList[i].transform.position) < distance)
                    target = collider2dList[i].transform.position;
            }

            return (target-thinker.transform.position).normalized;
        }
    }
}
