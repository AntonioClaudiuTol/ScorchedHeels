using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct ItemStruct
{
    public int itemCode;
    [Range(1, 999)]
    public int amount;
}

[CreateAssetMenu]
public class CraftingRecipe : ScriptableObject
{
    public List<ItemStruct> Materials;
    public List<ItemStruct> Results;
}
