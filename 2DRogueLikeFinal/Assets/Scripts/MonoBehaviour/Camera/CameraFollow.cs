using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform Player;

    public float smoothing = 5f;

    private Vector3 offset;

    private void OnEnable()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;

        transform.position.Set(Player.position.x,Player.position.y,-10);
        offset = transform.position - Player.position;
    }

    private void FixedUpdate()
    {
        Vector3 targetCameraPosition = Player.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetCameraPosition, smoothing * Time.deltaTime);
    }

}
