using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEditor.FilePathAttribute;

public class EditorDataCreator : MonoBehaviour
{

    [MenuItem("Window/3D Meteo/Data Creator/Create UI Settings Data")]
    public static void CreateUISettingsData()
    {
        UISettingsData newData = ScriptableObject.CreateInstance("UISettingsData") as UISettingsData;
        newData.name = "(rename me) New UISettings Data";

        Selection.activeObject = newData;

        string path = "Assets/Content/Datas/Settings/" + newData.name + ".asset";
        AssetDatabase.CreateAsset(newData, path);
    }

    [MenuItem("Window/3D Meteo/Data Creator/Create Game Settings Data")]
    public static void CreateGameSettingsData()
    {
        GameSettingsData newData = ScriptableObject.CreateInstance("GameSettingsData") as GameSettingsData;
        newData.name = "(rename me) New GameSettings Data";

        Selection.activeObject = newData;

        string path = "Assets/Content/Datas/Settings/" + newData.name + ".asset";
        AssetDatabase.CreateAsset(newData, path);
    }

    [MenuItem("Window/3D Meteo/Data Creator/Create Location Data")]
    public static void CreateLocationData()
    {
        LocationData newData = ScriptableObject.CreateInstance("LocationData") as LocationData;
        newData.name = "(rename me) New Location Data";

        Selection.activeObject = newData;

        string path = "Assets/Content/Datas/Locations/" + newData.name + ".asset";
        AssetDatabase.CreateAsset(newData, path);
    }
}
