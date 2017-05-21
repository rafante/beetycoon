using System.IO;
using UnityEngine;

public class GameTexts {

    private static bool isReady = false;
    private static string texts;
    public static I18nTexts[] allTexts;
    
    public static string get(string key, string lang = "pt-BR")
    {
        if (!isReady)
            init();
        for (int i = 0; i < allTexts.Length; i++)
        {
            if (allTexts[i].textKey.Equals(key))
            {
                return allTexts[i][lang];
            }
        }
        return null;
    }

    public static void init()
    {
        readTexts();
        setI18n();
    }

    private static void readTexts()
    {
        TextAsset file = Resources.Load("texts") as TextAsset;
        texts = file.ToString();
        //using (StreamReader sr = new StreamReader(Application.dataPath + "/Scripts/texts.json"))
        //{
        //    texts = sr.ReadToEnd();
        //}
    }

    public static void setI18n()
    {
        I18nList list = JsonUtility.FromJson<I18nList>(texts);
        allTexts = list.texts;
    }


}

[System.Serializable]
public class I18nList
{
    public I18nTexts[] texts;
}