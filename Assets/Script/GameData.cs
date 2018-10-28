using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class GameData
{
    public string Money;
    public string Score;
    public CellType[] SaveMap;
    public LevelData[] Level;
}
[System.Serializable]
public class LevelData
{
    public CellType[] Map;
}