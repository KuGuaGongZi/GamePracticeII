using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ShopData
{
    public int fileLength = 8;
    public Sprite[] spArr;
    GameObject obj;
    public Dictionary<string, ItemData> ShopDic;
    public ShopData()
    {
        spArr = new Sprite[fileLength];
        for (int i = 0; i < fileLength-1; i++)
        {
            obj = Resources.Load("ShopTexture/" + (i+1)) as GameObject;
            spArr[i] = obj.GetComponent<SpriteRenderer>().sprite;
        }
        GameObject bomb= Resources.Load("ShopTexture/9") as GameObject;
        spArr[7] = bomb.GetComponent<SpriteRenderer>().sprite;
        ShopDic = new Dictionary<string, ItemData>()
        {
            {"Item0",new ItemData(spArr[0],5)},
            {"Item1",new ItemData(spArr[1],10)},
            {"Item2",new ItemData(spArr[2],20)},
            {"Item3",new ItemData(spArr[3],40)},
            {"Item4",new ItemData(spArr[4],60)},
            {"Item5",new ItemData(spArr[5],100)},
            {"Item6",new ItemData(spArr[6],50)},
            {"Item7",new ItemData(spArr[7],100)},
        };

    }

}
public class ItemData
{
    public Sprite shopTexture;
    public float price;
    public ItemData(Sprite texture, float money)
    {
        shopTexture = texture;
        price = money;
    }
}