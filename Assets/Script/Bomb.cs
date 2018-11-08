using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class Bomb : MonoBehaviour
{
    //鼠标相对于指定容器（CellBox）的位置
    private Vector2 mousePos;
    private Vector2 touchCellCoord;
    void OnMouseDown()
    {
        mousePos = Tool.Instance.GetPositionByPoint(Input.mousePosition);
        touchCellCoord = Tool.Instance.PositionToIndex(mousePos);
        int x = (int)touchCellCoord.x;
        int y = (int)touchCellCoord.y;
        //点击在覆盖状态单元格上
        if (GameDataManager.Instance.gridArr[x, y].Status == CellStatus.Full && GameDataManager.Instance.gridArr[x, y].Type != CellType.Block)
        {
            GameDataManager.Instance.gridArr[x, y].Type = CellType.Boom;
            PoolManager.Instance.Delete(GameDataManager.Instance.gridArr[x, y].Entity);
            GameDataManager.Instance.gridArr[x, y].Entity = null;
            GameDataManager.Instance.gridArr[x, y].HasObj = false;
            PoolManager.Instance.Delete(gameObject);
            Tool.Instance.UseBomb(GameDataManager.Instance.gridArr[x, y].Position);
        }
        
    }
}
