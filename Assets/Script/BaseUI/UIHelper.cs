using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UIHelper
{
    public static Window CreateWindow(WindowType type, string openParam, object closeParam, Window.VoidHandle openHandle)
    {
        Window wnd = Activator.CreateInstance(Type.GetType(WindowManager.instance.GetWindowTypeName(type))) as Window;
        WindowManager.instance.Insert(wnd);
        //异步模式

        //wnd.wnd_load_handle = delegate(UnityEngine.Object obj)
        //{
        //    if (obj == null)
        //    {
        //        return;
        //    }
        //    if (wnd == null)
        //    {
        //        return;
        //    }
        //    wnd.Open_param = openParam;
        //    wnd.Close_param = closeParam;

        //    GameObject go = Instantiate(obj) as GameObject;
        //    wnd.OnLoad(go);
        //};

        //Create GameObj
        //默认 Window的 名字 和类的名字相同
        UnityEngine.Object obj = CreateUIGameObject(type.ToString(), wnd.wnd_load_handle);

        if (obj == null)
        {
            return null;
        }
        if (wnd == null)
        {
            return null;
        }
        wnd.Open_param = openParam;
        wnd.Close_param = closeParam;
        GameObject go = obj as GameObject;
        if (go != null)
        {
            wnd.OnLoad(go);
        }
        else
        {
            Debug.LogError("obj is null or empty!" + go.name);
            return null;
        }

        if (openHandle != null)
        {
            openHandle();
        }

        return wnd;
    }

    public static GameObject CreateUIGameObject(string name)
    {
        return null;
        //return ResourceManager.GetInstance().CreateUI(name) as GameObject;
    }
    public static UnityEngine.Object CreateUIObject(string nameFater, string nameSub)
    {
        return null;
        //return ResourceManager.GetInstance().CreateUI(nameFater, nameSub);
    }

    public static Sprite GetIcon(string assetName, string objName = null)
    {
        return null;
        //return ResourceManager.GetInstance().LoadIcon<Sprite>(assetName, objName);
    }

    private static UnityEngine.Object CreateUIGameObject(string name, UILoader.LoadHandle handle)
    {
        //return null;
        UILoader.instance.resourceHandle = handle;
        //return ResourceManager.GetInstance().CreateUI(name);

        return Instantiate(Resources.Load(name));
    }

    public static GameObject AddChildGameObject(GameObject parent, GameObject go, bool isInstance)
    {
        if (parent == null || go == null)
        {
            Debug.Log("go  or parent is null");
            return null;
        }
        Transform trans;
        if (isInstance)
        {
            trans = Instantiate(go).transform;
        }
        else
        {
            trans = go.transform;
        }

        trans.transform.SetParent(parent.transform, false);
        trans.gameObject.SetActive(true);

        //还原尺寸
        trans.localPosition = Vector3.zero;
        trans.localRotation = Quaternion.identity;
        trans.localScale = Vector3.one;

        Utility.SetLayer(trans.gameObject, parent.layer);

        return trans.gameObject;
    }

    public static GameObject Instantiate(GameObject src)
    {
        return Utility.Instantiate(src);
    }

    public static GameObject Instantiate(UnityEngine.Object src)
    {
        return Utility.Instantiate(src);
    }

    public static GameObject FindGameObject(GameObject go, string name)
    {
        return Utility.FindGameObject(go, name);
    }

    #region Register
    public static UIEventListener Register(GameObject go)
    {
        if (go == null)
            return null;

        return UIEventListener.Get(go);
    }
    #endregion

    #region SetIcon
    public static void SetIcon(RawImage img, string icon)
    {
        Utility.SetIcon(img, icon);
    }

    public static void SetImage(Image img, string atlasName, string name)
    {
        Utility.SetImage(img, atlasName, name);
    }
    #endregion
}
