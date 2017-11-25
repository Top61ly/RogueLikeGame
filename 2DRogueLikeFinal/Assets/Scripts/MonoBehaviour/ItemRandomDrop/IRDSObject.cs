using System.Collections;
using System.Collections.Generic;
using System;

public class IRDSObject
{
    float rdsProbability;
    bool rdsUnique;
    bool rdsAlways;
    bool rdsEnabled;

    public IRDSObject()
    {
        rdsProbability = 0;
        rdsUnique = false;
        rdsAlways = false;
        rdsEnabled = false;
    }

    public IRDSObject(float _rdsProbability,bool _rdsUnique,bool _rdsAlways,bool _rdsEnable)
    {
        rdsProbability =_rdsProbability;
        rdsUnique = _rdsUnique;
        rdsAlways = _rdsAlways;
        rdsEnabled = _rdsEnable;            
    }

    event EventHandler rdsPreResultEvaluation;

    event EventHandler rdsHit;

    event EventHandler<ResultEventArgs> rdsPostResultEvaluation;

    public virtual void OnRDSPreResultEvaluation(EventArgs e)
    { }

    public virtual void OnRDSHit(EventArgs e)
    { }

    public virtual void OnRDSPostResultEvaluation(ResultEventArgs e)
    { }


}

public class ResultEventArgs : EventArgs
{

}
