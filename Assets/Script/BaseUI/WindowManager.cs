using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WindowManager : Singleton<WindowManager>
{
    private bool m_closeing_all = false;

    private List<Window> m_wndList = new List<Window>();

    private List<Window> m_cache_list = new List<Window>();

    private Dictionary<WindowType, string> m_types = new Dictionary<WindowType, string>();

    #region Set Properties
    public List<Window> wndList
    {
        get
        {
            return m_wndList;
        }

        set
        {
            m_wndList = value;
        }
    }

    public List<Window> cache_list
    {
        get
        {
            return m_cache_list;
        }

        set
        {
            m_cache_list = value;
        }
    }

    public Dictionary<WindowType, string> types
    {
        get
        {
            return m_types;
        }

        set
        {
            m_types = value;
        }
    }

    public bool closeing_all
    {
        get
        {
            return m_closeing_all;
        }

        set
        {
            m_closeing_all = value;
        }
    }
    #endregion
    public void OnUpdate()
    {
        for (int i = 0; i < m_wndList.Count; i++)
        {
            m_wndList[i].OnUpdate();
        }
    }

    public void Insert(Window wnd)
    {
        if (CheckInsertWindow(wnd))
        {
            m_wndList.Add(wnd);
        }
    }

    public void Remove(Window wnd)
    {
        m_wndList.Remove(wnd);
    }

    public void CloseWindow(Window wnd)
    {
        if (wnd != null)
        {
            wnd.Close();
        }
    }
    public void CloseAllWindows()
    {
        CloseAllWindows(false);
    }
    #region Tool Function
    private void CloseAllWindows(bool force)
    {
        m_closeing_all = true;
        for (int i = 0; i < m_wndList.Count; i++)
        {
            Window wnd = m_wndList[i];

            if (wnd != null)
            {
                if (!force && wnd.Wnd_type == Window.WndType.TypeCache)
                {
                    wnd.Hide();
                }
                wnd.Close();
            }
        }
        m_wndList = new List<Window>();
    }
    private bool CheckInsertWindow(Window wnd)
    {
        for (int i = 0; i < m_wndList.Count; i++)
        {
            if (wnd == m_wndList[i])
            {
                return false;
            }
        }
        return true;
    }

    public string GetWindowTypeName(WindowType type)
    {
        string str;
        if (!m_types.TryGetValue(type, out str))
        {
            str = type.ToString();
            m_types.Add(type, str);
        }

        return str;
    }

    public Window FindWindow(WindowType type)
    {
        string windowName = GetWindowTypeName(type);
        for (int i = 0; i < m_wndList.Count; i++)
        {
            if (m_wndList[i].name == windowName)
                return m_wndList[i];
        }
        return null;
    }

    public Window FindWindow(string windowName)
    {
        for (int i = 0; i < m_wndList.Count; i++)
        {
            if (m_wndList[i].name == windowName)
                return m_wndList[i];
        }

        //foreach (var name in Enum.GetNames(typeof(WindowType)))
        //{
        //    if (name == windowName)
        //    {
        //        for (int i = 0; i < m_wndList.Count; i++)
        //        {
        //            if (m_wndList[i].name == windowName)
        //                return m_wndList[i];
        //        }
        //    }
        //}
        return null;
    }
    #endregion
}
