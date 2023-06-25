using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item2dData", menuName = "ScriptableObjects/Item2dData", order = 1)]
public class Item2dData : ScriptableObject
{
    public bool isSelected;

    public string itemName;
    public string itemType;
    public bool isBad;
    public int itemUserNum;
    public GameObject itemTreeSprite;
}
