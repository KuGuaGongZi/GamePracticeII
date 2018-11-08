using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class AskSellUI : SingletonWindow<AskSellUI>
{
    public AskSellUI()
    {
        m_sort_order = 3;
    }
    public override string windowName
    {
        get
        {
            return "AskSellUI";
        }
    }
    //点击出售按钮
    public void OnSellBtn(GameObject obj,PointerEventData data)
    {
        float scoreMoney = float.Parse(GameDataManager.Instance.gameData.Money);
        int x = (int)AddCell.currentTouchCoord.x;
        int y= (int)AddCell.currentTouchCoord.y;
        scoreMoney += 100;
        GameDataManager.Instance.gameData.Money = scoreMoney.ToString();
        MainUI.instance.SetMoney();
        GameDataManager.Instance.gridArr[x, y].Type =CellType.None;
        GameDataManager.Instance.gridArr[x, y].Status = CellStatus.None;
        GameDataManager.Instance.gridArr[x, y].HasObj = false;
        PoolManager.Instance.Delete(GameDataManager.Instance.gridArr[x, y].Entity);
        GameDataManager.Instance.gridArr[x, y].Entity = null;
        Hide();
        float currentMoney = float.Parse(GameDataManager.Instance.gameData.Money) +100;
        GameDataManager.Instance.gameData.Money = currentMoney.ToString();
        MainUI.instance.SetMoney();
    }
    //点击取消出售按钮
    public void OnCancelSellBtn(GameObject obj, PointerEventData data)
    {
        Hide();
    }
    protected override void Initialize()
    {
        base.Initialize();
        Register(Find("SellBtn")).onClick+=OnSellBtn;
        Register(Find("CancelSellBtn")).onClick += OnCancelSellBtn;
    }
}
