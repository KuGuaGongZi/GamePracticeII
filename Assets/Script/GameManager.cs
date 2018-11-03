using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    void Start()
    {
       GameDataManager.Instance.LoadData();
       WindowFactory.instance.CreateWindow(WindowType.MainUI);
    }
}
