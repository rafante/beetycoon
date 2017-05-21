using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BeesManager))]
public class BeeViewer : Popup
{
    public UnityEngine.UI.InputField beeNameInput;
    public StatIndicator foodIndicator, energyIndicator, lifeIndicator, ageIndicator;
    public Bee curBee;
    public static BeeViewer main;

	// Use this for initialization
	void Awake ()
	{
	    if (main == null)
	        main = this;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
	    if (BeesManager.main.bees.Count > 0)
	        curBee = BeesManager.main.bees[0];
	    if (curBee != null)
	    {
	        beeNameInput.text = curBee.name;
	        foodIndicator.curStat = curBee.curFood;
	        foodIndicator.maxStat = curBee.maxFood;

	        energyIndicator.curStat = curBee.curEnergy;
	        energyIndicator.maxStat = curBee.maxEnergy;

	        lifeIndicator.curStat = curBee.curLife;
	        lifeIndicator.maxStat = curBee.maxLife;

	        ageIndicator.curStat = curBee.curAge;
	        ageIndicator.maxStat = curBee.maxAge;
	    }
	}

    public void setBee(Bee bee)
    {
        curBee = bee;
        beeNameInput.text = curBee.name;
        if(!gameObject.activeSelf)
            gameObject.SetActive(true);
    }
}
