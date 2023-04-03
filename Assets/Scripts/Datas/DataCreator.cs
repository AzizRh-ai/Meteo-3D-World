using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DataCreator : MonoBehaviour
{
    public static LocationData CreateLocationData(string name)
    {
        LocationData newData = ScriptableObject.CreateInstance("LocationData") as LocationData;
        newData.name = name;

        Selection.activeObject = newData;

        return newData;
    }
}
