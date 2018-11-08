using UnityEngine;
using System.Collections;
public class GameManager : MonoBehaviour
{
    void Start()
    {
        if (GlobalData.isNewGame)
        {
            GameDataManager.Instance.InitData();
        }
        else
        {
            Debug.Log("继续游戏");
            GameDataManager.Instance.LoadData();
        }
       WindowFactory.instance.CreateWindow(WindowType.MainUI);
    }
}
