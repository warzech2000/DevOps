using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpassableNullHex : MonoBehaviour, IHexable
{
    // Start is called before the first frame update
    void Start()
    {
        FieldType          = HexType.NULL;
        IsLaunchingOnEnter = false;
        IsPassable         = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public HexType FieldType          { get; set; }
    public bool    IsLaunchingOnEnter { get; set; }
    public bool    IsPassable         { get; set; }
    public void    Interaction(Player player)
    {
        Debug.LogError("this shouldn't be possible");
    }

    public void    Depleted()
    {
        Debug.LogError("this shouldn't be possible");

    }
}
