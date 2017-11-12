using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOrthographic : MonoBehaviour
{
    public float ppu;
    public float ppuScale;

    private Camera m_Camera;

    private void Awake()
    {
        m_Camera = GetComponent<Camera>();
    }

    private void Start()
    {
        m_Camera.orthographicSize = Screen.height / ppu / ppuScale / 2;
    }

}
