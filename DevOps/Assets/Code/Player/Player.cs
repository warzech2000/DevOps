using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject hammerPrefab;
    public void InteractWithHexBelow(InputAction.CallbackContext value)
    {
        if(value.started) this.transform.parent.GetComponent<HexScript>().HandlePlayerInteraction(this);
        Debug.Log("interacted");
        if (!Inventory.GetInventoryInstance().IsHaving(hammerPrefab.GetComponent<Item>()))
        {
            Inventory.GetInventoryInstance().AddItemToInventory(hammerPrefab);
        }
    }

    public void MovePlayer(InputAction.CallbackContext value)
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
                }else if (objectOnHex == null)
                {
                    this.transform.SetParent(hit.collider.transform, false);

                }
            }
        }
    }

}
