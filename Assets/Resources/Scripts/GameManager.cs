using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class GameManager : MonoBehaviour{

    private int honey;
    private int propolis;
    private int wax;
    private int pollen;
    private int nectar;
    public UnityEngine.UI.Text honeyText, propolisText, waxText, pollenText, nectarText;
    public Tutorial tutorial;
    private Stopwatch watch = new Stopwatch();
    private Dictionary<int, TimeStamp> counters;
    private Dictionary<string, UnityEngine.UI.Text> containers;
    public static GameManager main;
    private int curKey = 0;
    public const int HONEY = 1;
    public const int WAX = 2;
    public const int PROPOLIS = 3;
    public const int POLLEN = 4;
    public const int NECTAR = 5;
    public const int COMMON = 0;
    public const int WARNING = 1;
    public const int ERROR = 2;
    public int logLevel;
    public bool logging;
    public bool testing;
    public string tutorialPrefix;
    public string lang;

    private void Awake()
    {
        main = this;
        watch.Start();
        containers = new Dictionary<string, UnityEngine.UI.Text>();
        counters = new Dictionary<int, TimeStamp>();
        curKey = counters.Count;
    }

    void Start()
    {
        GameTexts.init();
        setLanguage("pt-BR");
        refreshAllTexts();
    }

    private void refreshAllTexts()
    {
        UnityEngine.UI.Text[] allTexts = FindObjectsOfType<UnityEngine.UI.Text>();
        foreach (var uiText in allTexts)
        {
            string key = uiText.text;
            string text = GameTexts.get(key, lang);
            uiText.text = text;
        }
    }

    public bool counterComplete(int key)
    {
        if (!counters.ContainsKey(key))
            return false;
        long curTime = watch.ElapsedMilliseconds;
        if (curTime >= counters[key].finish)
        {
            long stamp = curTime + counters[key].delta();
            counters[key].set(curTime, stamp);
            log("Counter " + key + " completed in " + curTime + ", start changed to " + curTime + " and finish changed to " + stamp );
            return true;
        }
            
        return false;
    }

    public int addCounter(long milliseconds)
    {
        curKey++;
        long start = watch.ElapsedMilliseconds;
        long finish = start + milliseconds;
        counters.Add(curKey, new TimeStamp(start, finish));
        log("Added counter from " + start + " to " + finish + " under key " + curKey);
        return curKey;
    }

    public void increaseResource(int resourceKey, int amount = 0)
    {
        if (amount <= 0)
            amount = 1;
        switch (resourceKey)
        {
            case HONEY:
                this.honey += amount;
                break;
            case WAX:
                this.wax += amount;
                break;
            case PROPOLIS:
                this.propolis += amount;
                break;
            case POLLEN:
                this.pollen += amount;
                break;
            case NECTAR:
                this.nectar += amount;
                break;
            default:
                throw new System.Exception("Resource not found");
        }
        log("Resource " + resourceName(resourceKey) + " increased by " + amount);
    }

    void FixedUpdate()
    {
        honeyText.text = honey.ToString();
        waxText.text = wax.ToString();
        propolisText.text = propolis.ToString();
        pollenText.text = pollen.ToString();
        nectarText.text = nectar.ToString();
    }

    public string resourceName(int resourceId)
    {
        switch (resourceId)
        {
            case HONEY:
                return "HONEY";
            case WAX:
                return "WAX";
            case PROPOLIS:
                return "PROPOLIS";
            case POLLEN:
                return "POLLEN";
            case NECTAR:
                return "NECTAR";
            default:
                throw new System.Exception("Resource not found");
        }
    }

    public void log(object obj, int level = COMMON)
    {
        if (this.logging)
        {
            if (level <= logLevel)
                UnityEngine.Debug.Log(obj);
        }
    }

    public void setLanguage(string lang)
    {
        this.lang = lang;
        refreshTexts();
    }
    
    public void refreshTexts()
    {
        tutorial.refresh(GameTexts.allTexts, tutorialPrefix, lang);
    }
}

public class TimeStamp
{
    public long start;
    public long finish;

    public TimeStamp(long start, long finish)
    {
        this.start = start;
        this.finish = finish;
    }

    public void set(long start, long finish)
    {
        this.start = start;
        this.finish = finish;
    }

    public long delta()
    {
        return this.finish - this.start;
    }
    
}