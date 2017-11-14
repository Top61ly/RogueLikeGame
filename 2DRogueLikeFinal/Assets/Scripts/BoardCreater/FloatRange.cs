using System;

[Serializable]
public class FloatRange
{
    public float m_Min;
    public float m_Max;

    public FloatRange(float min, float max)
    {
        m_Min = min;
        m_Max = max;
    }

    public float Random
    {
        get
        {
            return UnityEngine.Random.Range(m_Min, m_Max);
        }
    }
}
