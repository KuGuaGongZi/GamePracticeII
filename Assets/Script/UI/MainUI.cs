using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using System;
using UnityEngine.SceneManagement;

public class MainUI : SingletonWindow<MainUI>
{
    public static Text scoreText;
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
    private Slider rewardSlider;

    private GameObject BgBox;
    private GameObject Bg;
    Transform rewardPlane;
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

    public void OnShopBtn(GameObject obj, PointerEventData data)
    {
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
        }
    }
    //设置交换盒子上的图片纹理
    public void SetChangeTexture()
    {
        if (GameDataManager.Instance.gameData.ChangeSprite ==null)
        {
            changeBox.transform.Find("changeImg").GetComponent<Image>().sprite = (Resources.Load("ShopTexture/10") as GameObject).GetComponent<SpriteRenderer>().sprite;
        }
        else
        {
            changeBox.transform.Find("changeImg").GetComponent<Image>().sprite = GameDataManager.Instance.gameData.ChangeSprite;
        }
        
    }
    //点击在交换盒子上
    public void OnChangeBox(GameObject obj,PointerEventData data)
    {
        if (AddCell.hasReadyCell)
        {
            Grid grid = Tool.Instance.GetReadyCell(Tool.readyCell);
            grid.Status = CellStatus.None;
            grid.Type = CellType.None;
            grid.HasObj = false;
            PoolManager.Instance.Delete(Tool.readyCell);
            Tool.readyCell = null;
            Tool.Instance.ClearAllArrwo();
        }
        int textureNum = (int)(GameDataManager.Instance.gameData.CoinType)+1;
        GameObject changeObj = Resources.Load("ShopTexture/" + textureNum) as GameObject;
        if (int.Parse(changeBox.transform.Find("changeImg").GetComponent<Image>().sprite.name) == 10)
        {
            changeBox.transform.Find("changeImg").GetComponent<Image>().sprite = changeObj.GetComponent<SpriteRenderer>().sprite;
            GameDataManager.Instance.gameData.CoinType = Tool.Instance.GetRandCoinType();
            Tool.Instance.SetCoinByType(GameDataManager.Instance.gameData.CoinType);
            GameDataManager.Instance.gameData.ChangeSprite = changeObj.GetComponent<SpriteRenderer>().sprite;
        }
        else
        {
            CoinType ctype= GameDataManager.Instance.gameData.CoinType;
            int spriteNum = int.Parse(changeBox.transform.Find("changeImg").GetComponent<Image>().sprite.name);
            changeBox.transform.Find("changeImg").GetComponent<Image>().sprite = (Resources.Load("ShopTexture/10") as GameObject).GetComponent<SpriteRenderer>().sprite;
            GameDataManager.Instance.gameData.CoinType = (CoinType)(spriteNum-1);
            Tool.Instance.SetCoinByType(GameDataManager.Instance.gameData.CoinType);
            Tool.changeCoin = ctype;
        }
    }
    //点击返回菜单按钮
    public void OnReturnBtn(GameObject obj,PointerEventData data)
    {
        GlobalData.nextScene = "Menu";
        SceneManager.LoadScene("Loading");
    }
    //设置分数
    public void SetScore()
    {
        scoreText.text = GameDataManager.Instance.gameData.Score;
    }
    //设置奖励条数值
    public void AddRewardSliderValue(float rewardValue)
    {
        rewardSlider.value += rewardValue;
        if (rewardSlider.value>=1)
        {
            MainUI.instance.Hide();
            Bg.SetActive(false);
            rewardPlane.gameObject.SetActive(true);
            rewardPlane.gameObject.AddComponent<RewardManager>();
        }
    }

    public void SetSliderZero()
    {
        rewardSlider.value = 0;
    }
    //设置金钱数
    public void SetMoney()
    {
        moneyText.text = GameDataManager.Instance.gameData.Money;
    }
    protected override void Initialize()
    {
        base.Initialize();
        BgBox = GameObject.Find("Bg");
        Bg = BgBox.transform.Find("BackGround").gameObject;
        rewardPlane = Camera.main.transform.Find("RewardPlane");
        rewardSlider = Find<Slider>("RewardSlider");
        Tool.Instance.SetCoinByType(GameDataManager.Instance.gameData.CoinType);
        moneyText = Find<Text>("moneyText");
        moneyText.text = GameDataManager.Instance.gameData.Money;
        scoreText = Find<Text>("scoreText");
        scoreText.text = GameDataManager.Instance.gameData.Score;
        //注册商城按钮的点击事件
        Register(Find("shopButton")).onClick += OnShopBtn;
        Register(Find("TipUI")).onClick += ShowTipPlane;
        changeBox = Find("ChangeBox");
        Register(changeBox).onClick += OnChangeBox;
        Register(Find("RetunBtn")).onClick += OnReturnBtn;
        SetMoney();
        SetScore();
        SetChangeTexture();
        tipPlane = Find("TipPlane");
        tipUI = Find("TipUI");
        tipX = tipUI.GetComponent<RectTransform>().anchoredPosition.x;
        tipY = tipUI.GetComponent<RectTransform>().anchoredPosition.y;
        tween = tipUI.GetComponent<RectTransform>().DOAnchorPos(new Vector2(tipX, tipY - distance), time);
        tween.Pause();
        tween.SetAutoKill(false);
    }
}
