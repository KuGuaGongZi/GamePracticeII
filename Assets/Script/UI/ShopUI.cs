using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShopUI : SingletonWindow<ShopUI>
{
    GameObject shopItemPre;
    GameObject content;
    //ShopData shopData;
    public override string windowName
    {
        get
        {
            return "ShopUI";
        }
    }
    public ShopUI()
    {
        m_sort_order = 3;
    }
    protected override void Initialize()
    {
        base.Initialize();
        
        //获取承载Item的父物体
        content = Find("Content");
        //获取模板item
        shopItemPre = Find("ShopItem");
        ShopData shopData = new ShopData();
        foreach (var item in shopData.ShopDic)
        {
            GameObject obj = GameObject.Instantiate(shopItemPre);
            obj.AddComponent<ShopItem>().SetCoinTexture(item.Value.shopTexture,item.Value.price);
            obj.transform.SetParent(content.transform);
            obj.transform.localScale = new Vector3(1,1,1);
            obj.SetActive(true);
           
        }
        Register(Find("closeButton")).onClick += OnCloseWindow;
    }
    public void OnCloseWindow(GameObject obj,PointerEventData data)
    {
        Hide();
    }

}
