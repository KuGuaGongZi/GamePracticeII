using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//标志元素类型
public enum CoinType
{
    CFood1,
    CFood2,
    CFood3,
    CFood4,
    CFood5,
    CFood6,
    CCleaner1, 
    CCleaner2,
    Bomb,
    None
}
//单元格元素类型
public enum CellType
{
    Food1,
    Food2,
    Food3,
    Food4,
    Food5,
    Food6,
    Cleaner1,
    Cleaner2,
    Bomb,
    Boom,
    Block,
    Soap,
    None
}
//单元格类型
public enum CellStatus
{
    Ready,
    Full,
    None
}
//奖励时间物品类型
public enum RewardType
{
    RewardFood1,
    RewardFood2,
    RewardFood3,
    RewardFood4,
    RewardFood5,
    RewardFood6,
    RewardCleaner1,
    RewardCleaner2,
    RewardBomb,
    None
}
