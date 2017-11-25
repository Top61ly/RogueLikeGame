using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DropItem
{
    public string description;

    public float probability;

    public GameObject item;
    public IntRange dropCount = new IntRange(1,1);
    public bool isUnique = true;
    public bool isAlways = false;
    public bool isEnabled = false;
}
