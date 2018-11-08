using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapData : MonoBehaviour
{
    private static MapData instance;
    private static CellType[] map1 ={
        CellType.None, CellType.None, CellType.None, CellType.None, CellType.None, CellType.Food2,
        CellType.None, CellType.None, CellType.None, CellType.None, CellType.None, CellType.None,
        CellType.None, CellType.None, CellType.None, CellType.None, CellType.Food1, CellType.None,
        CellType.None, CellType.Food1,CellType.None, CellType.None, CellType.None, CellType.None,
        CellType.None, CellType.None, CellType.None, CellType.None, CellType.None, CellType.None,
        CellType.Food1,CellType.None, CellType.Food1,CellType.None, CellType.None, CellType.None,
    };
    private static CellType[] map2 ={
        CellType.None, CellType.None, CellType.None, CellType.Food2, CellType.None, CellType.Food2,
        CellType.None, CellType.None, CellType.None, CellType.None, CellType.None, CellType.None,
        CellType.None, CellType.None, CellType.None, CellType.None, CellType.Food1, CellType.None,
        CellType.None, CellType.Food3,CellType.None, CellType.None, CellType.None, CellType.None,
        CellType.Food1, CellType.None, CellType.None, CellType.None, CellType.None, CellType.None,
        CellType.Food1,CellType.None, CellType.Food1,CellType.None, CellType.None, CellType.None,
    };
    private static CellType[] map3 ={
        CellType.None, CellType.None, CellType.None, CellType.None, CellType.None, CellType.Food2,
        CellType.None, CellType.Food2, CellType.None, CellType.None, CellType.None, CellType.None,
        CellType.None, CellType.None, CellType.None, CellType.None, CellType.Food1, CellType.None,
        CellType.None, CellType.None,CellType.None, CellType.None, CellType.None, CellType.None,
        CellType.None, CellType.None, CellType.Food1, CellType.None, CellType.None, CellType.None,
        CellType.Food1,CellType.None, CellType.Food1,CellType.None, CellType.None, CellType.None,
    };
    public Dictionary<string, CellType[]> mapList = new Dictionary<string, CellType[]>()
    {
        {"map1",map1},
        {"map2",map2},
        {"map3",map3}
    };
    public static MapData Instance
    {
        get
        {
            return instance;
        }
    }
    

    void Awake()
    {
        instance = this;
    }
 
}
