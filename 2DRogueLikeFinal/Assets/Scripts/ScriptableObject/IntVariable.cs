using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName ="myCreate/IntRange")]
public class IntVariable : ScriptableObject
{
#if UNITY_EDITOR
    [Multiline]
    public string DeveloperDescription = "";
#endif

    public int value;

    public void SetValue(int para)
    {
        value = para;
    }

    public void SetValue(IntVariable para)
    {
        value = para.value;
    }

    public void ApplyChange(int amount)
    {
        value += amount;
    }

    public void ApplyChange(IntVariable amount)
    {
        value += amount.value;
    }
}
