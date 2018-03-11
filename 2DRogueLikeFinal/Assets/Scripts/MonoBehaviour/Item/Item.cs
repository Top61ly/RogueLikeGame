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
        if (!itemGuiText && itemName.Length>0)
        {
            Transform canvas = GameObject.Find("Canvas").transform;
            Debug.Log(canvas.childCount);
            itemGuiText = WeaponNameSingleton.Instance.GenerateItemName(canvas, itemName, rareLevel);
            MarkedAsNormal();
        }
    }

    protected void Update()
    {
        if (itemGuiText && guiNamePosition)
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
        if (itemGuiText)
            itemGuiText.enabled = true;
    }

    public abstract void Use(Transform player);    
}
