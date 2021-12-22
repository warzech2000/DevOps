using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequireInterfaceAttribute : PropertyAttribute
{
    public System.Type requiredType { get; private set; }
    /// <summary>
    /// Wymagaj implementacji <see cref="T:RequireInterfaceAttribute"/> interfejsu.
    /// </summary>
    /// <param name="type">typ interfejsu.</param>
    public RequireInterfaceAttribute(System.Type type)
    {
        this.requiredType = type;
    }
}
