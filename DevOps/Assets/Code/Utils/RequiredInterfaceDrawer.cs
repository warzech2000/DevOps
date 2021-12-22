using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CustomPropertyDrawer(typeof(RequireInterfaceAttribute))]
public class RequiredInterfaceDrawer : PropertyDrawer
{
    /// <summary>
    /// Rysuje w gui fielda od interfejsów
    /// </summary>
    /// <param name="position">Pozycja/param>
    /// <param name="property">Właściwość</param>
    /// <param name="label">Label</param>
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // sprawddź czy to referencja
        if (property.propertyType == SerializedPropertyType.ObjectReference)
        {
            // Konwertuj
            var requiredAttribute = this.attribute as RequireInterfaceAttribute;
            // zacznić rysować
            EditorGUI.BeginProperty(position, label, property);
            // rysuj pole
            UnityEngine.Object obj = EditorGUI.ObjectField(position, label, property.objectReferenceValue, typeof(UnityEngine.Object), true);

            if(obj is GameObject g) property.objectReferenceValue = g.GetComponent(requiredAttribute.requiredType);
            // skończ rysować
            EditorGUI.EndProperty();
        }
        else
        {
            // jeśli pole nie jest referencją narysuj błąd
            // zapisz kolor i zmień obecny na czerwony
            var previousColor = GUI.color;
            GUI.color = Color.red;
            // pokaż labelke z błędem
            EditorGUI.LabelField(position, label, new GUIContent("Property is not a reference type"));
            // odwróć kolor
            GUI.color = previousColor;
        }
    }
}
