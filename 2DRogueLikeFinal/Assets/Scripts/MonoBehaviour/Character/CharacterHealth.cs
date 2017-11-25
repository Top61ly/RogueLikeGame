using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterHealth : MonoBehaviour
{
    public int playerIndex;

    public int healthPoints;

    public abstract void TakeDamage(int damage); 
}
