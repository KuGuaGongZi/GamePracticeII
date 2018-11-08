using UnityEngine;
using System.Collections;

public class TeachManager : MonoBehaviour
{
    void Start()
    {
        WindowFactory.instance.CreateWindow(WindowType.TeachMainUI);
        WindowFactory.instance.CreateWindow(WindowType.TalkUI);
    }
}
