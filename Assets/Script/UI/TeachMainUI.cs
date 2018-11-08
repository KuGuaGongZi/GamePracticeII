using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class TeachMainUI : SingletonWindow<TeachMainUI>
{
    public static Text moneyText;

    private GameObject tipPlane;
    private GameObject tipUI;
    private GameObject changeBox;

    private float tipX;
    private float tipY;
    private float distance = 415;//面板移动距离
    private float time = 1;//面板移动时间
    private Tween tween;
    private int clickCount = 0;
    public TeachMainUI()
    {
        m_sort_order = 0;

    }
    public override string windowName
    {
        get
        {
            return "TeachMainUI";
        }
    }
    public void OnShopBtn(GameObject obj, PointerEventData data)
    {
        //显示商城UI
        WindowFactory.instance.CreateWindow(WindowType.TeachShopUI);
    }
    //弹出提示面板
    public void ShowTipPlane(GameObject obj, PointerEventData data)
    {
        clickCount++;
        if (clickCount % 2 == 1)
        {
            tipPlane.SetActive(true);
            tween.PlayForward();
        }
        else
        {
            tipPlane.SetActive(false);
            tween.PlayBackwards();
            WindowFactory.instance.CreateWindow(WindowType.TalkUI);
            TalkUI.talkNum++;
            TalkUI.talkText.text = "";
            TalkUI.textTween = TalkUI.talkText.DOText(TalkUI.talk[TalkUI.talkNum], 2);
        }
    }
    public void Step1_1(GameObject obj, PointerEventData data)
    {
        Find("Hand1").gameObject.SetActive(false);
        Find("ReadyFood1").gameObject.SetActive(true);
        WindowFactory.instance.CreateWindow(WindowType.TalkUI);
        TalkUI.talkNum++;
        TalkUI.talkText.text = "";
        TalkUI.textTween=TalkUI.talkText.DOText(TalkUI.talk[TalkUI.talkNum], 2);
    }
    public void Step1_2()
    {
        Find("ReadyFood1").gameObject.SetActive(false);
        Find("FullFood2").gameObject.SetActive(true);
        Find("TeachCell1").gameObject.SetActive(false);
        Find("TeachCell2").gameObject.SetActive(false);
    }
    public void Step1_3()
    {
        Find("Step1").gameObject.SetActive(false);
        Find("Step2").gameObject.SetActive(true);
    }
    public void Step3_1(GameObject obj,PointerEventData data)
    {
        Find("Hand2").gameObject.SetActive(false);
        Find("changeImg").gameObject.SetActive(true);
        GameObject coinBox = GameObject.Find("CoinBox");
        coinBox.transform.Find("CFood1").gameObject.SetActive(true);
        coinBox.transform.Find("CFood3").gameObject.SetActive(false);
        WindowFactory.instance.CreateWindow(WindowType.TalkUI);
        TalkUI.talkNum++;
        TalkUI.talkText.text = "";
        TalkUI.textTween = TalkUI.talkText.DOText(TalkUI.talk[TalkUI.talkNum], 2);
    }
    public void Step1()
    {
        Find("Step1").gameObject.SetActive(true);
        Register(Find("Hand1")).onClick += Step1_1;
    }
    public void Step2()
    {
        Register(Find("shopButton")).onClick += OnShopBtn;
    }
    public void Step3()
    {
        Find("Hand2").gameObject.SetActive(true);
        Register(Find("Hand2")).onClick += Step3_1;
    }
    public void Step4()
    {
        Register(Find("TipUI")).onClick += ShowTipPlane;
    }
    protected override void Initialize()
    {
        base.Initialize();
        moneyText = Find<Text>("moneyText");
        //注册商城按钮的点击事件
        tipPlane = Find("TipPlane");
        tipUI = Find("TipUI");
        tipX = tipUI.GetComponent<RectTransform>().anchoredPosition.x;
        tipY = tipUI.GetComponent<RectTransform>().anchoredPosition.y;
        tween = tipUI.GetComponent<RectTransform>().DOAnchorPos(new Vector2(tipX, tipY - distance), time);
        tween.Pause();
        tween.SetAutoKill(false);
    }
}
