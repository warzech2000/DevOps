using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public void InteractWithHexBelow(InputAction.CallbackContext value)
    {
        if(value.started) this.transform.parent.GetComponent<HexScript>().HandlePlayerInteraction(this);
    }
}
