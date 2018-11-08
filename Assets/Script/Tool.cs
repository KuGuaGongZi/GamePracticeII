using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using DG.Tweening;

public class Tool : MonoBehaviour
{
    private static Tool _instance;
    private GameObject CellBox;
    private GameObject CoinBox;
    public static GameObject readyCell;//存储准备状态的单元格
    public static CoinType changeCoin=CoinType.None;
    public static Tool Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = Camera.main.transform.gameObject.AddComponent<Tool>();
            }
            return _instance;
        }
    }
    GameObject toolObj;
    void Awake()
    {
        _instance = this;
        CellBox = GameObject.Find("CellBox");
        CoinBox = GameObject.Find("CoinBox");
    }
    //真实坐标转数组下标
    public Vector2 PositionToIndex(Vector2 position)
    {
        if (position.x < 0 || position.x > Defines.CellSize.x * Defines.ColCount || position.y > 0 || position.y < -Defines.CellSize.y * Defines.RowCount)
        {
            return new Vector2(-1, -1);
        }

        return new Vector2((int)(Mathf.Abs(position.y) / Defines.CellSize.y), (int)(position.x / Defines.CellSize.x));
    }
    //数组下标转真实坐标 (这里的真实坐标不是指世界坐标，而是指相对于承载着格子的物体的相对坐标)
    public Vector2 IndexToPosition(Vector2 arrayIndex)
    {
        return new Vector2(arrayIndex.y * Defines.CellSize.x, -arrayIndex.x * Defines.CellSize.y);
    }
    //获得点击位置真实坐标（指的是相对于承载格子的物体的相对坐标）
    public Vector2 GetPositionByPoint(Vector3 mousePosion)
    {
        return (Vector2)(Camera.main.ScreenToWorldPoint(Input.mousePosition) - CellBox.transform.position);
    }
    //获得随机的元素
    public CoinType GetRandCoinType()
    {
        //CoinType CType = CoinType.Bomb;
        int randType = UnityEngine.Random.Range(0, 101);
        print(randType);
        CoinType CType = CoinType.CFood1;
        if (randType <= 40)
        {
            CType = CoinType.CFood1;
        }
        if (randType <= 60 && randType > 40)
        {
            CType = CoinType.CFood2;
        }
        if (randType <= 75 && randType > 60)
        {
            CType = CoinType.CFood3;
        }
        if (randType <= 85 && randType > 75)
        {
            CType = CoinType.CFood4;
        }
        if (randType <= 95 && randType > 85)
        {
            CType = CoinType.CCleaner1;
        }
        if (randType > 95)
        {
            CType = CoinType.Bomb;
        }
        return CType;
    }
    //获得地图中的准备状态单元格
    public Grid GetReadyCell(GameObject readyObj)
    {
        for (int rowIndex = 0; rowIndex < Defines.RowCount; rowIndex++)
        {
            for (int colIndex = 0; colIndex < Defines.ColCount; colIndex++)
            {
                if (readyObj.transform.localPosition == GameDataManager.Instance.gridArr[rowIndex, colIndex].Position)
                {
                    return GameDataManager.Instance.gridArr[rowIndex, colIndex];
                }
            }
        }
        return null;
    }
    //根据地图数据实例化地图
    public void UpdateByMap()
    {
        if (!AddCell.hasReadyCell && readyCell != null)
        {
            readyCell = null;
        }
        for (int rowIndex = 0; rowIndex < Defines.RowCount; rowIndex++)
        {
            for (int colIndex = 0; colIndex < Defines.ColCount; colIndex++)
            {
                //处理状态不为空且没有携带实体的单元格
                if (GameDataManager.Instance.gridArr[rowIndex, colIndex].Status != CellStatus.None && !GameDataManager.Instance.gridArr[rowIndex, colIndex].HasObj)
                {
                    GameDataManager.Instance.gridArr[rowIndex, colIndex].HasObj = true;
                    GameObject obj = null;
                    //处理覆盖状态的单元格
                    if (GameDataManager.Instance.gridArr[rowIndex, colIndex].Status == CellStatus.Full || readyCell == null)
                    {
                        obj = PoolManager.Instance.Get(GameDataManager.Instance.gridArr[rowIndex, colIndex].Status.ToString() + GameDataManager.Instance.gridArr[rowIndex, colIndex].Type.ToString()) as GameObject;
                    }
                    //处理准备状态的单元格
                    if (readyCell != null && GameDataManager.Instance.gridArr[rowIndex, colIndex].Status == CellStatus.Ready)
                    {
                        obj = readyCell;
                    }

                    obj.transform.SetParent(CellBox.transform);
                    obj.transform.localPosition = GameDataManager.Instance.gridArr[rowIndex, colIndex].Position;

                    if (GameDataManager.Instance.gridArr[rowIndex, colIndex].Status == CellStatus.Ready && readyCell == null)
                    {
                        readyCell = obj;
                    }
                    GameDataManager.Instance.gridArr[rowIndex, colIndex].Entity = obj;
                }
            }
        }
    }
    //放置特殊道具到场中
    public void SetToolPos(Vector3 pos)
    {
        //GameDataManager.Instance.gridArr[rowIndex, colIndex].Status.ToString() + GameDataManager.Instance.gridArr[rowIndex, colIndex].Type.ToString()
        CellType ct = (CellType)GameDataManager.Instance.gameData.CoinType;
        if (!AddCell.hasTool)
        {
            toolObj = PoolManager.Instance.Get("Full" + ct.ToString()) as GameObject;
        }
        toolObj.transform.SetParent(CellBox.transform);
        toolObj.transform.localPosition = pos;
        toolObj.GetIsNullComponent<BoxCollider2D>();
        toolObj.GetIsNullComponent<Bomb>();
    }
    //获得所有相同元素，超过2个返回一个元素坐标列表，没有返回null
    public List<Vector2> GetLinkCell(Grid gird)
    {
        List<Vector2> linkList = new List<Vector2>();
        //逻辑坐标的上下左右
        int LX = (int)gird.Coord.x - 1;
        int RX = (int)gird.Coord.x + 1;
        int UY = (int)gird.Coord.y - 1;
        int DY = (int)gird.Coord.y + 1;
        CellType type = gird.Type;
        if (LX >= 0)
        {
            if (GameDataManager.Instance.gridArr[LX, (int)gird.Coord.y].Type == type)
            {
                linkList.Add(GameDataManager.Instance.gridArr[LX, (int)gird.Coord.y].Coord);
                if (GetSecondLinkCell(GameDataManager.Instance.gridArr[LX, (int)gird.Coord.y], gird).Count > 0)
                {
                    linkList.Add(GetSecondLinkCell(GameDataManager.Instance.gridArr[LX, (int)gird.Coord.y], gird)[0]);
                }
            }
        }
        if (RX < Defines.RowCount)
        {
            if (GameDataManager.Instance.gridArr[RX, (int)gird.Coord.y].Type == type)
            {
                linkList.Add(GameDataManager.Instance.gridArr[RX, (int)gird.Coord.y].Coord);
                if (GetSecondLinkCell(GameDataManager.Instance.gridArr[RX, (int)gird.Coord.y], gird).Count > 0)
                {
                    linkList.Add(GetSecondLinkCell(GameDataManager.Instance.gridArr[RX, (int)gird.Coord.y], gird)[0]);
                }
            }
        }
        if (UY >= 0)
        {
            if (GameDataManager.Instance.gridArr[(int)gird.Coord.x, UY].Type == type)
            {
                linkList.Add(GameDataManager.Instance.gridArr[(int)gird.Coord.x, UY].Coord);
                if (GetSecondLinkCell(GameDataManager.Instance.gridArr[(int)gird.Coord.x, UY], gird).Count > 0)
                {
                    linkList.Add(GetSecondLinkCell(GameDataManager.Instance.gridArr[(int)gird.Coord.x, UY], gird)[0]);
                }
            }
        }
        if (DY < Defines.ColCount)
        {
            if (GameDataManager.Instance.gridArr[(int)gird.Coord.x, DY].Type == type)
            {
                linkList.Add(GameDataManager.Instance.gridArr[(int)gird.Coord.x, DY].Coord);
                if (GetSecondLinkCell(GameDataManager.Instance.gridArr[(int)gird.Coord.x, DY], gird).Count > 0)
                {
                    linkList.Add(GetSecondLinkCell(GameDataManager.Instance.gridArr[(int)gird.Coord.x, DY], gird)[0]);
                }
            }
        }
        return linkList;
    }
    //获得第二级的相同元素列表
    public List<Vector2> GetSecondLinkCell(Grid gird, Grid parent)
    {
        List<Vector2> linkList = new List<Vector2>();
        //逻辑坐标的上下左右
        int LX = (int)gird.Coord.x - 1;
        int RX = (int)gird.Coord.x + 1;
        int UY = (int)gird.Coord.y - 1;
        int DY = (int)gird.Coord.y + 1;
        CellType type = gird.Type;
        if (LX >= 0)
        {
            if (GameDataManager.Instance.gridArr[LX, (int)gird.Coord.y].Type == type && GameDataManager.Instance.gridArr[LX, (int)gird.Coord.y].Coord != parent.Coord)
            {
                linkList.Add(GameDataManager.Instance.gridArr[LX, (int)gird.Coord.y].Coord);

            }
        }
        if (RX < Defines.RowCount)
        {
            if (GameDataManager.Instance.gridArr[RX, (int)gird.Coord.y].Type == type && GameDataManager.Instance.gridArr[RX, (int)gird.Coord.y].Coord != parent.Coord)
            {
                linkList.Add(GameDataManager.Instance.gridArr[RX, (int)gird.Coord.y].Coord);
            }
        }
        if (UY >= 0)
        {
            if (GameDataManager.Instance.gridArr[(int)gird.Coord.x, UY].Type == type && GameDataManager.Instance.gridArr[(int)gird.Coord.x, UY].Coord != parent.Coord)
            {
                linkList.Add(GameDataManager.Instance.gridArr[(int)gird.Coord.x, UY].Coord);
            }
        }
        if (DY < Defines.ColCount)
        {
            if (GameDataManager.Instance.gridArr[(int)gird.Coord.x, DY].Type == type && GameDataManager.Instance.gridArr[(int)gird.Coord.x, DY].Coord != parent.Coord)
            {
                linkList.Add(GameDataManager.Instance.gridArr[(int)gird.Coord.x, DY].Coord);
            }
        }
        return linkList;
    }
    public Dictionary<CellType, float> ScoreDic = new Dictionary<CellType, float>()
    {
        { CellType.Food1,10},
        { CellType.Food2,20},
        { CellType.Food3,40},
        { CellType.Food4,80},
        { CellType.Food5,100},
        { CellType.Food6,150},
        { CellType.Cleaner1,50},
        { CellType.Cleaner2,70}
    };
    //合成元素并消除旧元素
    public void BlendAndClearCell(int x, int y)
    {
        CoinType currentCoin = GameDataManager.Instance.gameData.CoinType;
        while (Tool.Instance.GetLinkCell(GameDataManager.Instance.gridArr[x, y]).Count >= 2)
        {
            if (Tool.Instance.GetLinkCell(GameDataManager.Instance.gridArr[x, y]).Count>=3)
            {
                MainUI.instance.AddRewardSliderValue(0.2f);
            }
            float score;
            if (ScoreDic.TryGetValue(GameDataManager.Instance.gridArr[(int)x, (int)y].Type, out score))
            {
                float scoreText = float.Parse(GameDataManager.Instance.gameData.Score);
                scoreText += ((Tool.Instance.GetLinkCell(GameDataManager.Instance.gridArr[x, y]).Count + 1) * score);
                GameDataManager.Instance.gameData.Score = scoreText.ToString();
                MainUI.instance.SetScore();
            }
            foreach (Vector2 t in Tool.Instance.GetLinkCell(GameDataManager.Instance.gridArr[x, y]))
            {
                GameDataManager.Instance.gridArr[(int)t.x, (int)t.y].HasObj = false;
                GameDataManager.Instance.gridArr[(int)t.x, (int)t.y].Status = CellStatus.None;
                GameDataManager.Instance.gridArr[(int)t.x, (int)t.y].Type = CellType.None;
                PoolManager.Instance.Delete(GameDataManager.Instance.gridArr[(int)t.x, (int)t.y].Entity);
                GameDataManager.Instance.gridArr[x, y].Entity = null;
            }
            if (currentCoin + 1 <= CoinType.CFood6 || currentCoin + 1 == CoinType.CCleaner2)
            {
                GameDataManager.Instance.gridArr[x, y].Type = (CellType)(currentCoin + 1);
                currentCoin = currentCoin + 1;
            }
        }
    }
    //合成最终元素后生成油渍障碍元素(3个)
    public void CreateOils(Vector3 currentPos)
    {
        List<Grid> EmptyCellList = new List<Grid>();
        for (int rowIndex = 0; rowIndex < Defines.RowCount; rowIndex++)
        {
            for (int colIndex = 0; colIndex < Defines.ColCount; colIndex++)
            {
                if (GameDataManager.Instance.gridArr[rowIndex, colIndex].Status == CellStatus.None)
                {
                    EmptyCellList.Add(GameDataManager.Instance.gridArr[rowIndex, colIndex]);
                }
            }
        }
        if (EmptyCellList.Count > 0)
        {
            int emptyCellNum = 3;
            while (EmptyCellList.Count > 0 && emptyCellNum > 0)
            {
                int randEmptyCell = UnityEngine.Random.Range(0, EmptyCellList.Count);
                GameObject oil = PoolManager.Instance.Get("FullBlock") as GameObject;
                oil.transform.SetParent(CellBox.transform);
                oil.transform.localPosition = currentPos;
                oil.transform.DOLocalMove(EmptyCellList[randEmptyCell].Position, 0.7f);
                int cellx = (int)EmptyCellList[randEmptyCell].Coord.x;
                int celly = (int)EmptyCellList[randEmptyCell].Coord.y;
                GameDataManager.Instance.gridArr[cellx,celly].Type=CellType.Block;
                GameDataManager.Instance.gridArr[cellx, celly].Status =CellStatus.Full;
                GameDataManager.Instance.gridArr[cellx, celly].Entity = oil;
                GameDataManager.Instance.gridArr[cellx, celly].HasObj = true;
                EmptyCellList.Remove(EmptyCellList[randEmptyCell]);
                emptyCellNum--;
            }
        }

    }
    //使用柠檬喷雾清除随机一个油渍
    public void ClearRandOil()
    {
        List<Grid> OilList = new List<Grid>();
        for (int rowIndex = 0; rowIndex < Defines.RowCount; rowIndex++)
        {
            for (int colIndex = 0; colIndex < Defines.ColCount; colIndex++)
            {
                if (GameDataManager.Instance.gridArr[rowIndex, colIndex].Type == CellType.Block)
                {
                    OilList.Add(GameDataManager.Instance.gridArr[rowIndex, colIndex]);
                }
            }
        }
        if (OilList.Count>0)
        {
            int randOil = UnityEngine.Random.Range(0,OilList.Count);
            GameObject soap = PoolManager.Instance.Get("FullSoap") as GameObject;
            soap.transform.SetParent(CellBox.transform);
            soap.transform.localPosition = OilList[randOil].Position;
            int cellx = (int)OilList[randOil].Coord.x;
            int celly = (int)OilList[randOil].Coord.y;
            PoolManager.Instance.Delete(GameDataManager.Instance.gridArr[cellx, celly].Entity);
            GameDataManager.Instance.gridArr[cellx, celly].Type = CellType.Soap;
            GameDataManager.Instance.gridArr[cellx, celly].Status = CellStatus.Full;
            GameDataManager.Instance.gridArr[cellx, celly].Entity = soap;
            GameDataManager.Instance.gridArr[cellx, celly].HasObj = true;
        }
    }
    //使用炸弹清除一个除油渍外的元素
    public void UseBomb(Vector3 p)
    {
        GameObject boom = PoolManager.Instance.Get("FullBoom") as GameObject;
        boom.transform.SetParent(CellBox.transform);
        boom.transform.localPosition = p;
    }
    //在相同元素单元格上生成相对于parent参数8方向提示箭头
    public void CreateArrows(Vector2 coord, Vector2 parent)
    {
        GameObject obj = null;
        //在父物体左边
        if (coord.x - parent.x == 0 && coord.y - parent.y <= -1)
        {
            obj = PoolManager.Instance.Get("Arrow_R") as GameObject;
        }
        //在左上
        if (coord.x - parent.x <= -1 && coord.y - parent.y <= -1)
        {
            obj = PoolManager.Instance.Get("Arrow_RD") as GameObject;
        }
        //在上
        if (coord.x - parent.x <= -1 && coord.y - parent.y == 0)
        {
            obj = PoolManager.Instance.Get("Arrow_D") as GameObject;
        }
        //在右上
        if (coord.x - parent.x <= -1 && coord.y - parent.y >= 1)
        {
            obj = PoolManager.Instance.Get("Arrow_LD") as GameObject;
        }
        //在右
        if (coord.x - parent.x == 0 && coord.y - parent.y >= 1)
        {
            obj = PoolManager.Instance.Get("Arrow_L") as GameObject;
        }
        //在右下
        if (coord.x - parent.x >= 1 && coord.y - parent.y >= 1)
        {
            obj = PoolManager.Instance.Get("Arrow_LU") as GameObject;
        }
        //在下
        if (coord.x - parent.x >= 1 && coord.y - parent.y == 0)
        {
            obj = PoolManager.Instance.Get("Arrow_U") as GameObject;
        }
        //在左下
        if (coord.x - parent.x >= 1 && coord.y - parent.y <= -1)
        {
            obj = PoolManager.Instance.Get("Arrow_RU") as GameObject;
        }
        obj.transform.parent = CellBox.transform;
        obj.transform.localPosition = GameDataManager.Instance.gridArr[(int)coord.x, (int)coord.y].Position;
    }
    //清除所有箭头
    public void ClearAllArrwo()
    {
        GameObject[] obj = FindObjectsOfType(typeof(GameObject)) as GameObject[]; //获取所有gameobject元素给数组obj
        foreach (GameObject child in obj)    //遍历所有gameobject
        {
            //Debug.Log(child.gameObject.name);  //可以在unity控制台测试一下是否成功获取所有元素
            if (child.gameObject.name.Length > 5)    //进行操作
            {
                if (child.gameObject.name.Substring(0, 5) == "Arrow")
                {
                    PoolManager.Instance.Delete(child);
                }
            }
        }
    }
    //通过类型获得相应游戏对象
    public GameObject CreateObjByType(CellType type, CellStatus status)
    {
        //GameObject obj = null;
        string path = null;
        if (status == CellStatus.Ready)
        {
            path = "ReadyCell/Ready" + type;
        }
        if (status == CellStatus.Full)
        {
            path = "FullCell/Full" + type;
        }
        switch (type)
        {
            case CellType.Block:
                break;
            case CellType.Soap:
                break;
            default:
                break;
        }
        GameObject obj = Instantiate(Resources.Load(path)) as GameObject;
        return obj;
    }
    //隐藏标志元素
    public void HideCoin()
    {
        foreach (Transform t in CoinBox.GetComponentsInChildren<Transform>())
        {
            if (t.name != CoinBox.name)
            {
                t.gameObject.SetActive(false);
            }
        }
    }
    //根据类型显示相应的标志元素
    public void SetCoinByType(CoinType CType)
    {
        foreach (Transform t in CoinBox.GetComponentsInChildren<Transform>())
        {
            if (t.name != CoinBox.name)
            {
                t.gameObject.SetActive(false);
            }
        }
        if (changeCoin == CoinType.None)
        {
            CoinBox.transform.Find(Enum.GetName(typeof(CoinType), CType)).gameObject.SetActive(true);
            GameDataManager.Instance.gameData.CoinType = CType;
        }
        else
        {
            print("Nochange");
            CoinBox.transform.Find(Enum.GetName(typeof(CoinType), changeCoin)).gameObject.SetActive(true);
            GameDataManager.Instance.gameData.CoinType = changeCoin;
            changeCoin = CoinType.None;
        }
    }

}
