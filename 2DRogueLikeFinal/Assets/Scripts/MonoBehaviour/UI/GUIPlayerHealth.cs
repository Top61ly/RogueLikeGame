using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIPlayerHealth : MonoBehaviour
{
    public IntVariable playerHealth;
    public IntVariable playerMaxHealth;

    public Image healthPoint;

    private void Start()
    {
        for (int i = 0; i<playerMaxHealth.value;i++)
        {
            Instantiate(healthPoint, transform);
        }
        CheckHealth();
    }

    public void CheckHealth()
    {
        for (int i = playerMaxHealth.value - 1; i >=( playerHealth.value>0?playerHealth.value:0);i--)
        {
            var image = transform.GetChild(i).GetComponent<Image>();
            image.color = new Color(1, 1, 1, 0.2f);
        }
    }

}
