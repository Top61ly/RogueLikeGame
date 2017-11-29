using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingText : MonoBehaviour
{
    public Text numberText;

    public void SetNumber(int damage)
    {
        numberText.text = damage.ToString();
    }
}
