using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Brain : ScriptableObject
{
    public virtual void Initialize(CharacterThinker thinker)
    { }
    public abstract void Think(CharacterThinker thinker);
}
