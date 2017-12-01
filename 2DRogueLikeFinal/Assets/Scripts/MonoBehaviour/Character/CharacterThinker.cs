using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterThinker : MonoBehaviour
{

    public Brain brain;

    private void OnEnable()
    {
        if (brain)
            brain.Initialize(this);
    }

    private void Update()
    {
        brain.Think(this);
    }
}
