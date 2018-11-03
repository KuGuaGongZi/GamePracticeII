using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowFactory : Singleton<WindowFactory>
{
    public Window CreateWindow(WindowType winType)
    {
        return CreateWindow(winType, null, null, null);
    }
    public Window CreateWindow(WindowType winType, string openParam)
    {
        return CreateWindow(winType, openParam, null, null);
    }
    public Window CreateWindow(WindowType winType, object closeParam)
    {
        return CreateWindow(winType, null, closeParam, null);
    }
    public Window CreateWindow(WindowType winType, string openParam, object closeParam)
    {
        return CreateWindow(winType, openParam, closeParam, null);
    }
    public Window CreateWindow(WindowType winType, Window.VoidHandle openHandle)
    {
        return CreateWindow(winType, null, null, openHandle);
    }

    //Main Function
    public Window CreateWindow(WindowType type, string openParam, object closeParam, Window.VoidHandle openHandle)
    {
        Window wnd = WindowManager.instance.FindWindow(type);
        if (wnd != null)
        {
            wnd.show = true;
            if (openHandle != null)
            {
                openHandle();
            }
            wnd.Show();
        }
        else
        {
            wnd = UIHelper.CreateWindow(type, openParam, closeParam, openHandle);
        }

        return wnd;
    }

    public void CloseWindow(WindowType type)
    {
        Window wnd = WindowManager.instance.FindWindow(type);
        if (wnd == null)
            return;

        WindowManager.instance.CloseWindow(wnd);
    }
    //Show
    public void ShowWindow(WindowType type)
    {
        Window wid = WindowManager.instance.FindWindow(type);

        if (wid != null)
        {
            wid.Show();
        }
    }

    public void ShowWindow(string type)
    {
        Window wid = WindowManager.instance.FindWindow(type);
        if (wid != null)
        {
            wid.Show();
        }
    }

    //Hide

    public void HideWindow(WindowType type)
    {
        Window wid = WindowManager.instance.FindWindow(type);
        if (wid != null)
        {
            wid.Hide();
        }
    }

    public void HideWindow(string type)
    {
        Window wid = WindowManager.instance.FindWindow(type);
        if (wid != null)
        {
            wid.Hide();
        }
    }
}
