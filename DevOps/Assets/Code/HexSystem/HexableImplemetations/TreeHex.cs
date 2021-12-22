using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeHex : MonoBehaviour, IHexable
{
    //Pola Inspektora
    [Header("Costumizable Fields")]
    [Tooltip("How many times this field can be used")]
    [SerializeField] private int _maxUseCount = 5;      //Ile Razy pole może zostać użyte
    
    //Pola właściwości
    public HexType FieldType          { get; set; }
    public bool    IsLaunchingOnEnter { get; set; }
    public bool    IsPassable         { get; set; }
    
    //Zmienne Prywatne
    private int _useCount = 0;

    private void Start()
    {
        //ustawianie właściwości
        FieldType          = HexType.TREE;
        IsLaunchingOnEnter = false;
        IsPassable         = true;
    }

    #region InheritedFromIHexable
    public void    Interaction(Player player)
    {
        Debug.Log("Wood +1");
        _useCount++;
        Depleted();
    }

    public void    Depleted()
    {
        if (_useCount == _maxUseCount)
        {
            Destroy(this.gameObject);
        }
    }
    #endregion

}
