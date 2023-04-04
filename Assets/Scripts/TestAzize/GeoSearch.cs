using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class GeoSearch : MonoBehaviour
{
    [SerializeField] private EarthController _earth;
    [SerializeField] private TMP_InputField _latLongText;

    private string geocodingApiUrl = "https://geocoding-api.open-meteo.com/v1/search?name=";

    private void Start()
    {
        _latLongText.onEndEdit.AddListener(OnInputFieldSubmit);
    }

    private void OnInputFieldSubmit(string cityText)
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            StartCoroutine(FetchGeocodingData(cityText));
        }
    }

    private IEnumerator FetchGeocodingData(string cityName)
    {
        string url = geocodingApiUrl + UnityWebRequest.EscapeURL(cityName);
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + webRequest.error);
            }
            else
            {
                GeocodingData[] parsedData = JsonHelper.FromJson<GeocodingData>(webRequest.downloadHandler.text);

                if (parsedData.Length > 0)
                {
                    _earth.GetWeatherData(parsedData[0].latitude, parsedData[0].longitude);
                }
            }
        }
    }

    [Serializable]
    public class GeocodingData
    {
        public float latitude;
        public float longitude;
        public string country_code;
    }
}

public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.results;
    }

    [Serializable]
    private class Wrapper<T>
    {
        public T[] results;
    }
}
