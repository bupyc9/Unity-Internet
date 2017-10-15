using System;

[Serializable]
public class OpenWeatherMap {
    public string @base;
    public int visibility;
    public int dt;
    public int id;
    public string name;
    public int cod;
    public Clouds clouds;

    [Serializable]
    public class Clouds {
        public int all;
    }
}