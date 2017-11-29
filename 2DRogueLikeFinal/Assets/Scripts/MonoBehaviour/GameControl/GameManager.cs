using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player;  //player
    public Vector3 generatePoint = new Vector3(5,5,0);  //player point 
    
    public static GameManager instance;

    private void Awake()
    {   
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else if (instance != this)
        {
            Destroy (gameObject);
        }

        
    }


    ////Get the player with weapon;
    //public GameObject GetPlayer()
    //{
    //    return player;
    //}

    //public Vector2 GetPlayerPoint()
    //{
    //    return generatePlayerPoint;
    //}
}
