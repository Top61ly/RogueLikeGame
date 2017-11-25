using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new DropItemTable",menuName = "Create/DropItemTable")]
public class myItemDropTable : ScriptableObject
{
    public string description;

    public List<DropItem> itemList = new List<DropItem>();
    
    public GameObject generateItem()
    {
        IntRange itemRange = new IntRange(0, itemList.Count);

        return itemList[itemRange.Random].item;
    }  
   

}
