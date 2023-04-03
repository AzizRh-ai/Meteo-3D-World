using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationData : ScriptableObject
{
    #region Variables
    [Serializable]
    public class CoordonatesData
    {
        public float longitude = 0f;
        public float latitude = 0f;
        public float height = 0f;
        public float radius = 0f;

    }
    public CoordonatesData coordonates  = new CoordonatesData();

    [Serializable]
    public class SiteData
    {
        public string name = "";
        // TO COMPLETE
    }
    public SiteData site = new SiteData();

    [Serializable]
    public class MeteoData
    {
        public string temperature = "";
        // TO COMPLETE
    }
    public MeteoData meteo = new MeteoData();
    #endregion Variables


}
