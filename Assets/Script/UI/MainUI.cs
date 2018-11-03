using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainUI : SingletonWindow<MainUI> {
    public MainUI()
    {
        m_sort_order = 0;
        
    }
    public override string windowName
    {
        get
        {
            return "MainUI";
        }
    }
    public void OnShopBtn(GameObject obj,PointerEventData data)
    {
        Debug.Log("打开商城");
        if (AddCell.hasReadyCell)
        {
            Grid grid = Tool.Instance.GetReadyCell(Tool.readyCell);
            grid.Status = CellStatus.None;
            grid.Type = CellType.None;
            grid.HasObj = false;
            PoolManager.Instance.Delete(Tool.readyCell);
            Tool.readyCell = null;
            Tool.Instance.SetCoinByType(GameDataManager.Instance.gameData.CoinType);
            Tool.Instance.ClearAllArrwo();
        }
        //显示商城UI
        WindowFactory.instance.CreateWindow(WindowType.ShopUI);
    }
    protected override void Initialize()
    {
        base.Initialize();
        Debug.Log("MianUI init...");
        Find<Text>("moneyText").text = GameDataManager.Instance.gameData.Money;
        Find<Text>("scoreText").text = GameDataManager.Instance.gameData.Score;
        //注册商城按钮的点击事件
        Register(Find("shopButton")).onClick += OnShopBtn;
    }
}
