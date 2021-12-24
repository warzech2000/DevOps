using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private int              _slotNumber = 9;          //ile slotów ma być generowane na start
    [SerializeField] private Transform        _parentInventoryTransform;//Ojciec slotów, czyli pod co są podpinane
    [SerializeField] private GameObject       _slotPrefab;              //Prefab Slotów
    [SerializeField] private List<GameObject> _slotsList;               //Lista Slotów
    [SerializeField] private float            _maxWeight;               //Max obciążenie ekwipunku
    // Start is called before the first frame update
    private static Inventory _instance;                                 //instancja ekwipunku
    public static Inventory GetInventoryInstance()                      //Singleton Ekwipunku
    {
        if (_instance == null) _instance = FindObjectOfType<Inventory>();   //Znajdź Instancje eq w hierarchii
        return _instance;                                                   //zwróć instancje
    }
    void Start()
    {
        for (int i = 0; i < _slotNumber; i++)       //generacja slotów
        {
            _slotsList.Add(Instantiate(_slotPrefab, _parentInventoryTransform));
        }
    }
    public void AddItemToInventory(GameObject itemPrefab) //dodaje item do ekwipunku
    {
        var slotWithSameItem = SearchInSlotsWithNoFullQuantity(itemPrefab.GetComponent<Item>());
        if (slotWithSameItem != null)
        {
            slotWithSameItem.GetComponentInChildren<Item>().Quantity++;
        }
        else
        {
            var emptySlot = FindEmptySlot();
            if (emptySlot != null)
            {
                var  itemObject = Instantiate(itemPrefab, emptySlot.transform);
                itemObject.GetComponent<Item>().Quantity++;
            }
            else
            {
                //TODO Implementacja pełnego ekwipunku
            }
        }

        CalculateWeight();
    }

    public bool IsHaving(Item item)
    {
        var itemSlot = this.SearchInSlots(item);
        return itemSlot != null;
    }
    public bool SubstractFromInventory(Item item, int substractCount) //odejmuje podany item z ekwipunku
    {
        bool isDone      = false;
        var  actualCount = substractCount;
        if (this.GetSumOfItem(item) >= substractCount)
        {
            foreach (var slot in _slotsList)
            {
                if (slot.transform.childCount == 1)
                {
                    var child      = slot.transform.GetChild(0);
                    var itemInSlot = child.GetComponent<Item>();
                    if (itemInSlot.GetName() == item.GetName())
                    {
                        int toSubstract = Mathf.Clamp(actualCount, 0, item.GetMaxStackQuantity());
                        this.SubstractItem(slot, toSubstract);
                        actualCount -= toSubstract;
                        if (actualCount <= 0)
                        {
                            break;
                        }


                    }
                }
            }
        }
        return isDone;
    }
    private GameObject SearchInSlots(Item item)              //wyszukuje item podany we wszystkich slotach
    {
        GameObject slotWithItem = null;
        foreach (var slot in _slotsList)
        {
            if (slot.transform.childCount == 1)
            {
                var child = slot.transform.GetChild(0);
                if (child.GetComponent<Item>().GetName() == item.GetName())
                {
                    slotWithItem = slot;
                    break;
                }
            }
        }

        return slotWithItem;
    }
    private int GetSumOfItem(Item item)                      //Zbiera sume podanego itemu w eq
    {
        int sum = 0;
        foreach (var slot in _slotsList)
        {
            if (slot.transform.childCount == 1)
            {
                var child      = slot.transform.GetChild(0);
                var itemInSlot = child.GetComponent<Item>();
                if (itemInSlot.GetName() == item.GetName())
                {
                    sum += itemInSlot.Quantity;
                }
            }
        }
        return sum;
    }
    private GameObject SearchInSlotsWithNoFullQuantity(Item item)    //Wyszukuje item podany we wszystkich slotach i szuka nie pełnego
    {
        GameObject slotWithItem = null;
        foreach (var slot in _slotsList)
        {
            if (slot.transform.childCount == 1)
            {
                var child = slot.transform.GetChild(0);
                var itemInSlot  = child.GetComponent<Item>();
                if (itemInSlot.GetName() == item.GetName() && itemInSlot.Quantity < itemInSlot.GetMaxStackQuantity())
                {
                    slotWithItem = slot;
                    break;
                }
            }
        }

        return slotWithItem;
    }
    private float CalculateWeight()          //oblicza wagę całego ekwipunku
    {
        float weight = 0;
        foreach (var slot in _slotsList)
        {
            if (slot.transform.childCount == 1)
            {
                var child = slot.transform.GetChild(0);
                weight += child.GetComponent<Item>().GetWeight();
            }
        }
        return weight;
    }
    private GameObject FindEmptySlot()      //Znajduje pusty slot
    {
        GameObject emptySlot = null;
        foreach (var slot in _slotsList)
        {
            if (slot.transform.childCount == 0)
            {
                emptySlot = slot;
                break;
            }
        }
        return emptySlot;
    }
    private void SubstractItem(GameObject slotWithItem, int quantity) //handle od odejmowania itemów
    {
        var item = slotWithItem.GetComponentInChildren<Item>();
        item.Quantity -= quantity;
        if (item.Quantity == 0)
        {
            Destroy(slotWithItem.transform.GetChild(0));
        }
        CalculateWeight();
    }
}
