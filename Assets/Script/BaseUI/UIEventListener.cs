using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class UIEventListener : MonoBehaviour, IEventSystemHandler, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler,
    IPointerUpHandler, IPointerClickHandler, IDropHandler, IDragHandler
{

    public delegate void VoidHandle(GameObject go, PointerEventData data);

    public VoidHandle onClick;
    public VoidHandle onDown;
    public VoidHandle onEnter;
    public VoidHandle onExit;
    public VoidHandle onUp;
    public VoidHandle onSelect;
    public VoidHandle onUpdateSelect;
    public VoidHandle onDrag;
    public VoidHandle onDrop;
    public VoidHandle onBeginDrag;
    public VoidHandle onEndDrag;
    public VoidHandle onLongClick;
    public VoidHandle onLongPress;


    public static UIEventListener Get(GameObject go)
    {
        UIEventListener evt = go.GetComponent<UIEventListener>();
        if (evt == null)
        {
            evt = go.AddComponent<UIEventListener>();
        }

        return evt;
    }


    public void OnDrop(PointerEventData eventData)
    {
        if (onDrop!= null)
        {
            onDrop(gameObject, eventData);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (onClick!= null)
        {
            onClick(gameObject, eventData);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (onDown!= null)
        {
            onDown(gameObject, eventData);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (onEnter!= null)
        {
            onEnter(gameObject, eventData);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (onExit!=null)
        {
            onExit(gameObject, eventData);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (onUp!= null)
        {
            onUp(gameObject, eventData);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (onDrag != null)
        {
            onDrag(gameObject, eventData);
        }
    }
}