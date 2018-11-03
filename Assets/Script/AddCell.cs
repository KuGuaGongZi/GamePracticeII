using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AddCell : MonoBehaviour
{
    //鼠标相对于指定容器（CellBox）的位置
    private Vector2 mousePos;
    private Vector2 touchCellCoord;
    private Vector2 oldCoord;//储存上一个点击的位置变量
    public static bool hasReadyCell = false;
    void Start()
    {
        Tool.Instance.SetCoinByType(GameDataManager.Instance.gameData.CoinType);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && EventSystem.current.IsPointerOverGameObject() == false)
        {
            mousePos = Tool.Instance.GetPositionByPoint(Input.mousePosition);
            touchCellCoord = Tool.Instance.PositionToIndex(mousePos);
            if (touchCellCoord != new Vector2(-1, -1))
            {
                int x = (int)touchCellCoord.x;
                int y = (int)touchCellCoord.y;
                //点击在准备状态单元格上
                if (GameDataManager.Instance.gridArr[x, y].Status == CellStatus.Ready)
                {
                    Tool.Instance.ClearAllArrwo();
                    hasReadyCell = false;
                    PoolManager.Instance.Delete(GameDataManager.Instance.gridArr[x, y].Entity);
                    GameDataManager.Instance.gridArr[x, y].Entity = null;
                    Tool.Instance.BlendAndClearCell(x,y);
                    GameDataManager.Instance.gridArr[x, y].Status = CellStatus.Full;
                    GameDataManager.Instance.gridArr[x, y].HasObj = false;
                    GameDataManager.Instance.gameData.CoinType = Tool.Instance.GetRandCoinType();
                    Tool.Instance.SetCoinByType(GameDataManager.Instance.gameData.CoinType);
                }
                //点击在空位置
                if (GameDataManager.Instance.gridArr[x, y].Status == CellStatus.None)
                {
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
            }
            Tool.Instance.UpdateByMap();
        }
        if (Input.GetMouseButtonDown(1))
        {
            print("删除成功");
            GameDataManager.Instance.DeleteFile(Application.persistentDataPath, "GameData.json");
        }
    }
}
