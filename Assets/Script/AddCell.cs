using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AddCell : MonoBehaviour
{
    //鼠标相对于指定容器（CellBox）的位置
    private Vector2 mousePos;
    private Vector2 touchCellCoord;
    private Vector2 oldCoord;//储存上一个点击的位置变量
    public static bool hasReadyCell = false;
    public static bool hasTool = false;
    public static Vector2 currentTouchCoord;//当前点击格子的逻辑位置
    private GameObject BgBox;
    private GameObject Bg;
    Transform rewardPlane;

    void Start()
    {
        BgBox = GameObject.Find("Bg");
        Bg = BgBox.transform.Find("BackGround").gameObject;
        rewardPlane = transform.Find("RewardPlane");
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButtonDown(1))
        //{
        //    MainUI.instance.Hide();
        //    Bg.SetActive(false);
        //    rewardPlane.gameObject.SetActive(true);
        //    rewardPlane.gameObject.AddComponent<RewardManager>();
        //}
        if (Input.GetKeyDown(KeyCode.M)&& Input.GetKeyDown(KeyCode.O))
        {
            GameDataManager.Instance.gameData.Money = "1000";
            MainUI.instance.SetMoney();
        }
        if (Input.GetMouseButtonDown(0) && EventSystem.current.IsPointerOverGameObject() == false&& !rewardPlane.gameObject.activeInHierarchy)
        {
            mousePos = Tool.Instance.GetPositionByPoint(Input.mousePosition);
            touchCellCoord = Tool.Instance.PositionToIndex(mousePos);
            if (touchCellCoord != new Vector2(-1, -1) && GameDataManager.Instance.gameData.CoinType == CoinType.Bomb)
            {
                Tool.Instance.HideCoin();
                int x = (int)touchCellCoord.x;
                int y = (int)touchCellCoord.y;
                Tool.Instance.SetToolPos(GameDataManager.Instance.gridArr[x, y].Position);
                hasTool = true;
                return;
            }
            if (touchCellCoord != new Vector2(-1, -1))
            {
                int x = (int)touchCellCoord.x;
                int y = (int)touchCellCoord.y;
                //点击在覆盖状态单元格上
                if (GameDataManager.Instance.gridArr[x, y].Status == CellStatus.Full)
                {
                    if (GameDataManager.Instance.gridArr[x, y].Type == CellType.Food6)
                    {
                        currentTouchCoord = new Vector2(x, y);
                        WindowFactory.instance.CreateWindow(WindowType.AskSellUI);
                    }
                    if (GameDataManager.Instance.gridArr[x, y].Type == CellType.Cleaner2)
                    {
                        currentTouchCoord = new Vector2(x, y);
                        WindowFactory.instance.CreateWindow(WindowType.AskWashUI);
                    }
                }
                //点击在准备状态单元格上
                if (GameDataManager.Instance.gridArr[x, y].Status == CellStatus.Ready)
                {
                    Tool.Instance.ClearAllArrwo();
                    hasReadyCell = false;
                    PoolManager.Instance.Delete(GameDataManager.Instance.gridArr[x, y].Entity);
                    GameDataManager.Instance.gridArr[x, y].Entity = null;
                    Tool.Instance.BlendAndClearCell(x, y);
                    if (GameDataManager.Instance.gridArr[x, y].Type == CellType.Food6)
                    {
                        Tool.Instance.CreateOils(GameDataManager.Instance.gridArr[x, y].Position);
                    }
                    GameDataManager.Instance.gridArr[x, y].Status = CellStatus.Full;
                    GameDataManager.Instance.gridArr[x, y].HasObj = false;
                    GameDataManager.Instance.gameData.CoinType = Tool.Instance.GetRandCoinType();
                    Tool.Instance.SetCoinByType(GameDataManager.Instance.gameData.CoinType);
                    GameDataManager.Instance.SaveData();
                }
                //点击在空位置
                if (GameDataManager.Instance.gridArr[x, y].Status == CellStatus.None)
                {
                    Tool.Instance.HideCoin();
                    Tool.Instance.ClearAllArrwo();
                    Tool.Instance.HideCoin();
                    //场景中有待准备状态单元格
                    if (hasReadyCell)
                    {
                        int oldx = (int)oldCoord.x;
                        int oldy = (int)oldCoord.y;
                        GameDataManager.Instance.gridArr[oldx, oldy].Type = CellType.None;
                        GameDataManager.Instance.gridArr[oldx, oldy].Status = CellStatus.None;
                        //PoolManager.Instance.Delete(GameDataManager.Instance.gridArr[oldx, oldy].Entity);
                        GameDataManager.Instance.gridArr[oldx, oldy].HasObj = false;
                        GameDataManager.Instance.gridArr[x, y].Type = (CellType)GameDataManager.Instance.gameData.CoinType;
                        GameDataManager.Instance.gridArr[x, y].Status = CellStatus.Ready;
                    }
                    //场景中没有待准备状态单元格
                    if (!hasReadyCell)
                    {
                        hasReadyCell = true;
                        GameDataManager.Instance.gridArr[x, y].Type = (CellType)GameDataManager.Instance.gameData.CoinType;
                        GameDataManager.Instance.gridArr[x, y].Status = CellStatus.Ready;
                    }
                    if (Tool.Instance.GetLinkCell(GameDataManager.Instance.gridArr[x, y]).Count >= 2)
                    {
                        foreach (Vector2 t in Tool.Instance.GetLinkCell(GameDataManager.Instance.gridArr[x, y]))
                        {
                            Tool.Instance.CreateArrows(t, GameDataManager.Instance.gridArr[x, y].Coord);
                        }
                    }
                }
                //如果上个点击的单元格不是覆盖状态，则将他储存起来
                if (GameDataManager.Instance.gridArr[x, y].Status != CellStatus.Full)
                {
                    oldCoord = touchCellCoord;
                }
                Tool.Instance.UpdateByMap();
            }
        }
    }
}
