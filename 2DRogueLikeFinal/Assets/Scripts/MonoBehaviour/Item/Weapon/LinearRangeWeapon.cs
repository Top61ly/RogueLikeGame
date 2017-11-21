using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearRangeWeapon : RangeWeapon
{
    public float shootForce;

    public FloatRange angleRange;

    private Vector3 direction;
    private Transform startPosition;
    private Transform endPosition;

    private SpriteRenderer shootEffect;

    protected override void SpecificInit()
    {
        endPosition = transform.Find("EndPosition");
        startPosition = endPosition.Find("StartPosition");
        shootEffect = transform.Find("ShootEffect").GetComponent<SpriteRenderer>();
        shootEffect.enabled = false;
    }

    protected override void ImmediateShoot()
    {
        direction = (startPosition.position - endPosition.position).normalized;
        GameObject go = Instantiate(bullets, startPosition.position, Quaternion.identity) as GameObject;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        angle += angleRange.Random;
        var goQuaternion = Quaternion.AngleAxis(angle, Vector3.forward);
        go.transform.rotation = goQuaternion;
        direction = goQuaternion * Vector3.right;
        go.GetComponent<Rigidbody2D>().AddForce(direction * shootForce);
    }

    protected override void ImmediateEnableEffect()
    {
        shootEffect.enabled = true;
    }

    protected override void ImmediateDisableEffect()
    {
        shootEffect.enabled = false;
    }
}
