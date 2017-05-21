using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeHive : MonoBehaviour
{
    public int baseMaxNectar;
    public int baseMaxPollen;
    public int baseMaxHoney;
    public int baseMaxWax;
    public int baseMaxPropolis;
    public int baseMaxRoyalJam;
    private int _curNectar;
    private int _curPollen;
    private int _curHoney;
    private int _curWax;
    private int _curPropolis;
    private int _curRoyalJam;
    public static BeeHive main;

	// Use this for initialization
	void Awake ()
	{
	    if (main == null)
	        main = this;
	}

    public bool canStoreNectar()
    {
        return _curNectar < baseMaxNectar;
    }

    public bool hasMaxNectar()
    {
        return _curNectar >= getMaxNectar();
    }

    private int getMaxNectar()
    {
        return baseMaxNectar;
    }

    public void deliverNectar(int amount)
    {
        if (canStoreNectar())
            _curNectar += amount;
    }

    public int getCurNectar()
    {
        return _curNectar;
    }

    public int getCurPollen()
    {
        return _curPollen;
    }

    public int getCurHoney()
    {
        return _curHoney;
    }

    public int getCurWax()
    {
        return _curWax;
    }

    public int getCurPropolis()
    {
        return _curPropolis;
    }

    public int getCurRoyalJam()
    {
        return _curRoyalJam;
    }
}
