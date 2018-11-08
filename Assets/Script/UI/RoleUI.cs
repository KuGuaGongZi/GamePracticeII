using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class RoleUI : SingletonWindow<RoleUI>
{
    public RoleUI()
    {
        m_sort_order = 1;
    }
    public override string windowName
    {
        get
        {
            return "RoleUI";
        }
    }
    public void OnCloseBtn(GameObject obj,PointerEventData data)
    {
        Hide();
    }
    protected override void Initialize()
    {
        base.Initialize();
        Register(Find("CloseBtn")).onClick += OnCloseBtn;
    }
}
