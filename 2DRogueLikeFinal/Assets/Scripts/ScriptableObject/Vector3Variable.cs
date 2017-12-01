using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "myCreate/Vector3 Variable")]
public class Vector3Variable : ScriptableObject
{
#if UNITY_EDITOR
    [Multiline]
    public string developerDescription = "";
#endif

    public Vector3 value;      
    
    public void SetValue(Vector3 para)
    {
        value = para;
    } 

    public void SetValue(Vector3Variable para)
    {
        value = para.value;
    }
}
