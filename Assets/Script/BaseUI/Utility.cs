using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Utility
{
    #region normal help
    public static string GetColorString(string col, string text)
    {
        return "<color=#" + col + ">" + text + "</color>";
    }
    #endregion

    #region Line help

    #endregion

    public static void SetLayer(GameObject go, int layer)
    {
        if (go == null)
            return;

        Transform[] trans = go.GetComponentsInChildren<Transform>(true);
        for (int i = 0; i < trans.Length; i++)
        {
            trans[i].gameObject.layer = layer;
        }
    }

    public static GameObject FindGameObject(GameObject go, string name)
    {
        if (go == null)
            return null;

        if (go.name == name)
            return go;

        return FindGameObjectInChild(go.transform, name);
    }

    private static GameObject FindGameObjectInChild(Transform trans, string name)
    {
        if (trans == null)
            return null;

        Transform tmpTrans = trans.Find(name);
        if (tmpTrans != null)
            return tmpTrans.gameObject;

        for (int i = 0; i < trans.childCount; i++)
        {
            Transform child = trans.GetChild(i);
            GameObject tmp = FindGameObjectInChild(child, name);
            if (tmp != null)
                return tmp;
        }

        return null;
    }

    public static GameObject Instantiate(GameObject src)
    {
        return CodeInstantiate(src);
    }

    public static GameObject Instantiate(UnityEngine.Object src)
    {
        return CodeInstantiate(src as GameObject);
    }

    private static GameObject CodeInstantiate(GameObject src, string name = "")
    {
        if (src == null)
            return null;

        GameObject clone = GameObject.Instantiate(src);
        if (!string.IsNullOrEmpty(name))
            clone.name = name;

//#if !UNITY_STANDALONE
//        if (Application.platform != RuntimePlatform.Android && Application.platform != RuntimePlatform.IPhonePlayer)
//            ReBindShader(clone);
//#endif

        return clone;
    }

    #region Set Icon
    public static void SetIcon(RawImage img, string icon)
    {
        if (string.IsNullOrEmpty(icon) || img == null)
            return;

        //bool enable = img.enabled;
        Texture texImg = GetIconObj(icon) as Texture;
        //if (img.texture == null)
        //    Debug.LogError("you should set a default texture for this Texture, -- yd");
        if (texImg != null && img != null)
        {
            img.gameObject.SetActive(true);
            img.texture = texImg;
        }
        else
        {
            img.gameObject.SetActive(false);
        }

    }

    private static UnityEngine.Object GetIconObj(string bundleName)
    {
        if (string.IsNullOrEmpty(bundleName))
            return null;

        return null;
        //return ResourceManager.GetInstance().LoadIcon<Object>(bundleName);
    }



    public static void SetImage(Image img, string atlasName, string name)
    {
        //if (img == null || string.IsNullOrEmpty(atlasName) || string.IsNullOrEmpty(name))
        //    return;

        //Color preColor = img.color;
        //if (img.sprite == null && img.enabled)
        //{
        //    img.color = new Color(0, 0, 0, 0);
        //    //img.enabled = false;
        //}
    }
    #endregion
}
