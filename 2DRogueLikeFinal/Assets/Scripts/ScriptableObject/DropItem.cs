using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DropItem<T>
{
    public string description;

    public float probability;

    public T item;
    public IntRange dropCount = new IntRange(1,1);
    public bool isUnique = true;
    public bool isAlways = false;
    public bool isEnabled = false;
}
