using UnityEngine;

public class WeatherModel : MonoBehaviour
{
    // Start is called before the first frame update
    public class Rootobject
    {
        public float latitude { get; set; }
        public float longitude { get; set; }
        public float generationtime_ms { get; set; }
        public int utc_offset_seconds { get; set; }
        public string timezone { get; set; }
        public string timezone_abbreviation { get; set; }
        public float elevation { get; set; }
        public Hourly_Units hourly_units { get; set; }
        public Hourly hourly { get; set; }
    }

    public class Hourly_Units
    {
        public string time { get; set; }
        public string temperature_2m { get; set; }
        public string uv_index { get; set; }
        public string uv_index_clear_sky { get; set; }
        public string cape { get; set; }
        public string freezinglevel_height { get; set; }
    }

    public class Hourly
    {
        public string[] time { get; set; }
        public float[] temperature_2m { get; set; }
        public float[] uv_index { get; set; }
        public float[] uv_index_clear_sky { get; set; }
        public float[] cape { get; set; }
        public float[] freezinglevel_height { get; set; }
    }
}
