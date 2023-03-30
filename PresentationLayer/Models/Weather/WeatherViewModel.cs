namespace PresentationLayer.Models.Weather;
public class WeatherViewModel
{
    public int temp { get; set; }
    public int min_temp { get; set; }
    public int max_temp { get; set; }
    public int cloud_pct { get; set; }
    public float wind_speed { get; set; }
    public int humidity { get; set; }
}

