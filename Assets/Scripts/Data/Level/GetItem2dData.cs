using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetItem2dData : MonoBehaviour
{
    public Item2dData item2dData;
    [HideInInspector]
    public bool _isSelected;
    [HideInInspector]
    public string _itemName;
    [HideInInspector]
    public string _itemType;
    [HideInInspector]
    public bool _isBad;
    [HideInInspector]
    public int _itemUserNum;


   private void Awake() 
   {
     _isSelected = item2dData.isSelected;
     _itemName = item2dData.itemName;
     _itemType = item2dData.itemType;
     _isBad = item2dData.isBad;
     _itemUserNum = item2dData.itemUserNum;
   }
}
