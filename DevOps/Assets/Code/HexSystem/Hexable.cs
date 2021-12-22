using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public interface IHexable
{
    public HexType FieldType            //Zmienna mówiąca o typu pola
    {
        get;
        set;
    }

    public bool IsLaunchingOnEnter      //Zmienna, Która mówi czy pole aktywuje się po wejściu gracza
    {
        get;
        set;
    }

    public bool IsPassable              //Zmiena, Która mówi czy pole jest do przejścia
    {
        get;
        set;
    }
    public void Interaction(Player player); //Metoda mająca odpowiadać za interakcje
    public void Depleted();    //Metoda odpowiadająca za skończenie się zasobu
}

public enum HexType             //Typ Pola
{
    TREE, GRASS, NULL
}

namespace HexUtils
{
    public static class HexUtils
    {
        public static GameObject FindGrid(this GameObject parent, int x, int y) //Metoda rozszerzająca do szukania gridów
        {
            for (int i = 0; i < parent.transform.childCount; i++)               //zwykły for przez wszyskie dzieci parenta
            {
                var child = parent.transform.GetChild(i);               //weź dziecko
                var name  = child.gameObject.name;                         //skrót do nazwy
                if (int.Parse(name.Substring(5, 3)) == x && int.Parse(name.Substring(10,3)) == y) //zamień na inta od 5 znaku 3 kolejne i sprawdź czy są równe dla x i to samo dla y
                {
                    return child.gameObject;                                    //jeśli tak to zwórć dziecko
                }
            }

            return null;                                                        //w przeciwnym wypadku zwróć nic
        }
    }
}
