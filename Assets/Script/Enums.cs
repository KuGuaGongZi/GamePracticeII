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
