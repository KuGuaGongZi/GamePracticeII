using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System.IO;

public class GameDataManager : MonoBehaviour
{
    public Grid[,] gridArr
    {
        get;
        set;
    }
    public GameData gameData
    {
        get;
        set;
    }
    private static GameDataManager instance;
    public static GameDataManager Instance
    {
        get
        {
            return instance;
        }
    }
    void Awake()
    {
        instance = this;
        gameData = new GameData();

    }
    //初始化数据
    public void InitData()
    {
        gameData.Money = "0";
        gameData.Score = "0";
        gameData.CoinType = CoinType.CFood1;
        //分配内存空间
        gameData.Level = new LevelData[3];
        for (int level = 0; level < gameData.Level.Length; level++)
        {
            gameData.Level[level] = new LevelData();
            gameData.Level[level].Map = new CellType[Defines.RowCount * Defines.ColCount];
        }
        int index = 0;
        foreach (KeyValuePair<string, CellType[]> item in MapData.Instance.mapList)
        {
            gameData.Level[index].Map = item.Value;
            index++;
        }
        int randLevel = UnityEngine.Random.Range(0,gameData.Level.Length);

        //储备地图的初始化
        gameData.SaveMap = new CellType[Defines.RowCount * Defines.ColCount];
        gameData.SaveMap = gameData.Level[randLevel].Map;
        int arrIndex = 0;
        gridArr= new Grid[Defines.RowCount, Defines.ColCount];
        for (int rowIndex = 0; rowIndex < Defines.RowCount; rowIndex++)
        {
            for (int colIndex = 0; colIndex < Defines.ColCount; colIndex++)
            {
                gridArr[rowIndex, colIndex] = new Grid();
                //将定义好的初始化地图中记录的单元格类型赋值存进二维数组里
                gridArr[rowIndex, colIndex].Type = gameData.Level[randLevel].Map[arrIndex];
                gridArr[rowIndex, colIndex].Status = CellStatus.Full;
                gridArr[rowIndex, colIndex].HasObj = false;
                gridArr[rowIndex, colIndex].Entity = null;
                if (gridArr[rowIndex, colIndex].Type == CellType.None)
                {
                    gridArr[rowIndex, colIndex].Status = CellStatus.None;
                }
                gridArr[rowIndex, colIndex].Coord = new Vector2(rowIndex,colIndex);
                gridArr[rowIndex, colIndex].Position = Tool.Instance.IndexToPosition(gridArr[rowIndex, colIndex].Coord);
                arrIndex++;
            }

        }
        Tool.Instance.UpdateByMap();
        string json = JsonUtility.ToJson(gameData);
        CreateFile(Application.persistentDataPath, "GameData.json", json);

    }
    //储存数据
    public void SaveData()
    {
        //先删除文件
        DeleteFile(Application.persistentDataPath, "GameData.json");
        gameData.Money = "0";
        gameData.Score = "0";
        //分配内存空间
        gameData.Level = new LevelData[3];
        for (int level = 0; level < gameData.Level.Length; level++)
        {
            gameData.Level[level] = new LevelData();
            gameData.Level[level].Map = new CellType[Defines.RowCount * Defines.ColCount];
        }
        //储备地图的内存分配
        gameData.SaveMap = new CellType[Defines.RowCount*Defines.ColCount];
        int arrIndex = 0;
        //将二维数组里的地图数据存进储备地图里
        for (int row= 0; row < Defines.RowCount; row++)
        {
            for (int col = 0; col < Defines.ColCount; col++)
            {
                gameData.SaveMap[arrIndex] =gridArr[row,col].Type;
            }
        }
        string json = JsonUtility.ToJson(gameData);
        CreateFile(Application.persistentDataPath, "GameData.json", json);
    }
    //加载数据
    public void LoadData()
    {
        ReadData(Application.persistentDataPath, "GameData.json");

    }
    //读取数据
    public void ReadData(string path, string name)
    {
        //如果文件不存在则返回
        if (!File.Exists(path + "//" + name))
        {
            Debug.Log("文件不存在,开始初始化数据");
            InitData();
        }
        else
        {
            //使用流的形式读取
            StreamReader sr = null;
            string json = null;
            try
            {
                sr = File.OpenText(path + "//" + name);
                json = sr.ReadToEnd();
            }
            catch (Exception)
            {
                Debug.Log("读取文件错误");
            }
            if (!string.IsNullOrEmpty(json))
            {
                gameData = JsonUtility.FromJson<GameData>(json);
                int arrIndex = 0;
                gridArr= new Grid[Defines.RowCount, Defines.ColCount];
                for (int rowIndex = 0; rowIndex < Defines.RowCount; rowIndex++)
                {
                    for (int colIndex = 0; colIndex < Defines.ColCount; colIndex++)
                    {
                        gridArr[rowIndex, colIndex] = new Grid();
                        //将储备地图中记录的单元格类型赋值存进二维数组里
                        gridArr[rowIndex, colIndex].Type = gameData.SaveMap[arrIndex];
                        gridArr[rowIndex, colIndex].Status = CellStatus.Full;
                        if (gridArr[rowIndex, colIndex].Type == CellType.None)
                        {
                            gridArr[rowIndex, colIndex].Status = CellStatus.None;
                        }
                        gridArr[rowIndex, colIndex].Coord = new Vector2(rowIndex, colIndex);
                        gridArr[rowIndex, colIndex].Position= Tool.Instance.IndexToPosition(gridArr[rowIndex, colIndex].Coord);
                        gridArr[rowIndex, colIndex].HasObj = false;
                        gridArr[rowIndex, colIndex].Entity = null;
                        arrIndex++;
                    }
                }
                Tool.Instance.UpdateByMap();
            }
        }
    }
    //删除文件
    public void DeleteFile(string path, string name)
    {
        File.Delete(path + "//" + name);
    }
    //创建文件
    public void CreateFile(string path, string name, string info)
    {
        //文件流信息
        StreamWriter sw;
        FileInfo file = new FileInfo(path + "//" + name);
        //如果不存在则创建
        //存在则打开文件
        if (!file.Exists)
        {
            sw = file.CreateText();
        }
        else
        {
            sw = file.AppendText();
        }
        //以行的形式写入信息
        sw.WriteLine(info);
        //关闭流
        sw.Close();
        //销毁流
        sw.Dispose();
    }
}
