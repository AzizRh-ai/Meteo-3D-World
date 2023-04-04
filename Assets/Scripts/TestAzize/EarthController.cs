using System;
using System.Collections;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class EarthController : MonoBehaviour
{
    [SerializeField] private float _zoomSpeed = 10f;
    [SerializeField]
    private float _rotationSpeed = 50f;
    [SerializeField] private LayerMask _earthLayer;
    [SerializeField] private TextMeshProUGUI _weatherInfoText;


    public static string baseUrl = "https://api.open-meteo.com/v1/forecast";

    void Update()
    {
        float scrollWheel = Input.GetAxis("Mouse ScrollWheel");
        if (scrollWheel != 0)
        {
            Camera.main.transform.position += Camera.main.transform.forward * scrollWheel * _zoomSpeed * Time.deltaTime;
        }
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 getPosition = GetLatLongPosition();
            if (getPosition != Vector2.zero)
            {
                Debug.Log("Latitude: " + getPosition.x + ", Longitude: " + getPosition.y);
                GetWeatherData(getPosition.x, getPosition.y);
            }
        }

        if (Input.GetMouseButton(1))
        {
            float horizontal = Input.GetAxis("Mouse X");
            float vertical = Input.GetAxis("Mouse Y");

            transform.Rotate(Vector3.up, -horizontal * _rotationSpeed * Time.deltaTime, Space.World);
            transform.Rotate(Vector3.right, vertical * _rotationSpeed * Time.deltaTime, Space.World);
        }
    }

    private Vector2 GetLatLongPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000, _earthLayer))
        {
            Vector3 hitPoint = hit.point;
            Vector3 localHitPoint = transform.InverseTransformPoint(hitPoint);

            Vector3 normalizedHitPoint = localHitPoint.normalized;

            float latitude = Mathf.Asin(normalizedHitPoint.y) * Mathf.Rad2Deg;
            float longitude = Mathf.Atan2(normalizedHitPoint.z, normalizedHitPoint.x) * Mathf.Rad2Deg;

            return new Vector2(latitude, longitude);
        }
        return Vector2.zero;
    }


    public void GetWeatherData(float latitude, float longitude)
    {
        string latString = latitude.ToString(CultureInfo.InvariantCulture);
        string lonString = longitude.ToString(CultureInfo.InvariantCulture);
        string requestUrl = $"{baseUrl}?latitude={latString}&longitude={lonString}&hourly=temperature_2m,weathercode&current_weather=true";
        Debug.Log(requestUrl);

        StartCoroutine(FetchWeatherData(requestUrl));
    }

    private IEnumerator FetchWeatherData(string url)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + webRequest.error);
            }
            else
            {
                Debug.Log("Weather data: " + webRequest.downloadHandler.text);
                string weatherData = webRequest.downloadHandler.text;
                WeatherData parsedData = JsonUtility.FromJson<WeatherData>(weatherData);
                _weatherInfoText.text = "Latitude: " + parsedData.latitude + ", Longitude: " + parsedData.longitude;
            }
        }
    }
    [Serializable]
    public class WeatherData
    {
        public float latitude;
        public float longitude;
    }
}
