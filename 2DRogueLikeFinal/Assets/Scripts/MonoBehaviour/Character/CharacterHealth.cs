using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterHealth : PoolObject
{
    public int playerIndex;    

    public abstract void TakeDamage(int damage); 
}
