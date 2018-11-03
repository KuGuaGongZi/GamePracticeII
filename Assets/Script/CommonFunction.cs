using UnityEngine;
using System.Collections;

public static class CommonFunction
{
    /// <summary>
    /// 扩展方法
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static T GetIsNullComponent<T>(this GameObject obj)where T :Component
    {
        T t = obj.GetComponent<T>();
        if (t == null)
        {
            t = obj.AddComponent<T>();
        }
        return t;
    }
}
