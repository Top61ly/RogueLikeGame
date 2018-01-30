using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasControl : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            WeaponNameSingleton.Instance.GenerateItemName(transform,"aaa",RareLevel.N);
        }
    }

}
