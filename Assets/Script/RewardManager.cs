using UnityEngine;
using System.Collections;

public class RewardManager : MonoBehaviour
{
    private float force;
    private LineRenderer m_knife;
    private int count=0;
    private bool isCut;
    private RewardType rewardType;
    private GameObject knife;
    private Vector3 startPos;
    private float rewardTime;
    private bool isEndTime;
    private GameObject BgBox;
    private GameObject Bg;
    Transform rewardPlane;
    private bool isEnd;
    public static int hitcount;
    void Start()
    {
        hitcount = 0;
        isEnd = false;
        BgBox = GameObject.Find("Bg");
        Bg = BgBox.transform.Find("BackGround").gameObject;
        rewardPlane = Camera.main.transform.Find("RewardPlane");
        isEndTime = false;
        isCut = false;
        force = 450;
        rewardTime = 15;
        knife = GameObject.Find("Knife");
        startPos = new Vector3(-2000,-2000,0);
        knife.transform.position = startPos;
        //添加线条组件
        m_knife = gameObject.AddComponent<LineRenderer>();
        //设置线条的颜色为白色
        m_knife.material = new Material(Shader.Find("GUI/Text Shader"));
        //m_knife.material.color = Color.white;
        m_knife.startColor = Color.white;
        m_knife.endColor = Color.grey;
        m_knife.startWidth = 0.1f;
        m_knife.endWidth = 0.1f;
        Invoke("StartCreateReward", 2);
    }
    void StartCreateReward()
    {
        StartCoroutine(CreateReward());
    }
    void CreateRandReward()
    {
        GameObject obj=null;
        rewardType = (RewardType)(Random.Range(0,9));
        switch (rewardType)
        {
            case RewardType.RewardFood1:
                obj=PoolManager.Instance.Get("RewardFood1") as GameObject;
                break;
            case RewardType.RewardFood2:
                obj = PoolManager.Instance.Get("RewardFood2") as GameObject;
                break;
            case RewardType.RewardFood3:
                obj = PoolManager.Instance.Get("RewardFood3") as GameObject;
                break;
            case RewardType.RewardFood4:
                obj = PoolManager.Instance.Get("RewardFood4") as GameObject;
                break;
            case RewardType.RewardFood5:
                obj = PoolManager.Instance.Get("RewardFood5") as GameObject;
                break;
            case RewardType.RewardFood6:
                obj = PoolManager.Instance.Get("RewardFood6") as GameObject;
                break;
            case RewardType.RewardCleaner1:
                obj = PoolManager.Instance.Get("RewardCleaner1") as GameObject;
                break;
            case RewardType.RewardCleaner2:
                obj = PoolManager.Instance.Get("RewardCleaner2") as GameObject;
                break;
            case RewardType.RewardBomb:
                obj = PoolManager.Instance.Get("RewardBomb") as GameObject;
                break;
            case RewardType.None:
                break;
            default:
                break;
        }
        float randx = Random.Range(-10.0f,10.0f);
        float y = -Screen.height / 200;
        if (obj != null)
        {
            obj.GetIsNullComponent<RewardMove>();
            obj.transform.position = new Vector3(randx,y,0);
            obj.GetComponent<Rigidbody2D>().AddForce(new Vector2(0,force));
        }
    }
    void Update()
    {
        if (!isEnd)
        {
            rewardTime -= Time.deltaTime;
            if (Input.GetMouseButton(0))
            {
                knife.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1));
                m_knife.positionCount = ++count;
                m_knife.SetPosition(count - 1, pos);
            }
            if (Input.GetMouseButtonUp(0))
            {
                knife.transform.position = startPos;
                m_knife.positionCount = 0;
                count = 0;
            }
            if (rewardTime <= 5)
            {
                if (!isEndTime)
                {
                    isEndTime = true;
                    WindowFactory.instance.CreateWindow(WindowType.RewardUI);
                }
                else
                {
                    RewardUI.instance.SetTime((int)rewardTime + 1);
                    if ((int)rewardTime + 1 <= 0)
                    {
                        m_knife.positionCount = 0;
                        count = 0;
                        RewardUI.instance.Hide();
                        StopAllCoroutines();
                        rewardTime = 10;
                        isEnd = true;
                        Invoke("WaitToEnd",3);
                    }
                }
            }
        }
        
    }
    void WaitToEnd()
    {
        WindowFactory.instance.CreateWindow(WindowType.MainUI);
        float scoreText = float.Parse(GameDataManager.Instance.gameData.Score);
        scoreText += hitcount;
        GameDataManager.Instance.gameData.Score = scoreText.ToString();
        MainUI.instance.SetScore();
        MainUI.instance.SetSliderZero();
        Bg.SetActive(true);
        rewardPlane.gameObject.SetActive(false);
        Destroy(transform.GetComponent("RewardManager"));
    }
    IEnumerator CreateReward()
    {
        CreateRandReward();
        yield return new WaitForSeconds(1);
        StartCoroutine(CreateReward());
    }
}
