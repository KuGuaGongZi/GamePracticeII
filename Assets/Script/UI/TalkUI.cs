using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.UI;

public class TalkUI : SingletonWindow<TalkUI>
{
    public static string[] talk = { "屏幕左边的锅上放着当前出现的元素，玩家单击场地选择放置位置",
        "通过点击放置使得3个或更多相同元素在一起合成更高级元素",
        "现在，我们按指示来放置元素吧",
        "如果放置在两个相同元素旁边就会有箭头提示哦！",
        "现在，我们再次点击刚刚放置的元素看看吧",
        "可以看到，双击同个位置确定放置,三个相同的小麦就合成了一个大麦",
        "那么，如果左边刚好没出现我们想要的元素怎么办呢？",
        "像现在场中有两个面粉元素，我们只差一个面粉元素就能凑齐3个了",
        "没有钱解决不了的东西，我们点击左上角的灯笼来打开商店购买一袋面粉吧",
        "可以看到，左侧锅上的元素现在变成了我们购买的元素了",
        "现在我们如果想暂时不用这个元素，可以先把他放到左下角的篮子里哦",
        "这样，锅里的元素就暂时存储到盒子里了，再次点击盒子上的元素就能再次把他放回去哦",
        "随着元素的增多，如果忘记了哪些元素可以合成哪些元素，可以点击屏幕上方的菜单来查看哦！",
        "那么，我们现在来开始游戏吧！" };
    public static Tween textTween;
    public static Text talkText;
    public static int talkNum=0;
    public TalkUI()
    {
        m_sort_order = 10;
    }
    public override string windowName
    {
        get
        {
            return "TalkUI";
        }
    }
    public void OnCheck1(GameObject obj,PointerEventData data)
    {
        TeachMainUI.instance.Step1_2();
        Find("TalkPlane").gameObject.SetActive(true);
        talkNum ++;
        talkText.text = "";
        textTween = talkText.DOText(talk[talkNum], 2);
        Find("Check1").gameObject.SetActive(false);
    }
    public void Talk(GameObject obj,PointerEventData data)
    {
        if (talkNum<talk.Length)
        {
            if (talkNum==2)
            {
                TeachMainUI.instance.Step1();
                Hide();
                return;
            }
            if (talkNum == 4)
            {
                Register(Find("Check1")).onClick += OnCheck1;
                Find("TalkPlane").gameObject.SetActive(false);
                return;
            }
            if (talkNum == 5)
            {
                TeachMainUI.instance.Step1_3();
            }
            if (talkNum == 8)
            {
                Hide();
                TeachMainUI.instance.Step2();
                return;
            }
            if (talkNum == 10)
            {
                Hide();
                TeachMainUI.instance.Step3();
                return;
            }
            if (talkNum == 12)
            {
                Hide();
                TeachMainUI.instance.Step4();
                return;
            }
            if (talkNum == talk.Length-1)
            {
                Hide();
                WindowFactory.instance.CreateWindow(WindowType.EndTeachUI);
                return;
            }
            talkNum++;
            talkText.text = "";
            textTween.Pause();
            textTween = talkText.DOText(talk[talkNum], 2);
            textTween.PlayForward();
            textTween.SetAutoKill(false); 
        }
    }
    protected override void Initialize()
    {
        base.Initialize();
        Register(Find("TalkPlane")).onClick += Talk;
        talkText = Find<Text>("TalkText");
        talkNum = 0;
        textTween = talkText.DOText(talk[talkNum],2);
        textTween.SetAutoKill(false);
    }
}
