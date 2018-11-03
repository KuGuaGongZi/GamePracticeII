using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class ShopItem : MonoBehaviour,IPointerDownHandler
{
    private Sprite texture;
    private float price;

    void Start()
    {
        transform.Find("Item").GetComponent<Image>().sprite = texture;
        transform.Find("Price").GetComponent<Text>().text = price.ToString();
    }
    public void SetCoinTexture(Sprite t,float money)
    {
        texture = t;
        price = money;
    }
    //检测点击
    public void OnPointerDown(PointerEventData eventData)
    {
        WindowFactory.instance.CreateWindow(WindowType.ItemMessage);
        ItemMessage.instance.SetImg(transform.Find("Item").GetComponent<Image>().sprite);
        //Debug.Log((CoinType)(int.Parse(itemName)-1));
    }
}
