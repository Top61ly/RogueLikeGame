using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IRDSTable : IRDSObject
{
    int rdsCount;
    
    public IRDSTable(int _rdsCount, float _rdsProbability, bool _rdsUnique, bool _rdsAlways, bool _rdsEnable)
        :base(_rdsProbability,_rdsUnique,_rdsAlways,_rdsEnable)
    {
        rdsCount = _rdsCount;
    }

    IEnumerable<IRDSObject> rdsContents;
    IEnumerable<IRDSObject> rdsResult;  


}
