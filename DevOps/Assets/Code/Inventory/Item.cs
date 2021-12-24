using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.UI;

//Klasa itemu przypisanego do slota
public class Item : MonoBehaviour
{
    [Tooltip("How much item weight")]
    [SerializeField] private float    _weight;                //Waga itemu
    [Tooltip("How many will be in stack")]
    [SerializeField] private int      _maxStackQuantity = 64; //Max liczba w slocie
    [Tooltip("Item name")]
    [SerializeField] private string _name;   //Nazwa
    [Tooltip("Which item type is this item")]
    [SerializeField] private ItemType _type; //typ itemu
    [Space]
    [Header("UI")]
    [Tooltip("Display for quantity")]
    [SerializeField] private Text     _text;                  //Pole Textowe do obsługi ilości

    private int _quantity = 0;                              //ile jest aktualnie
    public int Quantity
    {
        get { return _quantity;}
        set
        {
            _quantity   = value;
            _text.text = value.ToString();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region getters
    public float GetWeight()
    {
        return _weight;
    }
    public string GetName()
    {
        return _name;
    }
    public int GetMaxStackQuantity()
    {
        return _maxStackQuantity;
    }    
    #endregion

}

public enum ItemType    //typ itemu
{
    RESOURCE, TOOL
}
