﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager:MonoBehaviour
{
    public static WeaponManager instance = null;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

}
