using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour
{
    void Start()
    {
        WindowFactory.instance.CreateWindow(WindowType.MenuMainUI);
    }
}
