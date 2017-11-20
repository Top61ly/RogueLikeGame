using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public float timerGap;
        
    public abstract void TriggerWeapon();
    public abstract void DisableEffect();
}
