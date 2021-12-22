using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class HexScript : MonoBehaviour
{
    [Header("Grid Fields")] [Tooltip("Field for object attached on this hexagon")] [SerializeField] [RequireInterface(typeof(IHexable))]
    private Object _objectOnField;    //Obiekt interaktywny na hexie

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
