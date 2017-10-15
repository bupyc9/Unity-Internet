using UnityEngine;
using System.Collections;

public class WeatherManager : MonoBehaviour, IGameManager {
    public ManagerStatus status { get; private set; }

    private NetworkService _network;
    public float cloudValue { get; private set; }

    public void Startup(NetworkService service) {
        Debug.Log("Weather manager starting...");

        _network = service;
        StartCoroutine(_network.GetWeather(OnDataLoaded));

        status = ManagerStatus.Initializing;
    }

    public void OnDataLoaded(string json) {
        OpenWeatherMap data = JsonUtility.FromJson<OpenWeatherMap>(json);
        
        cloudValue = data.clouds.all / 100f;
        Debug.Log($"Value: {cloudValue}");
        Messenger.Broadcast(GameEvent.WEATHER_UPDATED);
        
        status = ManagerStatus.Started;
    }
}
