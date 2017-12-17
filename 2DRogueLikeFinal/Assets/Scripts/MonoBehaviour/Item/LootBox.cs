using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBox : Item
{
    public List<GameObject> weaponList;    

    public override void Use(Transform player)
    {
        int index = Random.Range(0, weaponList.Count);
        Instantiate(weaponList[index], transform.position, Quaternion.identity);
        GetComponent<PoolObject>().Destroy();
    }
}
