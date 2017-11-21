using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class IRDSValue<T> : IRDSObject
{
    T rdsValue;

    public IRDSValue(T _rdsValue,float _rdsProbability, bool _rdsUnique, bool _rdsAlways, bool _rdsEnable)
        :base(_rdsProbability,_rdsUnique,_rdsAlways,_rdsEnable)
    {
        rdsValue = _rdsValue;
    }

}

