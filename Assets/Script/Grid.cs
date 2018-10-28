using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour
{
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
    void OnMouseDown()
    {
        Debug.Log("摁下");
    } 
}
