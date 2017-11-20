using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearRangeWeapon : RangeWeapon
{
    public float shootForce;

    private Vector3 direction;
    private Transform startPosition;
    private Transform endPosition;

    private void Start()
    {
        startPosition = transform.Find("StartPosition");
        endPosition = startPosition.Find("EndPosition");
    }

    protected override void ImmediateShoot()
    {
        direction = (startPosition.position - endPosition.position).normalized;
        Debug.Log(direction);
        GameObject go = Instantiate(bullets, startPosition.position, Quaternion.identity) as GameObject;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        go.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        go.GetComponent<Rigidbody2D>().AddForce(direction * shootForce);
    }
}
