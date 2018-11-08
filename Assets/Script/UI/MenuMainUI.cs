using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuMainUI :SingletonWindow<MenuMainUI>
{
    public MenuMainUI()
    {
        m_sort_order = 0;
    }
    public override string windowName
    {
        get
        {
            return "MemuMainUI";
        }
    }
    //开始按钮
    public void OnStartBtn(GameObject obj,PointerEventData data)
    {
        GlobalData.nextScene = "Game";
        SceneManager.LoadScene("Loading");
        GlobalData.isNewGame = true;
    }
    //继续游戏按钮
    public void OnContinueBtn(GameObject obj, PointerEventData data)
    {
        GlobalData.nextScene = "Game";
        SceneManager.LoadScene("Loading");
        GlobalData.isNewGame = false;
    }
    //教学按钮
    public void OnTeachBtn(GameObject obj, PointerEventData data)
    {
        GlobalData.isNewGame = true;
        GlobalData.nextScene = "Teach";
        SceneManager.LoadScene("Loading");
    }
    //规则按钮
    public void OnRoleBtn(GameObject obj, PointerEventData data)
    {
        WindowFactory.instance.CreateWindow(WindowType.RoleUI);
    }
    protected override void Initialize()
    {
        base.Initialize();
        Register(Find("StartBtn")).onClick+=OnStartBtn;
        Register(Find("ContinueBtn")).onClick += OnContinueBtn;
        Register(Find("TeachBtn")).onClick += OnTeachBtn;
        Register(Find("RoleBtn")).onClick += OnRoleBtn;
    }
}
