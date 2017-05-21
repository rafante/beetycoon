using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class Utils : ScriptableObject
{
    
}

public static class ExtensionMethods
{
    #region World state hashset

    public static object get(this HashSet<KeyValuePair<string, object>> pairs, string key)
    {
        foreach (var pair in pairs)
        {
            if (pair.Key.Equals(key))
            {
                return pair.Value;
            }
        }
        throw new Exception("There's no such key " + key + " in the hashset");
    }
    public static bool getBool(this HashSet<KeyValuePair<string, object>> pairs, String key)
    {
        object value = get(pairs, key);
        if (value is bool)
            return (bool) value;
        throw new Exception("Tried to find bool object with key " + key + ", but value is of type: " + value.GetType());
    }

    public static int getInt(this HashSet<KeyValuePair<string, object>> pairs, String key)
    {
        object value = get(pairs, key);
        if (value is int)
            return (int) value;
        throw new Exception("Tried to find int object with key " + key + ", but value is of type: " + value.GetType());
    }

    public static float getFloat(this HashSet<KeyValuePair<string, object>> pairs, String key)
    {
        object value = get(pairs, key);
        if (value is float)
            return (float) value;
        throw new Exception("Tried to find float object with key " + key + ", but value is of type: " + value.GetType());
    }
    #endregion
}
[System.Serializable()]
public class MultiText
{
    public string lang;
    public string text;
}
[System.Serializable()]
public class I18nTexts
{
    public string textKey;
    public MultiText[] texts;

    public string this[string lang]
    {
        get
        {
            foreach (var texts in texts)
            {
                if (texts.lang.Equals(lang))
                    return texts.text;
            }
            return null;
        }
    }
}