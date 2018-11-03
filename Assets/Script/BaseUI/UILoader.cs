using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILoader : Singleton<UILoader>
{
    public delegate void LoadHandle(Object go);

    private LoadHandle m_resourceHandle;


    public LoadHandle resourceHandle
    {
        get
        {
            return m_resourceHandle;
        }

        set
        {
            m_resourceHandle = value;
        }
    }
}
