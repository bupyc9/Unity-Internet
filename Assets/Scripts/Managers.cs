using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[RequireComponent(typeof(WeatherManager))]
[RequireComponent(typeof(ImagesManager))]

public class Managers : MonoBehaviour {
    public static WeatherManager Weather { get; private set; }
    public static ImagesManager Images { get; private set; }

    private List<IGameManager> _startSequence;

    private void Awake() {
        Weather = GetComponent<WeatherManager>();
        Images = GetComponent<ImagesManager>();

        _startSequence = new List<IGameManager>() { Weather, Images };
        StartCoroutine(StartupManagers());
    }

    private IEnumerator StartupManagers() {
        var network = new NetworkService();

        foreach(IGameManager manager in _startSequence) {
            manager.Startup(network);
        }

        yield return null;

        var numModels = _startSequence.Count;
        var numReady = 0;

        while(numReady < numModels) {
            var lastReady = numReady;
            numReady = 0;

            foreach(IGameManager manager in _startSequence) {
                if(manager.status == ManagerStatus.Started) {
                    numReady++;
                }
            }

            if(numReady > lastReady) {
                Debug.Log($"Progress: {numReady} / {numModels}");
            }

            yield return null;         
        }

        Debug.Log("All managers started up");
    }
}
