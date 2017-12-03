using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


public class TriggerTheDoor : MonoBehaviour
{
    public EventHandler levelComplete;    

    private void OnTriggerEnter2D(Collider2D collision)
    {        
        if (collision.CompareTag("Player"))
        {
            GameManager.instance.player = collision.gameObject;

            if (levelComplete != null)
                levelComplete.Invoke(this,new EventArgs());

            Destroy(gameObject);
           //SceneManager.LoadScene("test");
        }
    }
}
