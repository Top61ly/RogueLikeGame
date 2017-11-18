using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponControl : MonoBehaviour
{
    public bool isWeaponInRange;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {

        }
    }

    void ChangeWeapon()
    {
        if (isWeaponInRange)
        {

        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon"))
        {
            isWeaponInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon"))
        {
            isWeaponInRange = false;
        }
    }
}
