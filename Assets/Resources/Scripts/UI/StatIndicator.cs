using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatIndicator : MonoBehaviour
{

    private int _curStatBuffer;
    private Vector2 _maxBarSize;
    public int curStat;
    public int maxStat;
    private RectTransform _maxStatBarRect;
    public UnityEngine.UI.Image curStatValueBar;

	// Use this for initialization
	void Start ()
	{
	    _curStatBuffer = curStat;
	    _maxStatBarRect = curStatValueBar.rectTransform;
	    _maxBarSize = _maxStatBarRect.sizeDelta;
	    changeSize();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	    if (_curStatBuffer != curStat)
	    {
	        changeSize();
	    }
	}

    private void changeSize()
    {
        _curStatBuffer = curStat;
        float statPercent = ((curStat * 100.0f) / maxStat) / 100.0f;
        _maxStatBarRect.sizeDelta = new Vector2(statPercent * _maxBarSize.x, _maxBarSize.y);

    }
}
