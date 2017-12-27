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
    public string name;
    public RareLevel rareLevel = RareLevel.N;

    public Transform guiNamePosition;
    public Text guiText;

    public virtual void Init()
    {
        guiText = WeaponNameSingleton.Instance.GenerateItemName(name,rareLevel);        
    }

    protected void Update()
    {
                
    }

    private void SetGuiTextPosition()
    {
        Vector3 guiPosition = Camera.main.WorldToScreenPoint(guiNamePosition.position);
        guiText.transform.position = guiPosition;
    }

    public abstract void Use(Transform player);    
}
