using UnityEngine;

public class NetworkSettingsData : ScriptableObject
{
    #region Variables
    // Weather: open-meteo.com
    public static string API_OM_URL = "https://api.open-meteo.com/v1";

    // Openstreetmap.org convert coord to string: country,county,city
    public static string API_OSM_URL = "https://nominatim.openstreetmap.org/reverse";

    #endregion Variables


}
