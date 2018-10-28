using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddCell : MonoBehaviour
{
    private Transform CellBox;

    void Start()
    {
        CellBox = GameObject.Find("CellBox").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            print("开始读取数据");
            GameDataManager.Instance.LoadData();
        }
        if (Input.GetMouseButtonDown(1))
        {
            print("删除成功");
            GameDataManager.Instance.DeleteFile(Application.persistentDataPath, "GameData.json");
        }
    }
}
