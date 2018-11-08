using UnityEngine;
using System.Collections;

public class AnimEvent : MonoBehaviour
{
    //清除肥皂
    public void DeleteSnop()
    {
        for (int rowIndex = 0; rowIndex < Defines.RowCount; rowIndex++)
        {
            for (int colIndex = 0; colIndex < Defines.ColCount; colIndex++)
            {
                if (GameDataManager.Instance.gridArr[rowIndex, colIndex].Position == transform.localPosition)
                {
                    GameDataManager.Instance.gridArr[rowIndex, colIndex].Type = CellType.None;
                    GameDataManager.Instance.gridArr[rowIndex, colIndex].Status = CellStatus.None;
                    GameDataManager.Instance.gridArr[rowIndex, colIndex].HasObj = false;
                    PoolManager.Instance.Delete(GameDataManager.Instance.gridArr[rowIndex, colIndex].Entity);
                    GameDataManager.Instance.gridArr[rowIndex, colIndex].Entity = null;
                    Tool.Instance.UpdateByMap();
                    GameDataManager.Instance.SaveData();
                    return;
                }
            }
        }
    }
    public void DeleteBoom()
    {
        for (int rowIndex = 0; rowIndex < Defines.RowCount; rowIndex++)
        {
            for (int colIndex = 0; colIndex < Defines.ColCount; colIndex++)
            {
                if (GameDataManager.Instance.gridArr[rowIndex, colIndex].Position == transform.localPosition)
                {
                    GameDataManager.Instance.gridArr[rowIndex, colIndex].Type = CellType.None;
                    GameDataManager.Instance.gridArr[rowIndex, colIndex].Status = CellStatus.None;
                    PoolManager.Instance.Delete(gameObject);
                    GameDataManager.Instance.gameData.CoinType = Tool.Instance.GetRandCoinType();
                    Tool.Instance.SetCoinByType(GameDataManager.Instance.gameData.CoinType);
                    AddCell.hasTool = false;
                    Tool.Instance.UpdateByMap();
                    GameDataManager.Instance.SaveData();
                    return;
                }
            }
        }
    }
}
