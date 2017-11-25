using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearRangeWeapon : RangeWeapon
{
    public float shootForce;

    public FloatRange angleRange;

    public float effectTime;
    public float effectForce;

    private Vector3 direction;
    public Transform startPosition;
    public Transform endPosition;    

    public SpriteRenderer shootEffect;

    protected override void SpecificInit()
    {
        var bullet = bullets.GetComponent<ProjectionBullets>();

        bullet.damage = damage;

        if (bullet)
        {
            bullet.playerIndex = playerIndex;
            bullet.effectForce = effectForce;
            bullet.effectTime = effectTime;
        }
        ImmediateDisableEffect();
    }

    protected override void ImmediateShoot()
    {
        if (weaponAnimator)
            weaponAnimator.SetTrigger("Shoot");
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
