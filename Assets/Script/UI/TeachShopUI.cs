using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class TeachShopUI : SingletonWindow<TeachShopUI>
{
    //ShopData shopData;
    public override string windowName
    {
        get
        {
            return "TeachShopUI";
        }
    }
    public TeachShopUI()
    {
        m_sort_order = 3;
    }
    public void OnItem(GameObject obj, PointerEventData data)
    {
        WindowFactory.instance.CreateWindow(WindowType.TeachItemUI);
    }
    protected override void Initialize()
    {
        base.Initialize();
        Register(Find("ShopItem3")).onClick += OnItem;
    }
    public void OnCloseWindow(GameObject obj, PointerEventData data)
    {
        Hide();
    }

}
