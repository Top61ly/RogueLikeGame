using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IRDSTable : IRDSObject
{
    int rdsCount;
    IEnumerable<IRDSObject> rdsContents;
    IEnumerable<IRDSObject> rdsResult;  
}
