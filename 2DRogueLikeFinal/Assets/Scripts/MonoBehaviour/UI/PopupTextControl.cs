using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupTextControl : MonoBehaviour
{
    private static GameObject canvas;
    private static FloatingText popupText;

    private void Start()
    {
        Intialize();
    }

    public static void Intialize()
    {
        if (!canvas)
            canvas = GameObject.Find("Canvas");
        if (!popupText)
            popupText = Resources.Load<FloatingText>("Prefabs/PopupTextParent"); 
    }

    public static void CreateFloatingText(int damage, Transform transform)
    {
        FloatingText instance = Instantiate(popupText);
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position);

        instance.transform.SetParent(canvas.transform, false);
        IntRange randomRange = new IntRange(-10, 10);

        instance.transform.position = new Vector3(screenPosition.x + randomRange.Random, screenPosition.y + randomRange.Random, 0);
        instance.SetNumber(damage);
    }

}
