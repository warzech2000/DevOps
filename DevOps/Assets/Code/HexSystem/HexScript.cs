using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class HexScript : MonoBehaviour
{
    [Header("Grid Fields")] [Tooltip("Field for object attached on this hexagon")] [SerializeField] [RequireInterface(typeof(IHexable))]
    private Object _objectOnField;    //Obiekt interaktywny na hexie

    [SerializeField] private float _radiusOfNearHexCheck = 5f;

    [SerializeField] private bool             _isDrawingGizmos = true;
    private                  List<GameObject> _surroundingHexes;

    private void Start()
    {
        _surroundingHexes = new List<GameObject>();
        RaycastHit2D[] raycastHits2D =
            Physics2D.CircleCastAll(transform.position, _radiusOfNearHexCheck, Vector2.zero);
        foreach (var hit in raycastHits2D)
        {
            if (hit.collider.tag == "Hex")
            {
                _surroundingHexes.Add(hit.collider.gameObject);
            }
        }

    }

    public void OnDrawGizmosSelected()
    {
        if (_isDrawingGizmos)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(this.transform.position, _radiusOfNearHexCheck);
        }
    }

    public bool IsAdjecent(GameObject hexToCheck)    //Czy hex jest sąsiadujący
    {
        foreach (var hex in _surroundingHexes)
        {
            if (hex.GetInstanceID() == hexToCheck.GetInstanceID())
            {
                return true;
                break;
            }
        }

        return false;
    }

    public void HandlePlayerEnter(Player player) //Metoda odpowiadająca za wejscie gracza na pole
    {
        if (_objectOnField != null)             //Jeśli obiekt nie jest pusty
        {
            
            var hexConversion = _objectOnField as IHexable; //Konwertuj na Hexable
            if (hexConversion != null &&                    //czy konwersja się udała
                hexConversion.IsLaunchingOnEnter )          //i sprawdź czy reaguje na starcie
            {
                hexConversion.Interaction(player); //Niech zareaguje
            }
        }
    }


    public void HandlePlayerInteraction(Player player)  //Metoda decyzyjnej interakcji z graczem
    {
        if (_objectOnField != null) //Jeśli obiekt nie jest pusty
        {
            var hexConversion = _objectOnField as IHexable; //Konwertuj na Hexable
            if (hexConversion != null)                      //Sprawdź czy konwersja się udała
            {
                hexConversion.Interaction(player); //Reaguj
            }
        }
    }
}
