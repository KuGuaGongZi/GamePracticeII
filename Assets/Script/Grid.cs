using UnityEngine;
using System.Collections;

public class Grid
{
    public bool HasObj
    {
        get;
        set;
    }
    //方格的逻辑坐标
    public Vector2 Coord
    {
        get;
        set;
    }
    //方格的类型
    public CellType Type
    {
        get;
        set;
    }
    //方格的状态
    public CellStatus Status
    {
        get;
        set;
    }
    //方格的实际位置
    public Vector3 Position
    {
        get;
        set;
    }
    //方格携带的游戏物体
    public GameObject Entity
    {
        get;
        set;
    }
}
