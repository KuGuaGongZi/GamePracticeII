using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Singleton<T>
{
    private static readonly T ms_instance = Activator.CreateInstance<T>();

    public static T instance
    {
        get
        {
            return ms_instance;
        }
    }
}


public class Role : Singleton<Role>
{


}
