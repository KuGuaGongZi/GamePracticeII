using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class AskWashUI : SingletonWindow<AskWashUI>
{
    public AskWashUI()
    {
        m_sort_order = 3;
    }
    public override string windowName
    {
        get
        {
            return "AskWashUI";
        }
    }
    //点击清洗按钮
    public void OnWashBtn(GameObject obj, PointerEventData data)
    {
        int x = (int)AddCell.currentTouchCoord.x;
        int y = (int)AddCell.currentTouchCoord.y;
        GameDataManager.Instance.gridArr[x, y].Type = CellType.None;
        GameDataManager.Instance.gridArr[x, y].Status = CellStatus.None;
        GameDataManager.Instance.gridArr[x, y].HasObj = false;
        PoolManager.Instance.Delete(GameDataManager.Instance.gridArr[x, y].Entity);
        GameDataManager.Instance.gridArr[x, y].Entity = null;
        Tool.Instance.ClearRandOil();
        GameDataManager.Instance.gameData.CoinType = Tool.Instance.GetRandCoinType();
        Tool.Instance.SetCoinByType(GameDataManager.Instance.gameData.CoinType);
        Hide();
    }
    //点击取消清洗按钮
    public void OnCancelWashBtn(GameObject obj, PointerEventData data)
    {
        Hide();
    }
    protected override void Initialize()
    {
        base.Initialize();
        Register(Find("UseBtn")).onClick += OnWashBtn;
        Register(Find("CancelUseBtn")).onClick += OnCancelWashBtn;
    }
}
