using System;
using System.Collections;
using UnityEngine;

public class NetworkService {
    private const string url = "http://api.openweathermap.org/data/2.5/weather";
    private const string appId = "0f308300649da1c530c7755ab52ceba5";
    private const string city = "Rostov-on-don,ru";

    private const string webImage = "http://newsinmir.com/uploads/posts/2017-01/thumbs/1483650987_canada-moraine-lake-1.jpg";

    private bool IsResponseValid(WWW www) {
        if(www.error != null) {
            Debug.Log("bad connection");
            return false;
        } else if (string.IsNullOrEmpty(www.text)) {
            Debug.Log("bad data");
            return false;
        } else {
            return true;
        }
    }

    private IEnumerator CallApi(string url, Action<string> callback) {
        var www = new WWW(url);
        yield return www;
        
        if (!IsResponseValid(www)) {
            yield break;
        }

        callback(www.text);
    }

    public IEnumerator GetWeather(Action<string> callback) {
        return CallApi($"{url}?q={city}&appid={appId}", callback);
    }

    public IEnumerator DownloadImage(Action<Texture2D> callback) {
        var www = new WWW(webImage);
        yield return www;
        callback(www.texture);
    }
}