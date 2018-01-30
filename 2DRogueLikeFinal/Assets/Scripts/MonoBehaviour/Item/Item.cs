using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum RareLevel
{
    N = 0,
    R,
    SR
}

public abstract class Item : MonoBehaviour
{
    public string itemName;
    public RareLevel rareLevel = RareLevel.N;

    public Transform guiNamePosition;
    public Text itemGuiText;

    public virtual void Init()
    {
        if (!itemGuiText)
        {
            Transform canvas = GameObject.Find("Canvas").transform;
            itemGuiText = WeaponNameSingleton.Instance.GenerateItemName(canvas, name, rareLevel);
            MarkedAsNormal();
        }
    }

    protected void Update()
    {
        if (itemGuiText)
            SetitemGuiTextPosition();      
    }

    private void SetitemGuiTextPosition()
    {
        Vector3 guiPosition = Camera.main.WorldToScreenPoint(guiNamePosition.position);
        itemGuiText.transform.position = guiPosition;
    }

    public virtual void MarkedAsNormal()
    {
        itemGuiText.enabled = false;
    }

    public virtual void MarkedAsUsable()
    {
        itemGuiText.enabled = true;
    }

    public abstract void Use(Transform player);    
}
