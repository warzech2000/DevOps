using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject _hammerPrefab;          //Prefab młotka, póki nie ma craftingu i siekiery
    [SerializeField] private GameObject _inventoryHandle;       //uchwyt do ui inv
    public void InteractWithHexBelow(InputAction.CallbackContext value) //input interakcji z hexem na którym stoimy
    {
        if (value.started)
        {
            this.transform.parent.GetComponent<HexScript>().HandlePlayerInteraction(this);
            Debug.Log("interacted");
            if (!Inventory.GetInventoryInstance()
                          .IsHaving(_hammerPrefab
                                        .GetComponent<Item>())) //Debug - dodanie młotka, żeby móc ścinać drzewa
            {
                Inventory.GetInventoryInstance().AddItemToInventory(_hammerPrefab);
            }
        }
    }

    public void MovePlayer(InputAction.CallbackContext value)               //input poruszania się
    {
        if (value.started)
        {
            var          mousePosition = Mouse.current.position.ReadValue();
            Vector2      inWorldSpace  = Camera.main.ScreenToWorldPoint(mousePosition);
            RaycastHit2D hit           = Physics2D.Raycast(inWorldSpace, Vector2.zero);
            if (hit.collider != null && hit.collider.tag == "Hex")
            {
                var hex         = this.transform.parent.GetComponent<HexScript>();
                var objectOnHex = hit.collider.GetComponentInChildren<IHexable>();
                if (this.transform.parent.GetComponent<HexScript>().IsAdjecent(hit.collider.gameObject))
                {
                    if (objectOnHex != null && objectOnHex.IsPassable)
                    {
                        this.transform.SetParent(hit.collider.transform, false);
                    }
                    else if (objectOnHex == null)
                    {
                        this.transform.SetParent(hit.collider.transform, false);

                    }
                }
            }
        }
    }

    public void ToggleInventory(InputAction.CallbackContext value)                   //przełączanie ekwipunku
    {
        if (value.started)
        {
            _inventoryHandle.SetActive(!_inventoryHandle.activeSelf);
        }
    }

}
