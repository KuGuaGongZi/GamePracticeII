using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemMessage : SingletonWindow<ItemMessage>
{
    private Sprite mSprite;
    public override string windowName
    {
        get
        {
            return "ItemMessage";
        }
    }
    public ItemMessage()
    {
        m_sort_order = 2;
    }
    protected override void Initialize()
    {
        base.Initialize();
        Register(Find("BuyBtn")).onClick += OnBuyBtn;
        Register(Find("CancelBtn")).onClick += OnCancelBtn;
    }
    //设置物品信息图示
    public void SetImg(Sprite sp)
    {
        Find("Img").transform.GetComponent<Image>().sprite = sp;
        mSprite = sp;
    }
    //点击购买按钮
    public void OnBuyBtn(GameObject obj, PointerEventData data)
    {
        GameDataManager.Instance.gameData.CoinType = (CoinType)(int.Parse(mSprite.name) - 1);
        Tool.Instance.SetCoinByType((CoinType)(int.Parse(mSprite.name) - 1));
        Hide();
        ShopUI.instance.Hide();
    }
    //点击取消购买按钮
    public void OnCancelBtn(GameObject obj, PointerEventData data)
    {
        Hide();
    }
}
