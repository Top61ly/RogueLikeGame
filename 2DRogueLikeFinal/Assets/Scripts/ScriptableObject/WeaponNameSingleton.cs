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
        
    private Transform _canvas;
    
    /// <summary>
    /// Set canvas transform
    /// </summary>
    /// <param name="canvas"></param>
    public void SetCanvas(Transform canvas)
    {
        _canvas = canvas;
    }
    
    public Text GenerateItemName(string name, RareLevel rareLevel)
    {
        var text = Instantiate(Resources.Load("Prefabs/WeaponName"), _canvas) as Text;
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


}
