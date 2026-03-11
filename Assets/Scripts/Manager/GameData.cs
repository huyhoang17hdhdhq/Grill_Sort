using UnityEngine;

public static class GameData
{
    public enum Key
    {
        Level,
        Gold,
        Diamond,
        HighScore,
        Sound,
        Music
    }

    public static int GetInt(Key key, int defaultValue = 0)
    {
        return PlayerPrefs.GetInt(key.ToString(), defaultValue);
    }

    public static void SetInt(Key key, int value)
    {
        PlayerPrefs.SetInt(key.ToString(), value);
        PlayerPrefs.Save();
    }

    public static float GetFloat(Key key, float defaultValue = 0)
    {
        return PlayerPrefs.GetFloat(key.ToString(), defaultValue);
    }

    public static void SetFloat(Key key, float value)
    {
        PlayerPrefs.SetFloat(key.ToString(), value);
        PlayerPrefs.Save();
    }

    public static string GetString(Key key, string defaultValue = "")
    {
        return PlayerPrefs.GetString(key.ToString(), defaultValue);
    }

    public static void SetString(Key key, string value)
    {
        PlayerPrefs.SetString(key.ToString(), value);
        PlayerPrefs.Save();
    }
}