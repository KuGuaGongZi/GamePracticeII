using UnityEngine;
using System.Collections;

public class Tool : MonoBehaviour
{
    private static Tool _instance;
    public static Tool Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = Camera.main.transform.gameObject.AddComponent<Tool>();
            }
            return _instance;
        }
    }
    void Awake()
    {
        _instance = this;
    }
    //真实坐标转数组下标
    public Vector2 PositionToIndex(Vector2 position)
    {
        if (position.x < 0 || position.x > Defines.CellSize.x * Defines.ColCount || position.y > 0 || position.y < -Defines.CellSize.y * Defines.RowCount)
        {
            return Vector2.zero;
        }

        return new Vector2(Mathf.Abs(position.y) / Defines.CellSize.y, position.x / Defines.CellSize.x);
    }
    //数组下标转真实坐标 (这里的真实坐标不是指世界坐标，而是指相对于承载着格子的物体的相对坐标)
    public Vector2 IndexToPosition(Vector2 arrayIndex)
    {
        return new Vector2(arrayIndex.y * Defines.CellSize.x,- arrayIndex.x * Defines.CellSize.y);
    }
    //获得点击位置真实坐标（指的是相对于承载格子的物体的相对坐标）
    public Vector2 GetPositionByPoint(Vector3 mousePosion, GameObject box)
    {
        return (Vector2)(Camera.main.ScreenToWorldPoint(Input.mousePosition) - box.transform.position);
    }
    //通过类型获得相应游戏对象
    public GameObject CreateObjByType(CellType type)
    {
        GameObject obj = null;
        switch (type)
        {
            case CellType.Food1:
                break;
            case CellType.Food2:
                break;
            case CellType.Food3:
                break;
            case CellType.Food4:
                break;
            case CellType.Food5:
                break;
            case CellType.Food6:
                break;
            case CellType.Block:
                break;
            case CellType.Cleaner1:
                break;
            case CellType.Cleaner2:
                break;
            case CellType.None:
                obj = null;
                break;
            default:
                break;
        }
        return obj;
    }

}
