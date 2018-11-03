using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonWindow<T> : Window where T : Window
{
    private static T ms_instance = null;

    public static T instance
    {
        get
        {
            return ms_instance;
        }
        set
        {
            ms_instance = value;
        }
    }

    protected SingletonWindow()
    {
        ms_instance = this as T;
    }

    public override void Close()
    {
        base.Close();

        ms_instance = null;
    }
}
