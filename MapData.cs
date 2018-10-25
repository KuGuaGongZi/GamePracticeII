using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class MapData
{
    public List<LevelData> Level;
}


[System.Serializable]
public class LevelData
{
    public int RowCount;
    public int ColCount;
    public string[] Map;
}
