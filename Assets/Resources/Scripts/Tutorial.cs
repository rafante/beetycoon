using System;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    private SortedDictionary<string,string> phases;
    private int curPhase;
    private string lang;
    private I18nTexts[] tutorialTexts;
    private string tutorialPrefix;
    public UnityEngine.UI.Text tutorialText;

    private void Awake()
    {
        phases = new SortedDictionary<string, string>();
        if(tutorialText == null)
            throw new Exception("No tutorial text field is assigned");
        lang = "pt-BR";
        curPhase = 1;
    }

    private void refreshTexts(I18nTexts[] tutorialTexts, string tutorialPrefix, string lang = "pt-BR")
    {
        clearTexts();
        this.tutorialPrefix = tutorialPrefix;
        this.lang = lang;
        this.tutorialTexts = tutorialTexts;
        cacheTexts();
    }

    private void cacheTexts()
    {
        List<I18nTexts> i18nTexts = new List<I18nTexts>();
        foreach (var text in tutorialTexts)
        {
            if(!text.textKey.StartsWith(tutorialPrefix))
                continue;
            i18nTexts.Add(text);
        }
        tutorialTexts = i18nTexts.ToArray();
    }

    private void clearTexts()
    {
        phases = new SortedDictionary<string, string>();
    }

    public bool next()
    {
        if (curPhase < phases.Count - 1)
        {
            curPhase++;
            return true;
        }
        return false;
    }

    public bool previous()
    {
        if (curPhase > 0)
        {
            curPhase--;
            return true;
        }
        return false;
    }

    public void setLang(string lang)
    {
        this.lang = lang;
    }

    public string getPhaseText()
    {
        foreach (var text in tutorialTexts)
        {
            if (text.textKey.Equals(tutorialPrefix + curPhase))
            {
                if (text[lang] == null)
                    throw new Exception("There's no text for key " + curPhase + " in language " + lang);
                return text[lang];
            }
        }
        throw new Exception("there's no current tutorial phase");
    }

    public void turnOn()
    {
        gameObject.SetActive(true);
    }

    public void turnOff()
    {
        gameObject.SetActive(false);
    }

    public void refresh(I18nTexts[] tutorialTexts, string tutorialPrefix, string lang = "pt-BR")
    {
        refreshTexts(tutorialTexts, tutorialPrefix, lang);
        tutorialText.text = getPhaseText();
    }
}