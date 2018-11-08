using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using DG.Tweening;

public class TeachItemUI : SingletonWindow<TeachItemUI>
{
    public override string windowName
    {
        get
        {
            return "TeachItemUI";
        }
    }
    public TeachItemUI()
    {
        m_sort_order = 4;
    }
    //点击购买按钮
    public void OnBuyBtn(GameObject obj, PointerEventData data)
    {
        Hide();
        TeachMainUI.moneyText.text = "0";
        GameObject coinBox= GameObject.Find("CoinBox");
        coinBox.transform.Find("CFood1").gameObject.SetActive(false);
        coinBox.transform.Find("CFood3").gameObject.SetActive(true);
        TeachShopUI.instance.Hide();
        WindowFactory.instance.CreateWindow(WindowType.TalkUI);
        TalkUI.talkNum++;
        TalkUI.talkText.text = "";
        TalkUI.textTween = TalkUI.talkText.DOText(TalkUI.talk[TalkUI.talkNum], 2);
    }
    //点击取消购买按钮
    public void OnCancelBtn(GameObject obj, PointerEventData data)
    {
        Hide();
    }
    protected override void Initialize()
    {
        base.Initialize();
        Register(Find("BuyBtn")).onClick += OnBuyBtn;
        Register(Find("CancelBtn")).onClick += OnCancelBtn;
    }
}
