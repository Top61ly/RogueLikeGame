using UnityEngine;

public class AllWeapons : ResettableScriptableObject
{
    private static AllWeapons instance;

    private const string loadPath = "AllWeapons";

    public static AllWeapons Instance
    {
        get
        {
            if (!instance)
                instance = FindObjectOfType<AllWeapons>();

            if (!instance)
                instance = Resources.Load<AllWeapons>(loadPath);

            if (!instance)
                Debug.LogError("AllWeapons has not been created yet");

            return instance;
        }
        set
        {
            instance = value;
        }
    }

    public void UnLockWeapon()
    {

    }

    public override void Reset()
    {

    }
}
