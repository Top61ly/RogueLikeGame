using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DropTable<T> : ScriptableObject
{
    public List<DropItem<T>> dropItems = new List<DropItem<T>>();

    public List<T> GetResult()
    {
        List<T> result = new List<T>();

        return result;
    }
}


public class testItem
{
    public GameObject go;
    public float percentage;
}

