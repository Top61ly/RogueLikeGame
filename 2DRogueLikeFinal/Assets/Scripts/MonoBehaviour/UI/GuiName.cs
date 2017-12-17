using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuiName : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Transform guiNamePosition;

    public Text guiText;

    private void Update()
    {
        SetGuiTextPosition();
        if (Input.GetMouseButtonDown(0))
        {
            guiText.enabled = !guiText.enabled;
        }
    }

    private void SetGuiTextPosition()
    {
        Vector3 guiPosition = Camera.main.WorldToScreenPoint(guiNamePosition.position);
        guiText.transform.position = guiPosition;
    }
}
