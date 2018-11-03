using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class Window
{
    public Window()
    {
        m_name = GetType().Name;
    }
    public enum WndType
    {
        TypeNormal = 1,
        TypeCache = 2,
        TypeRelease = 3,
    }

    public delegate void VoidHandle();

    private Transform m_Parent = null;
    private WndType wnd_type = WndType.TypeNormal;
    protected VoidHandle m_open_handle = null;
    protected VoidHandle m_close_handle = null;
    protected UILoader.LoadHandle m_wnd_load_handle = null;
    protected string m_name = string.Empty;

    protected int m_sort_order = 0;
    protected GameObject m_go = null;
    protected Canvas m_canvas;
    
    protected bool m_show = true;
    private bool m_done = false;
    private string open_param = string.Empty;
    private object close_param = null;

    protected Dictionary<string, GameObject> m_cache_gos = new Dictionary<string, GameObject>();

    #region Set Properties
    public virtual string windowName
    {
        get
        {
            return string.Empty;
        }
    }

    public WndType Wnd_type
    {
        get
        {
            return wnd_type;
        }

        set
        {
            wnd_type = value;
        }
    }

    public UILoader.LoadHandle wnd_load_handle
    {
        get
        {
            return m_wnd_load_handle;
        }
        set
        {
            m_wnd_load_handle = value;
        }
    }

    public string name
    {
        get { return m_name; }
    }

    public bool show
    {
        get
        {
            return m_show;
        }

        set
        {
            m_show = value;
        }
    }

    public string Open_param
    {
        get
        {
            return open_param;
        }

        set
        {
            open_param = value;
        }
    }

    public object Close_param
    {
        get
        {
            return close_param;
        }

        set
        {
            close_param = value;
        }
    }

    protected bool done
    {
        get
        {
            return m_done;
        }

        set
        {
            m_done = value;
        }
    }
    #endregion

    public virtual void Close()
    {
        //..
        m_show = false;
        m_done = false;
        m_cache_gos.Clear();

        if (m_go != null)
        {
            UIEventListener[] evts = m_go.GetComponentsInChildren<UIEventListener>();
            for (int i = 0; i < evts.Length; i++)
            {
                UIEventListener evt = evts[i];
                evt.onClick = null;
                evt.onDown = null;
                evt.onEnter = null;
                evt.onExit = null;
                evt.onUp = null;
                evt.onSelect = null;
                evt.onUpdateSelect = null;
                evt.onDrag = null;
                evt.onBeginDrag = null;
                evt.onEndDrag = null;
                evt.onLongClick = null;
                evt.onLongPress = null;
            }

            GameObject.Destroy(m_go);
            open_param = string.Empty;
        }

        m_go = null;
        m_canvas = null;
        WindowManager.instance.Remove(this);
    }

    public virtual void Show()
    {
        //..
        m_show = true;
        if (!m_done)
        {
            return;
        }

        if (m_canvas != null)
        {
            m_canvas.enabled = true;
        }
    }

    public virtual void Hide()
    {
        m_show = false;
        if (!m_done)
        {
            return;
        }

        if (m_canvas!= null)
        {
            m_canvas.enabled = false;
        }
    }

    public virtual void OnUpdate()
    {
        if (m_go == null || m_show == false)
            return;
    }

    protected virtual void Initialize()
    {

    }

    public void OnLoad(GameObject go)
    {
        m_go = go;
        if (m_go.GetComponent<GraphicRaycaster>() == null)
        {
            m_go.AddComponent<GraphicRaycaster>();
        }

        m_Parent = GameObject.Find("Main").GetComponent<Transform>();
        if (m_Parent != null)
        {
            m_go.transform.SetParent(m_Parent,false);
        }
        else
        {
            Debug.LogError("Main ui window is null or empty!!");
        }


        m_canvas = m_go.GetComponent<Canvas>();
        if (m_canvas == null)
        {
            m_canvas = m_go.AddComponent<Canvas>();
        }
        m_canvas.overrideSorting = true;
        m_canvas.sortingOrder = m_sort_order;

        Initialize();
        m_done = true;

        if (m_show)
        {
            Show();
            //..
            if (m_open_handle != null)
            {
                m_open_handle();//..
                m_open_handle = null;
            }
        }
        else
        {
            Hide();
        }
        //..
    }

    //..
    public void ReLoad()
    {

    }

    #region Tool Function
    //Find obj

    protected GameObject Find(string name)
    {
        if (m_cache_gos.ContainsKey(name))
            return m_cache_gos[name];

        m_cache_gos.Add(name, UIHelper.FindGameObject(m_go, name));
        return m_cache_gos[name];
    }

    protected GameObject Find(string parent, string name)
    {
        if (!m_cache_gos.ContainsKey(parent))
        {
            m_cache_gos.Add(parent, UIHelper.FindGameObject(m_go, parent));
        }

        return UIHelper.FindGameObject(m_cache_gos[parent], name);
    }

    protected GameObject Find(GameObject obj, string name)
    {
        return UIHelper.FindGameObject(obj, name);
    }

    protected T Find<T>(GameObject go) where T : UnityEngine.Object
    {
        if (go != null)
            return go.GetComponent<T>();

        return null;
    }

    protected T Find<T>(string name) where T : UnityEngine.Object
    {
        GameObject go = Find(name);
        if (go != null)
            return go.GetComponent<T>();

        return null;
    }

    protected T Find<T>(string parent, string name) where T : UnityEngine.Object
    {
        GameObject go = Find(parent, name);
        if (go != null)
            return go.GetComponent<T>();

        return null;
    }

    protected T Find<T>(GameObject obj, string name) where T : UnityEngine.Object
    {
        GameObject go = Find(obj, name);
        if (go != null)
            return go.GetComponent<T>();

        return null;
    }

    //add child 

    protected GameObject AddChild(GameObject parent, GameObject template,bool isInstance)
    {
        return UIHelper.AddChildGameObject(parent, template, isInstance);
    }

    protected GameObject AddChild(GameObject template, string bindParentName,bool isInstance)
    {
        GameObject parent = Find(bindParentName);
        return UIHelper.AddChildGameObject(parent, template, isInstance);
    }

    // 要添加的物体的父亲 。  要添加的物体本身 。要添加到的面板上
    protected GameObject AddChild(string parentName, string goName, string bindParentName,bool isInstance)
    {
        GameObject parent = Find(bindParentName);
        GameObject clone = Find(parentName, goName);

        return UIHelper.AddChildGameObject(parent, clone, isInstance);
    }
    protected GameObject AddChild(string goName, string bindParentName, bool isInstance)
    {
        GameObject parent = Find(bindParentName);
        GameObject clone = Find(goName);

        return UIHelper.AddChildGameObject(parent, clone, isInstance);
    }


    //set icon
    protected void SetIcon(RawImage img, string icon)
    {
        if (img != null && !string.IsNullOrEmpty(icon))
        {
            UIHelper.SetIcon(img, icon);
        }
    }

    protected void SetImage(Image img, string atlasName, string name)
    {
        UIHelper.SetImage(img, atlasName, name);
    }
    #endregion

    #region Register Event
    //protected UIEventListener Register(GameObject go, bool drag, bool scroll, float cd, Image cdImg, FunctionType type = FunctionType.Type_None)
    //{
    //    WindowType windowType = (WindowType)Enum.Parse(typeof(WindowType), name, true);
    //    if (type != FunctionType.Type_None)
    //    {
    //        OpenFunctionManager.instance.AddInWndFunction(windowType, go, type);
    //    }
    //    if (windowType == WindowType.MainFunctionWindow)
    //    {
    //        FunctionGuideManager.instance.FunGuideAddItems(windowType, go);
    //    }
    //    return UIHelper.Register(go, drag, scroll, cd, cdImg);
    //}

    //protected UIEventListener Register(GameObject go, bool drag, bool scroll, float cd)
    //{
    //    return Register(go, drag, scroll, cd, null);
    //}

    //protected UIEventListener Register(GameObject go, bool drag, bool scroll)
    //{
    //    return Register(go, drag, scroll, 0, null);
    //}

    //protected UIEventListener Register(GameObject go, bool drag)
    //{
    //    return Register(go, drag, false, 0, null);
    //}

    //protected UIEventListener Register(GameObject go, float cd, Image cdImg)
    //{
    //    return Register(go, false, false, cd, cdImg);
    //}

    //protected UIEventListener Register(GameObject go, float cd)
    //{
    //    return Register(go, false, false, cd, null);
    //}

    //protected UIEventListener Register(GameObject go, float cd, FunctionType type)
    //{
    //    return Register(go, false, false, cd, null, type);
    //}

    //protected UIEventListener Register(GameObject go, FunctionType type)
    //{
    //    return Register(go, false, false, 0, null, type);
    //}

    //protected UIEventListener Register(GameObject go)
    //{
    //    return Register(go, false, false, 0, null);
    //}

    //protected UIEventListener Register(string name, bool drag, bool scroll, float cd, Image cdImg)
    //{
    //    GameObject go = Find(name);
    //    return Register(go, drag, scroll, cd, cdImg);
    //}

    //protected UIEventListener Register(string name, bool drag, bool scroll, float cd)
    //{
    //    GameObject go = Find(name);
    //    return Register(go, drag, scroll, cd, null);
    //}

    //protected UIEventListener Register(string name, bool drag, bool scroll)
    //{
    //    GameObject go = Find(name);
    //    return Register(go, drag, scroll, 0, null);
    //}

    //protected UIEventListener Register(string name, bool drag)
    //{
    //    GameObject go = Find(name);
    //    return Register(go, drag, false, 0, null);
    //}

    //protected UIEventListener Register(string name, float cd, Image cdImg)
    //{
    //    GameObject go = Find(name);
    //    return Register(go, false, false, cd, cdImg);
    //}

    //protected UIEventListener Register(string name, float cd)
    //{
    //    GameObject go = Find(name);
    //    return Register(go, false, false, cd, null);
    //}

    //protected UIEventListener Register(string name)
    //{
    //    GameObject go = Find(name);
    //    return Register(go, false, false, 0, null);
    //}

    //protected UIEventListener Register(string parent, string name, bool drag, bool scroll, float cd, Image cdImg)
    //{
    //    GameObject go = Find(parent, name);
    //    return Register(go, drag, scroll, cd, cdImg);
    //}

    //protected UIEventListener Register(string parent, string name, bool drag, bool scroll, float cd)
    //{
    //    GameObject go = Find(parent, name);
    //    return Register(go, drag, scroll, cd, null);
    //}

    //protected UIEventListener Register(string parent, string name, bool drag, bool scroll)
    //{
    //    GameObject go = Find(parent, name);
    //    return Register(go, drag, scroll, 0, null);
    //}

    //protected UIEventListener Register(string parent, string name, bool drag)
    //{
    //    GameObject go = Find(parent, name);
    //    return Register(go, drag, false, 0, null);
    //}

    //protected UIEventListener Register(string parent, string name, float cd, Image cdImg)
    //{
    //    GameObject go = Find(parent, name);
    //    return Register(go, false, false, cd, cdImg);
    //}

    //protected UIEventListener Register(string parent, string name, float cd)
    //{
    //    GameObject go = Find(parent, name);
    //    return Register(go, false, false, cd, null);
    //}

    //protected UIEventListener Register(string name, FunctionType type)
    //{
    //    GameObject go = Find(name);
    //    return Register(go, false, false, 0, null, type);
    //}

    //protected UIEventListener Register(string parent, string name, FunctionType type)
    //{
    //    GameObject go = Find(parent, name);
    //    return Register(go, false, false, 0, null, type);
    //}

    //protected UIEventListener Register(string parent, string name)
    //{
    //    GameObject go = Find(parent, name);
    //    return Register(go, false, false, 0, null);
    //}

    //protected UIEventListener Register(GameObject obj, string name, bool drag, bool scroll, float cd, Image cdImg)
    //{
    //    GameObject go = Find(obj, name);
    //    return Register(go, drag, scroll, cd, cdImg);
    //}

    //protected UIEventListener Register(GameObject obj, string name, bool drag, bool scroll, float cd)
    //{
    //    GameObject go = Find(obj, name);
    //    return Register(go, drag, scroll, cd, null);
    //}

    //protected UIEventListener Register(GameObject obj, string name, bool drag, bool scroll)
    //{
    //    GameObject go = Find(obj, name);
    //    return Register(go, drag, scroll, 0, null);
    //}

    //protected UIEventListener Register(GameObject obj, string name, bool drag)
    //{
    //    GameObject go = Find(obj, name);
    //    return Register(go, drag, false, 0, null);
    //}

    //protected UIEventListener Register(GameObject obj, string name, float cd, Image cdImg)
    //{
    //    GameObject go = Find(obj, name);
    //    return Register(go, false, false, cd, cdImg);
    //}

    //protected UIEventListener Register(GameObject obj, string name, float cd)
    //{
    //    GameObject go = Find(obj, name);
    //    return Register(go, false, false, cd, null);
    //}

    //protected UIEventListener Register(GameObject obj, string name, FunctionType type)
    //{
    //    GameObject go = Find(obj, name);
    //    return Register(go, false, false, 0, null, type);
    //}

    //protected UIEventListener Register(GameObject obj, string name)
    //{
    //    GameObject go = Find(obj, name);
    //    return Register(go, false, false, 0, null);
    //}
    #endregion

    protected UIEventListener Register(GameObject go)
    {
        if (go == null)
            return null;

        return UIHelper.Register(go);
    }
    protected UIEventListener Register(string parent,string name)
    {
        GameObject go = Find(parent, name);
        return Register(go);
    }
}
