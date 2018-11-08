using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PoolManager : MonoBehaviour
{
    private static PoolManager instance;
    private static Dictionary<string, ArrayList> poolDic;
    public static PoolManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject obj = new GameObject();
                instance = obj.AddComponent<PoolManager>();
                poolDic = new Dictionary<string, ArrayList>();
            }
            return instance;
        }
    }

    void Awake()
    {
        instance = this;
        poolDic = new Dictionary<string, ArrayList>();
    }
    public object Get(string objName)
    {
        string folder = "";
        if (objName.Substring(0, 5) == "Ready")
        {
            folder = "ReadyCell/";
        }
        if (objName.Substring(0, 5) == "Arrow")
        {
            folder = "Arrow/";
        }
        if (objName.Substring(0, 6) == "Reward")
        {
            folder = "RewardCell/";
        }
        if (objName.Substring(0, 4) == "Full")
        {
            folder = "FullCell/";
        }
        string name = objName + "(Clone)";
        object obj = null;
        if (poolDic.ContainsKey(name) && poolDic[name].Count > 0)
        {
            ArrayList list = poolDic[name];
            obj = list[0];
            (obj as GameObject).SetActive(true);
            list.RemoveAt(0);
        }
        else
        {
            obj = Instantiate(Resources.Load(folder + objName));
        }
        return obj;
    }
    public object Delete(GameObject obj)
    {
        string name = obj.name;
        if (poolDic.ContainsKey(name))
        {
            poolDic[name].Add(obj);
        }
        else
        {
            ArrayList arr = new ArrayList();
            arr.Add(obj);
            poolDic[name] = arr;
        }
        obj.SetActive(false);
        return obj;
    }

}
