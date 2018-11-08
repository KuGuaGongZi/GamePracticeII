using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class EndTeachUI : SingletonWindow<EndTeachUI>
{
    public EndTeachUI()
    {
        m_sort_order = 5;
    }
    public override string windowName
    {
        get
        {
            return "EndTeachUI";
        }
    }
    //点击开始新游戏按钮
    public void OnStartGame(GameObject obj,PointerEventData data)
    {
        GlobalData.nextScene = "Game";
        SceneManager.LoadScene("Loading");
    }
    //点击重来一遍教程按钮
    public void OnRestartGame(GameObject obj, PointerEventData data)
    {
        GlobalData.nextScene = "Teach";
        SceneManager.LoadScene("Loading");
    }
    protected override void Initialize()
    {
        base.Initialize();
        Register(Find("RestartBtn")).onClick += OnRestartGame;
        Register(Find("StartBtn")).onClick += OnStartGame;
    }
}
