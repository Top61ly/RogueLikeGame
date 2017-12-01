using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "myCreate/Brains/Player Control Brain")]
public class PlayerControlBrain : Brain
{

    private PlayerMovement playerMovement;

    public override void Initialize(CharacterThinker thinker)
    {
        playerMovement = thinker.GetComponent<PlayerMovement>();
    }

    public override void Think(CharacterThinker thinker)
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        playerMovement.movement = new Vector2(h, v);

        playerMovement.facePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);


    }
}
