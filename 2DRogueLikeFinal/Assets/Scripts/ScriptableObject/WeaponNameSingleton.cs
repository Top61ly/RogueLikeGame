using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "ScriptableSingleton/WeaponName")]
public class WeaponNameSingleton : SingletonScriptableObject<WeaponNameSingleton>
{

    public Color normalItemColor;
    public Color rareItemColor;
    public Color superRareItemColor;    
  
    
    public Text GenerateItemName(Transform canvas, string name, RareLevel rareLevel)
    {      
        var go = Instantiate(Resources.Load("Prefabs/WeaponName"),canvas,false) as GameObject;
        Text text = go.GetComponent<Text>();
        text.text = name;
        switch (rareLevel)
        {
            case RareLevel.N:
                text.color = normalItemColor;
                break;
            case RareLevel.R:
                text.color = rareItemColor;
                break;
            case RareLevel.SR:
                text.color = superRareItemColor;
                break;
            default:
                break;
        }
        return text;
    }

    public void Test(Transform canvas)
    {
        Instantiate(Resources.Load("Prefabs/WeaponName"), canvas, false);
    }

}
